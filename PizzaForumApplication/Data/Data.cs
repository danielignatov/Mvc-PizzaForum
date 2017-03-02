namespace PizzaForumApplication.Data
{
    public class Data
    {
        private static PizzaForumContext context;

        public static PizzaForumContext Context => context ?? (context = new PizzaForumContext());
    }
}