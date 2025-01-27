using AspNetCoreBackEnd.Core.Models;
using AspNetCoreBackEnd.Core.Repositories;
using AspNetCoreBackEnd.Data;

namespace AspNetCoreBackEnd.Repository.Repositories
{
    public class FirmaResimRepository : GenericRepository<FirmaResimleri>, IFirmaResimRepository
    {
        public FirmaResimRepository(AppDbContext context) : base(context)
        {
        }
    }
}
