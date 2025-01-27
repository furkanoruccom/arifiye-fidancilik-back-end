using System.Text.RegularExpressions;

namespace AspNetCoreBackEnd.Web.Helpers
{
    public class UrlFormatter
    {
        public static string Format(string title)
        {
            // Türkçe karakterleri İngilizce karakterlere dönüştür
            string formattedTitle = title
                .Replace("ı", "i")
                .Replace("İ", "I")
                .Replace("ğ", "g")
                .Replace("Ğ", "G")
                .Replace("ü", "u")
                .Replace("Ü", "U")
                .Replace("ş", "s")
                .Replace("Ş", "S")
                .Replace("ö", "o")
                .Replace("Ö", "O")
                .Replace("ç", "c")
                .Replace("Ç", "C");

            // Boşlukları tireye dönüştür
            formattedTitle = Regex.Replace(formattedTitle, @"\s+", "-");

            // URL için uygun hale getir
            formattedTitle = Regex.Replace(formattedTitle, @"[^a-zA-Z0-9\-]", "");

            return formattedTitle;
        }
    }
}
