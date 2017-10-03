using System;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.Web;
using Autofac.Integration.WebApi;
using Serilog;
using Serilog.Events;
using SerilogWeb.Classic.Enrichers;
using Umbraco.Core;
using Umbraco.Web;


namespace UmbracoBackofficeSample
{
    public class Global : UmbracoApplication, IContainerProviderAccessor
    {
        protected override void OnApplicationError(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            var httpException = exception as HttpException;
            if (httpException != null && httpException.GetHttpCode() >= 500)
            {
                Log.Error(httpException, "Application error");
            }
        }
        protected override IBootManager GetBootManager()
        {
            return new WebBootManager(this);
        }

        static IContainerProvider _containerProvider;
        public IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }

        protected override void OnApplicationStarted(object sender, EventArgs e)
        {
            base.OnApplicationStarted(sender, e);

            var runtimeEnvironment = ConfigurationManager.AppSettings["Environment"];
            var seqUrl = ConfigurationManager.AppSettings["SeqUrl"];

            var log = new LoggerConfiguration()
                .MinimumLevel.Is(LogEventLevel.Debug)
                .Enrich.WithProcessId()
                .Enrich.WithProperty("MachineName", Environment.MachineName)
                .Enrich.WithProperty("Source", "Umbraco Web")
                .Enrich.WithProperty("Environment", runtimeEnvironment)
                .Enrich.With<UserNameEnricher>()
                .Enrich.With<HttpRequestUrlEnricher>()
                .Enrich.With<HttpRequestUserAgentEnricher>()
                .Enrich.With<HttpSessionIdEnricher>()
                .Enrich.With<HttpRequestIdEnricher>()
                .Enrich.FromLogContext()
                .WriteTo.Seq(seqUrl)
                .CreateLogger();
            Log.Logger = log;

            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            // Register Umbraco Context, MVC Controllers and API Controllers.
            builder.Register(c => UmbracoContext.Current).AsSelf();
            builder.Register(c => ApplicationContext.Current.Services.ContentService).As<Umbraco.Core.Services.IContentService>();

            builder.RegisterControllers(Assembly.GetExecutingAssembly()); // Register our MVC controllers
            builder.RegisterApiControllers(typeof(UmbracoApplication).Assembly); // Register umbraco backoffice controllers
            builder.RegisterApiControllers(typeof(Global).Assembly); // Register our API controllers

            builder.RegisterInstance(log).As<ILogger>();
            builder.Register(c => new HttpContextWrapper(HttpContext.Current)).As<HttpContextBase>();
            //builder.RegisterType<PagingService>().As<IPagingService>();
            //builder.RegisterType<WebApiCsvGenerator>().As<IWebApiCsvGenerator>();

            var container = builder.Build();
            _containerProvider = new ContainerProvider(container);
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_containerProvider.ApplicationContainer));

            AppDomain.CurrentDomain.UnhandledException += (s, args) =>
            {
                var exp = (Exception)args.ExceptionObject;
                Log.Error(exp, "Unhandled exception");
            };

            Log.Information("Website Started.");
        }
    }
}