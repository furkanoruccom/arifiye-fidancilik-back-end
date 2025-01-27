using AspNetCoreBackEnd.Core.Models;
using AspNetCoreBackEnd.Core.Repositories;
using AspNetCoreBackEnd.Core.Services;
using AspNetCoreBackEnd.Core.TableModels;
using AspNetCoreBackEnd.Core.UnitOfWorks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Security.Claims;

namespace AspNetCoreBackEnd.Service.Services
{
    public class TextService : Service<Texts>, ITextService
    {
        private readonly ITextRepository _main_repository;
        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemberService _memberService;

        public TextService(IGenericRepository<Texts> repository, IUnitOfWork unitOfWork, IMapper mapper, ITextRepository main_repository, IHttpContextAccessor httpContextAccessor, IMemberService memberService)
            : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _main_repository = main_repository;
            _httpContextAccessor = httpContextAccessor;
            _memberService = memberService;
        }



        private int activeUserId => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        private int activeUserRoleId => _memberService.GetRoleIdUser(activeUserId);
        private Users activeUser => _memberService.FindByUserId(activeUserId);



        public async Task<TextReturnModel> TableDataAsync(DataTableModel table)
        {
            var query = _main_repository.GetAll(false)
                        .OrderByDescending(i => i.CreatedDate).AsQueryable();



            // Arama
            if (!string.IsNullOrEmpty(table.search.value))
            {
                query = query.Where(i => i.name.Contains(table.search.value) || i.CreatedDate.ToString().Contains(table.search.value));
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

            return (new TextReturnModel()
            {
                data = dataListTask.Result,
                draw = table.draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsTotal
            });
        }

    }
}
