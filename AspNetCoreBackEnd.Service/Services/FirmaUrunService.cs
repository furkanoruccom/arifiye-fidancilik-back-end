using AspNetCoreBackEnd.Core.Models;
using AspNetCoreBackEnd.Core.Repositories;
using AspNetCoreBackEnd.Core.Services;
using AspNetCoreBackEnd.Core.TableModels;
using AspNetCoreBackEnd.Core.UnitOfWorks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace AspNetCoreBackEnd.Service.Services
{
    public class FirmaUrunService : Service<FirmaUrunler>, IFirmaUrunService
    {
        private readonly IFirmaUrunRepository _main_repository;
        private readonly IMapper _mapper;

        public FirmaUrunService(IGenericRepository<FirmaUrunler> repository, IUnitOfWork unitOfWork, IMapper mapper, IFirmaUrunRepository main_repository)
            : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _main_repository = main_repository;
        }

        public async Task<FirmaUrunReturnModel> TableDataAsync(DataTableModel table, int firmaId)
        {
            var query = _main_repository.Where(i => i.FirmaId == firmaId).OrderByDescending(i => i.OrderNo).AsQueryable();

            // Arama
            if (!string.IsNullOrEmpty(table.search.value))
            {
                query = query.Where(i => i.MediaName.Contains(table.search.value) || i.CreatedDate.ToString().Contains(table.search.value));
            }

            int recordsTotal = await query.CountAsync();

            //Sıralama
            if (!string.IsNullOrEmpty(table.orderColumnName))
            {
                var ordering = table.orderDir.Equals("asc", StringComparison.OrdinalIgnoreCase) ? $"{table.orderColumnName} ascending" : $"{table.orderColumnName} descending";
                query = query.OrderBy(ordering);
            }


            var dataListTask = query.Skip(table.start).Take(table.lenght).ToListAsync();


            await Task.WhenAll(dataListTask);

            return (new FirmaUrunReturnModel()
            {
                data = dataListTask.Result,
                draw = table.draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsTotal
            });
        }
    }

}
