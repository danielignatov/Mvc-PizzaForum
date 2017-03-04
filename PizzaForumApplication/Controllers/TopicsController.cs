

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

        // Delete
        [HttpGet]
        public IActionResult Delete(HttpResponse response, HttpSession session, int id)
        {
            if (this.signInManagerService.IsAuthenticated(session))
            {
                if (this.topicsService.CanThisUserDeleteGivenTopic(session, id))
                {
                    this.topicsService.DeleteTopic(id);
                }
            }

            this.Redirect(response, "/home/topics");
            return null;
        }

        // Details
        [HttpGet]
        public IActionResult<TopicsDetailsViewModel> Details(HttpSession session, int id)
        {
            TopicsDetailsViewModel tdvm = this.topicsService.GenerateTopicsDetailsViewModel(session, id);

            return this.View(tdvm);
        }

        [HttpPost]
        public void Details(HttpResponse response, HttpSession session, NewReplyBindingModel nrbm, int id)
        {
            if (this.signInManagerService.IsAuthenticated(session))
            {
                if (this.topicsService.IsNewReplyBindingModelValid(session, nrbm, id))
                {
                    this.topicsService.AddNewReplyToTopic(session, nrbm, id);
                }
            }

            this.Redirect(response, $"/topics/details?id={id}");
        }
    }
}
