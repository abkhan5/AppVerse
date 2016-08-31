using System;
using System.Data.Entity;
using System.Linq;

namespace AppVerse.Desktop.LodgeModels
{
    public abstract class SqlContextBase : DbContext
    {

        public SqlContextBase(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }



        public T Find<T>(int id) where T : class
        {
            return Set<T>().Find(id);
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return this.Set<T>();
        }

        public void Save<T>(T entity) where T : class
        {
            try
            {
                var entry = this.Entry(entity);

                if (entry.State == EntityState.Detached)
                    this.Set<T>().Add(entity);

                SaveChanges();
            }
            catch (Exception exception)
            {

                throw;
            }
        }
    }
}
