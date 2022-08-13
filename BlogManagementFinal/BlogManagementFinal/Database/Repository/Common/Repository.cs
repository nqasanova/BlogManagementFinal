using System;
using System.Collections.Generic;
using BlogManagementFinal.Database.Models.Common;

namespace BlogManagementFinal.Database.Repository.Common
{
    public class Repository<T, TId>
        where T : Entity<TId>
    {
        protected static List<T> DbContext { get; set; } = new List<T>()
        {

        };

        public T Add(T entry)
        {
            DbContext.Add(entry);
            return entry;
        }

        public void Delete(T entry)
        {
            DbContext.Remove(entry);
        }

        public List<T> GetAll()
        {
            return DbContext;
        }

        public List<T> GetAll(Predicate<T> predicate)
        {
            List<T> list = new List<T>();

            foreach (T entity in DbContext)
            {
                if (predicate(entity))
                {
                    list.Add(entity);
                }
            }

            return list;
        }

        public T GetById(TId id)
        {
            foreach (T entry in DbContext)
            {
                if (Equals(entry.Id, id))
                {
                    return entry;
                }
            }

            return default(T);
        }

        public T Get(Predicate<T> expression)
        {
            foreach (T entry in DbContext)
            {
                if (expression(entry))
                {
                    return entry;
                }
            }

            return null;
        }

        public T Update(TId id, T newEntry)
        {
            T entry = GetById(id);
            newEntry.CreatedAt = entry.CreatedAt;
            newEntry.Id = entry.Id;
            entry = newEntry;

            return entry;
        }
    }
}
