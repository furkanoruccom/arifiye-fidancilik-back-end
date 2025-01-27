using AutoMapper;
using AspNetCoreBackEnd.Core.Services;
using AspNetCoreBackEnd.Core.TableModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreBackEnd.Web.Controllers
{

    [Authorize(Roles = "Admin")]
    public class MailController : Controller
    {
        private readonly IResponseService _responseService;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;

        public MailController(IMailService mailService, IResponseService responseService, IMapper mapper)
        {
            _mailService = mailService;
            _responseService = responseService;
            _mapper = mapper;
        }



        public IActionResult Index()
        {
            return View();
        }



        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<JsonResult> RemoveJson(int id)
        {
            var data = await _mailService.GetByIdAsync(id);

            await _mailService.RemoveAsync(data);
            return Json(_responseService.HandleSuccess("Successfully Deleted!"));
        }






        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<JsonResult> TableData(int draw, int start, int length, string orderColumnName, string orderDir, [FromForm] Search search)
        {
            var data = await _mailService.TableDataAsync(new DataTableModel()
            {
                draw = draw,
                start = start,
                lenght = length,
                orderColumnName = orderColumnName,
                orderDir = orderDir,
                search = search
            });
            return Json(new { draw = data.draw, recordsFiltered = data.recordsTotal, recordsTotal = data.recordsTotal, data = data.data });
        }

    }
}
