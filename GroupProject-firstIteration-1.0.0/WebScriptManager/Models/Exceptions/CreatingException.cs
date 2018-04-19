using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScriptManager.Models.Exceptions
{

    /// <summary>
    /// класс ошибок, возникающий, когда невозможно создать элемент
    /// </summary>
    [Serializable]
    public class CreatingException : Exception
    {
        public CreatingException() { }
        /// <summary>
        /// возникает, когда не возможно создать элемент
        /// </summary>
        /// <param name="message">название создаваемого элемента</param>
        public CreatingException(string message) : base(message + " невозможно создать") { }
        public CreatingException(string message, Exception inner) : base(message + " невозможно создать", inner) { }
        protected CreatingException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}