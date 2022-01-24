using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using System.Reflection.Emit;
using System.Threading;
using System.Collections;
using System.Data.Objects.DataClasses;
using System.Data.Objects;

namespace _2_Library
{
    public static class Extensions
    {

        public static IEnumerable<TEntity> OrderBy<TEntity>(this IEnumerable<TEntity> source1, string sortExpression) where TEntity : class
        {
            IQueryable<TEntity> source = source1.AsQueryable<TEntity>();


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
            if (string.IsNullOrEmpty(sortExpression))
                return source;

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

        public static void LoadAllProperties<T>(this T entity, ObjectContext context)
        {
            Type type = entity.GetType();
            context.Refresh(RefreshMode.ClientWins, entity);
            foreach (var property in type.GetProperties())
            {
                if (property.PropertyType.Name.StartsWith("ICollection"))
                {
                    context.LoadProperty(entity, property.Name);
                    var itemCollection = property.GetValue(entity, null) as IEnumerable;
                    foreach (object item in itemCollection)
                    {
                        item.LoadAllProperties(context);
                    }
                }
            }
        }

    }

}