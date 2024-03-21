using System.Globalization;

namespace CleanArchitecture.Date.Commans
{
    public class LocalizableEntity
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        public string GetLocalized()
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            if (cultureInfo.TwoLetterISOLanguageName.ToLower().Equals("ar"))
                return NameAr;
            return NameEn;
        }
    }
}
