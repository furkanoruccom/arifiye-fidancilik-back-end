using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreBackEnd.Core.viewModels
{
    public class TextUpdateVM
    {
        public int Id { get; set; }
        public string name { get; set; }

        [Column(TypeName = "LONGTEXT")]
        public string tr_Aciklama { get; set; }
        [Column(TypeName = "LONGTEXT")]
        public string en_Aciklama { get; set; }



    }
}
