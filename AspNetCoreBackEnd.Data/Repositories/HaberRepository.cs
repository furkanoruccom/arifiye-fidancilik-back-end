using AspNetCoreBackEnd.Core.Models;
using AspNetCoreBackEnd.Core.Repositories;
using AspNetCoreBackEnd.Data;

namespace AspNetCoreBackEnd.Repository.Repositories
{
    public class HaberRepository : GenericRepository<Haberler>, IHaberRepository
    {
        public HaberRepository(AppDbContext context) : base(context)
        {
        }
    }
}
