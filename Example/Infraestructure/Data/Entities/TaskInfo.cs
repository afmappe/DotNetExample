using System;

namespace Example.Infraestructure.Data.Entities
{
    /// <summary>
    ///
    /// </summary>
    public class TaskInfo
    {
        /// <summary>
        ///
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime? CompletDate { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int StatusId { get; set; }
    }
}