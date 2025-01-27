using AspNetCoreBackEnd.Core.Models;
using AspNetCoreBackEnd.Core.Repositories;
using AspNetCoreBackEnd.Data;

namespace AspNetCoreBackEnd.Repository.Repositories
{
    public class FirmaUrunRepository : GenericRepository<FirmaUrunler>, IFirmaUrunRepository
    {
        public FirmaUrunRepository(AppDbContext context) : base(context)
        {
        }
    }
}
