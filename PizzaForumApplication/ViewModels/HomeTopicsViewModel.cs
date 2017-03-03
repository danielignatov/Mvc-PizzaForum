using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaForumApplication.ViewModels
{
    public class HomeTopicsViewModel
    {
        public HomeTopicsViewModel()
        {
            this.Topics = new List<TopicViewModel>();
        }

        public NavbarViewModel Navbar { get; set; }

        public List<TopicViewModel> Topics { get; set; }
    }
}
