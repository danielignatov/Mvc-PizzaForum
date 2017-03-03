namespace PizzaForumApplication.Views.Forum
{
    using SimpleMVC.Interfaces;

    public class Logout : IRenderable
    {
        public string Render()
        {
            return $"Logged out...";
        }
    }
}