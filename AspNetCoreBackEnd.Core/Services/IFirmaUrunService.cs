using AspNetCoreBackEnd.Core.Models;
using AspNetCoreBackEnd.Core.TableModels;

namespace AspNetCoreBackEnd.Core.Services
{
    public interface IFirmaUrunService : IService<FirmaUrunler>
    {
        Task<FirmaUrunReturnModel> TableDataAsync(DataTableModel table, int firmaId);
    }
}
