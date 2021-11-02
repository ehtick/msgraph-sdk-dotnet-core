// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

using Microsoft.Kiota.Abstractions;

namespace Microsoft.Graph
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Kiota.Abstractions;

    /// <summary>
    /// The GraphResponse Object
    /// </summary>
    public class GraphResponse<T> : GraphResponse
    {
        /// <summary>
        /// The GraphResponse Constructor
        /// </summary>
        /// <param name="iBaseRequest">The Request made for the response</param>
        /// <param name="httpResponseMessage">The response</param>
        public GraphResponse(RequestInformation iBaseRequest, HttpResponseMessage httpResponseMessage)
            : base(iBaseRequest, httpResponseMessage)
        {
        }

        //TODO fixme
        // /// <summary>
        // /// Gets the deserialized object 
        // /// </summary>
        // public async Task<T> GetResponseObjectAsync()
        // {
        //     return await this.BaseRequest.ResponseHandler.HandleResponse<T>(this.ToHttpResponseMessage());
        // }
    }
}