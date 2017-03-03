namespace PizzaForumApplication.Controllers
{
    using BindingModels;
    using SimpleMVC.Interfaces;
    using Services;
    using SimpleHttpServer.Models;
    using SimpleMVC.Attributes.Methods;
    using SimpleMVC.Controllers;

    public class ForumController : Controller
    {
        private ForumService forumService;

        public ForumController()
        {
            this.forumService = new ForumService();
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
            var user = this.forumService.GetCorrespondingLoginUser(lubm);

            if (user == null)
            {
                this.Redirect(response, "/forum/login");
                return null;
            }

            forumService.LoginUser(user, session.Id);

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
            if (this.forumService.IsRegisterBindingModelValid(rubm))
            {
                forumService.RegisterUserFromBindingModel(rubm);

                this.Redirect(response, "/forum/login");
                return null;
            }

            return View();
        }

        // Logout
        [HttpGet]
        public void Logout(HttpResponse response, HttpSession session)
        {
            this.forumService.Logout(response, session.Id);
            this.Redirect(response, "/home/topics");
        }
    }
}