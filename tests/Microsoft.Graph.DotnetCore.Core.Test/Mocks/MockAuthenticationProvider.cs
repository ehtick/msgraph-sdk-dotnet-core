// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;

namespace Microsoft.Graph.DotnetCore.Core.Test.Mocks
{
    using Moq;
    using Microsoft.Kiota.Abstractions;
    using Microsoft.Kiota.Abstractions.Authentication;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class MockAuthenticationProvider : Mock<IAuthenticationProvider>
    {
        public MockAuthenticationProvider(string accessToken = null)
            : base(MockBehavior.Strict)
        {
            this.SetupAllProperties();

            this.Setup(
                provider => provider.AuthenticateRequestAsync(It.IsAny<RequestInformation>()))
                .Callback<HttpRequestMessage>(r => r.Headers.Authorization = new AuthenticationHeaderValue(CoreConstants.Headers.Bearer, accessToken ?? "Default-Token"))
                .Returns(Task.FromResult(0));
        }
    }
}
