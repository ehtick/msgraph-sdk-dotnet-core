// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions;

namespace Microsoft.Graph.Core.Requests
{
    using System.Collections.Generic;

    /// <summary>
    /// The type BatchRequestBuilder
    /// </summary>
    public class BatchRequestBuilder
    {
        /// <summary>Current path for the request</summary>
        private string CurrentPath { get; set; }
        /// <summary>Whether the current path is a raw URL</summary>
        private bool IsRawUrl { get; set; }
        /// <summary>Path segment to use to build the URL for the current request builder</summary>
        private string PathSegment { get; set; }
        /// <summary>The http core service to use to execute the requests.</summary>
        private IRequestAdapter RequestAdapter { get; set; }

        /// <summary>
        /// Instantiates a new BatchRequestBuilder and sets the default values.
        /// <param name="currentPath">Current path for the request</param>
        /// <param name="isRawUrl">Whether the current path is a raw URL</param>
        /// <param name="requestAdapter">The http core service to use to execute the requests.</param>
        /// </summary>
        public BatchRequestBuilder(string currentPath, IRequestAdapter requestAdapter, bool isRawUrl = true)
        {
            if (string.IsNullOrEmpty(currentPath)) throw new ArgumentNullException(nameof(currentPath));
            _ = requestAdapter ?? throw new ArgumentNullException(nameof(requestAdapter));
            PathSegment = "/$batch";
            RequestAdapter = requestAdapter;
            CurrentPath = currentPath;
            IsRawUrl = isRawUrl;
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

        public RequestInformation CreatePostRequestInformation(BatchRequestContent batchRequestContent, Action<IDictionary<string, string>> h = default, IEnumerable<IRequestOption> o = default)
        {
            _ = batchRequestContent ?? throw new ArgumentNullException(nameof(batchRequestContent));
            var requestInfo = new RequestInformation
            {
                HttpMethod = HttpMethod.POST,
            };
            requestInfo.SetURI(CurrentPath, PathSegment, IsRawUrl);
            requestInfo.Headers.Add("Content-Type", "application/json");
            requestInfo.SetStreamContent(batchRequestContent.ReadAsStream());
            h?.Invoke(requestInfo.Headers);
            requestInfo.AddRequestOptions(o?.ToArray());
            return requestInfo;
        }
    }
}