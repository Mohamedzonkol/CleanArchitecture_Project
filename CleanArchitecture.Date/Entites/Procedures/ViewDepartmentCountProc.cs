using CleanArchitecture.Date.Commans;

namespace CleanArchitecture.Date.Entites.Procedures
{
    public class ViewDepartmentCountProc : GeneralLocalizableEntity
    {
        public int DID { get; set; }
        public string DNameAr { get; set; }
        public string DNameEn { get; set; }
        public int StudentCount { get; set; }
    }
    public class DepartmentCountProcParameter
    {
        public int DID { get; set; } = 0;
    }
}
