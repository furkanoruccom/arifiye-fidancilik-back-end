using AspNetCoreBackEnd.Core.Models;

namespace AspNetCoreBackEnd.Core.TableModels
{
    public class FirmaResimReturnModel
    {
        public int draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public List<FirmaResimleri> data { get; set; }
    }
}
