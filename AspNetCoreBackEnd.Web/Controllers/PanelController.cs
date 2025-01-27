using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreBackEnd.Web.Controllers
{


    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {

        public IActionResult Index()
        {
            ViewData["Title"] = "Home";

            return View();
        }
    }
}
