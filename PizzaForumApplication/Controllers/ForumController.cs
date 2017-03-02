namespace PizzaForumApplication.Controllers
{
    using BindingModels;
    using SimpleMVC.Interfaces;
    using Services;
    using SimpleHttpServer.Models;
    using SimpleMVC.Attributes.Methods;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SimpleMVC.Controllers;

    public class ForumController : Controller
    {
        private ForumService service;

        public ForumController()
        {
            this.service = new ForumService();
        }

        // Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(HttpResponse response, HttpSession session, LoginUserBindingModel lubm)
        {
            var user = this.service.GetCorrespondingLoginUser(lubm);

            if (user == null)
            {
                this.Redirect(response, "/forum/login");
                return null;
            }

            service.LoginUser(user, session.Id);

            this.Redirect(response, "/home/topics");
            return null;
        }

        // Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(HttpResponse response, RegisterUserBindingModel rubm)
        {
            if (this.service.IsRegisterBindingModelValid(rubm))
            {
                service.RegisterUserFromBindingModel(rubm);

                this.Redirect(response, "/forum/login");
                return null;
            }

            return View();
        }
    }
}