using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScriptManager.Models.Exceptions
{
    [Serializable]
    public class NoElementException : Exception
    {
        public NoElementException() { }
        public NoElementException(string message) : base(message + " не существует") {
        }
        public NoElementException(string message, Exception inner) : base(message + " не существует", inner) { }
        protected NoElementException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}