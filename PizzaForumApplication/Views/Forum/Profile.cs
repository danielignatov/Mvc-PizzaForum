namespace PizzaForumApplication.Views.Forum
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

    public class Profile : IRenderable<ForumProfileViewModel>
    {
        public ForumProfileViewModel Model { get; set; }

        public string Render()
        {
            StringBuilder forumProfilePageBuilder = new StringBuilder();

            // Header
            forumProfilePageBuilder.Append(File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.HeaderPath));

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

            forumProfilePageBuilder.Append(nav);

            // Page Content - Profile Page
            // If logged in user profile - add option to delete his post
            StringBuilder topicsBuilder = new StringBuilder();
            string content = File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.ProfilePagePath);

            content = content.Replace("##username##", Model.ProfileName);

            // Own profile
            if (Model.Navbar.UserId == Model.ProfileId)
            {
                content = content.Replace("##deletebutton##", "<th>Delete</th>");

                foreach (var topic in Model.Topics)
                {
                    topicsBuilder.Append($"<tr><td><a href=\"/topics/details?id={topic.TopicId}\">{topic.TopicName}</a></td><td><a href=\"/categories/topics?categoryName={topic.Category.CategoryName}\">{topic.Category.CategoryName}</a></td><td>{topic.PublishedOn.ToShortDateString()}</td><td>{topic.Replies.Count}</td><td><a href=\"/topics/delete?id={topic.TopicId}\" class=\"btn btn-danger\"/>Delete</a></td></tr>");
                }
            }
            // Foreign profile
            else
            {
                content = content.Replace("##deletebutton##", "");

                foreach (var topic in Model.Topics)
                {
                    topicsBuilder.Append($"<tr><td><a href=\"/topics/details?id={topic.TopicId}\">{topic.TopicName}</a></td><td><a href=\"/categories/topics?categoryName={topic.Category.CategoryName}\">{topic.Category.CategoryName}</a></td><td>{topic.PublishedOn.ToShortDateString()}</td><td>{topic.Replies.Count}</td></tr>");
                }
            }

            content = content.Replace("##topics##", topicsBuilder.ToString());

            forumProfilePageBuilder.Append(content);

            // Footer
            forumProfilePageBuilder.Append(File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.FooterPath));

            return forumProfilePageBuilder.ToString();
        }
    }
}