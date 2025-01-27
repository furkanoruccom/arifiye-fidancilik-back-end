namespace AspNetCoreBackEnd.Core.viewModels
{
    public class FirmaUrunUpdateVM : BaseEntityVM
    {
        public string MediaName { get; set; }
        public int OrderNo { get; set; }



        public string tr_Baslik { get; set; }
        public string en_Baslik { get; set; }


        public string? tr_Aciklama { get; set; }
        public string? en_Aciklama { get; set; }

        public int FirmaId { get; set; }

    }
}
