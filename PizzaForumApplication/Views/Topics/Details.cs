namespace PizzaForumApplication.Views.Topics
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

    public class Details : IRenderable<TopicsDetailsViewModel>
    {
        public TopicsDetailsViewModel Model { get; set; }

        public string Render()
        {
            StringBuilder topicsDetailsPageBuilder = new StringBuilder();

            // Header
            topicsDetailsPageBuilder.Append(File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.HeaderPath));

            // Navigation
            string nav;

            if (Model.Navbar.LoggedIn == true)
            {
                nav = File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.NavbarLoggedPath);

                if (Model.Navbar.UserLevel == 3)
                {
                    string adminButton = File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.AdminNavButton);

                    nav = nav.Replace("##admindropdownmenu##", adminButton);
                }
                else
                {
                    nav = nav.Replace("##admindropdownmenu##", "");
                }

                nav = nav.Replace("##userid##", Model.Navbar.UserId.ToString());
                nav = nav.Replace("##username##", Model.Navbar.Username);
            }
            else
            {
                nav = File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.NavbarNotLoggedPath);
            }

            topicsDetailsPageBuilder.Append(nav);

            // Site Content - Topic details and replies
            // If the user is logged in, he can post reply
            StringBuilder contentBuilder = new StringBuilder();
            string container = File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.ContainerPath);

            // Topic
            contentBuilder.Append($"<div class=\"thumbnail\"><h4><strong><a href=\"/topics/details?id={Model.Topic.TopicId}\">{Model.Topic.TopicName}</a><strong></h4><p><a href=\"/forum/profile?id={Model.Topic.Author.UserId}\">{Model.Topic.Author.Username}</a> {Model.Topic.PublishedOn.ToShortDateString()}</p><p>{Model.Topic.Content}</p></div>");

            // Replies
            foreach (var reply in Model.Topic.Replies)
            {
                if (reply.ImgUrl == null)
                {
                    contentBuilder.Append($"<div class=\"thumbnail reply\"><h5><strong><a href=\"/forum/profile?id={reply.User.UserId}\">{reply.User.Username}</a><strong> {reply.PublishedOn.ToShortDateString()}</h5><p>{reply.Content}</p></div>");
                }
                else
                {
                    contentBuilder.Append($"<div class=\"thumbnail reply\"><h5><strong><a href=\"/forum/profile?id={reply.User.UserId}\">{reply.User.Username}</a><strong> {reply.PublishedOn.ToShortDateString()}</h5><p>{reply.Content}</p><img src=\"{reply.ImgUrl}\" /></div>");
                }
            }

            // Reply form
            if (Model.Navbar.LoggedIn == true)
            {
                string form = File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.ReplyFormPath);

                form = form.Replace("##topicid##", Model.Topic.TopicId.ToString());

                contentBuilder.Append(form);
            }

            container = container.Replace("##content##", contentBuilder.ToString());

            topicsDetailsPageBuilder.Append(container);

            // Footer
            topicsDetailsPageBuilder.Append(File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.FooterPath));

            return topicsDetailsPageBuilder.ToString();
        }
    }
}