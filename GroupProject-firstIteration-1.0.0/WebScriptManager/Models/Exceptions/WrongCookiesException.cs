using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebScriptManager.Models.Exceptions
{
    /// <summary>
    /// класс ошибок, возникающих, когда не верно переданы Cookie 
    /// </summary>
    [Serializable]
    public class WrongCookiesException : Exception
    {
        public WrongCookiesException() { }
        /// <summary>
        /// ошибка, вызванная не верным Cookie
        /// </summary>
        /// <param name="cookieName">название не верного Cookie</param>
        public WrongCookiesException( string cookieName) : base("отсутствует значение "+cookieName ) { }
        public WrongCookiesException(string cookieName, Exception inner) : base("отсутствует значение " + cookieName, inner) { }
        protected WrongCookiesException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
    
}