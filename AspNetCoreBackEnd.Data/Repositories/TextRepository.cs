using AspNetCoreBackEnd.Core.Models;
using AspNetCoreBackEnd.Core.Repositories;
using AspNetCoreBackEnd.Data;

namespace AspNetCoreBackEnd.Repository.Repositories
{
    public class TextRepository : GenericRepository<Texts>, ITextRepository
    {
        public TextRepository(AppDbContext context) : base(context)
        {
        }
    }
}
