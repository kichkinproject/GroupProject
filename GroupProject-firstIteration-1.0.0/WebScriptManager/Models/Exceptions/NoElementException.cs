using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScriptManager.Models.Exceptions
{
    /// <summary>
    /// класс ошибок, возникающий в ситуации поиска элемента, которого не существует
    /// </summary>
    [Serializable]
    public class NoElementException : Exception
    {
        public NoElementException() { }
        /// <summary>
        /// Ошибка, если искомый элемент не найден
        /// </summary>
        /// <param name="message">название элемента</param>
        public NoElementException(string message) : base(message + " не существует") {
        }
        public NoElementException(string message, Exception inner) : base(message + " не существует", inner) { }
        protected NoElementException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}