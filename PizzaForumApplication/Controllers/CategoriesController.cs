namespace PizzaForumApplication.Controllers
{
    using PizzaForumApplication.Services;
    using SimpleHttpServer.Models;
    using SimpleMVC.Attributes.Methods;
    using SimpleMVC.Controllers;
    using SimpleMVC.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CategoriesController : Controller
    {
        private CategoriesService categoriesService;
        private SignInManagerService signInManagerService;

        public CategoriesController()
        {
            this.categoriesService = new CategoriesService();
            this.signInManagerService = new SignInManagerService();
        }

        // All
        [HttpGet]
        public IActionResult All(HttpResponse response, HttpSession session)
        {
            if (this.signInManagerService.IsAuthenticated(session))
            {
                if (this.signInManagerService.GetAuthenticatedUser(session).Role == Enums.UserRole.Administrator)
                {
                    return this.View();
                }
            }

            this.Redirect(response, "/home/topics");
            return null;
        }
    }
}