using System;
using PizzaForumApplication.ViewModels;
using SimpleHttpServer.Models;
using System.Linq;
using System.Collections.Generic;
using PizzaForumApplication.Models;

namespace PizzaForumApplication.Services
{
    public class HomeService : Service
    {
        private SignInManagerService signInManagerService;

        public HomeService()
        {
            this.signInManagerService = new SignInManagerService();
        }

        public HomeTopicsViewModel GenerateHomeTopicsViewModel(HttpSession session)
        {
            HomeTopicsViewModel viewModel = new HomeTopicsViewModel();
            NavbarViewModel nvm = new NavbarViewModel();

            if (this.signInManagerService.IsAuthenticated(session))
            {
                User user = this.signInManagerService.GetAuthenticatedUser(session);

                nvm.LoggedIn = true;
                nvm.Username = user.Username;
                nvm.UserId = user.Id;
                nvm.UserLevel = (int)user.Role;
            }
            else
            {
                nvm.LoggedIn = false;
            }

            viewModel.Navbar = nvm;

            foreach (var topic in this.Context.Topics.OrderByDescending(t => t.PublishDate).Take(10))
            {
                TopicViewModel topicViewModel = new TopicViewModel();
                UserViewModel userViewModel = new UserViewModel();
                CategoryViewModel categoryViewModel = new CategoryViewModel();
                List<ReplyViewModel> replyViewModelList = new List<ReplyViewModel>();

                userViewModel.Username = topic.Author.Username;
                userViewModel.UserId = topic.Author.Id;

                categoryViewModel.CategoryName = topic.Category.Name;
                categoryViewModel.CategoryId = topic.Category.Id;

                foreach (var reply in topic.Replies)
                {
                    ReplyViewModel replyViewModel = new ReplyViewModel();
                    UserViewModel replyUserViewModel = new UserViewModel();

                    replyUserViewModel.Username = reply.Author.Username;
                    replyUserViewModel.UserId = reply.Author.Id;

                    replyViewModel.User = replyUserViewModel;
                    replyViewModelList.Add(replyViewModel);
                }

                topicViewModel.Author = userViewModel;
                topicViewModel.Category = categoryViewModel;
                topicViewModel.Replies = replyViewModelList;
                topicViewModel.TopicId = topic.Id;
                topicViewModel.TopicName = topic.Title;
                topicViewModel.PublishedOn = topic.PublishDate;

                viewModel.Topics.Add(topicViewModel);
            }

            return viewModel;
        }

        public HomeCategoriesViewModel GenerateHomeCategoriesViewModel(HttpSession session)
        {
            HomeCategoriesViewModel hcvm = new HomeCategoriesViewModel();
            NavbarViewModel nvm = new NavbarViewModel();
            List<CategoryViewModel> lcvm = new List<CategoryViewModel>();

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

            foreach (var category in this.Context.Categories)
            {
                CategoryViewModel cvm = new CategoryViewModel()
                {
                    CategoryName = category.Name,
                    CategoryId = category.Id
                };

                lcvm.Add(cvm);
            }

            hcvm.Navbar = nvm;
            hcvm.Categories = lcvm;

            return hcvm;
        }
    }
}