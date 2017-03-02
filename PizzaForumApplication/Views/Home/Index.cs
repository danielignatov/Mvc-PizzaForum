namespace PizzaForumApplication.Views.Home
{
    using SimpleMVC.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Index : IRenderable
    {
        public string Render()
        {
            string header = File.ReadAllText("../../Content/header.html");
            string footer = File.ReadAllText("../../Content/footer.html");

            return header + footer;
        }
    }
}