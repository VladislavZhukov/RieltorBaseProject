namespace RieltorBase.WebSite
{
    using System.Net;
    using System.Net.Http;
    using System.Security.Authentication;
    using System.Web.Http.Filters;

    /// <summary>
    /// Общий фильтр исключений.
    /// http://www.asp.net/web-api/overview/error-handling/exception-handling
    /// 
    /// Необязательно читать:
    /// ASP.NET Web API: Correct way to return a 401 / unauthorised response:
    /// http://stackoverflow.com/questions/31205599/asp-net-web-api-correct-way-to-return-a-401-unauthorised-response
    /// How do you return status 401 from WebAPI to AngularJS and also include a custom message?:
    /// http://stackoverflow.com/questions/23025884/how-do-you-return-status-401-from-webapi-to-angularjs-and-also-include-a-custom
    /// </summary>
    public class CommonExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// Raises the exception event.
        /// </summary>
        /// <param name="context">The context for the action.</param>
        public override void OnException(
            HttpActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                if (context.Exception is AuthenticationException)
                {
                    context.Response = new HttpResponseMessage(
                        HttpStatusCode.Unauthorized);
                }
                else
                {
                    context.Response = new HttpResponseMessage(
                        HttpStatusCode.OK);
                }

                context.Response.Content = new StringContent(
                    context.Exception.Message);
            }
        }
    }
}