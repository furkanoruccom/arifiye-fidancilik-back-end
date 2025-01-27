using AspNetCoreBackEnd.Core.Models;

namespace AspNetCoreBackEnd.Core.TableModels
{
    public class MailReturnModel
    {
        public int draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public List<Mailings> data { get; set; }
    }
}
