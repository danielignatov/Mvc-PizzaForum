namespace PizzaForumApplication.ViewModels
{
    public class TopicsDetailsViewModel
    {
        public NavbarViewModel Navbar { get; set; }

        // TopicViewModel contain list or replies
        public TopicViewModel Topic { get; set; }
    }
}