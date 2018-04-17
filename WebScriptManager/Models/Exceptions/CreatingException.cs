using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScriptManager.Models.Exceptions
{

    [Serializable]
    public class CreatingException : Exception
    {
        public CreatingException() { }
        public CreatingException(string message) : base(message + " невозможно создать") { }
        public CreatingException(string message, Exception inner) : base(message + " невозможно создать", inner) { }
        protected CreatingException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}