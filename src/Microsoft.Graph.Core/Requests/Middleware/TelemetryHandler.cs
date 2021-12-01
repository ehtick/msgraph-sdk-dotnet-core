// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

namespace Microsoft.Graph
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Net.Http.Headers;
    using System.Collections.Generic;

// TODO: Add feature flag for telemetry.

    /// <summary>
    /// A <see cref="DelegatingHandler"/> implementation that handles compression.
    /// </summary>
    public class TelemetryHandler: DelegatingHandler
    {
        /// The version for current assembly.
        private static Version assemblyVersion = typeof(GraphClientFactory).GetTypeInfo().Assembly.GetName().Version;

        /// The value for the SDK version header.
        private static string SdkVersionHeaderValue = string.Format(
                    CoreConstants.Headers.SdkVersionHeaderValueFormatString,
                    assemblyVersion.Major,
                    assemblyVersion.Minor,
                    assemblyVersion.Build);

        /// <summary>
        /// Constructs a new <see cref="TelemetryHandler"/>.
        /// </summary>
        public TelemetryHandler()
        {
            // TODO: Add SdkVersion header if not already present. Otherwise, append value in case of service library has already appended header value. 

        }

        /// <summary>
        /// Constructs a new <see cref="TelemetryHandler"/>.
        /// </summary>
        /// <param name="innerHandler">An HTTP message handler to pass to the <see cref="HttpMessageHandler"/> for sending requests.</param>
        public TelemetryHandler(HttpMessageHandler innerHandler)
            :this()
        {
            InnerHandler = innerHandler;
        }

        /// <summary>
        /// Sends a HTTP request.
        /// </summary>
        /// <param name="httpRequest">The <see cref="HttpRequestMessage"/> to be sent.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequest, CancellationToken cancellationToken)
        {

            // TODO: Add or update SdkVersion header.
            // TODO: Remove from the GraphClientFactory.
            if (httpRequest.Headers.Contains(CoreConstants.Headers.SdkVersionHeaderName))
            {
                // Service library or another component is using the header, we need to append.
                var sdkVersion = httpRequest.Headers.GetValues(CoreConstants.Headers.SdkVersionHeaderName);
                var currentSdkVersionValue = sdkVersion[0]; // there should only be one entry.

                httpRequest.Headers.Remove(CoreConstants.Headers.SdkVersionHeaderName);

                httpRequest.Headers.Add(CoreConstants.Headers.SdkVersionHeaderName, SdkVersionHeaderValue);
            }
            else
            {
                httpRequest.Headers.Add(CoreConstants.Headers.SdkVersionHeaderName, SdkVersionHeaderValue);
            }

            // TODO: Add client-request-id if not already present.
            // TODO: Remove client-request-id from other parts of core.

            if (!httpRequest.Headers.Contains(CoreConstants.Headers.ClientRequestId))
            {
                httpRequest.Headers.Add(CoreConstants.Headers.SdkVersionHeaderName, SdkVersionHeaderValue);
            }


            HttpResponseMessage response = await base.SendAsync(httpRequest, cancellationToken);

            return response;
        }
    }
}
