// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Kiota.Abstractions;

namespace Microsoft.Graph
{
    using Microsoft.Graph.Core.Models;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The UploadSessionRequest class
    /// </summary>
    internal class UploadSessionRequest
    {
        private readonly UploadResponseHandler responseHandler;

        /// <summary>
        /// The sessionUrl for the upload
        /// </summary>
        public string SessionUrl { get; private set; }

        /// <summary>
        /// The <see cref="IRequestAdapter"/> to use for the upload
        /// </summary>
        public IRequestAdapter RequestAdapter { get; private set; }

        /// <summary>
        /// Create a new UploadSessionRequest
        /// </summary>
        /// <param name="session">The IUploadSession to use in the request.</param>
        /// <param name="requestAdapter">The <see cref="IRequestAdapter"/> for handling requests.</param>
        public UploadSessionRequest(IUploadSession session, IRequestAdapter requestAdapter)
        {
            this.responseHandler = new UploadResponseHandler();
            this.RequestAdapter = requestAdapter;
            this.SessionUrl = session.UploadUrl;
        }

        /// <summary>
        /// Deletes the specified Session
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The task to await.</returns>
        public async Task DeleteAsync(CancellationToken cancellationToken = default)
        {
            var requestInfo = CreateDeleteRequestInformation();
            await RequestAdapter.SendNoContentAsync(requestInfo);
        }

        /// <summary>
        /// Gets the specified UploadSession.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> for the request.</param>
        /// <returns>The Item.</returns>
        public async Task<IUploadSession> GetAsync(CancellationToken cancellationToken = default)
        {
            var requestInfo = CreateGetRequestInformation();
            return await RequestAdapter.SendPrimitiveAsync<UploadSession>(requestInfo, this.responseHandler);
        }

        /// <summary>
        /// Create a <see cref="RequestInformation"/> for the GET request
        /// <param name="h">Request headers</param>
        /// <param name="o">Request options</param>
        /// </summary>
        public RequestInformation CreateGetRequestInformation(Action<IDictionary<string, string>> h = default, IEnumerable<IRequestOption> o = default)
        {
            var requestInfo = new RequestInformation
            {
                HttpMethod = HttpMethod.GET,
                UrlTemplate = this.SessionUrl,
                PathParameters = new Dictionary<string, object>(),
            };
            h?.Invoke(requestInfo.Headers);
            requestInfo.AddRequestOptions(o?.ToArray());
            return requestInfo;
        }

        /// <summary>
        /// Create a <see cref="RequestInformation"/> for the DELETE request
        /// <param name="h">Request headers</param>
        /// <param name="o">Request options</param>
        /// </summary>
        public RequestInformation CreateDeleteRequestInformation(Action<IDictionary<string, string>> h = default, IEnumerable<IRequestOption> o = default)
        {
            var requestInfo = new RequestInformation
            {
                HttpMethod = HttpMethod.GET,
                UrlTemplate = this.SessionUrl,
                PathParameters = new Dictionary<string, object>(),
            };
            h?.Invoke(requestInfo.Headers);
            requestInfo.AddRequestOptions(o?.ToArray());
            return requestInfo;
        }
    }
}