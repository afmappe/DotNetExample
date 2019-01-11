using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Example.Entities
{
    /// <summary>
    /// Tipo de ordenamiento
    /// </summary>
    public enum SortDirectionCode
    {
        /// <summary>
        /// Ordenamiento ascendente
        /// </summary>
        Asc,

        /// <summary>
        /// Ordenamiento descendente
        /// </summary>
        Desc
    }

    /// <summary>
    /// Representa una lista paginadas
    /// </summary>
    /// <typeparam name="T">Tipo de dato del listados</typeparam>
    [DataContract]
    public class ListInfo<T>
    {
        /// <summary>
        /// Cantidad Total de elementos que existen
        /// </summary>
        [DataMember(Name = "count")]
        public int Count { get; set; }

        /// <summary>
        /// Los datos que viajan serializados
        /// </summary>
        [DataMember(Name = "data")]
        public List<T> Data { get; set; }
    }

    /// <summary>
    /// Entidad de paginación
    /// </summary>
    [DataContract]
    [Serializable]
    public class PaginationInfo
    {
        /// <summary>
        /// Columna por la cual se ordena la búsqueda
        /// </summary>
        [DataMember(Name = "columnOrder")]
        public string ColumnOrder { get; set; }

        /// <summary>
        /// Número de items que componen la página
        /// </summary>
        [DataMember(Name = "pageSize")]
        public int PageSize { get; set; }

        /// <summary>
        /// Determina si el ordenamiento es ascendente
        /// </summary>
        [DataMember(Name = "sortDirection")]
        public SortDirectionCode SortDirection { get; set; }

        /// <summary>
        /// Indice donde empieza la búsqueda
        /// </summary>
        [DataMember(Name = "startIndex")]
        public int StartIndex { get; set; }
    }
}