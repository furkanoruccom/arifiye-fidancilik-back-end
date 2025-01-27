using AspNetCoreBackEnd.Core.Models;
using AspNetCoreBackEnd.Core.TableModels;

namespace AspNetCoreBackEnd.Core.Services
{
    public interface IMailService : IService<Mailings>
    {
        Task<MailReturnModel> TableDataAsync(DataTableModel table);
    }
}
