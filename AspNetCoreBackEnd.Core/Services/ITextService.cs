using AspNetCoreBackEnd.Core.Models;
using AspNetCoreBackEnd.Core.TableModels;

namespace AspNetCoreBackEnd.Core.Services
{
    public interface ITextService : IService<Texts>
    {
        Task<TextReturnModel> TableDataAsync(DataTableModel table);
    }
}
