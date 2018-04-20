using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebScriptManager.Views.Shared
{
    public class HtmlExceptionView: ActionResult
    {
        private string htmlCode;
        public HtmlExceptionView(string text)
        {
            htmlCode = text;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            string fullHtmlCode = ""; //сюда можно добавить оформление;
            fullHtmlCode += htmlCode;
            fullHtmlCode += ""; //сюда тоже можно добавить оформление;
            context.HttpContext.Response.Write(fullHtmlCode);
        }
    }
}