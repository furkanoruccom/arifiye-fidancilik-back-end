using AspNetCoreBackEnd.Core.Models;

namespace AspNetCoreBackEnd.Web.Models
{
    public class HomePageData
    {
        public IEnumerable<Firmalar> Firmalar { get; set; }
        public IEnumerable<Haberler> Haberler { get; set; }
    }
}
