﻿namespace PizzaForumApplication.Services
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

            tnvm.UserId = this.signInManagerService.GetAuthenticatedUser(session).Id;
            tnvm.Username = this.signInManagerService.GetAuthenticatedUser(session).Username;
            tnvm.UserLevel = (int)this.signInManagerService.GetAuthenticatedUser(session).Role;
            List<CategoryViewModel> categoryViewModelList = new List<CategoryViewModel>();

            foreach (var category in this.Context.Categories)
            {
                CategoryViewModel categoryViewModel = new CategoryViewModel();

                categoryViewModel.CategoryName = category.Name;
                categoryViewModel.CategoryId = category.Id;

                categoryViewModelList.Add(categoryViewModel);
            }

            tnvm.Categories = categoryViewModelList;

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
    }
}