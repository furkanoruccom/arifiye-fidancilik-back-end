using AspNetCoreBackEnd.Core.Services;
using AspNetCoreBackEnd.Core.TableModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreBackEnd.Web.Controllers
{

    [Authorize(Roles = "Admin")]
    public class FormController : Controller
    {
        private readonly IFormService _service;

        private readonly IResponseService _responseService;
        private readonly IMapper _mapper;

        public FormController(IFormService service, IResponseService responseService, IMapper mapper)
        {
            _service = service;
            _responseService = responseService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }



        [ValidateAntiForgeryToken]
        public async Task<JsonResult> RemoveJson(int id)
        {
            var data = await _service.GetByIdIncludeThenIncludeAsync(id, false);

            await _service.RemoveAsync(data);
            return Json(_responseService.HandleSuccess("Başarıyla Silindi!"));
        }








        [ValidateAntiForgeryToken]
        public async Task<JsonResult> TableData(int draw, int start, int length, string orderColumnName, string orderDir, [FromForm] Search search)
        {
            var data = await _service.TableDataAsync(new DataTableModel()
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
