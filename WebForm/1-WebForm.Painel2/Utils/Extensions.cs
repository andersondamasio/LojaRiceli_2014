using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Reflection;
using System.Data;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.EntityClient;
//using System.Data.Metadata.Edm;
using System.Web.Caching;
using System.IO;

namespace Loja
{
    public static class Extensions
    {

        // <summary>
        /// Extends method which allow to sort by string field name.
        /// Allow to use a relative object definition for sorting (ex:LinkedObject.FieldsName1)
        /// </summary>
        /// <typeparam name=”TEntity”>Current Object type for query</typeparam>
        /// <param name=”source”>list of defined object</param>
        /// <param name=”sortExpression”>string name of the field we want to sort by</param>
        /// <returns>Query sorted by sortExpression</returns>
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string sortExpression) where TEntity : class
        {
            var type = typeof(TEntity);
            // Remember that for ascending order GridView just returns the column name and
            // for descending it returns column name followed by DESC keyword
            // Therefore we need to examine the sortExpression and separate out Column Name and
            // order (ASC/DESC)
            string[] expressionParts = sortExpression.Split(' '); // Assuming sortExpression is like [ColumnName DESC] or [ColumnName]
            string orderByProperty = expressionParts[0];
            string sortDirection = "ASC";
            string methodName = "OrderBy";
            //if sortDirection is descending
            if (expressionParts.Length > 1 && expressionParts[1] == "DESC")
            {
                sortDirection = "Descending";
                methodName += sortDirection; // Add sort direction at the end of Method name
            }
            MethodCallExpression resultExp = null;
            if (!orderByProperty.Contains("."))
            {
                var property = type.GetProperty(orderByProperty);
                var parameter = Expression.Parameter(type, "p");
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), methodName,
                new Type[] { type, property.PropertyType },
                source.Expression, Expression.Quote(orderByExp));
            }
            else
            {
                Type relationType = type.GetProperty(orderByProperty.Split('.')[0]).PropertyType;
                PropertyInfo relationProperty = type.GetProperty(orderByProperty.Split('.')[0]);
                PropertyInfo relationProperty2 = relationType.GetProperty(orderByProperty.Split('.')[1]);
                var parameter = Expression.Parameter(type, "p");
                var propertyAccess = Expression.MakeMemberAccess(parameter, relationProperty);
                var propertyAccess2 = Expression.MakeMemberAccess(propertyAccess, relationProperty2);
                var orderByExp = Expression.Lambda(propertyAccess2, parameter);
                resultExp = Expression.Call(typeof(Queryable), methodName,
                new Type[] { type, relationProperty2.PropertyType },
                source.Expression, Expression.Quote(orderByExp));
            }
            return source.Provider.CreateQuery<TEntity>(resultExp);
        }

        /// <summary>
        /// Allow to add another sorting on a query with a string representation of the field to sort by.
        /// </summary>
        /// <typeparam name=”TEntity”>Current Object type for query</typeparam>
        /// <param name=”source”>list of defined object</param>
        /// <param name=”sortExpression”>string name of the field we want to sort by</param>
        /// <returns>Query sorted by sortExpression</returns>
        public static IOrderedQueryable<TEntity> ThenBy<TEntity>(
        this IOrderedQueryable<TEntity> source, string sortExpression) where TEntity : class
        {
            var type = typeof(TEntity);
            // Remember that for ascending order GridView just returns the column name and
            // for descending it returns column name followed by DESC keyword
            // Therefore we need to examine the sortExpression and separate out Column Name and
            // order (ASC/DESC)
            string[] expressionParts = sortExpression.Split(' '); // Assuming sortExpression is like [ColumnName DESC] or [ColumnName]
            string orderByProperty = expressionParts[0];
            string sortDirection = "ASC";
            string methodName = "ThenBy";
            //if sortDirection is descending
            if (expressionParts.Length > 1 && expressionParts[1] == "DESC")
            {
                sortDirection = "Descending";
                methodName += sortDirection; // Add sort direction at the end of Method name
            }
            MethodCallExpression resultExp = null;
            if (!orderByProperty.Contains("."))
            {
                var property = type.GetProperty(orderByProperty);
                var parameter = Expression.Parameter(type, "p");
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), methodName,
                new Type[] { type, property.PropertyType },
                source.Expression, Expression.Quote(orderByExp));
            }
            else
            {
                Type relationType = type.GetProperty(orderByProperty.Split('.')[0]).PropertyType;
                PropertyInfo relationProperty = type.GetProperty(orderByProperty.Split('.')[0]);
                PropertyInfo relationProperty2 = relationType.GetProperty(orderByProperty.Split('.')[1]);
                var parameter = Expression.Parameter(type, "p");
                var propertyAccess = Expression.MakeMemberAccess(parameter, relationProperty);
                var propertyAccess2 = Expression.MakeMemberAccess(propertyAccess, relationProperty2);
                var orderByExp = Expression.Lambda(propertyAccess2, parameter);
                resultExp = Expression.Call(typeof(Queryable), methodName,
                new Type[] { type, relationProperty2.PropertyType },
                source.Expression, Expression.Quote(orderByExp));
            }
            return (IOrderedQueryable<TEntity>)source.Provider.CreateQuery<TEntity>(resultExp);
        }

        public static DataTable ToDataTable(IOrderedQueryable iOrderedQueryable)
        {
            ObjectQuery oQuery = (ObjectQuery)iOrderedQueryable;
            string cmdSQL = oQuery.ToTraceString();
            DataTable dataTable = new DataTable();
            using (SqlCommand fbCommand = new SqlCommand())
            {



                EntityConnectionStringBuilder builder = new EntityConnectionStringBuilder(ConfigurationManager.ConnectionStrings["LojaEntities"].ToString());
                SqlConnectionStringBuilder sbuilder = new SqlConnectionStringBuilder(builder.ProviderConnectionString);



                using (SqlConnection fbConnection = new SqlConnection(sbuilder.ConnectionString))//Conexao.GetConexaoFrete())
                {
                    fbCommand.Connection = fbConnection;
                    fbCommand.CommandText = cmdSQL;

                    foreach (var param in oQuery.Parameters)
                    {
                        fbCommand.Parameters.Add(new SqlParameter(param.Name, param.Value));
                    }

                    using (SqlDataAdapter fbDataAdapter = new SqlDataAdapter())
                    {
                        fbDataAdapter.SelectCommand = fbCommand;
                        dataTable.Locale = System.Globalization.CultureInfo.InvariantCulture;
                        fbDataAdapter.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }

        public static object ToNonAnonymousList<T>(this List<T> list, Type t)
        {

            //define system Type representing List of objects of T type:
            var genericType = typeof(List<>).MakeGenericType(t);

            //create an object instance of defined type:
            var l = Activator.CreateInstance(genericType);

            //get method Add from from the list:
            MethodInfo addMethod = l.GetType().GetMethod("Add");

            //loop through the calling list:
            foreach (T item in list)
            {

                //convert each object of the list into T object 
                //by calling extension ToType<T>()
                //Add this object to newly created list:
                addMethod.Invoke(l, new object[] { item.ToType(t) });
            }

            //return List of T objects:
            return l;
        }

        public static object ToType<T>(this object obj, T type)
        {

            //create instance of T type object:
            var tmp = Activator.CreateInstance(Type.GetType(type.ToString()));

            //loop through the properties of the object you want to covert:          
            foreach (PropertyInfo pi in obj.GetType().GetProperties())
            {
                try
                {

                    //get the value of property and try 
                    //to assign it to the property of T type object:
                    tmp.GetType().GetProperty(pi.Name).SetValue(tmp,
                                              pi.GetValue(obj, null), null);
                }
                catch { }
            }

            //return the T type object:         
            return tmp;
        }

    /*    public static SqlCommand ToSqlCommand<T>(this DbQuery<T> query)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = query.ToString();

            var objectQuery = query.ToObjectQuery();

            foreach (var param in objectQuery.Parameters)
            {
                command.Parameters.AddWithValue(param.Name, param.Value);
            }
            return command;
        }*/

        public static IEnumerable<T> Cached<T>(this ObjectQuery<T> query)
        {
            string queryString = query.ToString();
            string tableName = queryString.Substring(queryString.LastIndexOf('.') + 1).TrimEnd(']');
            string key = "ObjectQuery_" + tableName + query.ToTraceString();

            IEnumerable<T> result = HttpRuntime.Cache[key] as IEnumerable<T>;

            if (result == null)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["pubsConnectionString2"].ConnectionString;
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(query.ToTraceString(), cn);
                    cmd.Notification = null;
                    cmd.NotificationAutoEnlist = true;
                    SqlCacheDependencyAdmin.EnableNotifications(connectionString);

                    if (!SqlCacheDependencyAdmin.GetTablesEnabledForNotifications(connectionString).Contains(tableName))
                        SqlCacheDependencyAdmin.EnableTableForNotifications(connectionString, tableName);

                    SqlCacheDependency dependency = new SqlCacheDependency(cmd);
                    cmd.ExecuteNonQuery();
                    result = query.ToList();
                    HttpRuntime.Cache.Insert(key, result, dependency);
                }
            }
            return result;
        }

    /*    public static List<int?> LinqToCacheAdd<T>(this T iqueryable, string nomeTabela,string chaveCache, ObjectResult<int?> objectResult)
        {
            int loj_id = new _2_Library.Dao.LojaX.LojaTd().SelectLoja(null).loj_id;
            string diretorio = HttpContext.Current.Request.PhysicalApplicationPath + @"Cache\xml\" + loj_id + @"\";
            string arquivo = nomeTabela + ".xml";

            chaveCache = loj_id + chaveCache;

            System.Web.Caching.Cache cache = HttpContext.Current.Cache;
            List<int?> result = (List<int?>)HttpRuntime.Cache[chaveCache];
            if (result == null)
            {
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                result = objectResult.ToList();

            }

            System.Web.Caching.CacheDependency cacheDependency = new System.Web.Caching.CacheDependency(diretorio + arquivo);
            HttpRuntime.Cache.Insert(chaveCache, result, cacheDependency);

            return result;
        }*/

        public static List<T> LinqToCacheAdd<T>(this IEnumerable<T> iqueryable, string nomeTabela, string chaveCache)
        {
            int loj_id = new _2_Library.Dao.LojaX.LojaTd().SelectLoja(null).loj_id;
            string diretorio = HttpContext.Current.Request.PhysicalApplicationPath + @"Cache\xml\" + loj_id + @"\";
            string arquivo = diretorio + "loja-" + loj_id + "-" + nomeTabela + ".xml";
            ObjectQuery query = (ObjectQuery)iqueryable;
            string metodo = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name;

            //string queryString = query.ToTraceString();
            string chave = loj_id + metodo + chaveCache;

            System.Web.Caching.Cache cache = HttpContext.Current.Cache;
            List<T> result = HttpRuntime.Cache[chave] as List<T>;
            if (result == null)
            {
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);
                
                if (!File.Exists(arquivo))
                    File.AppendAllText(arquivo, DateTime.Now.ToString());

                result = iqueryable.ToList();

                System.Web.Caching.CacheDependency cacheDependency = new System.Web.Caching.CacheDependency(arquivo);
                HttpRuntime.Cache.Insert(chave, result, cacheDependency);
            }
            return result;
        }

        public static Int32 LinqToCacheAdd<T>(this IQueryable<T> iqueryable, string nomeTabela,string chaveCache, Boolean count)
        {
            int loj_id = new _2_Library.Dao.LojaX.LojaTd().SelectLoja(null).loj_id;

            string diretorio = HttpContext.Current.Request.PhysicalApplicationPath + @"Cache\xml\" + loj_id + @"\";
            string arquivo = "loja-" + loj_id + "-" + nomeTabela + ".xml";

            ObjectQuery query = (ObjectQuery)iqueryable;
            string metodo = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name;
            string chave = loj_id + metodo + chaveCache;

            System.Web.Caching.Cache cache = HttpContext.Current.Cache;
            int? result = HttpRuntime.Cache[chave] as int?;
            if (result == null)
            {
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);
                result = iqueryable.Count();
            }

            System.Web.Caching.CacheDependency cacheDependency = new System.Web.Caching.CacheDependency(diretorio + arquivo);
            HttpRuntime.Cache.Insert(chave, result, cacheDependency);

            result = result ?? 0;

            return result.Value;
        }

  /*      public static ObjectQuery<T> ToObjectQuery<T>(this DbQuery<T> query)
        {
            var internalQuery = query.GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(field => field.Name == "_internalQuery")
                .Select(field => field.GetValue(query))
                .First();

            var objectQuery = internalQuery.GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(field => field.Name == "_objectQuery")
                .Select(field => field.GetValue(internalQuery))
                .Cast<ObjectQuery<T>>()
                .First();

            return objectQuery;
        }*/

        public static void LinqToCacheRemove(string nomeTabela)
        {
            int loj_id = new _2_Library.Dao.LojaX.LojaTd().SelectLoja(null).loj_id;
            string diretorio = HttpContext.Current.Request.PhysicalApplicationPath + @"Cache\xml\" + loj_id + @"\";
            string arquivo = diretorio + "loja-" + loj_id + "-" + nomeTabela + ".xml";

            if (File.Exists(arquivo))
                File.AppendAllText(arquivo, DateTime.Now.ToString());
        }

        static void Check<T>(Expression<Func<T>> expr)
        {
            var body = ((MemberExpression)expr.Body);
            Console.WriteLine("Name is: {0}", body.Member.Name);
        }
    }
}
namespace ManagerWeb.Utils
{

}