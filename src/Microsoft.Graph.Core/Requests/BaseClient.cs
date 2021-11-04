// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

namespace Microsoft.Graph
{
    using Microsoft.Graph.Core.Requests;
    using Microsoft.Kiota.Abstractions;
    using Microsoft.Kiota.Abstractions.Authentication;
    using Microsoft.Kiota.Http.HttpClientLibrary;
    using System.Net.Http;

    /// <summary>
    /// A default client implementation for microsoft graph
    /// </summary>
    public class BaseClient
    {
        /// <summary>
        /// The request adapter for making requests
        /// </summary>
        internal IRequestAdapter RequestAdapter { get; set; }

        /// <summary>
        /// Constructs a new <see cref="BaseClient"/>.
        /// </summary>
        protected BaseClient()
        {
        }

        /// <summary>
        /// Constructs a new <see cref="BaseClient"/> with an <see cref="AnonymousAuthenticationProvider"/> and the given httpClient.
        /// </summary>
        /// <param name="httpClient">The custom <see cref="HttpClient"/> to be used for making requests</param>
        protected internal BaseClient(HttpClient httpClient)
        {
            this.RequestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(),httpClient:httpClient);
        }

        /// <summary>
        /// Gets the <see cref="BatchRequestBuilder"/> for building batch Requests
        /// </summary>
        public BatchRequestBuilder Batch
        {
            get
            {
                return new BatchRequestBuilder(RequestAdapter);
            }
        }
    }
}
