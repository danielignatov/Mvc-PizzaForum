namespace PizzaForumApplication.ViewModels
{
    using System.Collections.Generic;

    public class CategorisTopicsViewModel
    {
        public CategorisTopicsViewModel()
        {
            this.Topics = new List<TopicViewModel>();
        }

        public NavbarViewModel Navbar { get; set; }

        public List<TopicViewModel> Topics { get; set; }
    }
}