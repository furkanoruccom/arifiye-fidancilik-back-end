using AspNetCoreBackEnd.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Web.Controllers
{

    //[Authorize(Roles = "Admin")]
    public class MediaController : Controller
    {
        private readonly IMediaService _imageService;
        private readonly IResponseService _responseService;

        public MediaController(IMediaService imageService, IResponseService responseService)
        {
            _imageService = imageService;
            _responseService = responseService;
        }


        //[RequestSizeLimit(900_000_000)]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> VideoUploadJson(string _imageFolderPath)
        {
            var file = Request.Form.Files["uploadedFile"];
            if (file != null && file.Length > 0)
            {
                var fileName = await _imageService.UploadVideoAync(file, _imageFolderPath);
                return Json(fileName);
            }
            else
            {
                return Json(null);
            }
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> SoundUploadJson(string _imageFolderPath)
        {
            var file = Request.Form.Files["uploadedFile"]; // "uploadedFile" olarak isimlendirilen dosyayı al
            if (file != null && file.Length > 0)
            {
                var fileName = await _imageService.UploadSoundAync(file, _imageFolderPath);
                return Json(fileName);
            }
            else
            {
                return Json(null);
            }
        }

        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> PdfUploadJson(string _pdfFolderPath)
        {
            try
            {
                var file = Request.Form.Files["uploadedFile"];
                if (file != null && file.Length > 0)
                {
                    var fileName = await _imageService.UploadPdfAsync(file, _pdfFolderPath);
                    return Json(_responseService.HandleSuccessData("Successfuly!", fileName));
                }
                else
                {
                    return Json(_responseService.HandleError("File not found!"));
                }
            }
            catch (Exception e)
            {
                return Json(_responseService.HandleError(e.Message));
            }

        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ImageUploadBase64(string base64, string _imageFolderPath)
        {
            try
            {
                var fileName = await _imageService.UploadBase64ImageAsync(base64, _imageFolderPath);
                return Json(_responseService.HandleSuccessData("Successfuly!", fileName));
            }
            catch (Exception e)
            {
                return Json(_responseService.HandleError(e.Message));
            }
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ImageUpload(IFormFile image, string _imageFolderPath)
        {
            try
            {
                var fileName = await _imageService.UploadImageAsync(image, _imageFolderPath);
                return Json(_responseService.HandleSuccessData("Successfuly!", fileName));
            }
            catch (Exception e)
            {
                return Json(_responseService.HandleError(e.Message));
            }
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ImageUploads(List<IFormFile> images, string _imageFolderPath)
        {
            try
            {
                var fileName = await _imageService.BulkUploadImagesAsync(images, _imageFolderPath);
                return Json(_responseService.HandleSuccessData("Successfuly!", fileName));
            }
            catch (Exception e)
            {
                return Json(_responseService.HandleError(e.Message));
            }
        }


        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DownloadAndSaveImageJson(string image, string _imageFolderPath)
        {
            try
            {
                var fileName = await _imageService.DownloadAndSaveImageAsync(image, _imageFolderPath);
                return Json(_responseService.HandleSuccessData("Successfuly!", fileName));
            }
            catch (Exception e)
            {
                return Json(_responseService.HandleError(e.Message));
            }
        }

        [ValidateAntiForgeryToken]
        public IActionResult FileDelete(string mediaName, string _imageFolderPath)
        {
            try
            {
                _imageService.DeleteFile(mediaName, _imageFolderPath);
                return Ok(_responseService.HandleSuccess("Successfully!"));
            }
            catch (Exception e)
            {
                return Ok(_responseService.HandleError(e.Message));
            }

        }


    }
}
