namespace PizzaForumApplication.Services
{
    using Models;
    using PizzaForumApplication.Data;
    using SimpleHttpServer.Models;
    using System.Linq;

    public class SignInManagerService : Service
    {
        public SignInManagerService()
        {

        }

        public bool IsAuthenticated(HttpSession session)
        {
            if (session == null)
            {
                return false;
            }

            return this.Context.Logins.Any(u => u.SessionId == session.Id && u.IsActive == true);
        }

        public User GetAuthenticatedUser(HttpSession session)
        {
            return this.Context.Logins.Where(u => u.SessionId == session.Id && u.IsActive == true).Select(u => u.User).FirstOrDefault();
        }
    }
}