﻿using CleanArchitecture.Date.Entites.Idetitiy;
using CleanArchitecture.Date.Helpers;
using CleanArchitecture.Infrastructre.Abstract;
using CleanArchitecture.Infrastructre.Data;
using CleanArchitecture.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CleanArchitecture.Services.Services
{
    public class AuthenticationServices(JwtSettings jwtSettings, AppDbContext context, IEmailServices emailServices,
        IRefreshTokenReporesatory refreshTokenReporesatory,
        UserManager<ApplicationUser> userManager) : IAuthenticationServices
    {
        private readonly ConcurrentDictionary<string, RefreshToken> _userRefreshTokens = new();

        public async Task<JwtAuthResult> GetJwtToken(ApplicationUser user)
        {
            var (JwtToken, accessToken) = await generateJwtSecurityToken(user);
            RefreshToken refreshToken = GetRefreshToken(user.UserName);
            UserRefreshToken userRefreashToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(jwtSettings.RefreshTokenExpireDate),
                IsUsed = true,
                IsExpired = false,
                JwtId = JwtToken.Id,
                RefreshToken = refreshToken.TokenString,
                Token = accessToken,
                UserId = user.Id
            };
            await refreshTokenReporesatory.AddAsync(userRefreashToken).ConfigureAwait(false);
            var response = new JwtAuthResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return response;
        }

        public async Task<JwtAuthResult> GetRefreshToken(string AccessToken, string RefreshToken)
        {
            var token = readJwt(AccessToken);
            if (token == null || !token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
                throw new SecurityTokenException("Algorithms Is Wrong");
            if (token.ValidTo > DateTime.UtcNow)
                throw new SecurityTokenException("Token Is Not Expired");
            string userId = token.Claims.FirstOrDefault(x => x.Type == nameof(UserClaims.UserId)).Value;
            var userRefreshToken = await refreshTokenReporesatory.GetTableNoTracking().FirstOrDefaultAsync(x =>
                x.Token == AccessToken &&
                x.RefreshToken == RefreshToken && x.UserId == userId).ConfigureAwait(false);
            if (userRefreshToken == null)
                throw new SecurityTokenException("Refresh Token Is  Not Found");
            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                userRefreshToken.IsExpired = true;
                userRefreshToken.IsUsed = false;
                await refreshTokenReporesatory.UpdateAsync(userRefreshToken);
                throw new SecurityTokenException("Refresh Token Is  Expired");
            }

            var user = await userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (user is null)
                throw new SecurityTokenException("User Is Not Found");
            var (jwtSecurityToken, newToken) = await generateJwtSecurityToken(user);
            var response = new JwtAuthResult();
            var refreshTokenResult = new RefreshToken();
            response.AccessToken = newToken;
            refreshTokenResult.UserName = token.Claims.FirstOrDefault(x => x.Type == nameof(UserClaims.UserName)).Value;
            refreshTokenResult.TokenString = RefreshToken;
            refreshTokenResult.ExpireAt = userRefreshToken.ExpiryDate;
            response.RefreshToken = refreshTokenResult;
            return response;
        }

        public async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { jwtSettings.Issuer },
                ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidAudience = jwtSettings.Audience,
                ValidateAudience = jwtSettings.ValidateAudience,
                ValidateLifetime = jwtSettings.ValidateLifeTime,
            };
            var valditor = handler.ValidateToken(accessToken, parameters, out SecurityToken Security);

            try
            {
                if (valditor is null) throw new SecurityTokenException("Invalid Token");
                return "Not Expired";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> ConfirmEmail(string userId, string code)
        {
            var user = await userManager.FindByIdAsync(userId).ConfigureAwait(false);
            var confirmation = await userManager.ConfirmEmailAsync(user, code);
            if (!confirmation.Succeeded) return "Failed";
            return "Success";
        }

        public async Task<string> SendCodeResetPassword(string Email)
        {
            var trans = await context.Database.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                var user = await userManager.FindByEmailAsync(Email).ConfigureAwait(false);
                if (user is null) return "NotFound";
                Random generator = new();
                string code = generator.Next(0, 1000000).ToString("D6");
                user.Code = code;
                IdentityResult update = await userManager.UpdateAsync(user).ConfigureAwait(false);
                if (!update.Succeeded) return "FailedUpdate";
                await emailServices.SendEmail(Email, $"Code For Reset Password : {code}", "Reset Password")
                    .ConfigureAwait(false);
                await trans.CommitAsync().ConfigureAwait(false);
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync().ConfigureAwait(false);
                return "Failed";
            }
        }

        public async Task<string> ResetPassword(string code, string email)
        {
            var user = await userManager.FindByEmailAsync(email).ConfigureAwait(false);
            if (user is null) return "NotFound";
            var userCode = user.Code;
            if (userCode == code)
                return "Success";
            return "Failed";
        }

        public async Task<string> ForgetPassword(string password, string email)
        {
            var trans = await context.Database.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                var user = await userManager.FindByEmailAsync(email).ConfigureAwait(false);
                if (user is null) return "NotFound";
                await userManager.RemovePasswordAsync(user).ConfigureAwait(false);
                await userManager.AddPasswordAsync(user, password).ConfigureAwait(false);
                await trans.CommitAsync().ConfigureAwait(false);
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync().ConfigureAwait(false);
                return "Failed";
            }
        }

        private async Task<(JwtSecurityToken, string)> generateJwtSecurityToken(ApplicationUser user)
        {
            var claims = await getClaims(user);
            var jwtToken = new JwtSecurityToken(
                jwtSettings.Issuer,
                jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddDays(jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return (jwtToken, accessToken);
        }
        private JwtSecurityToken readJwt(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken)) throw new ArgumentNullException(nameof(accessToken));
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
        }
        private RefreshToken GetRefreshToken(string username)
        {
            var refreshToken = new RefreshToken
            {
                ExpireAt = DateTime.Now.AddDays(jwtSettings.RefreshTokenExpireDate),
                UserName = username,
                TokenString = GenerateRefreshToken()
            };
            _userRefreshTokens.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, t) => refreshToken);
            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[20];
            RandomNumberGenerator randomNumberGenertor = RandomNumberGenerator.Create();
            randomNumberGenertor.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private async Task<List<Claim>> getClaims(ApplicationUser user)
        {
            var roles = await userManager.GetRolesAsync(user).ConfigureAwait(false);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(nameof(UserClaims.PhoneNumber), user.PhoneNumber),
                new Claim(nameof(UserClaims.UserId), user.Id)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var userClaims = await userManager.GetClaimsAsync(user).ConfigureAwait(false);
            claims.AddRange(userClaims);
            return claims;
        }
    }
}
