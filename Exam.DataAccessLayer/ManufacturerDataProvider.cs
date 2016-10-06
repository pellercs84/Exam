using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Exam.DataAccessLayer
{
    internal class ManufacturerDataProvider:IDataProvider<Manufacturer>
    {
        public IList<Manufacturer> GetAll(params Expression<Func<Manufacturer, object>>[] navigationProperties)
        {
            List<Manufacturer> list;
            using (var context = new DataContext())
            {
                IQueryable<Manufacturer> dbQuery = context.Set<Manufacturer>();

                foreach (Expression<Func<Manufacturer, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include<Manufacturer, object>(navigationProperty);
                }
                //fetching into a new clean list is required to avoid unneeded serialization of EF objects.
                list = dbQuery
                    .AsNoTracking()
                    .ToList()
                    .Select(i=>new Manufacturer
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

        public Manufacturer GetSingle(Func<Manufacturer, bool> where, params Expression<Func<Manufacturer, object>>[] navigationProperties)
        {
            Manufacturer item = null;
            using (var context = new DataContext())
            {
                IQueryable<Manufacturer> dbQuery = context.Set<Manufacturer>();

                //Apply eager loading
                foreach (Expression<Func<Manufacturer, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include<Manufacturer, object>(navigationProperty);
                }

                item = dbQuery
                    .AsNoTracking() //Don't track any changes for the selected item
                    .FirstOrDefault(where); //Apply where clause
            }

            Manufacturer dtoItem = null;
            //fetching into a new clean item is required to avoid unneeded serialization of EF objects.
            if (item != null)
                {
                    dtoItem = new Manufacturer();
                    dtoItem.ID = item.ID;

                    dtoItem.CreationTime = item.CreationTime;
                    dtoItem.Creator = item.Creator;
                    dtoItem.ImagePath = item.ImagePath;
                    dtoItem.Name = item.Name;
                
                    dtoItem.Updater = item.Updater;
                }
            
            return dtoItem;
        }

        public void Add(Manufacturer item)
        {
            using (var context = new DataContext())
            {
                context.Entry(item).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(Manufacturer item)
        {
            using (var context = new DataContext())
            {
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Remove(int manufacturerID)
        {
            using (var context = new DataContext())
            {
                var result = from item in context.Manufacturers
                             where item.ID == manufacturerID
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
