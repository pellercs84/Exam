using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Exam.DataAccessLayer
{
    class ProductDataProvider:IDataProvider<Product>
    {
        public IList<Product> GetAll(params Expression<Func<Product, object>>[] navigationProperties)
        {
            List<Product> list;
            using (var context = new DataContext())
            {

                IQueryable<Product> dbQuery = context.Products;

                foreach (Expression<Func<Product, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include<Product, object>(navigationProperty);
                }

                //fetching into a new clean list is required to avoid unneeded serialization of EF objects.
                list = dbQuery
                    .AsNoTracking()
                    .ToList().Select(i=>new Product()
                                                      {
                                                          ID = i.ID,
                                                          CreationTime = i.CreationTime,
                                                          Creator = i.Creator,
                                                          ImagePath = i.ImagePath,
                                                          Name = i.Name,
                                                          UpdateTime = i.UpdateTime,
                                                          Updater = i.Updater
                                                          
                                                      }).ToList();
            }
            return list;
        }

        public Product GetSingle(Func<Product, bool> where, params Expression<Func<Product, object>>[] navigationProperties)
        {
            Product item = null;
            using (var context = new DataContext())
            {
                IQueryable<Product> dbQuery = context.Set<Product>();

                foreach (Expression<Func<Product, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include<Product, object>(navigationProperty);
                }

                item = dbQuery
                    .AsNoTracking() 
                    .FirstOrDefault(where);
            }

            Product dtoItem = null;
            //fetching into a new clean container is required to avoid unneeded serialization of EF objects.
            if (item != null)
                {
                    dtoItem = new Product();
                    dtoItem.ID = item.ID;
                    dtoItem.CreationTime = item.CreationTime;
                    dtoItem.Creator = item.Creator;
                    dtoItem.ImagePath = item.ImagePath;
                    dtoItem.Name = item.Name;
                
                    dtoItem.Updater = item.Updater;
                }
            
            return dtoItem;
        }

        public void Add(Product item)
        {
            using (var context = new DataContext())
            {
                context.Entry(item).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(Product item)
        {
            using (var context = new DataContext())
            {
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Remove(int productId)
        {
            using (var context = new DataContext())
            {
                var result = from item in context.Products
                             where item.ID == productId
                             select item;

                if (result.Any())
                {
                    context.Entry(result.First()).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
        }
    }
}
