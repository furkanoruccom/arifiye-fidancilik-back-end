using AspNetCoreBackEnd.Core.Models;
using AspNetCoreBackEnd.Core.Services;
using AspNetCoreBackEnd.Core.TableModels;
using AspNetCoreBackEnd.Core.viewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreBackEnd.Web.Controllers
{


    public class FirmaResimController : Controller
    {
        private readonly IFirmaResimService _service;
        private readonly IFirmaService _firmaService;
        private readonly IMediaService _mediaService;

        private readonly IResponseService _responseService;
        private readonly IMapper _mapper;

        public FirmaResimController(
             IResponseService responseService,
             IFirmaUrunService slideMediaService,
             IMapper mapper,
             IFirmaResimService service,
             IFirmaService caseService,
             IMediaService mediaService)
        {
            _responseService = responseService;
            _mapper = mapper;
            _service = service;
            _firmaService = caseService;
            _mediaService = mediaService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var data = await _firmaService.GetByIdAsync(id, false);
            return View(data);
        }

        //[Route("/FirmaResimCreate/{id}")]
        public async Task<IActionResult> Create(int id)
        {

            var data = await _firmaService.GetByIdAsync(id, false);
            return View(data);
        }
        //[Route("/FirmaResimMultipleCreate/{id}")]
        public async Task<IActionResult> MultipleCreate(int id)
        {
            var data = await _firmaService.GetByIdAsync(id, false);
            return View(data);
        }


        //[Route("/FirmaResimUpdate/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var data = await _service.GetByIdIncludeThenIncludeAsync(id, false, include: query => query.Include(i => i.Firma));
            return View(data);
        }


        //[ValidateAntiForgeryToken]
        //public async Task<JsonResult> CreateMediaJson(FirmaResimMediaVM FirmaResimMedia)
        //{
        //    if (!ModelState.IsValid) return Json(new { errors = ModelState }); // Hata Kontrol
        //    var response = await _slideMediaService.AddAsync(_mapper.Map<FirmaUrunler>(FirmaResimMedia));

        //    TempData["FirmaResimMessage"] = "Added successfully!";
        //    return Json(_responseService.HandleSuccessData("Added successfully!"));
        //}




        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateJson(FirmaResimVM model)
        {
            if (!ModelState.IsValid) return Json(new { errors = ModelState }); // Hata Kontrol
            var response = await _service.AddAsync(_mapper.Map<FirmaResimleri>(model));

            return Json(_responseService.HandleSuccessData("Added successfully!", response.Id));
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> UpdateJson(FirmaResimUpdateVM model)
        {

            if (!ModelState.IsValid) return Json(new { errors = ModelState }); // Hata Kontrol
            await _service.UpdateAsync(_mapper.Map<FirmaResimleri>(model));


            TempData["FirmaResimMessage"] = "Updated successfully!";
            return Json(_responseService.HandleSuccessData("Updated successfully!", model.Id));
        }



        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<JsonResult> RemoveJson(int id)
        {
            var data = await _service.GetByIdAsync(id);

            _mediaService.DeleteFile(data.MediaName, "wwwroot/uploads/");

            await _service.RemoveAsync(data);
            return Json(_responseService.HandleSuccess("Successfully Deleted!"));
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> TableDataJson(int draw, int start, int length, string orderColumnName, string orderDir, int firmaId, [FromForm] Search search)
        {
            var data = await _service.TableDataAsync(new DataTableModel()
            {
                draw = draw,
                start = start,
                lenght = length,
                orderColumnName = orderColumnName,
                orderDir = orderDir,
                search = search
            }, firmaId);
            return Json(new { draw = data.draw, recordsFiltered = data.recordsTotal, recordsTotal = data.recordsTotal, data = data.data });
        }

    }
}
