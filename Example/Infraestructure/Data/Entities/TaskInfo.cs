using System;

namespace Example.Infraestructure.Data.Entities
{
    /// <summary>
    /// Representa el estado de la ejecución de una tarea
    /// </summary>
    public enum TaskStatusCode
    {
        /// <summary>
        /// Ejecución fallida
        /// </summary>
        Fail = 0,

        /// <summary>
        /// Ejecución finalizada exitosamente
        /// </summary>
        Success = 1,

        /// <summary>
        /// Ejecución pendiente
        /// </summary>
        NotStarted = 2,

        /// <summary>
        /// Ejecución en proceso
        /// </summary>
        Running = 3,
    }

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
        public TaskStatusCode Status { get; set; }
    }
}