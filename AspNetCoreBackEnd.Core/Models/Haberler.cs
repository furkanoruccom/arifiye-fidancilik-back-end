using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreBackEnd.Core.Models
{
    public class Haberler : BaseEntity
    {


        public string MediaName { get; set; }
        public int OrderNo { get; set; }



        public string tr_Baslik { get; set; }
        public string en_Baslik { get; set; }

        [Column(TypeName = "LONGTEXT")]
        public string tr_Aciklama { get; set; }
        [Column(TypeName = "LONGTEXT")]
        public string en_Aciklama { get; set; }


        public string tr_KisaAciklama { get; set; }
        public string en_KisaAciklama { get; set; }

    }
}
