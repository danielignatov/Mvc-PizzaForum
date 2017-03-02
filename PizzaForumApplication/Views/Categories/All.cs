namespace PizzaForumApplication.Views.Categories
{
    using SimpleMVC.Interfaces;
    using System.IO;

    public class All : IRenderable
    {
        public string Render()
        {
            string header = File.ReadAllText("../../Content/header.html");
            string navigation = File.ReadAllText("../../Content/nav-logged.html");
            string categories = File.ReadAllText("../../Content/admin-categories.html");
            string footer = File.ReadAllText("../../Content/footer.html");

            return header + navigation + categories + footer;
        }
    }
}