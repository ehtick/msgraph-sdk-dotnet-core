// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

namespace Microsoft.Graph.Core.Requests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Kiota.Abstractions;
    using System.Collections.Generic;

    /// <summary>
    /// The type BatchRequestBuilder
    /// </summary>
    public class BatchRequestBuilder
    {
        private string UrlTemplate { get; set; }
        /// <summary>The http core service to use to execute the requests.</summary>
        internal IRequestAdapter RequestAdapter { get; set; }

        /// <summary>
        /// Instantiates a new BatchRequestBuilder and sets the default values.
        /// <param name="requestAdapter">The http core service to use to execute the requests.</param>
        /// </summary>
        public BatchRequestBuilder(IRequestAdapter requestAdapter)
        {
            _ = requestAdapter ?? throw new ArgumentNullException(nameof(requestAdapter));
            UrlTemplate = "https://graph.microsoft.com/v1.0/$batch";
            RequestAdapter = requestAdapter;
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>The built request.</returns>
        public async Task<BatchResponseContent> PostAsync(BatchRequestContent batchRequestContent, Action<IDictionary<string, string>> h = default, IEnumerable<IRequestOption> o = default, IResponseHandler responseHandler = default)
        {
            _ = batchRequestContent ?? throw new ArgumentNullException(nameof(batchRequestContent));
            var requestInfo = CreatePostRequestInformation(batchRequestContent, h, o);
            return await RequestAdapter.SendPrimitiveAsync<BatchResponseContent>(requestInfo); // TODO add responseHandler
        }

        /// <summary>
        /// Creates a <see cref="RequestInformation"/> for the batch request
        /// </summary>
        /// <param name="batchRequestContent"></param>
        /// <param name="h"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public RequestInformation CreatePostRequestInformation(BatchRequestContent batchRequestContent, Action<IDictionary<string, string>> h = default, IEnumerable<IRequestOption> o = default)
        {
            _ = batchRequestContent ?? throw new ArgumentNullException(nameof(batchRequestContent));
            var requestInfo = new RequestInformation
            {
                HttpMethod = HttpMethod.GET,
                UrlTemplate = UrlTemplate,
                PathParameters = new Dictionary<string, object>(),
            };
            requestInfo.Headers.Add("Content-Type", "application/json");
            requestInfo.SetStreamContent(batchRequestContent.ReadAsStream());
            h?.Invoke(requestInfo.Headers);
            requestInfo.AddRequestOptions(o?.ToArray());
            return requestInfo;
        }
    }
}