﻿namespace PizzaForumApplication.Views.Categories
{
    using SimpleMVC.Interfaces.Generic;
    using System.IO;
    using System.Text;
    using ViewModels;

    public class All : IRenderable<CategoriesAllViewModel>
    {
        public CategoriesAllViewModel Model { get; set; }

        public string Render()
        {
            StringBuilder categoriesAllPageBuilder = new StringBuilder();

            // Header
            categoriesAllPageBuilder.Append(File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.HeaderPath));

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

            categoriesAllPageBuilder.Append(nav);

            // Site Content - Categories (Admin view)
            StringBuilder categoriesBuilder = new StringBuilder();
            string container = File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.AdminCategoriesPath);

            foreach (var category in Model.Categories)
            {
                categoriesBuilder.Append($"<tr><td><a href=\"/categories/topics?categoryName={category.CategoryName}\">{category.CategoryName}</a></td><td><a href=\"/categories/edit?id={category.CategoryId}\" class=\"btn btn-primary\"/>Edit</a></td><td><a href=\"/categories/delete?id={category.CategoryId}\" class=\"btn btn-danger\"/>Delete</a></td></tr>");
            }

            container = container.Replace("##categories##", categoriesBuilder.ToString());

            categoriesAllPageBuilder.Append(container);

            // Footer
            categoriesAllPageBuilder.Append(File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.FooterPath));

            return categoriesAllPageBuilder.ToString();
        }
    }
}