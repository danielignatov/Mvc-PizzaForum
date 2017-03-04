namespace PizzaForumApplication.Views.Categories
{
    using SimpleMVC.Interfaces.Generic;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ViewModels;

    public class Topics : IRenderable<CategorisTopicsViewModel>
    {
        public CategorisTopicsViewModel Model { get; set; }

        public string Render()
        {
            StringBuilder categoriesTopicsPageBuilder = new StringBuilder();

            // Header
            categoriesTopicsPageBuilder.Append(File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.HeaderPath));

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

            categoriesTopicsPageBuilder.Append(nav);

            // Site Content - Categories
            StringBuilder topicsBuilder = new StringBuilder();
            string container = File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.ContainerPath);

            foreach (var topic in Model.Topics)
            {
                topicsBuilder.Append($"<div class=\"thumbnail\"><h4><strong><a href=\"/topics/details?id={topic.TopicId}\">{topic.TopicName}</a><strong> <small><a href=\"/categories/topics?categoryname={topic.Category.CategoryName}\">{topic.Category.CategoryName}</a></small></h4><p><a href=\"/forum/profile?id={topic.Author.UserId}\">{topic.Author.Username}</a> | Replies: {topic.Replies.Count} | {topic.PublishedOn.ToShortDateString()}</p></div>");
            }

            container = container.Replace("##content##", topicsBuilder.ToString());

            categoriesTopicsPageBuilder.Append(container);

            // Footer
            categoriesTopicsPageBuilder.Append(File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.FooterPath));

            return categoriesTopicsPageBuilder.ToString();
        }
    }
}