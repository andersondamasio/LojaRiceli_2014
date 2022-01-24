using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Library.Modelo;

namespace _2_Library.Config
{
    public interface IRepositorio<T> : IDisposable where T : class
    {
        //LojaEntities lojaEntities { get; }
        //DbSet<T> Entity { get; }
        T Find(params object[] Keys);
        IQueryable<T> Select();
        T FindFirst();
        Boolean Remove(T entity);
        void Add(T entity);
        void Add(IList<T> entities);
        Boolean Update(T entity);
        Boolean Update(IList<T> Entities);
        void Attach(T entity);

    }
}
