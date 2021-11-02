// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

using Microsoft.Kiota.Abstractions;

namespace Microsoft.Graph
{
    using Microsoft.Kiota.Abstractions;
    using Microsoft.Graph.Core.Models;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Text.Json;

    /// <summary>
    /// The ResponseHandler for upload requests
    /// </summary>
    internal class UploadResponseHandler : IResponseHandler
    {
        private readonly ISerializer _serializer;

        /// <summary>
        /// Constructs a new <see cref="IResponseHandler"/>.
        /// </summary>
        /// <param name="serializer"></param>
        public UploadResponseHandler(ISerializer serializer = null)
        {
            this._serializer = serializer ?? new Serializer();
        }

        /// <summary>
        /// Process raw HTTP response from Upload request
        /// </summary>
        /// <typeparam name="T">The type to return</typeparam>
        /// <param name="response">The HttpResponseMessage to handle.</param>
        /// <returns></returns>
        public async Task<UploadResult<T>> HandleResponse<T>(HttpResponseMessage response) 
        {
            if (response.Content == null)
            {
                throw new ServiceException(new Error
                {
                    Code = ErrorConstants.Codes.GeneralException,
                    Message = ErrorConstants.Messages.NoResponseForUpload
                });
            }

            // Give back the info from the server for ongoing upload as the upload is ongoing
            using (Stream responseSteam = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
            {
                try
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        ErrorResponse errorResponse = this._serializer.DeserializeObject<ErrorResponse>(responseSteam);
                        Error error = errorResponse.Error;
                        string rawResponseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        // Throw exception to know something went wrong.
                        throw new ServiceException(error, response.Headers, response.StatusCode, rawResponseBody);
                    }

                    var uploadResult = new UploadResult<T>();

                    /*
                     * Check if we have a status code 201 to know if the upload completed successfully.
                     * This will be returned when uploading a FileAttachment with a location header but empty response hence
                     * This could also be returned when uploading a DriveItem with  an ItemResponse but no location header.
                     */
                    if (response.StatusCode == HttpStatusCode.Created)
                    {
                        if(responseSteam.Length > 0) //system.text.json wont deserialize an empty string
                        {
                            uploadResult.ItemResponse = this._serializer.DeserializeObject<T>(responseSteam);
                        }
                        uploadResult.Location = response.Headers.Location;
                    }
                    else
                    {
                        /*
                         * The response could be either a 200 or a 202 response.
                         * DriveItem Upload returns the upload session in a 202 response while FileAttachment in a 200 response
                         * However, successful upload completion for a DriveItem the response could also come in a 200 response and
                         * hence we validate this by checking the NextExpectedRanges parameter which is present in an ongoing upload
                         */
                        UploadSession uploadSession = this._serializer.DeserializeObject<UploadSession>(responseSteam);
                        if (uploadSession?.NextExpectedRanges != null)
                        {
                            uploadResult.UploadSession = uploadSession;
                        }
                        else
                        {
                            //Upload is most likely done as DriveItem info may come in a 200 response
                            responseSteam.Position = 0; //reset 
                            uploadResult.ItemResponse = this._serializer.DeserializeObject<T>(responseSteam);
                        }
                    }

                    return uploadResult;
                }
                catch (JsonException exception)
                {
                    string rawResponseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    throw new ServiceException(new Error()
                        {
                            Code = ErrorConstants.Codes.GeneralException,
                            Message = ErrorConstants.Messages.UnableToDeserializeContent,
                        }, 
                        response.Headers,
                        response.StatusCode,
                        rawResponseBody,
                        exception);
                }
            }
        }

        /// <summary>
        /// Handle the delta response to return the desired model
        /// </summary>
        /// <typeparam name="TNativeResponseType">The type of the response to be handled</typeparam>
        /// <typeparam name="TModelType">The type to be returned from handling of the response</typeparam>
        /// <param name="response">The response to handle</param>
        /// <returns>An instance of the desired type</returns>
        public Task<TModelType> HandleResponseAsync<TNativeResponseType, TModelType>(TNativeResponseType response)
        {
            if (response is HttpResponseMessage responseMessage)
            {
                //TODO fixme!!!
                // var responseObject = await HandleResponse(responseMessage);
                // return responseObject;
            }

            return default;
        }
    }
}