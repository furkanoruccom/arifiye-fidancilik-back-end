using AspNetCoreBackEnd.Core.Models;

namespace AspNetCoreBackEnd.Core.TableModels
{
    public class TextReturnModel
    {
        public int draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public List<Texts> data { get; set; }
    }
}
