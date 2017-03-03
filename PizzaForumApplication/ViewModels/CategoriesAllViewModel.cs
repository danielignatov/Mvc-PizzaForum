namespace PizzaForumApplication.ViewModels
{
    using System.Collections.Generic;

    public class CategoriesAllViewModel
    {
        public CategoriesAllViewModel()
        {
            this.Categories = new List<CategoryViewModel>();
        }

        public NavbarViewModel Navbar { get; set; }

        public List<CategoryViewModel> Categories { get; set; }
    }
}