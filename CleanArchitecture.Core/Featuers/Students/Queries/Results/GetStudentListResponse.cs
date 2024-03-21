namespace CleanArchitecture.Core.Featuers.Students.Queries.Results
{
    public class GetStudentListResponse
    {
        public int StudID { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }
        public string? Department { get; set; }
    }
}
