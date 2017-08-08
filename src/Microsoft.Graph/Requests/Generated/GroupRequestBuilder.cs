// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.
// ------------------------------------------------------------------------------

// **NOTE** This file was generated by a tool and any changes will be overwritten.

// Template Source: Templates\CSharp\Requests\EntityRequestBuilder.cs.tt

namespace Microsoft.Graph
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// The type GroupRequestBuilder.
    /// </summary>
    public partial class GroupRequestBuilder : DirectoryObjectRequestBuilder, IGroupRequestBuilder
    {

        /// <summary>
        /// Constructs a new GroupRequestBuilder.
        /// </summary>
        /// <param name="requestUrl">The URL for the built request.</param>
        /// <param name="client">The <see cref="IBaseClient"/> for handling requests.</param>
        public GroupRequestBuilder(
            string requestUrl,
            IBaseClient client)
            : base(requestUrl, client)
        {
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns>The built request.</returns>
        public new IGroupRequest Request()
        {
            return this.Request(null);
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <param name="options">The query and header options for the request.</param>
        /// <returns>The built request.</returns>
        public new IGroupRequest Request(IEnumerable<Option> options)
        {
            return new GroupRequest(this.RequestUrl, this.Client, options);
        }
    
        /// <summary>
        /// Gets the request builder for Members.
        /// </summary>
        /// <returns>The <see cref="IGroupMembersCollectionWithReferencesRequestBuilder"/>.</returns>
        public IGroupMembersCollectionWithReferencesRequestBuilder Members
        {
            get
            {
                return new GroupMembersCollectionWithReferencesRequestBuilder(this.AppendSegmentToRequestUrl("members"), this.Client);
            }
        }

        /// <summary>
        /// Gets the request builder for MemberOf.
        /// </summary>
        /// <returns>The <see cref="IGroupMemberOfCollectionWithReferencesRequestBuilder"/>.</returns>
        public IGroupMemberOfCollectionWithReferencesRequestBuilder MemberOf
        {
            get
            {
                return new GroupMemberOfCollectionWithReferencesRequestBuilder(this.AppendSegmentToRequestUrl("memberOf"), this.Client);
            }
        }

        /// <summary>
        /// Gets the request builder for CreatedOnBehalfOf.
        /// </summary>
        /// <returns>The <see cref="IDirectoryObjectWithReferenceRequestBuilder"/>.</returns>
        public IDirectoryObjectWithReferenceRequestBuilder CreatedOnBehalfOf
        {
            get
            {
                return new DirectoryObjectWithReferenceRequestBuilder(this.AppendSegmentToRequestUrl("createdOnBehalfOf"), this.Client);
            }
        }

        /// <summary>
        /// Gets the request builder for Owners.
        /// </summary>
        /// <returns>The <see cref="IGroupOwnersCollectionWithReferencesRequestBuilder"/>.</returns>
        public IGroupOwnersCollectionWithReferencesRequestBuilder Owners
        {
            get
            {
                return new GroupOwnersCollectionWithReferencesRequestBuilder(this.AppendSegmentToRequestUrl("owners"), this.Client);
            }
        }

        /// <summary>
        /// Gets the request builder for Settings.
        /// </summary>
        /// <returns>The <see cref="IGroupSettingsCollectionRequestBuilder"/>.</returns>
        public IGroupSettingsCollectionRequestBuilder Settings
        {
            get
            {
                return new GroupSettingsCollectionRequestBuilder(this.AppendSegmentToRequestUrl("settings"), this.Client);
            }
        }

        /// <summary>
        /// Gets the request builder for Extensions.
        /// </summary>
        /// <returns>The <see cref="IGroupExtensionsCollectionRequestBuilder"/>.</returns>
        public IGroupExtensionsCollectionRequestBuilder Extensions
        {
            get
            {
                return new GroupExtensionsCollectionRequestBuilder(this.AppendSegmentToRequestUrl("extensions"), this.Client);
            }
        }

        /// <summary>
        /// Gets the request builder for Threads.
        /// </summary>
        /// <returns>The <see cref="IGroupThreadsCollectionRequestBuilder"/>.</returns>
        public IGroupThreadsCollectionRequestBuilder Threads
        {
            get
            {
                return new GroupThreadsCollectionRequestBuilder(this.AppendSegmentToRequestUrl("threads"), this.Client);
            }
        }

        /// <summary>
        /// Gets the request builder for Calendar.
        /// </summary>
        /// <returns>The <see cref="ICalendarRequestBuilder"/>.</returns>
        public ICalendarRequestBuilder Calendar
        {
            get
            {
                return new CalendarRequestBuilder(this.AppendSegmentToRequestUrl("calendar"), this.Client);
            }
        }

        /// <summary>
        /// Gets the request builder for CalendarView.
        /// </summary>
        /// <returns>The <see cref="IGroupCalendarViewCollectionRequestBuilder"/>.</returns>
        public IGroupCalendarViewCollectionRequestBuilder CalendarView
        {
            get
            {
                return new GroupCalendarViewCollectionRequestBuilder(this.AppendSegmentToRequestUrl("calendarView"), this.Client);
            }
        }

        /// <summary>
        /// Gets the request builder for Events.
        /// </summary>
        /// <returns>The <see cref="IGroupEventsCollectionRequestBuilder"/>.</returns>
        public IGroupEventsCollectionRequestBuilder Events
        {
            get
            {
                return new GroupEventsCollectionRequestBuilder(this.AppendSegmentToRequestUrl("events"), this.Client);
            }
        }

        /// <summary>
        /// Gets the request builder for Conversations.
        /// </summary>
        /// <returns>The <see cref="IGroupConversationsCollectionRequestBuilder"/>.</returns>
        public IGroupConversationsCollectionRequestBuilder Conversations
        {
            get
            {
                return new GroupConversationsCollectionRequestBuilder(this.AppendSegmentToRequestUrl("conversations"), this.Client);
            }
        }

        /// <summary>
        /// Gets the request builder for Photo.
        /// </summary>
        /// <returns>The <see cref="IProfilePhotoRequestBuilder"/>.</returns>
        public IProfilePhotoRequestBuilder Photo
        {
            get
            {
                return new ProfilePhotoRequestBuilder(this.AppendSegmentToRequestUrl("photo"), this.Client);
            }
        }

        /// <summary>
        /// Gets the request builder for Photos.
        /// </summary>
        /// <returns>The <see cref="IGroupPhotosCollectionRequestBuilder"/>.</returns>
        public IGroupPhotosCollectionRequestBuilder Photos
        {
            get
            {
                return new GroupPhotosCollectionRequestBuilder(this.AppendSegmentToRequestUrl("photos"), this.Client);
            }
        }

        /// <summary>
        /// Gets the request builder for AcceptedSenders.
        /// </summary>
        /// <returns>The <see cref="IGroupAcceptedSendersCollectionRequestBuilder"/>.</returns>
        public IGroupAcceptedSendersCollectionRequestBuilder AcceptedSenders
        {
            get
            {
                return new GroupAcceptedSendersCollectionRequestBuilder(this.AppendSegmentToRequestUrl("acceptedSenders"), this.Client);
            }
        }

        /// <summary>
        /// Gets the request builder for RejectedSenders.
        /// </summary>
        /// <returns>The <see cref="IGroupRejectedSendersCollectionRequestBuilder"/>.</returns>
        public IGroupRejectedSendersCollectionRequestBuilder RejectedSenders
        {
            get
            {
                return new GroupRejectedSendersCollectionRequestBuilder(this.AppendSegmentToRequestUrl("rejectedSenders"), this.Client);
            }
        }

        /// <summary>
        /// Gets the request builder for Drive.
        /// </summary>
        /// <returns>The <see cref="IDriveRequestBuilder"/>.</returns>
        public IDriveRequestBuilder Drive
        {
            get
            {
                return new DriveRequestBuilder(this.AppendSegmentToRequestUrl("drive"), this.Client);
            }
        }

        /// <summary>
        /// Gets the request builder for Drives.
        /// </summary>
        /// <returns>The <see cref="IGroupDrivesCollectionRequestBuilder"/>.</returns>
        public IGroupDrivesCollectionRequestBuilder Drives
        {
            get
            {
                return new GroupDrivesCollectionRequestBuilder(this.AppendSegmentToRequestUrl("drives"), this.Client);
            }
        }

        /// <summary>
        /// Gets the request builder for Sites.
        /// </summary>
        /// <returns>The <see cref="IGroupSitesCollectionRequestBuilder"/>.</returns>
        public IGroupSitesCollectionRequestBuilder Sites
        {
            get
            {
                return new GroupSitesCollectionRequestBuilder(this.AppendSegmentToRequestUrl("sites"), this.Client);
            }
        }

        /// <summary>
        /// Gets the request builder for Planner.
        /// </summary>
        /// <returns>The <see cref="IPlannerGroupRequestBuilder"/>.</returns>
        public IPlannerGroupRequestBuilder Planner
        {
            get
            {
                return new PlannerGroupRequestBuilder(this.AppendSegmentToRequestUrl("planner"), this.Client);
            }
        }

        /// <summary>
        /// Gets the request builder for Onenote.
        /// </summary>
        /// <returns>The <see cref="IOnenoteRequestBuilder"/>.</returns>
        public IOnenoteRequestBuilder Onenote
        {
            get
            {
                return new OnenoteRequestBuilder(this.AppendSegmentToRequestUrl("onenote"), this.Client);
            }
        }
    
        /// <summary>
        /// Gets the request builder for GroupSubscribeByMail.
        /// </summary>
        /// <returns>The <see cref="IGroupSubscribeByMailRequestBuilder"/>.</returns>
        public IGroupSubscribeByMailRequestBuilder SubscribeByMail()
        {
            return new GroupSubscribeByMailRequestBuilder(
                this.AppendSegmentToRequestUrl("microsoft.graph.subscribeByMail"),
                this.Client);
        }

        /// <summary>
        /// Gets the request builder for GroupUnsubscribeByMail.
        /// </summary>
        /// <returns>The <see cref="IGroupUnsubscribeByMailRequestBuilder"/>.</returns>
        public IGroupUnsubscribeByMailRequestBuilder UnsubscribeByMail()
        {
            return new GroupUnsubscribeByMailRequestBuilder(
                this.AppendSegmentToRequestUrl("microsoft.graph.unsubscribeByMail"),
                this.Client);
        }

        /// <summary>
        /// Gets the request builder for GroupAddFavorite.
        /// </summary>
        /// <returns>The <see cref="IGroupAddFavoriteRequestBuilder"/>.</returns>
        public IGroupAddFavoriteRequestBuilder AddFavorite()
        {
            return new GroupAddFavoriteRequestBuilder(
                this.AppendSegmentToRequestUrl("microsoft.graph.addFavorite"),
                this.Client);
        }

        /// <summary>
        /// Gets the request builder for GroupRemoveFavorite.
        /// </summary>
        /// <returns>The <see cref="IGroupRemoveFavoriteRequestBuilder"/>.</returns>
        public IGroupRemoveFavoriteRequestBuilder RemoveFavorite()
        {
            return new GroupRemoveFavoriteRequestBuilder(
                this.AppendSegmentToRequestUrl("microsoft.graph.removeFavorite"),
                this.Client);
        }

        /// <summary>
        /// Gets the request builder for GroupResetUnseenCount.
        /// </summary>
        /// <returns>The <see cref="IGroupResetUnseenCountRequestBuilder"/>.</returns>
        public IGroupResetUnseenCountRequestBuilder ResetUnseenCount()
        {
            return new GroupResetUnseenCountRequestBuilder(
                this.AppendSegmentToRequestUrl("microsoft.graph.resetUnseenCount"),
                this.Client);
        }
    
    }
}
