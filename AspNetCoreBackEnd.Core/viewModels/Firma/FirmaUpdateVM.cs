namespace AspNetCoreBackEnd.Core.viewModels
{
    public class FirmaUpdateVM : BaseEntityVM
    {
        public string? MediaName { get; set; }

        public int OrderNo { get; set; }

        public int status { get; set; }

        public string tr_Baslik { get; set; }
        public string en_Baslik { get; set; }

        public string? tr_Aciklama { get; set; }
        public string? en_Aciklama { get; set; }


        public string? tr_Buttons { get; set; }
        public string? en_Buttons { get; set; }

    }
}
