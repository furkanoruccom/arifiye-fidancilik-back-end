using AspNetCoreBackEnd.Core.Models;

namespace AspNetCoreBackEnd.Core.TableModels
{
    public class FirmaUrunReturnModel
    {
        public int draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public List<FirmaUrunler> data { get; set; }
    }
}
