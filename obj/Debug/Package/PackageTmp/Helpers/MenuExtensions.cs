using System;
using System.Web.Mvc;

namespace DashBoard.Helpers
{
    public static class MenuExtensions
    {
        public static MvcHtmlString MenuItem(this HtmlHelper htmlHelper,string text,string action,string controller)
        {
            var li = new TagBuilder("span");
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");
            var espacio = new TagBuilder("span");
            if (string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase))
            {
                li.AddCssClass("primero");
                TagBuilder spanBuilder = new TagBuilder("span");
#if DEBUG
                li.MergeAttribute("onclick", " window.location.assign('http://localhost:5692/"+controller+"');");
#else
                    li.MergeAttribute("onclick", " window.location.assign('http://amsaxum.com/"+controller+"');");
#endif


                espacio.AddCssClass("espacios");
                li.InnerHtml = text ;
            }
            else
            {
                li.AddCssClass("otro");
#if DEBUG
                li.MergeAttribute("onclick", " window.location.assign('http://localhost:5692/" + controller + "');");
#else
                    li.MergeAttribute("onclick", " window.location.assign('http://amsaxum.com/"+controller+"');");
#endif
                li.InnerHtml = text;
            }

            return MvcHtmlString.Create(espacio.ToString() + li);
        }
    }
}