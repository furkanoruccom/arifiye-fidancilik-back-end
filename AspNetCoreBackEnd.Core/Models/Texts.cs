using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreBackEnd.Core.Models
{
    public class Texts : BaseEntity
    {
        public string name { get; set; }

        [Column(TypeName = "LONGTEXT")]
        public string tr_Aciklama { get; set; }
        [Column(TypeName = "LONGTEXT")]
        public string en_Aciklama { get; set; }

    }
}
