using System;
using System.Collections.Generic;
using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Loja.Utils
{
 /*   public class EntityChangeNotifier<TEntity, TDbContext>
      : IDisposable
        where TEntity : class
        where TDbContext : DbContext, new()
    {
        private DbContext _context;
        private Expression<Func<TEntity, bool>> _query;
        private string _connectionString;

        public EntityChangeNotifier(Expression<Func<TEntity, bool>> query)
        {
            _context = new TDbContext();
            _query = query;
            _connectionString = _context.Database.Connection.ConnectionString;

            SqlDependency.Start(_connectionString);
            RegisterNotification();
        }

        private void RegisterNotification()
        {
            _context = new TDbContext();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = GetCommand())
                {
                    command.Connection = connection;
                    connection.Open();

                    var sqlDependency = new SqlDependency(command);
                    sqlDependency.OnChange += new OnChangeEventHandler(_sqlDependency_OnChange);

                    // NOTE: You have to execute the command, or the notification will never fire.
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                    }
                }
            }
        }

        private SqlCommand GetCommand()
        {
            var q = GetCurrent();

            return q.ToSqlCommand();
        }

        private System.Data.Entity.Infrastructure.DbQuery<TEntity> GetCurrent()
        {
            var query = _context.Set<TEntity>().Where(_query) as DbQuery<TEntity>;

            return query;
        }

        private void _sqlDependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            Console.WriteLine("Info: {0}; Source: {1}; Type: {2}", e.Info, e.Source, e.Type);
            RegisterNotification();
        }

        public void Dispose()
        {
            SqlDependency.Stop(_connectionString);
        }


    }
*/}