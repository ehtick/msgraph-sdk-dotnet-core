// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

using System.Text.Json;
using Microsoft.Kiota.Abstractions;

namespace Microsoft.Graph
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Monitor for async operations to the Graph service on the client.
    /// </summary>
    /// <typeparam name="T">The object type to return.</typeparam>
    public class AsyncMonitor<T> : IAsyncMonitor<T>
    {
        private AsyncOperationStatus asyncOperationStatus;
        private IRequestAdapter requestAdapter;

        internal string monitorUrl;

        /// <summary>
        /// Construct an Async Monitor.
        /// </summary>
        /// <param name="requestAdapter">The client to monitor.</param>
        /// <param name="monitorUrl">The URL to monitor.</param>
        public AsyncMonitor(IRequestAdapter requestAdapter, string monitorUrl)
        {
            this.requestAdapter = requestAdapter;
            this.monitorUrl = monitorUrl;
        }
        
        /// <summary>
        /// Poll to check for completion of an async call to the Graph service.
        /// </summary>
        /// <param name="progress">The progress status.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The operation task.</returns>
        public async Task<T> PollForOperationCompletionAsync(IProgress<AsyncOperationStatus> progress, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var requestInformation = new RequestInformation()
                {
                    HttpMethod = Kiota.Abstractions.HttpMethod.GET,
                    UrlTemplate = this.monitorUrl
                };
                var responseHandler = new AsyncMonitorResponseHandler();
                // TODO fixme. The type T might not be returned
                this.asyncOperationStatus = await requestAdapter.SendPrimitiveAsync<AsyncOperationStatus>(requestInformation, responseHandler);
                if (this.asyncOperationStatus == null)
                {
                    throw new ServiceException(
                        new Error
                        {
                            Code = ErrorConstants.Codes.GeneralException,
                            Message = "Error retrieving monitor status."
                        });
                }

                if (string.Equals(this.asyncOperationStatus.Status, "cancelled", StringComparison.OrdinalIgnoreCase))
                {
                    return default(T);
                }

                if (string.Equals(this.asyncOperationStatus.Status, "failed", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(this.asyncOperationStatus.Status, "deleteFailed", StringComparison.OrdinalIgnoreCase))
                {
                    object message = null;
                    if (this.asyncOperationStatus.AdditionalData != null)
                    {
                        this.asyncOperationStatus.AdditionalData.TryGetValue("message", out message);
                    }

                    throw new ServiceException(
                        new Error
                        {
                            Code = ErrorConstants.Codes.GeneralException,
                            Message = message as string
                        });
                }

                if (progress != null)
                {
                    progress.Report(this.asyncOperationStatus);
                }
        
                await Task.Delay(CoreConstants.PollingIntervalInMs, cancellationToken).ConfigureAwait(false);
            }
            
            return default(T);
        }
    }

    class AsyncMonitorResponseHandler : IResponseHandler
    {
        public AsyncOperationStatus asyncOperationStatus;
        public async Task<ModelType> HandleResponseAsync<NativeResponseType, ModelType>(NativeResponseType response)
        {
            if (response is HttpResponseMessage responseMessage)
            {
                // The monitor service will return an Accepted status for any monitor operation that hasn't completed.
                // If we have a success code that isn't Accepted, the operation is complete. Return the resulting object.
                if (responseMessage.StatusCode != HttpStatusCode.Accepted && responseMessage.IsSuccessStatusCode)
                {
                    await using var responseStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    return await JsonSerializer.DeserializeAsync<ModelType>(responseStream);
                }

                await using var asyncOperationStatusResponseStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
                this.asyncOperationStatus = await JsonSerializer.DeserializeAsync<AsyncOperationStatus>(asyncOperationStatusResponseStream);

            }

            return default;
        }
    }
}
