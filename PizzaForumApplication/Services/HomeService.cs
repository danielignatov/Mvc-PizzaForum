using System;
using PizzaForumApplication.ViewModels;
using SimpleHttpServer.Models;
using System.Linq;
using System.Collections.Generic;

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

            if (this.signInManagerService.IsAuthenticated(session))
            {
                viewModel.IsUserLogged = true;
                viewModel.Username = this.signInManagerService.GetAuthenticatedUser(session).Username;
                viewModel.UserId = this.signInManagerService.GetAuthenticatedUser(session).Id;
            }
            else
            {
                viewModel.IsUserLogged = false;
            }

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
    }
}