using AspNetCoreBackEnd.Core.Models;
using AspNetCoreBackEnd.Core.TableModels;

namespace AspNetCoreBackEnd.Core.Services
{
    public interface IFirmaService : IService<Firmalar>
    {
        Task<FirmaReturnModel> TableDataAsync(DataTableModel table);
    }
}
