namespace PizzaForumApplication.Views.Categories
{
    using PizzaForumApplication.ViewModels;
    using SimpleMVC.Interfaces.Generic;
    using System.IO;
    using System.Text;

    public class Edit : IRenderable<CategoriesEditViewModel>
    {
        public CategoriesEditViewModel Model { get; set; }

        public string Render()
        {
            StringBuilder categoriesEditPageBuilder = new StringBuilder();

            // Header
            categoriesEditPageBuilder.Append(File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.HeaderPath));

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

            categoriesEditPageBuilder.Append(nav);

            // Site Content - New Category Form (Admin view)
            string content = File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.AdminCategoryEditFormPath);

            content = content.Replace("##categoryname##", Model.CategoryName);

            categoriesEditPageBuilder.Append(content);

            // Footer
            categoriesEditPageBuilder.Append(File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.FooterPath));

            return categoriesEditPageBuilder.ToString();
        }
    }
}