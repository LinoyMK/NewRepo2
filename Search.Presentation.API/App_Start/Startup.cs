#region  Frontiers File Header
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Frontiers">
//   © 2007 - 2016 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the Startup type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
#endregion


using Frontiers.Web.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using Search.Presentation.API;
using System;

[assembly: OwinStartup(typeof(Startup))]

namespace Search.Presentation.API
{
    /// <summary>
    /// Startup Implementation
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Configurations the specified application builder.
        /// </summary>
        /// <param name="appBuilder">The application builder.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public void Configuration(IAppBuilder appBuilder)
        {
            if (appBuilder == null)
            {
                throw new ArgumentNullException(nameof(appBuilder));
            }

            appBuilder.UseCors(CorsOptions.AllowAll);
            appBuilder.UseNinjectMiddleware(() => Ninject.ConfiguredKernel);
            appBuilder.UseNinjectWebApi(WebApi.GetConfig(OData.CreateModel()));

            appBuilder.Use<NotFoundMiddleware>();

        }

    }
}