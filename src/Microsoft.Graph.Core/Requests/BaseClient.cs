// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

using System.Linq;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Authentication.Azure;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace Microsoft.Graph
{
    using System;
    using System.Net.Http;
    using Microsoft.Graph.Core.Requests;
    using System.Collections.Generic;
    using Azure.Core;

    /// <summary>
    /// A default <see cref="IBaseClient"/> implementation.
    /// </summary>
    public class BaseClient
    {
        private string PathSegment { get; set; }
        public IRequestAdapter RequestAdapter { get; set; }

        /// <summary>
        /// Constructs a new <see cref="BaseClient"/>.
        /// </summary>
        /// <param name="baseUrl">The base service URL. For example, "https://graph.microsoft.com/v1.0."</param>
        /// <param name="authenticationProvider">The <see cref="IAuthenticationProvider"/> for authenticating request messages.</param>
        /// <param name="httpProvider">The <see cref="IHttpProvider"/> for sending requests.</param>
        public BaseClient(
            string baseUrl,
            IAuthenticationProvider authenticationProvider)
        {
            this.PathSegment = baseUrl;
        }

        /// <summary>
        /// Constructs a new <see cref="BaseClient"/>.
        /// </summary>
        /// <param name="baseUrl">The base service URL. For example, "https://graph.microsoft.com/v1.0."</param>
        /// <param name="tokenCredential">The <see cref="TokenCredential"/> for authenticating request messages.</param>
        /// <param name="scopes">List of scopes for the authentication context.</param>
        /// <param name="httpProvider">The <see cref="IHttpProvider"/> for sending requests.</param>
        public BaseClient(
            string baseUrl,
            TokenCredential tokenCredential,
            IEnumerable<string> scopes = null)
        {
            this.PathSegment = baseUrl;
        }

        /// <summary>
        /// Constructs a new <see cref="BaseClient"/>.
        /// </summary>
        /// <param name="baseUrl">The base service URL. For example, "https://graph.microsoft.com/v1.0."</param>
        /// <param name="httpClient">The custom <see cref="HttpClient"/> to be used for making requests</param>
        public BaseClient(
            string baseUrl,
            HttpClient httpClient)
        {
            this.PathSegment = baseUrl;
        }

        /// <summary>
        /// Gets the <see cref="BatchRequestBuilder"/> for building batch Requests
        /// </summary>
        public BatchRequestBuilder Batch
        {
            get
            {
                return new BatchRequestBuilder(this.PathSegment + "/$batch", RequestAdapter);
            }
        }
    }
}
