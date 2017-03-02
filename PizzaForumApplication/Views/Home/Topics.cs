namespace PizzaForumApplication.Views.Home
{
    using SimpleMVC.Interfaces;
    using SimpleMVC.Interfaces.Generic;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ViewModels;

    public class Topics : IRenderable<HomeTopicsViewModel>
    {
        public HomeTopicsViewModel Model { get; set; }

        public string Render()
        {
            string header = File.ReadAllText("../../Content/header.html");
            string navigation;
            string newTopicButton = "<br>";
            StringBuilder topicsBuilder = new StringBuilder();

            if (Model.IsUserLogged)
            {
                navigation = File.ReadAllText("../../Content/nav-logged.html");
                navigation = navigation.Replace("##userid##", $"{Model.UserId}");
                navigation = navigation.Replace("##username##", $"{Model.Username}");
                newTopicButton = File.ReadAllText("../../Content/topic-new-button.html");
            }
            else
            {
                navigation = File.ReadAllText("../../Content/nav-not-logged.html");
            }

            foreach (var topic in Model.Topics)
            {
                topicsBuilder.Append($"<div class=\"thumbnail\"><h4><strong><a href=\"/topics/details?id={topic.TopicId}\">{topic.TopicName}</a><strong> <small><a href=\"/categories/topics?categoryname={topic.Category.CategoryName}\">{topic.Category.CategoryName}</a></small></h4><p><a href=\"/forum/profile?id={topic.Author.UserId}\">{topic.Author.Username}</a> | Replies: {topic.Replies.Count} | {topic.PublishedOn.ToShortDateString()}</p></div>");
            }
            
            string footer = File.ReadAllText("../../Content/footer.html");

            return header + navigation + "<div class=\"container\">" + newTopicButton + topicsBuilder.ToString() + "</div>" + footer;
        }
    }
}