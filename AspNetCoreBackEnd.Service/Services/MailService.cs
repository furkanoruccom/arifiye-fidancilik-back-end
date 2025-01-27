using AutoMapper;
using AspNetCoreBackEnd.Core.Models;
using AspNetCoreBackEnd.Core.Repositories;
using AspNetCoreBackEnd.Core.Services;
using AspNetCoreBackEnd.Core.TableModels;
using AspNetCoreBackEnd.Core.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Security.Claims;

namespace AspNetCoreBackEnd.Service.Services
{
    public class MailService : Service<Mailings>, IMailService
    {
        private readonly IMailRepository _mailService;
        private readonly IMapper _mapper;


        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemberService _memberService;


        public MailService(IGenericRepository<Mailings> repository, IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, IMemberService memberService, IMailRepository mailService)
            : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _memberService = memberService;
            _mailService = mailService;
        }


        private int activeUserId => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        private int activeUserRoleId => _memberService.GetRoleIdUser(activeUserId);
        private Users activeUser => _memberService.FindByUserId(activeUserId);



        public async Task<MailReturnModel> TableDataAsync(DataTableModel table)
        {
            var query = _mailService.GetAll(false).OrderByDescending(i => i.SendingDate).AsQueryable();


            // Arama
            if (!string.IsNullOrEmpty(table.search.value))
            {
                query = query.Where(i => i.Name.Contains(table.search.value) || i.SendingDate.ToString().Contains(table.search.value));
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

            return (new MailReturnModel()
            {
                data = dataListTask.Result,
                draw = table.draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsTotal
            });
        }
    }
}
