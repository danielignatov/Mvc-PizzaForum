namespace PizzaForumApplication.Controllers
{
    using BindingModels;
    using PizzaForumApplication.Services;
    using SimpleHttpServer.Models;
    using SimpleMVC.Attributes.Methods;
    using SimpleMVC.Controllers;
    using SimpleMVC.Interfaces;
    using SimpleMVC.Interfaces.Generic;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ViewModels;

    public class CategoriesController : Controller
    {
        private CategoriesService categoriesService;
        private SignInManagerService signInManagerService;

        public CategoriesController()
        {
            this.categoriesService = new CategoriesService();
            this.signInManagerService = new SignInManagerService();
        }

        // All
        [HttpGet]
        public IActionResult<CategoriesAllViewModel> All(HttpResponse response, HttpSession session)
        {
            if (this.signInManagerService.IsAuthenticated(session))
            {
                if (this.signInManagerService.GetAuthenticatedUser(session).Role == Enums.UserRole.Administrator)
                {
                    CategoriesAllViewModel cavm = this.categoriesService.GenerateCategoriesAllViewModel(session);

                    return this.View(cavm);
                }
            }

            this.Redirect(response, "/home/topics");
            return null;
        }

        // New
        [HttpGet]
        public IActionResult<NavbarViewModel> New(HttpResponse response, HttpSession session)
        {
            if (this.signInManagerService.IsAuthenticated(session))
            {
                if (this.signInManagerService.GetAuthenticatedUser(session).Role == Enums.UserRole.Administrator)
                {
                    NavbarViewModel nvm = this.categoriesService.GenerateNavbarViewModel(session);

                    return this.View(nvm);
                }
            }

            this.Redirect(response, "/home/topics");
            return null;
        }

        [HttpPost]
        public IActionResult<NavbarViewModel> New(HttpResponse response, HttpSession session, NewCategoryBindingModel ncbm)
        {
            if (this.signInManagerService.IsAuthenticated(session))
            {
                if (this.signInManagerService.GetAuthenticatedUser(session).Role == Enums.UserRole.Administrator)
                {
                    if (this.categoriesService.IsNewCategoryBindingModelValid(ncbm))
                    {
                        this.categoriesService.AddNewCategory(ncbm);

                        this.Redirect(response, "/categories/all");
                        return null;
                    }

                    this.Redirect(response, "/categories/new");
                    return null;
                }
            }

            this.Redirect(response, "/home/topics");
            return null;
        }

        // Edit
        [HttpGet]
        public IActionResult<CategoriesEditViewModel> Edit(HttpResponse response, HttpSession session, int id)
        {
            if (this.signInManagerService.IsAuthenticated(session))
            {
                if (this.signInManagerService.GetAuthenticatedUser(session).Role == Enums.UserRole.Administrator)
                {
                    CategoriesEditViewModel cevm = this.categoriesService.GenerateCategoriesEditViewModel(session, id);

                    return this.View(cevm);
                }
            }

            this.Redirect(response, "/home/topics");
            return null;
        }

        [HttpPost]
        public IActionResult<CategoriesEditViewModel> Edit(HttpResponse response, HttpSession session, RenameCategoryBindingModel rcbm)
        {
            if (this.signInManagerService.IsAuthenticated(session))
            {
                if (this.signInManagerService.GetAuthenticatedUser(session).Role == Enums.UserRole.Administrator)
                {
                    if (this.categoriesService.IsRenameCategoryBindingModelValid(rcbm))
                    {
                        this.categoriesService.RenameCategory(rcbm);

                        this.Redirect(response, "/categories/all");
                        return null;
                    }

                    this.Redirect(response, "/categories/edit");
                    return null;
                }
            }

            this.Redirect(response, "/home/topics");
            return null;
        }

        // Delete
        [HttpGet]
        public IActionResult<CategoriesEditViewModel> Delete(HttpResponse response, HttpSession session, int id)
        {
            if (this.signInManagerService.IsAuthenticated(session))
            {
                if (this.signInManagerService.GetAuthenticatedUser(session).Role == Enums.UserRole.Administrator)
                {
                    if (this.categoriesService.IsDeleteCategoryIdValid(id))
                    {
                        this.categoriesService.DeleteCategory(id);

                        this.Redirect(response, "/categories/all");
                        return null;
                    }
                }
            }

            this.Redirect(response, "/home/topics");
            return null;
        }

        // Topics
        [HttpGet]
        public IActionResult<CategorisTopicsViewModel> Topics(HttpSession session, string categoryname)
        {
            CategorisTopicsViewModel ctvm = this.categoriesService.GenerateCategorisTopicsViewModel(session, categoryname);

            return View(ctvm);
        }
    }
}