using CleanArchitecture.Date.Commans;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Date.Entites.Views
{
    [Keyless]
    public class ViewDepartment : GeneralLocalizableEntity
    {
        public int DID { get; set; }
        public string DNameAr { get; set; }
        public string DNameEn { get; set; }
        public int StudentCount { get; set; }
    }
}
