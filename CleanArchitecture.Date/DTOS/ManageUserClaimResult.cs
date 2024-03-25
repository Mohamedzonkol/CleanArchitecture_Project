namespace CleanArchitecture.Date.DTOS
{
    public class ManageUserClaimResult
    {

        public string UserId { get; set; }
        public List<UserClaim> UserClaims { get; set; }
    }

    public class UserClaim
    {
        public string ClaimType { get; set; }
        public bool Value { get; set; } = false;
    }
}
