namespace PizzaForumApplication.Views.Forum
{
    using SimpleMVC.Interfaces;
    using System.IO;

    public class Register : IRenderable
    {
        public string Render()
        {
            string header = File.ReadAllText("../../Content/header.html");
            string navigation = File.ReadAllText("../../Content/nav-not-logged.html");
            string register = File.ReadAllText("../../Content/register.html");
            string footer = File.ReadAllText("../../Content/footer.html");

            return header + navigation + register + footer;
        }
    }
}