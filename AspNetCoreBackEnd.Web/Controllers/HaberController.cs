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
    public class HaberController : Controller
    {
        private readonly IHaberService _service;

        private readonly IMediaService _mediaService;
        private readonly IResponseService _responseService;
        private readonly IMapper _mapper;

        public HaberController(IHaberService service, IMediaService mediaService, IResponseService responseService, IMapper mapper)
        {
            _service = service;
            _mediaService = mediaService;
            _responseService = responseService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }




        //[Route("/HaberCreate")]
        public IActionResult Create()
        {
            return View();
        }

        //[Route("/HaberUpdate/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var data = await _service.GetByIdIncludeThenIncludeAsync(id, false);
            return View(data);
        }




        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateJson(HaberVM model)
        {

            if (!ModelState.IsValid) return Json(new { errors = ModelState }); // Hata Kontrol
            var response = await _service.AddAsync(_mapper.Map<Haberler>(model));

            TempData["HaberMessage"] = "Başarıyla eklendi";
            return Json(_responseService.HandleSuccessData("Başarıyla eklendi!", response.Id));
        }


        [ValidateAntiForgeryToken]
        public async Task<JsonResult> UpdateJson(HaberUpdateVM model)
        {
            //Ürünün güncellenmesi
            if (!ModelState.IsValid) return Json(new { errors = ModelState }); // Hata Kontrol
            await _service.UpdateAsync(_mapper.Map<Haberler>(model));



            TempData["HaberMessage"] = "Başarıyla güncellendi";
            return Json(_responseService.HandleSuccessData("Başarıyla güncellendi!", model.Id));
        }



        [ValidateAntiForgeryToken]
        public async Task<JsonResult> RemoveJson(int id)
        {
            var data = await _service.GetByIdIncludeThenIncludeAsync(id, false);

            _mediaService.DeleteFile(data.MediaName, "wwwroot/uploads");


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
