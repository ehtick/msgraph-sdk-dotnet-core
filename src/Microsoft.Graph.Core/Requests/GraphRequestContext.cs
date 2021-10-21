// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

using Microsoft.Kiota.Abstractions;

namespace Microsoft.Graph
{
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// The graph request context class
    /// </summary>
    public class GraphRequestContext
    {
        /// <summary>
        /// A ClientRequestId property
        /// </summary>
        public string ClientRequestId { get; set; }

        /// <summary>
        /// A MiddlewareOptions property
        /// </summary>
        public IDictionary<string, IRequestOption> MiddlewareOptions {
            get => _middlewareOptions ??= new Dictionary<string, IRequestOption>();
            set => _middlewareOptions = value;
        }

        /// <summary>
        /// A CancellationToken property
        /// </summary>
        public CancellationToken CancellationToken { get; set; }

        /// <summary>
        /// A FeatureUsage property
        /// </summary>
        public FeatureFlag FeatureUsage { get; set; }

        private IDictionary<string, IRequestOption> _middlewareOptions;
    }
}
