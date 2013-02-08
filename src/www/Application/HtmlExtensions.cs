using System;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace nTestSwarm.Application
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString RenderException(this HtmlHelper helper, Exception exception)
        {
            return helper.Partial("Exception", exception);
        }

        public static MvcHtmlString StringList(this HtmlHelper helper, IEnumerable values)
        {
            var builder = new StringBuilder();

            foreach (var item in values)
            {
                builder.AppendFormat("{0}<br />", item);    
            }

            return new MvcHtmlString(builder.ToString());
        }

        public static string GetIpAddress(this HttpRequestBase request)
        {
            string ip;
            if (!string.IsNullOrWhiteSpace(request.ServerVariables["HTTP_CLIENT_IP"]))
            {
                ip = request.ServerVariables["HTTP_CLIENT_IP"];
            }
            else if (!string.IsNullOrWhiteSpace(request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
            {
                ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            else
            {
                ip = request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }
    }
}