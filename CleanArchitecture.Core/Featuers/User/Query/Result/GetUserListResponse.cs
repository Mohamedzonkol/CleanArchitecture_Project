namespace CleanArchitecture.Core.Featuers.User.Query.Result
{
    public class GetUserListResponse
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
    }
}
