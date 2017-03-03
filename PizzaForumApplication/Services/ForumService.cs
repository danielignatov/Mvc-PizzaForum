namespace PizzaForumApplication.Services
{
    using BindingModels;
    using Models;
    using PizzaForumApplication.Data;
    using System.Linq;
    using System;
    using System.Text.RegularExpressions;
    using SimpleHttpServer.Models;
    using SimpleHttpServer.Utilities;
    using ViewModels;
    using System.Collections.Generic;

    public class ForumService : Service
    {
        private SignInManagerService signInManagerService;

        public ForumService()
        {
            this.signInManagerService = new SignInManagerService();
        }

        public User GetCorrespondingLoginUser(LoginUserBindingModel lubm)
        {
            var user = this.Context.Users.FirstOrDefault(
                  u => u.Username == lubm.Username
                  && u.Password == lubm.Password);

            return user;
        }

        public void LoginUser(User user, string sessionId)
        {
            Login login = new Login()
            {
                IsActive = true,
                SessionId = sessionId,
                User = user
            };

            this.Context.Logins.Add(login);
            this.Context.SaveChanges();
        }

        public void Logout(HttpResponse response, string sessionId)
        {
            Login currentLogin = this.Context.Logins.FirstOrDefault(login => login.SessionId == sessionId);
            currentLogin.IsActive = false;
            Context.SaveChanges();

            var session = SessionCreator.Create();
            var sessionCookie = new Cookie("sessionId", session.Id + "; HttpOnly; path=/");
            response.Header.AddCookie(sessionCookie);
        }

        public bool IsRegisterBindingModelValid(RegisterUserBindingModel rubm)
        {
            // Check if username has more than 3 characters
            if (rubm.Username.Length < 3)
            {
                return false;
            }

            // Check if username contain only lowercase letters and numbers
            Regex regex = new Regex("^[a-z0-9]+$");
            if (!regex.IsMatch(rubm.Username))
            {
                return false;
            }

            // Check if email contain '@'
            if (!rubm.Email.Contains("@"))
            {
                return false;
            }

            // Check if password contain exactly 4 numbers
            // or the password and confirm password does not match
            Regex passRegex = new Regex("^[0-9]{4}$");
            if (!passRegex.IsMatch(rubm.Password) || rubm.Password != rubm.PasswordConfirm)
            {
                return false;
            }

            // Check if username is already registered
            if (this.Context.Users.Any(u => u.Username == rubm.Username))
            {
                return false;
            }

            // Check if email is already registered
            if (this.Context.Users.Any(u => u.Email == rubm.Email))
            {
                return false;
            }

            return true;
        }

        public ForumProfileViewModel GenerateForumProfileViewModel(HttpSession session, int id)
        {
            ForumProfileViewModel fpvm = new ForumProfileViewModel();
            NavbarViewModel nvm = new NavbarViewModel();
            List<TopicViewModel> ltvp = new List<TopicViewModel>();

            fpvm.ProfileId = id;
            fpvm.ProfileName = this.Context.Users.Where(i => i.Id == id).Select(n => n.Username).FirstOrDefault();

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

            // May throw exception
            foreach (var topic in this.Context.Topics.Where(i => i.Author.Id == id))
            {
                TopicViewModel tvm = new TopicViewModel();

                tvm.TopicName = topic.Title;
                tvm.TopicId = topic.Id;
                tvm.PublishedOn = topic.PublishDate;

                CategoryViewModel cvm = new CategoryViewModel()
                {
                    CategoryName = topic.Category.Name,
                    CategoryId = topic.Category.Id
                };

                tvm.Category = cvm;

                List<ReplyViewModel> lrvm = new List<ReplyViewModel>();

                foreach (var reply in topic.Replies)
                {
                    ReplyViewModel rvm = new ReplyViewModel();

                    lrvm.Add(rvm);
                }

                tvm.Replies = lrvm;

                ltvp.Add(tvm);
            }

            fpvm.Navbar = nvm;
            fpvm.Topics = ltvp;

            return fpvm;
        }

        public void RegisterUserFromBindingModel(RegisterUserBindingModel rubm)
        {
            User user = new User()
            {
                Username = rubm.Username,
                Email = rubm.Email,
                Password = rubm.Password
            };

            if (this.Context.Users.Count() != 0)
            {
                user.Role = Enums.UserRole.RegularUser;
            }
            else
            {
                user.Role = Enums.UserRole.Administrator;
            }

            this.Context.Users.Add(user);
            this.Context.SaveChanges();
        }
    }
}