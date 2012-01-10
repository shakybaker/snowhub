using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using facebook.Schema;
using facebook.Web;
using facebook.Linq;

namespace facebook
{


	/// <summary>
	/// The Facebook Database. Queries on tables on an instance will be done against the facebook servers.
	/// </summary>
	public class FacebookDataContext : FqlDataContext
	{
		/// <summary>
		/// Creates a FacebookDataContext using the specified api
		/// </summary>
		/// <param name="api"></param>
		public FacebookDataContext(API api)
			: base(api)
		{
		}

		/// <summary>
		/// Creates a FacebookDataContext using the FacebookSession.Current Api.
		/// </summary>
		public FacebookDataContext()
			: this( GetDefaultApi())
		{

		}

		static API GetDefaultApi()
		{
			var context = FacebookContext.Current;
			if(context!=null && context.Session!=null && context.Session.Api!=null)
				return context.Session.Api;
			return new API();
		}

		public long uid
		{
			get
			{
				return Api.uid;
			}
		}

		/// <summary>
		/// The FQL album table. Query this table to return information about a photo album.
		/// </summary>
		public FqlTable<album> album
		{
			get
			{
				return GetTable<album>();
			}
		}


		/// <summary>
		/// The FQL cookies table. Query this table to return information about a cookie.
		/// </summary>
		public FqlTable<cookie> cookie
		{
			get
			{
				return GetTable<cookie>();
			}
		}

		/// <summary>
		/// The FQL event table. Query this table to return information about an event.
		/// </summary>
		public FqlTable<facebookevent> facebookevent
		{
			get
			{
				return GetTable<facebookevent>();
			}
		}

		/// <summary>
		/// The FQL event_member table. Query this table to return information about a user's status for an event.
		/// </summary>
		public FqlTable<event_member> event_member
		{
			get
			{
				return GetTable<event_member>();
			}
		}

		/// <summary>
		/// The FQL friend table. Query this table to determine whether two users are linked together as friends.
		/// </summary>
		public FqlTable<friend_info> friend_info
		{
			get
			{
				return GetTable<friend_info>();
			}
		}

		///// <summary>
		///// The FQL friend_request table. Query this table either to determine which users have sent friend requests to the logged-in user or to query whether a friend request has been sent from the logged-in user to a specific user. You can run this query only when the uid_to is set to the logged-in user or when uid_from is set to the logged-in user and uid_to is set to a specific user.
		///// </summary>
		//public FqlTable<FriendRequest> FriendRequests
		//{
		//  get
		//  {
		//    return GetTable<FriendRequest>();
		//  }
		//}

		/// <summary>
		/// The FQL friendlist table. Query this table to return any friend lists owned by the specified user. You can run this query only when the owner is set to the logged-in user. You can store flids, but you cannot expose this information to anyone but the logged in user, as it is private.
		/// </summary>
		public FqlTable<friendlist> friendlist
		{
			get
			{
				return GetTable<friendlist>();
			}
		}


		/// <summary>
		/// The FQL group table. Query this table to return information about a group.
		/// </summary>
		public FqlTable<group> group
		{
			get
			{
				return GetTable<group>();
			}
		}

		/// <summary>
		/// The FQL group_member table. Query this table to return information about the members of a group.
		/// </summary>
		public FqlTable<group_member> group_member
		{
			get
			{
				return GetTable<group_member>();
			}
		}

		/// <summary>
		/// The FQL listing table. Query this table to return information about a listing in Facebook Marketplace.
		/// </summary>
		public FqlTable<listing> listing
		{
			get
			{
				return GetTable<listing>();
			}
		}

		/// <summary>
		/// The FQL metrics table. Query this table to retrieve metrics about your application. All metrics are identified by a name, and a period over which they've been collected (e.g. one day or seven days).
		/// </summary>
		public FqlTable<metrics> metrics
		{
			get
			{
				return GetTable<metrics>();
			}
		}

		/// <summary>
		/// The FQL page table. Query this table to return information about a Facebook Page.
		/// </summary>
		public FqlTable<page> page
		{
			get
			{
				return GetTable<page>();
			}
		}

		///// <summary>
		///// The FQL page_admin table. Query this table to return information about the admin of a Facebook Page.
		///// </summary>
		//public FqlTable<FacebookPageAdmin> PageAdmins
		//{
		//  get
		//  {
		//    return GetTable<FacebookPageAdmin>();
		//  }
		//}

		///// <summary>
		///// The FQL page_fan table. Query this table to return information about the fan of a Facebook Page.
		///// </summary>
		//public FqlTable<FacebookPageFan> PageFans
		//{
		//  get
		//  {
		//    return GetTable<FacebookPageFan>();
		//  }
		//}

		/// <summary>
		/// The FQL photo table. Query this table to return information about a photo.
		/// </summary>
		public FqlTable<photo> photo
		{
			get
			{
				return GetTable<photo>();
			}
		}

		/// <summary>
		/// The FQL photo_tag table. Query this table to return information about a photo tag.
		/// </summary>
		public FqlTable<photo_tag> photo_tag
		{
			get
			{
				return GetTable<photo_tag>();
			}
		}

		///// <summary>
		///// The FQL standard_user_info table. Query this table to return standard information about a user, for use when you need analytic information only. Don't display this information to any users. If you need to display information to other users, query the user FQL table instead.
		///// </summary>
		//public FqlTable<standard_user_info> StandardUsers
		//{
		//  get
		//  {
		//    return GetTable<StandardUserInfo>();
		//  }
		//}

		/// <summary>
		/// The FQL user table. Query this table to return detailed information from a user's profile. If you need user information for analytic purposes, query the standard_user_info table instead.
		/// </summary>
		public FqlTable<user> user
		{
			get
			{
				return GetTable<user>();
			}
		}


		/// <summary>
		/// The current Facebook user (null if logged off)
		/// </summary>
		public user CurrentUser
		{
			get
			{
				return GetObject<user>(Api.uid, true);
			}
		}

		#region TODO
		

		///// <summary>
		///// The FQL comment table. Query this table to obtain comments associated with an fb:comments/Feed story comment XID.
		///// </summary>
		//public FqlTable<Comment> Comments
		//{
		//  get
		//  {
		//    return GetTable<Comment>();
		//  }
		//}
		///// <summary>
		///// The FQL friendlist_member table. Query this table to determine which users are members of a friend list. You can run this query only when the flid is owned by the logged-in user. You cannot expose this information to anyone but the logged in user, as it is private. In addition, you cannot store flids.
		///// </summary>
		//public FqlTable<FriendListMember> FriendListMembers
		//{
		//  get
		//  {
		//    return GetTable<FriendListMember>();
		//  }
		//}
		///// <summary>
		///// The FQL permissions table. Query this table to return the extended permissions the current user has granted to the application.
		///// </summary>
		//public FqlTable<UserPermission> UserPermissions
		//{
		//  get
		//  {
		//    return GetTable<UserPermission>();
		//  }
		//}

		#endregion


	}
}
