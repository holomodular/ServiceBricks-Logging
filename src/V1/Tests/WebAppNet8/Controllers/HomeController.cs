using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceBricks;

//using ServiceBricks.Security.Api;
using WebApp.ViewModel.Home;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    [Route("")]
    [Route("Home")]
    public class HomeController : Controller
    {
        private IServiceBus _serviceBus;

        public HomeController(IServiceBus serviceBus)
        {
            _serviceBus = serviceBus;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            return View(model);
        }

        [HttpGet]
        [Route("ServiceBus")]
        public IActionResult ServiceBus()
        {
            var log = new CreateApplicationLogBroadcast(new ApplicationLogDto()
            {
                Application = "ApplicationTest",
                CreateDate = DateTimeOffset.UtcNow,
                Category = "CategoryTest",
                Exception = "ExceptionTest",
                Level = "LevelTest",
                Message = "MessageTest",
                Path = "PathTest",
                Properties = "PropertiesTest",
                Server = "ServerTest",
                StorageKey = "StorageKeyTest",
                UserStorageKey = "UserStorageKeyTest"
            });
            _serviceBus.Send(log);

            HomeViewModel model = new HomeViewModel();
            return View("Index", model);
        }

        [HttpGet]
        [Route("Error")]
        public IActionResult Error(string message = null)
        {
            var model = new ErrorViewModel()
            {
                Message = message
            };
            return View("Error", model);
        }
    }
}