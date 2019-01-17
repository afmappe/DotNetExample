using System;
using System.Collections.Generic;

namespace Example.Entities
{
    [Serializable]
    public class ExecutionInfo
    {
        /// <summary>
        ///
        /// </summary>
        public Dictionary<string, string> Paramenters { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string TaskId { get; set; }
    }
}