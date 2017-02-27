namespace PizzaForumApplication
{
    using SimpleHttpServer;
    using SimpleMVC;

    class AppStart
    {
        static void Main()
        {
            HttpServer server = new HttpServer(8081, RoutesTable.Routes);
            MvcEngine.Run(server, "PizzaForumApplication");
        }
    }
}