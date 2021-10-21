// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

using Microsoft.Kiota.Abstractions;

namespace Microsoft.Graph.DotnetCore.Core.Test.TestModels.ServiceModels
{
    /// <summary>
    /// The type UserEventsCollectionPage.
    /// </summary>
    public partial class TestEventDeltaCollectionPage : CollectionPage<TestEvent>
    {
        /// <summary>
        /// Gets the next page <see cref="TestEventDeltaRequest"/> instance.
        /// </summary>
        public TestEventDeltaRequest NextPageRequest { get; private set; }

        /// <summary>
        /// Initializes the NextPageRequest property.
        /// </summary>
        public void InitializeNextPageRequest(IRequestAdapter client, string nextPageLinkString)
        {
            if (!string.IsNullOrEmpty(nextPageLinkString))
            {
                this.NextPageRequest = new TestEventDeltaRequest(
                    nextPageLinkString,
                    client,
                    null);
            }
        }
    }
}