﻿namespace PizzaForumApplication.ViewModels
{
    using System.Collections.Generic;

    public class TopicsNewViewModel
    {
        public NavbarViewModel Navbar { get; set; }

        public List<CategoryViewModel> Categories { get; set; }
    }
}