// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

namespace Microsoft.Graph.DotnetCore.Core.Test.Requests
{
    using Moq;
    using Xunit;
    using Microsoft.Graph.Core.Requests;
    using Microsoft.Kiota.Abstractions;

    public class BatchRequestBuilderTests
    {
        [Fact]
        public void BatchRequestBuilder()
        {
            // Arrange
            var requestUrl = "https://localhost";
            var requestAdapter = new Mock<IRequestAdapter>().Object;

            // Act
            var batchRequestBuilder = new BatchRequestBuilder(requestUrl, requestAdapter);

            // Assert
            // Assert.Equal(requestUrl, batchRequestBuilder.RequestUrl); // TODO verify if we still need the baseUrl
            Assert.Equal(requestAdapter, batchRequestBuilder.RequestAdapter);
        }
    }
}