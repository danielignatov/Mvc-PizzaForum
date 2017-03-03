namespace PizzaForumApplication.Views.Home
{
    using PizzaForumApplication.ViewModels;
    using SimpleMVC.Interfaces.Generic;
    using System.IO;
    using System.Text;

    public class Categories : IRenderable<HomeCategoriesViewModel>
    {
        public HomeCategoriesViewModel Model { get; set; }

        public string Render()
        {
            StringBuilder homeCategoriesPageBuilder = new StringBuilder();

            // Header
            homeCategoriesPageBuilder.Append(File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.HeaderPath));

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

            homeCategoriesPageBuilder.Append(nav);

            // Site Content - Categories
            StringBuilder categoriesBuilder = new StringBuilder();
            string container = File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.ContainerPath);

            foreach (var category in Model.Categories)
            {
                categoriesBuilder.Append($"<div class=\"thumbnail\"><h4><strong><a href=\"/categories/topics?categoryname={category.CategoryName}\">{category.CategoryName}</a><strong></h4><p>Category</p></div>");
            }

            container = container.Replace("##content##", categoriesBuilder.ToString());

            homeCategoriesPageBuilder.Append(container);

            // Footer
            homeCategoriesPageBuilder.Append(File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.FooterPath));

            return homeCategoriesPageBuilder.ToString();
        }
    }
}