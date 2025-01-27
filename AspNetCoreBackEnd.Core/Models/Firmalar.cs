namespace AspNetCoreBackEnd.Core.Models
{
    public class Firmalar : BaseEntity
    {


        public string? MediaName { get; set; }


        public int OrderNo { get; set; }


        public string tr_Baslik { get; set; }
        public string en_Baslik { get; set; }

        public string? tr_Aciklama { get; set; }
        public string? en_Aciklama { get; set; }


        public string? tr_Buttons { get; set; }
        public string? en_Buttons { get; set; }




        public List<FirmaResimleri> FirmaResimleri { get; set; }
        public List<FirmaUrunler> FirmaUrunleri { get; set; }



    }
}
