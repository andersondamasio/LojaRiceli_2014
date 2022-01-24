using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using _2_Library.Modelo;
using System.Linq.Expressions;
using System.Threading.Tasks;
using _2_Library.Utils;

namespace _2_Library.Config
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        protected DbSet<T> DbSet; //{ get; private set; }
        protected LojaEntities lojaEntities;

        public Repositorio()
        {
            lojaEntities = new LojaEntities();
            this.DbSet = this.lojaEntities.Set<T>();

        }
        public Repositorio(LojaEntities _lojaEntities)
        {
            lojaEntities = new LojaEntities();
            this.DbSet = this.lojaEntities.Set<T>();
        }

        public T Find(params object[] Keys)
        {
            return this.DbSet.Find(Keys);
        }

        public IQueryable<T> Select()
        {
            return DbSet;
        }

        public IQueryable<T> Clonar(System.Linq.Expressions.Expression<Func<T, bool>> Where)
        {
            return this.DbSet.Where(Where).AsNoTracking<T>();
        }

        public IQueryable<T> Select(System.Linq.Expressions.Expression<Func<T, bool>> Where)
        {
            return this.DbSet.Where(Where).AsQueryable<T>();
        }

        public T FindFirst()
        {
            return this.DbSet.FirstOrDefault();
        }

        public Boolean Remove(T entity)
        {
            DbSet.Remove(entity);
            if (SaveChanges() > 0)
                return true;
            else return false;
        }

        public void Remove(IQueryable<T> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                foreach (T entity in entities)
                {
                    DbSet.Remove(entity);
                }
                SaveChanges();
            }
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



     /*   internal IEnumerable<T> Get(
    Expression<Func<T, bool>> filter = null,
    Func<IQueryable<T>,
        IOrderedQueryable<T>> orderBy = null,
    List<Expression<Func<T, object>>>
        includeProperties = null,
    int? page = null,
    int? pageSize = null)
        {
            IQueryable<T> query = Entity;

            if (includeProperties != null)
                includeProperties.ForEach(i => query.Include(i));

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);


            return query.ToList();
        }

        private readonly List<Expression<Func<T, object>>>
        _includeProperties;
        private Expression<Func<T, bool>> _filter;
        private Func<IQueryable<T>,
            IOrderedQueryable<T>> _orderByQuerable;
        private int? _page;
        private int? _pageSize;

        public IEnumerable<T> GetPage(
        int page, int pageSize, out int totalCount)
        {
            _page = page;
            _pageSize = pageSize;
            totalCount = Get(_filter).Count();

            return Get(
                _filter,
                _orderByQuerable, _includeProperties, _page, _pageSize);
        }

        public void Index(int? page)
        {
            var pageNumber = page ?? 1;
            const int pageSize = 20;

            int totalCustomerCount;

            var customers =
                unitOfWork.Repository<Customer>()
                    .Query().GetPage(pageNumber, pageSize, out totalCustomerCount);

            ViewBag.Customers = new StaticPagedList<Customer>(
                customers, pageNumber, pageSize, totalCustomerCount);

            unitOfWork.Save();

            return View();
        }*/


        public void Attach(T entity)
        {
            DbSet.Attach(entity);
        }

        public Boolean Update(T entity)
        {
            var entry = lojaEntities.Entry(entity);
            var modifiedProperties = entry.CurrentValues.PropertyNames.Where(propertyName => entry.Property(propertyName).IsModified).ToList();

            if (modifiedProperties.Count() > 0)
            {
                var dataHora = entity.GetType().GetProperties().Where(s => s.Name.EndsWith("_dataHoraAtualizacao")).FirstOrDefault();
                if (dataHora != null)
                    dataHora.SetValue(entity, DateTime.Now, null);
            }
            

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

                     //lojaEntities.Entry(entity).State = System.Data.EntityState.Modified;
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
                int save = lojaEntities.SaveChanges();

                return save;
            }
            catch (DbEntityValidationException e)
            {
                string mensagemErro = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    

                    mensagemErro = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);

                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        mensagemErro +=  System.Environment.NewLine+string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                       eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw new DbEntityValidationException(mensagemErro, e.EntityValidationErrors);
            }
        }

        public void Dispose()
        {
            lojaEntities.Dispose();
            GC.SuppressFinalize(this);
            lojaEntities = null;
        }


    }

    public class LoadAPI<T> where T : class
    {
        public static T LoadRecord(Dictionary<string, string> dataDic)
        {
            var instance = Activator.CreateInstance<T>();

            RecursiveLoad(instance, dataDic);

            return instance;
        }

        private static void RecursiveLoad(object instance, Dictionary<string, string> dataDic)
        {
            foreach (var property in instance.GetType().GetProperties())
            {
                Console.WriteLine(property.PropertyType);
                Console.WriteLine(typeof(string));

                if (property.PropertyType == typeof(string) && dataDic.Keys.Contains(property.Name))
                {
                    property.SetValue(instance, dataDic[property.Name], null);
                    continue;
                }

                if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    var innerInstance = Activator.CreateInstance(property.PropertyType);

                    RecursiveLoad(innerInstance, dataDic);

                    property.SetValue(instance, innerInstance, null);
                }
            }
        }
        public void FixEfProviderServicesProblem()
        {
            //The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            //for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            //Make sure the provider assembly is available to the running application. 
            //See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.

            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}