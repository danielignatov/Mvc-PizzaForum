namespace PizzaForumApplication.Views.Topics
{
    using SimpleMVC.Interfaces.Generic;
    using System.IO;
    using System.Text;
    using ViewModels;

    public class New : IRenderable<TopicsNewViewModel>
    {
        public TopicsNewViewModel Model { get; set; }

        public string Render()
        {
            StringBuilder topicsNewPageBuilder = new StringBuilder();

            // Header
            topicsNewPageBuilder.Append(File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.HeaderPath));

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

            topicsNewPageBuilder.Append(nav);

            // Page Content - New Topic Form
            string container = File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.ContainerPath);
            string newTopicForm = File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.NewTopicFormPath);
            StringBuilder categorySelectBuilder = new StringBuilder();

            foreach (var category in Model.Categories)
            {
                categorySelectBuilder.Append($"<option>{category.CategoryName}</option>");
            }

            newTopicForm = newTopicForm.Replace("##categories##", $"{categorySelectBuilder.ToString()}");
            container = container.Replace("##content##", newTopicForm);

            topicsNewPageBuilder.Append(container);

            // Footer
            topicsNewPageBuilder.Append(File.ReadAllText(Constants.Constants.ContentPath + Constants.Constants.FooterPath));
            
            return topicsNewPageBuilder.ToString();
        }
    }
}