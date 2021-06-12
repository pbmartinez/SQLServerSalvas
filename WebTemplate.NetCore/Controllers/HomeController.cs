using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebTemplate.NetCore.Models;

namespace WebTemplate.NetCore.Controllers
{
    public class HomeController : Controller
    {
        private Models.Message ResponseMessage;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ResponseMessage = new Message
            {
                MessageSeverity = Enums.EnumMessageSeverity.danger,
                Content = Core.Language.Resources.ExceptionError
            };
            TempData["ResponseMessage"] = JsonConvert.SerializeObject(ResponseMessage);

            var error = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id,
                Exception = exceptionHandlerPathFeature.Error,
                PathRequest = exceptionHandlerPathFeature.Path
            };

            return View(error);
        }

        public IActionResult Handle(int code)
        {
            TempData["code"] = code;
            return View();
        }
    }
}
