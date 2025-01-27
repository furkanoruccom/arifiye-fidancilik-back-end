
namespace AspNetCoreBackEnd.Core.Models
{
    public class FirmaResimleri : BaseEntity
    {

        public string MediaName { get; set; }
        public int OrderNo { get; set; }

        public int FirmaId { get; set; }
        public Firmalar Firma { get; set; }


    }
}
