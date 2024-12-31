using Store.DataAccess.Data;
using Store.DataAccess.Repository.IRepository;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository//Here I am implementing both interface and class which has all the methods
    {
        private readonly ApplicatonDbContext context;
        public CategoryRepository(ApplicatonDbContext context) : base(context) //I need to pass the context to the base class of the method
        {
            this.context = context;
        }
        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Category obj)
        {
            context.Categories.Update(obj);
        }
    }
}
