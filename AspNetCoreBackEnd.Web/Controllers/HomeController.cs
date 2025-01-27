using AspNetCoreBackEnd.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreBackEnd.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMailService _mailService;
        private readonly ITextService _textService;
        private readonly IFirmaService _firmaService;
        private readonly IFirmaUrunService _firmaUrunService;
        private readonly IHaberService _haberService;
        private readonly IFormService _formService;
        private readonly IMapper _mapper;

        private readonly IResponseService _responseService;


        public HomeController(IFirmaService firmaService, IHaberService haberService, IFirmaUrunService firmaUrunService, ITextService textService, IMailService mailService, IFormService formService, IMapper mapper, IResponseService responseService)
        {
            _firmaService = firmaService;
            _haberService = haberService;
            _firmaUrunService = firmaUrunService;
            _textService = textService;
            _mailService = mailService;
            _formService = formService;
            _mapper = mapper;
            _responseService = responseService;
        }


        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                });
            return Redirect(Request.Headers["Referer"].ToString());
        }
        private string lan = Thread.CurrentThread.CurrentCulture.Name;




        [Route("/Anasayfa")]
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            if (lan == "en-US")
                ViewData["Title"] = "Home";
            else
                ViewData["Title"] = "Anasayfa";

            //var data = new HomePageData()
            //{
            //    Firmalar = await _firmaService.GetAllAsync(false),
            //    Haberler = await _haberService.GetAllAsync(false)
            //};


            var firmalar = (await _firmaService.GetAllAsync()).OrderBy(i => i.OrderNo).ToList();
            return View(firmalar);

        }


        [Route("/Hakkimizda")]
        public IActionResult About()
        {
            if (lan == "en-US")
                ViewData["Title"] = "Home";
            else
                ViewData["Title"] = "Anasayfa";


            return View();
        }


        [Route("/Cerezler")]
        public IActionResult Cookies()
        {
            if (lan == "en-US")
                ViewData["Title"] = "Cookies";
            else
                ViewData["Title"] = "Çerezler";


            return View();
        }


        [Route("/Ihracat")]
        public IActionResult Export()
        {
            if (lan == "en-US")
                ViewData["Title"] = "Export";
            else
                ViewData["Title"] = "İhracat";


            return View();
        }


        [Route("/Kvkk")]
        public IActionResult Kvkk()
        {
            if (lan == "en-US")
                ViewData["Title"] = "Kvkk";
            else
                ViewData["Title"] = "Kvkk";


            return View();
        }

        [Route("/SistemPolitikasi")]
        public IActionResult SystemPolicy()
        {
            if (lan == "en-US")
                ViewData["Title"] = "System Policy";
            else
                ViewData["Title"] = "Sistem Politikası";


            return View();
        }

        [Route("/KalitePolitikasi")]
        public IActionResult Privacy()
        {
            if (lan == "en-US")
                ViewData["Title"] = "Quality Policy";
            else
                ViewData["Title"] = "Kalite Politikası";


            return View();
        }

        [Route("/Sertifikalar")]
        public IActionResult Certificates()
        {
            if (lan == "en-US")
                ViewData["Title"] = "Certificates";
            else
                ViewData["Title"] = "Sertifikalar";


            return View();
        }



        [Route("/UretimAlanlari")]
        public async Task<IActionResult> ProductionAreas()
        {
            if (lan == "en-US")
                ViewData["Title"] = "Production Areas";
            else
                ViewData["Title"] = "Üretim ALanları";


            var firmalar = (await _firmaService.GetAllAsync()).OrderBy(i => i.OrderNo).ToList();
            return View(firmalar);

        }

        [Route("/UretimAlani/{baslik}/{id}")]
        public async Task<IActionResult> ProductionArea(int id)
        {
            var data = await _firmaService.GetByIdIncludeThenIncludeAsync(id, false, include: i => i.Include(i => i.FirmaResimleri).Include(i => i.FirmaUrunleri));
            if (lan == "en-US")
                ViewData["Title"] = data.en_Baslik;
            else
                ViewData["Title"] = data.tr_Baslik;

            return View(data);
        }


        [Route("/Iletisim")]
        public IActionResult Iletisim()
        {
            if (lan == "en-US")
                ViewData["Title"] = "Contact Us";
            else
                ViewData["Title"] = "İletişim";

            return View();
        }


        public async Task<JsonResult> SendMessage(string name, string mail, string phone, string message)
        {


            var returnMessgae = await _mailService.AddAsync(new()
            {
                Name = name,
                Mail = mail,
                Phone = phone,
                Message = message,
                SendingDate = DateTime.Now
            });




            if (lan == "en-US")
            {
                return Json(new { success = true, message = "Your message was sent successfully." });
            }
            else
            {
                return Json(new { success = true, message = "Mesajınız başarıyla gönderildi." });
            }

        }

        public override bool Equals(object? obj)
        {
            return obj is HomeController controller &&
                   EqualityComparer<ITextService>.Default.Equals(_textService, controller._textService);
        }
    }
}
