using AspNetCoreBackEnd.Core.Models;
using AspNetCoreBackEnd.Core.TableModels;

namespace AspNetCoreBackEnd.Core.Services
{
    public interface IFirmaResimService : IService<FirmaResimleri>
    {
        Task<FirmaResimReturnModel> TableDataAsync(DataTableModel table, int firmaId);
    }
}
