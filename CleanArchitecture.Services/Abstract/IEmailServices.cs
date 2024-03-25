namespace CleanArchitecture.Services.Abstract
{
    public interface IEmailServices
    {
        Task<string> SendEmail(string email, string message, string? reason);
    }
}
