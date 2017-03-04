namespace PizzaForumApplication.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BindingModels;
    using ViewModels;
    using SimpleHttpServer.Models;
    using Models;

    public class TopicsService : Service
    {
        private SignInManagerService signInManagerService;

        public TopicsService()
        {
            this.signInManagerService = new SignInManagerService();
        }

        public TopicsNewViewModel GenerateTopicsNewViewModel(HttpSession session)
        {
            TopicsNewViewModel tnvm = new TopicsNewViewModel();
            NavbarViewModel nvm = new NavbarViewModel();
            List<CategoryViewModel> lcvm = new List<CategoryViewModel>();

            var user = this.signInManagerService.GetAuthenticatedUser(session);

            nvm.UserId = user.Id;
            nvm.Username = this.signInManagerService.GetAuthenticatedUser(session).Username;
            nvm.UserLevel = (int)user.Role;
            nvm.LoggedIn = true;

            foreach (var category in this.Context.Categories)
            {
                CategoryViewModel categoryViewModel = new CategoryViewModel();

                categoryViewModel.CategoryName = category.Name;
                categoryViewModel.CategoryId = category.Id;

                lcvm.Add(categoryViewModel);
            }

            tnvm.Navbar = nvm;
            tnvm.Categories = lcvm;

            return tnvm;
        }

        public bool IsNewTopicBindingModelValid(NewTopicBindingModel ntbm)
        {
            if (ntbm.Title.Length > 30)
            {
                return false;
            }

            if (this.Context.Categories.Where(cn => cn.Name == ntbm.CategoryName).Count() == 0)
            {
                return false;
            }

            return true;
        }

        public void CreateNewTopic(HttpSession session, NewTopicBindingModel ntbm)
        {
            Topic topic = new Topic();

            topic.Author = this.signInManagerService.GetAuthenticatedUser(session);
            topic.Content = ntbm.Content;
            topic.PublishDate = DateTime.Now;
            topic.Title = ntbm.Title;
            topic.Category = this.Context.Categories.Where(cn => cn.Name == ntbm.CategoryName).FirstOrDefault();

            this.Context.Topics.Add(topic);
            this.Context.SaveChanges();
        }

        public void DeleteTopic(int id)
        {
            this.Context.Topics.Remove(this.Context.Topics.Find(id));
            this.Context.SaveChanges();
        }

        public TopicsDetailsViewModel GenerateTopicsDetailsViewModel(HttpSession session, int id)
        {
            TopicsDetailsViewModel tdvm = new TopicsDetailsViewModel();
            NavbarViewModel nvm = new NavbarViewModel();
            TopicViewModel tvm = new TopicViewModel();

            if (this.signInManagerService.IsAuthenticated(session))
            {
                var user = this.signInManagerService.GetAuthenticatedUser(session);

                nvm.LoggedIn = true;
                nvm.UserId = user.Id;
                nvm.Username = user.Username;
                nvm.UserLevel = (int)user.Role;
            }
            else
            {
                nvm.LoggedIn = false;
            }

            tdvm.Navbar = nvm;

            var topic = this.Context.Topics.Where(tid => tid.Id == id).FirstOrDefault();

            // Generate tvm
            UserViewModel uvm = new UserViewModel()
            {
                Username = topic.Author.Username,
                UserId = topic.Author.Id
            };

            tvm.Author = uvm;

            CategoryViewModel cvm = new CategoryViewModel()
            {
                CategoryName = topic.Category.Name,
                CategoryId = topic.Category.Id
            };

            tvm.Category = cvm;

            tvm.PublishedOn = topic.PublishDate;

            tvm.TopicId = topic.Id;

            tvm.TopicName = topic.Title;

            tvm.Content = topic.Content;

            List<ReplyViewModel> lrvm = new List<ReplyViewModel>();

            foreach (var reply in topic.Replies)
            {
                ReplyViewModel rvm = new ReplyViewModel();

                UserViewModel replyUserViewModel = new UserViewModel()
                {
                    Username = reply.Author.Username,
                    UserId = reply.Author.Id
                };

                rvm.Content = reply.Content;
                rvm.User = replyUserViewModel;
                rvm.PublishedOn = reply.PublishDate;
                rvm.ImgUrl = reply.ImgUrl;

                lrvm.Add(rvm);
            }

            tvm.Replies = lrvm;

            tdvm.Topic = tvm;

            return tdvm;
        }

        public bool CanThisUserDeleteGivenTopic(HttpSession session, int id)
        {
            if (this.signInManagerService.GetAuthenticatedUser(session).Topics.Any(tid => tid.Id == id))
            {
                return true;
            }

            if (this.signInManagerService.GetAuthenticatedUser(session).Role == Enums.UserRole.Administrator)
            {
                return true;
            }

            return false;
        }

        public void AddNewReplyToTopic(HttpSession session, NewReplyBindingModel nrbm, int id)
        {
            User user = this.signInManagerService.GetAuthenticatedUser(session);

            Reply reply = new Reply();

            reply.Author = user;
            reply.Content = nrbm.Content;
            reply.PublishDate = DateTime.Now;

            if (nrbm.ImgUrl != "")
            {
                reply.ImgUrl = nrbm.ImgUrl;
            }

            reply.Topic = this.Context.Topics.Where(tid => tid.Id == id).FirstOrDefault();

            this.Context.Replies.Add(reply);

            this.Context.SaveChanges();
        }

        public bool IsNewReplyBindingModelValid(HttpSession session, NewReplyBindingModel nrbm, int id)
        {
            if (this.Context.Topics.Any(tid => tid.Id == id))
            {
                return true;
            }

            return false;
        }
    }
}