namespace PizzaForumApplication.Services
{
    using ViewModels;
    using SimpleHttpServer.Models;
    using System.Collections.Generic;
    using BindingModels;
    using System;
    using Models;
    using System.Linq;

    public class CategoriesService : Service
    {
        private SignInManagerService signInManagerService;

        public CategoriesService()
        {
            this.signInManagerService = new SignInManagerService();
        }

        public CategoriesAllViewModel GenerateCategoriesAllViewModel(HttpSession session)
        {
            CategoriesAllViewModel cavm = new CategoriesAllViewModel();
            NavbarViewModel nvm = new NavbarViewModel();
            List<CategoryViewModel> lcvm = new List<CategoryViewModel>();

            if (this.signInManagerService.IsAuthenticated(session))
            {
                var user = this.signInManagerService.GetAuthenticatedUser(session);

                nvm.LoggedIn = true;
                nvm.UserId = user.Id;
                nvm.Username = user.Username;
                nvm.UserLevel = (int)user.Role;
            }
            else
            {
                nvm.LoggedIn = false;
            }

            foreach (var category in this.Context.Categories)
            {
                CategoryViewModel cvm = new CategoryViewModel()
                {
                    CategoryName = category.Name,
                    CategoryId = category.Id
                };

                lcvm.Add(cvm);
            }

            cavm.Navbar = nvm;
            cavm.Categories = lcvm;

            return cavm;
        }

        public NavbarViewModel GenerateNavbarViewModel(HttpSession session)
        {
            NavbarViewModel nvm = new NavbarViewModel();

            if (this.signInManagerService.IsAuthenticated(session))
            {
                var user = this.signInManagerService.GetAuthenticatedUser(session);

                nvm.LoggedIn = true;
                nvm.UserId = user.Id;
                nvm.Username = user.Username;
                nvm.UserLevel = (int)user.Role;
            }
            else
            {
                nvm.LoggedIn = false;
            }

            return nvm;
        }

        public bool IsNewCategoryBindingModelValid(NewCategoryBindingModel ncbm)
        {
            // Name lenght check
            if (ncbm.Name.Length > 30)
            {
                return false;
            }

            // Same name check
            if (this.Context.Categories.Any(n => n.Name == ncbm.Name))
            {
                return false;
            }

            return true;
        }

        public void AddNewCategory(NewCategoryBindingModel ncbm)
        {
            Category category = new Category()
            {
                Name = ncbm.Name
            };

            this.Context.Categories.Add(category);
            this.Context.SaveChanges();
        }

        public CategoriesEditViewModel GenerateCategoriesEditViewModel(HttpSession session, int categoryId)
        {
            CategoriesEditViewModel cevm = new CategoriesEditViewModel();
            NavbarViewModel nvm = new NavbarViewModel();

            if (this.signInManagerService.IsAuthenticated(session))
            {
                var user = this.signInManagerService.GetAuthenticatedUser(session);

                nvm.LoggedIn = true;
                nvm.UserId = user.Id;
                nvm.Username = user.Username;
                nvm.UserLevel = (int)user.Role;
            }
            else
            {
                nvm.LoggedIn = false;
            }

            cevm.Navbar = nvm;
            cevm.CategoryName = this.Context.Categories.Where(id => id.Id == categoryId).Select(cn => cn.Name).FirstOrDefault();

            return cevm;
        }

        public bool IsRenameCategoryBindingModelValid(RenameCategoryBindingModel rcbm)
        {
            // Name lenght check
            if (rcbm.NewName.Length > 30)
            {
                return false;
            }

            // Same name check
            if (this.Context.Categories.Any(n => n.Name == rcbm.NewName))
            {
                return false;
            }

            // Invalid previous name
            if (!this.Context.Categories.Any(n => n.Name == rcbm.OldName))
            {
                return false;
            }

            return true;
        }

        public void RenameCategory(RenameCategoryBindingModel rcbm)
        {
            Category category = this.Context.Categories.Where(cn => cn.Name == rcbm.OldName).SingleOrDefault();

            category.Name = rcbm.NewName;

            this.Context.SaveChanges();
        }

        public bool IsDeleteCategoryIdValid(int id)
        {
            // If exists
            if (this.Context.Categories.Any(i => i.Id == id))
            {
                return true;
            }

            return false;
        }

        public void DeleteCategory(int id)
        {
            this.Context.Categories.Remove(this.Context.Categories.Find(id));
            this.Context.SaveChanges();
        }
    }
}