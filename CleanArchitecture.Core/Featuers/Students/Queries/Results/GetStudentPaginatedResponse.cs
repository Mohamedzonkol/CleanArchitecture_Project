namespace CleanArchitecture.Core.Featuers.Students.Queries.Results
{
    public class GetStudentPaginatedResponse(int studId, string name, string address, string department)
    {
        public int StudID { get; set; } = studId;

        public string? Name { get; set; } = name;

        public string? Address { get; set; } = address;
        public string? Department { get; set; } = department;
    }
}
