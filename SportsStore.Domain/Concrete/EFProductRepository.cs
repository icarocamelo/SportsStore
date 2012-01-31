using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Mappings;

namespace SportsStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Product> Products
        {
            get { return context.Products; }
        }


        public bool Delete(Product p)
        {
            var prod = context.Products.Remove(p);
            context.SaveChanges();
            return true;
        }


        public Product Save(Product p)
        {
            if (p == null)
                return null;

            if (p.ProductID == 0)
                context.Products.Add(p);
            else
                context.Entry(p).State = System.Data.EntityState.Modified;
            
            context.SaveChanges();

            return p;
        }
    }
}
