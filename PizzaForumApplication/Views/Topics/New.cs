namespace PizzaForumApplication.Views.Topics
{
    using SimpleMVC.Interfaces;
    using SimpleMVC.Interfaces.Generic;
    using System;
    using System.IO;
    using System.Text;
    using ViewModels;

    public class New : IRenderable<TopicsNewViewModel>
    {
        public TopicsNewViewModel Model { get; set; }

        public string Render()
        {
            string header = File.ReadAllText("../../Content/header.html");
            string navigation = File.ReadAllText("../../Content/nav-logged.html");
            string newTopicForm = File.ReadAllText("../../Content/topic-new-form.html");
            string footer = File.ReadAllText("../../Content/footer.html");

            navigation = navigation.Replace("##userid##", $"{Model.UserId}");
            navigation = navigation.Replace("##username##", $"{Model.Username}");

            StringBuilder categorySelectBuilder = new StringBuilder();

            foreach (var category in Model.Categories)
            {
                categorySelectBuilder.Append($"<option>{category.CategoryName}</option>");
            }

            newTopicForm = newTopicForm.Replace("##categories##", $"{categorySelectBuilder.ToString()}");
            
            return header + navigation + "<div class=\"container\">" + newTopicForm + "</div>" + footer;
        }
    }
}
