namespace PizzaForumApplication.Views.Forum
{
    using SimpleMVC.Interfaces;
    using System.IO;

    public class Login : IRenderable
    {
        public string Render()
        {
            string header = File.ReadAllText("../../Content/header.html");
            string navigation = File.ReadAllText("../../Content/nav-not-logged.html");
            string login = File.ReadAllText("../../Content/login.html");
            string footer = File.ReadAllText("../../Content/footer.html");

            return header + navigation + login + footer;
        }
    }
}