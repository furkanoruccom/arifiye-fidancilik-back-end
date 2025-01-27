using AspNetCoreBackEnd.Core.Models;
using AspNetCoreBackEnd.Core.TableModels;

namespace AspNetCoreBackEnd.Core.Services
{
    public interface IFormService : IService<Forms>
    {
        Task<FormReturnModel> TableDataAsync(DataTableModel table);
    }
}
