namespace PizzaForumApplication.Views.Categories
{
    using PizzaForumApplication.ViewModels;
    using SimpleMVC.Interfaces.Generic;
    using System.IO;
    using System.Text;

    public class New : IRenderable<NavbarViewModel>
    {
        public NavbarViewModel Model { get; set; }

        public string Render()
        {
            StringBuilder categoriesNewPageBuilder = new StringBuilder();

            // Header
            categoriesNewPageBuilder.Append(File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.HeaderPath));

            // Navigation
            string nav;

            if (Model.LoggedIn == true)
            {
                nav = File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.NavbarLoggedPath);

                if (Model.UserLevel == 3)
                {
                    string adminButton = File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.AdminNavButton);

                    nav = nav.Replace("##admindropdownmenu##", adminButton);
                }
                else
                {
                    nav = nav.Replace("##admindropdownmenu##", "");
                }

                nav = nav.Replace("##userid##", Model.UserId.ToString());
                nav = nav.Replace("##username##", Model.Username);
            }
            else
            {
                nav = File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.NavbarNotLoggedPath);
            }

            categoriesNewPageBuilder.Append(nav);

            // Site Content - New Category Form (Admin view)
            string container = File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.AdminCategoryNewFormPath);

            categoriesNewPageBuilder.Append(container);

            // Footer
            categoriesNewPageBuilder.Append(File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.FooterPath));

            return categoriesNewPageBuilder.ToString();
        }
    }
}