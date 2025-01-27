using AspNetCoreBackEnd.Core.Models;
using AspNetCoreBackEnd.Core.Repositories;
using AspNetCoreBackEnd.Data;

namespace AspNetCoreBackEnd.Repository.Repositories
{
    public class FormRepository : GenericRepository<Forms>, ITFormRepository
    {
        public FormRepository(AppDbContext context) : base(context)
        {
        }
    }
}
