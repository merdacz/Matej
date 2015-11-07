namespace TrafficAnalyzer.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using TrafficAnalyzer.Shared;
    using TrafficAnalyzer.Web.Controllers;

    /// <summary>
    /// Fixed to <see cref="DashboardController"/> for now. 
    /// </summary>
    public class DependencyInjectingFactory : DefaultControllerFactory
    {
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            return new DashboardController(new SimpleDataLogStorage());
        }
    }
}