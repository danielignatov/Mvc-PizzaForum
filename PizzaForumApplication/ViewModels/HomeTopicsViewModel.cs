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

        public bool IsUserLogged { get; set; }

        public string Username { get; set; }

        public int UserId { get; set; }

        public List<TopicViewModel> Topics { get; set; }
    }
}
