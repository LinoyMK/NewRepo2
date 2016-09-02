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

using Ninject;
using Search.Infrastructure.Injection;
using System;

namespace Search.Presentation.API
{
    /// <summary>
    /// Partial implementations of Startup.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Ninject Implementations.
        /// </summary>
        public static class Ninject
        {
            #region PRIVATE FIELDS

            /// <summary>
            /// The _kenel bindings
            /// </summary>
            private static readonly Lazy<IKernel> _kenelBindings = new Lazy<IKernel>(GetKernel, true);

            #endregion

            #region PUBLIC PROPERTIES

            /// <summary>
            /// Gets the configured kernel.
            /// </summary>
            /// <value>
            /// The configured kernel.
            /// </value>
            public static IKernel ConfiguredKernel => _kenelBindings.Value;

            #endregion

            #region METHODS

            /// <summary>
            /// Gets the kernel.
            /// </summary>
            /// <returns></returns>
            public static IKernel GetKernel()
            {
                StandardKernel kernel = new StandardKernel(
                    new MainNinjectModule());
                return kernel;
            }

            #endregion
        }
    }
}
