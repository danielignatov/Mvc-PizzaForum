namespace PizzaForumApplication.Controllers
{
    using BindingModels;
    using Services;
    using SimpleHttpServer.Models;
    using SimpleMVC.Attributes.Methods;
    using SimpleMVC.Controllers;
    using SimpleMVC.Interfaces;
    using SimpleMVC.Interfaces.Generic;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ViewModels;

    public class HomeController : Controller
    {
        private HomeService homeService;

        public HomeController()
        {
            this.homeService = new HomeService();
        }

        // Index
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Topics
        [HttpGet]
        public IActionResult<HomeTopicsViewModel> Topics(HttpResponse response, HttpSession session)
        {
            HomeTopicsViewModel viewModel = new HomeTopicsViewModel();

            viewModel = this.homeService.GenerateHomeTopicsViewModel(session);

            return View(viewModel);
        }
    }
}