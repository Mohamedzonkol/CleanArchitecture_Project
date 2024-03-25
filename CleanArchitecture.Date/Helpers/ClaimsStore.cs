using System.Security.Claims;

namespace CleanArchitecture.Date.Helpers
{
    public static class ClaimsStore
    {
        public static List<Claim> Claims = new()
        {
            new Claim("Create New Student","false"),
            new Claim("Edit Student","false"),
            new Claim("Delete Student","false"),

        };

    }
}
