using AspNetCoreBackEnd.Core.Models;
using AspNetCoreBackEnd.Core.Services;
using AspNetCoreBackEnd.Core.TableModels;
using AspNetCoreBackEnd.Core.viewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreBackEnd.Web.Controllers
{

    [Authorize(Roles = "Admin")]
    public class FirmaController : Controller
    {
        private readonly IFirmaService _services;
        private readonly IResponseService _responseService;
        private readonly IMediaService _mediaService;
        private readonly IMapper _mapper;

        public FirmaController(IFirmaService services, IResponseService responseService, IMapper mapper, IMediaService mediaService)
        {
            _services = services;
            _responseService = responseService;
            _mapper = mapper;
            _mediaService = mediaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[Route("/Create")]
        public IActionResult Create()
        {
            return View();
        }

        //[Route("/Update/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var data = await _services.GetByIdIncludeThenIncludeAsync(id, false);

            return View(data);
        }






        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<JsonResult> CreateJson(FirmaVM model)
        {
            if (!ModelState.IsValid) return Json(new { errors = ModelState }); // Hata Kontrol
            var response = await _services.AddAsync(_mapper.Map<Firmalar>(model));

            TempData["FirmaMessage"] = "Added successfully!";
            return Json(_responseService.HandleSuccessData("Added successfully!", response.Id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<JsonResult> UpdateJson(FirmaUpdateVM model)
        {

            if (!ModelState.IsValid) return Json(new { errors = ModelState });

            await _services.UpdateAsync(_mapper.Map<Firmalar>(model));


            TempData["FirmaMessage"] = "Updated successfully!";
            return Json(_responseService.HandleSuccessData("Updated successfully!", model.Id));

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<JsonResult> RemoveJson(int id)
        {
            var data = await _services.GetByIdAsync(id);

            _mediaService.DeleteFile(data.MediaName, "wwwroot/uploads");

            await _services.RemoveAsync(data);
            return Json(_responseService.HandleSuccess("Successfully Deleted!"));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<JsonResult> TableData(int draw, int start, int length, string orderColumnName, string orderDir, [FromForm] Search search)
        {
            var data = await _services.TableDataAsync(new DataTableModel()
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
