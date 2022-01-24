using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Modelo;


namespace _2_Library.Config
{
    public class RepositorioCorr<T> : IRepositorioCorr<T> where T : class
    {
        private CorreiosEntities correiosEntities;
        private DbSet<T> DbSet;
       
        public RepositorioCorr()
        {
            this.correiosEntities = new CorreiosEntities();
            this.DbSet = this.correiosEntities.Set<T>();
        }
        public RepositorioCorr(CorreiosEntities correiosEntities)
        {
            this.correiosEntities = correiosEntities;
            this.DbSet = this.correiosEntities.Set<T>();
        }

        public T Find(params object[] Keys)
        {
            return DbSet.Find(Keys);
        }

        public IQueryable<T> Select()
        {
            return DbSet;
        }

        public IQueryable<T> Select(System.Linq.Expressions.Expression<Func<T, bool>> Where)
        {
            return DbSet.Where(Where).AsQueryable<T>();
        }

        public T FindFirst()
        {
            return DbSet.FirstOrDefault();
        }

        public Boolean Remove(T entity)
        {
            DbSet.Remove(entity);
            if (SaveChanges() > 0)
                return true;
            else return false;
        }

        public void Add(T entity)
        {
            var dataHora = entity.GetType().GetProperties().Where(s => s.Name.EndsWith("_dataHora")).FirstOrDefault();
            if (dataHora != null)
                dataHora.SetValue(entity, DateTime.Now, null);

            DbSet.Add(entity);
            SaveChanges();
        }

        public void Add(IList<T> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                foreach (T entity in entities)
                {
                    var dataHora = entity.GetType().GetProperties().Where(s => s.Name.EndsWith("_dataHora")).FirstOrDefault();
                    if (dataHora != null)
                        dataHora.SetValue(entity, DateTime.Now, null);

                    DbSet.Add(entity);
                }
                SaveChanges();
            }
        }

        public void Attach(T entity)
        {
            DbSet.Attach(entity);
        }

        public Boolean Update(T entity)
        {
            if (SaveChanges() > 0)
                return true;
            else return false;
        }

        public Boolean Update(IList<T> entities)
         {
             if (entities != null && entities.Count() > 0)
             {
                 foreach (T entity in entities)
                 {
                     var dataHora = entity.GetType().GetProperties().Where(s => s.Name.EndsWith("_dataHoraAtualizacao")).FirstOrDefault();
                     if (dataHora != null)
                         dataHora.SetValue(entity, DateTime.Now, null);
                 }
             }
             if (SaveChanges() > 0)
                 return true;
             else return false;
         }

        private int SaveChanges()
        {
            try
            {
                return correiosEntities.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public void Dispose()
        {
            correiosEntities.Dispose();
            GC.SuppressFinalize(this);
            correiosEntities = null;
        }
    }
}