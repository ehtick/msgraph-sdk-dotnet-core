// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

using System;
using System.Linq;
using Microsoft.Kiota.Abstractions;

namespace Microsoft.Graph
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using System;
    using System.Linq;
    using Microsoft.Kiota.Abstractions;

    /// <summary>
    /// The UploadSliceRequest class to help with uploading file slices
    /// </summary>
    /// <typeparam name="T">The type to be uploaded</typeparam>
    internal class UploadSliceRequest<T>
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
        /// The beginning of the slice range to send.
        /// </summary>
        public long RangeBegin { get; private set; }

        /// <summary>
        /// The end of the slice range to send.
        /// </summary>
        public long RangeEnd { get; private set; }

        /// <summary>
        /// The length in bytes of the session.
        /// </summary>
        public long TotalSessionLength { get; private set; }

        /// <summary>
        /// The range length of the slice to send.
        /// </summary>
        public int RangeLength => (int)(this.RangeEnd - this.RangeBegin + 1);

        /// <summary>
        /// Request for uploading one slice of a session
        /// </summary>
        /// <param name="sessionUrl">URL to upload the slice.</param>
        /// <param name="requestAdapter">The <see cref="IRequestAdapter"/> used for sending the slice.</param>
        /// <param name="rangeBegin">Beginning of range of this slice</param>
        /// <param name="rangeEnd">End of range of this slice</param>
        /// <param name="totalSessionLength">Total session length. This MUST be consistent
        /// across all slice.</param>
        public UploadSliceRequest(
            string sessionUrl,
            IRequestAdapter requestAdapter,
            long rangeBegin,
            long rangeEnd,
            long totalSessionLength)
        {
            this.SessionUrl = sessionUrl;
            this.RequestAdapter = requestAdapter;
            this.RangeBegin = rangeBegin;
            this.RangeEnd = rangeEnd;
            this.TotalSessionLength = totalSessionLength;
            this.responseHandler = new UploadResponseHandler();
        }

        /// <summary>
        /// Uploads the slice using PUT.
        /// </summary>
        /// <param name="stream">Stream of data to be sent in the request.</param>
        /// <returns>The status of the upload.</returns>
        public Task<UploadResult<T>> PutAsync(Stream stream)
        {
            return this.PutAsync(stream, CancellationToken.None);
        }

        /// <summary>
        /// Uploads the slice using PUT.
        /// </summary>
        /// <param name="stream">Stream of data to be sent in the request. Length must be equal to the length
        /// of this slice (as defined by this.RangeLength)</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The status of the upload. If UploadSession.AdditionalData.ContainsKey("successResponse")
        /// is true, then the item has completed, and the value is the created item from the server.</returns>
        public virtual async Task<UploadResult<T>> PutAsync(Stream stream, CancellationToken cancellationToken)
        {
            var requestInfo = CreatePutRequestInformation(stream);
            return await RequestAdapter.SendPrimitiveAsync<UploadResult<T>>(requestInfo, new UploadResponseHandler());
        }

        /// <summary>
        /// Read-only.
        /// <param name="stream"></param>
        /// <param name="h">Request headers</param>
        /// <param name="o">Request options</param>
        /// </summary>
        public RequestInformation CreatePutRequestInformation(Stream stream, Action<IDictionary<string, string>> h = default, IEnumerable<IRequestOption> o = default)
        {
            _ = stream ?? throw new ArgumentNullException(nameof(stream));
            var requestInfo = new RequestInformation
            {
                HttpMethod = Kiota.Abstractions.HttpMethod.PUT,
                UrlTemplate = this.SessionUrl,
                PathParameters = new Dictionary<string, object>(),
            };
            requestInfo.SetStreamContent(stream);
            h?.Invoke(requestInfo.Headers);
            requestInfo.AddRequestOptions(o?.ToArray());
            return requestInfo;
        }
    }
}
