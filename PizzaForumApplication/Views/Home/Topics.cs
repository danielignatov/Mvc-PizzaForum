namespace PizzaForumApplication.Views.Home
{
    using SimpleMVC.Interfaces.Generic;
    using System.IO;
    using System.Text;
    using ViewModels;

    public class Topics : IRenderable<HomeTopicsViewModel>
    {
        public HomeTopicsViewModel Model { get; set; }

        public string Render()
        {
            StringBuilder homeTopicsPageBuilder = new StringBuilder();

            // Header
            homeTopicsPageBuilder.Append(File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.HeaderPath));

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

            homeTopicsPageBuilder.Append(nav);
            
            // Page Content - New Topic Button if user logged in
            if (Model.Navbar.LoggedIn == true)
            {
                homeTopicsPageBuilder.Append(File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.NewTopicButtonPath));
            }

            // Page Content - Last 10 Topics by date
            StringBuilder topicsBuilder = new StringBuilder();
            string container = File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.ContainerPath);

            foreach (var topic in Model.Topics)
            {
                topicsBuilder.Append($"<div class=\"thumbnail\"><h4><strong><a href=\"/topics/details?id={topic.TopicId}\">{topic.TopicName}</a><strong> <small><a href=\"/categories/topics?categoryname={topic.Category.CategoryName}\">{topic.Category.CategoryName}</a></small></h4><p><a href=\"/forum/profile?id={topic.Author.UserId}\">{topic.Author.Username}</a> | Replies: {topic.Replies.Count} | {topic.PublishedOn.ToShortDateString()}</p></div>");
            }

            container = container.Replace("##content##", topicsBuilder.ToString());

            homeTopicsPageBuilder.Append(container);

            // Footer
            homeTopicsPageBuilder.Append(File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.FooterPath));

            return homeTopicsPageBuilder.ToString();
        }
    }
}