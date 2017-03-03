namespace PizzaForumApplication.ViewModels
{
    using System.Collections.Generic;

    public class HomeCategoriesViewModel
    {
        public HomeCategoriesViewModel()
        {
            this.Categories = new List<CategoryViewModel>();
        }

        public NavbarViewModel Navbar { get; set; }

        public List<CategoryViewModel> Categories { get; set; }
    }
}