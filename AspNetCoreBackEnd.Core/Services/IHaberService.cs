using AspNetCoreBackEnd.Core.Models;
using AspNetCoreBackEnd.Core.TableModels;

namespace AspNetCoreBackEnd.Core.Services
{
    public interface IHaberService : IService<Haberler>
    {
        Task<HaberReturnModel> TableDataAsync(DataTableModel table);
    }
}
