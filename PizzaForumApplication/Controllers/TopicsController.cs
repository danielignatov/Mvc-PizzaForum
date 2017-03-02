

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

    public class TopicsController : Controller
    {
        private TopicsService topicsService;
        private SignInManagerService signInManagerService;

        public TopicsController()
        {
            this.topicsService = new TopicsService();
            this.signInManagerService = new SignInManagerService();
        }

        // New
        [HttpGet]
        public IActionResult<TopicsNewViewModel> New(HttpResponse response, HttpSession session)
        {
            if (this.signInManagerService.IsAuthenticated(session))
            {
                TopicsNewViewModel tnvm = this.topicsService.GenerateTopicsNewViewModel(session);

                return this.View(tnvm);
            }

            this.Redirect(response, "/home/topics");
            return null;
        }

        [HttpPost]
        public IActionResult<TopicsNewViewModel> New(HttpResponse response, HttpSession session, NewTopicBindingModel ntbm)
        {
            TopicsNewViewModel tnvm = this.topicsService.GenerateTopicsNewViewModel(session);

            if (this.topicsService.IsNewTopicBindingModelValid(ntbm))
            {
                this.topicsService.CreateNewTopic(session, ntbm);

                this.Redirect(response, "/home/topics");
                return null;
            }

            return this.View(tnvm);
        }
    }
}
