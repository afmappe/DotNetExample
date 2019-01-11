using Example.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Example.Extensions
{
    /// <summary>
    /// Extensión para ordenamiento y paginación
    /// </summary>
    public static class IQueryableExt
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="paginator"></param>
        /// <returns></returns>
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, PaginationInfo paginator)
        {
            return query.Paginate(paginator.ColumnOrder, paginator.StartIndex, paginator.PageSize, paginator.SortDirection);
        }

        /// <summary>
        /// Extension Ordenamiento y paginación para consultas
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto</typeparam>
        /// <param name="query">Consulta</param>
        /// <param name="field">Campo para ordenar</param>
        /// <param name="startIndex">indice donde empieza la búsqueda</param>
        /// <param name="pageSize">Cantidad de registros que se van a traer</param>
        /// <param name="direction">Ordenamiento ascendente(Asc) o descendente (Desc)</param>
        /// <returns>Consulta ajustada a los parámetros</returns>
        private static IQueryable<T> Paginate<T>(this IQueryable<T> query, string field, int startIndex, int pageSize, SortDirectionCode direction)
        {
            var parameter = Expression.Parameter(typeof(T), "p");

            string[] properties = field.Split('.');

            MemberExpression mex = Expression.Property(parameter, properties[0]);
            for (int i = 1; i < properties.Length; i++)
            {
                mex = Expression.Property(mex, properties[i]);
            }
            var exp = Expression.Lambda(mex, parameter);

            Type[] types = { query.ElementType, exp.Body.Type };

            string method = direction == SortDirectionCode.Asc ? "OrderBy" : "OrderByDescending";

            var mce = Expression.Call(typeof(Queryable), method, types, query.Expression, exp);

            var temp = query.Provider.CreateQuery<T>(mce).Skip(startIndex);

            IQueryable<T> result = pageSize == 0 ? temp : temp.Take(pageSize);

            return result;
        }
    }
}