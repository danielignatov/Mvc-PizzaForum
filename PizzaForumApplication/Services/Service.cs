namespace PizzaForumApplication.Services
{
    using System;
    using BindingModels;
    using PizzaForumApplication.Data;

    public abstract class Service
    {
        public Service()
        {
            this.Context = Data.Context;
        }

        protected PizzaForumContext Context { get; }
    }
}