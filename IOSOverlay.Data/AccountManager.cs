using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOSOverlay.Data.Models;

namespace IOSOverlay.Data {
	public static class AccountManager {
		private static UserModel _ActiveUser;
		/// <summary>
		/// The temporary user
		/// <para>Overrides the _ActiveUser when not-null,
		/// used for logging into the admin panel and other areas.</para>
		/// </summary>
		private static UserModel _TempUser;
		private static event EventHandler _UserActivated;

		/// <summary>
		/// Gets a value indicating whether [active user is student].
		/// </summary>
		/// <value>
		///   <c>true</c> if [active user is student]; otherwise, <c>false</c>.
		/// </value>
		public static bool ActiveUserIsStudent => ActiveType == UserTypes.Student;
		/// <summary>
		/// Gets a value indicating whether [active user is instructor].
		/// </summary>
		/// <value>
		///   <c>true</c> if [active user is instructor]; otherwise, <c>false</c>.
		/// </value>
		public static bool ActiveUserIsInstructor => ActiveType >= UserTypes.Instructor;
		/// <summary>
		/// Gets a value indicating whether [active user is moderator].
		/// </summary>
		/// <value>
		///   <c>true</c> if [active user is moderator]; otherwise, <c>false</c>.
		/// </value>
		public static bool ActiveUserIsModerator => ActiveType >= UserTypes.Moderator;
		/// <summary>
		/// Gets a value indicating whether [active user is admin].
		/// </summary>
		/// <value>
		///   <c>true</c> if [active user is admin]; otherwise, <c>false</c>.
		/// </value>
		public static bool ActiveUserIsAdmin => ActiveType >= UserTypes.Admin;
		/// <summary>
		/// Gets a value indicating whether [active user is master].
		/// </summary>
		/// <value>
		///   <c>true</c> if [active user is master]; otherwise, <c>false</c>.
		/// </value>
		public static bool ActiveUserIsMaster => ActiveType == UserTypes.Master;

		/// <summary>
		/// Gets the active user.
		/// </summary>
		/// <value>
		/// The active user.
		/// </value>
		public static UserModel ActiveUser {
			get {
				if(_TempUser != null) return _TempUser;
				return _ActiveUser;
			}
			private set {
				_ActiveUser = value;
				RaiseUserActivated();
			}
		}
		/// <summary>
		/// Gets a value indicating whether a user is logged in outside of the temp user.
		/// </summary>
		/// <value>
		///   <c>true</c> if [primary user set]; otherwise, <c>false</c>.
		/// </value>
		public static bool PrimaryUserSet => _ActiveUser != null;
		/// <summary>
		/// Occurs when [user activated].
		/// </summary>
		public static event EventHandler UserActivated {
			add { _UserActivated += value; }
			remove { _UserActivated -= value; }
		}
		/// <summary>
		/// Gets the type of the active.
		/// </summary>
		/// <value>
		/// The type of the active.
		/// </value>
		public static UserTypes ActiveType => ActiveUser?.UserTypeFlag ?? UserTypes.None;
		/// <summary>
		/// Gets the permissions of the active user.
		/// </summary>
		/// <value>
		/// The active user's permissions.
		/// </value>
		public static UserPermissionFlags ActivePermissions => ActiveUser?.UserPermissions ?? UserPermissionFlags.None;
		/// <summary>
		/// Gets ID of the active user.
		/// </summary>
		/// <value>
		/// The active user ID
		/// </value>
		[Obsolete]
		public static int ActiveID => ActiveUser?.ID ?? -1;
		public static Guid ActiveUID => ActiveUser?.UID ?? Guid.Empty;

		private static void RaiseUserActivated() {
			if(_UserActivated != null) _UserActivated.Invoke(ActiveUser, new EventArgs());
		}
		public static void GenerateMaster() {
			ModelManager.UserCollection.AddNew(new User() { FirstName = "IES", LastName = "NACB", Password = "1986", Permissions = (int)UserPermissionFlags.DefaultMaster, UserType = (int)UserTypes.Master });
			ModelManager.UserCollection.EndEditActiveSelection();
		}
		/// <summary>
		/// Sets the active user.
		/// </summary>
		/// <param name="user">The user.</param>
		public static void SetActiveUser(UserModel user) {
			if(user == null) return;
			ActiveUser = user;
			ActiveUser.SetLastActive(DateTime.Now);
		}
		/// <summary>
		/// Attempts to set the active user to the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="password">The password.</param>
		/// <returns></returns>
		public static bool TryActivate(UserModel user, string password) {
			if(CheckPassword(user, password)) {
				SetActiveUser(user);
				return true;
			}
			return false;
		}
		/// <summary>
		/// Attempts to set the active temporary user
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="password">The password.</param>
		/// <returns></returns>
		public static bool TryActivateTemp(UserModel user, string password) {
			if(CheckPassword(user, password)) {
				_TempUser = user;
				ActiveUser.SetLastActive(DateTime.Now);
				return true;
			}
			return false;
		}
		/// <summary>
		/// Checks if the specified password matches that of the specified user
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="password">The password.</param>
		/// <returns></returns>
		public static bool CheckPassword(UserModel user, string password) {
#if DEBUG
			if(ModelManager.SimulatorSettings.EnableDebugMode) {
				return user != null;
			} else {
				return user != null && user.Password.Equals(password);
			}
#else
				return user != null && user.Password.Equals(password);
#endif
		}
		/// <summary>
		/// Deactivates the active user.
		/// </summary>
		/// <returns>False if no user is active, otherwise True</returns>
		public static bool Deactivate() {
			if(ActiveUser != null) {
				ActiveUser = null;
				return true;
			}
			return false;
		}
		/// <summary>
		/// Deactivates the active temp user
		/// </summary>
		/// <returns>False if there is no temp user, otherwise True.</returns>
		public static bool DeactivateTemp() {
			if(_TempUser != null) {
				_TempUser = null;
				return true;
			}
			return false;
		}
		/// <summary>
		/// Accounts the identifier to uid.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		public static Guid AccountIDToUID(int id) {
			return ModelManager.UserCollection[id]?.UID ?? Guid.Empty;
		}
		/// <summary>
		/// Accounts the uid to identifier.
		/// </summary>
		/// <param name="uid">The uid.</param>
		/// <returns></returns>
		public static int AccountUIDToID(Guid uid) {
			return ModelManager.UserCollection[uid]?.ID ?? -1;
		}
		/// <summary>
		/// Checks if the active user has the specified permission flag(s) set.
		/// </summary>
		/// <param name="flag">The permissions flag.</param>
		/// <returns></returns>
		public static bool ActiveHasPermission(UserPermissionFlags flag) => ActiveUser?.UserPermissions.HasFlag(flag) ?? false;
		/// <summary>
		/// Determines whether the specified user is the active user
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public static bool IsActiveUser(UserModel user) => (ActiveUser != null && user != null) ? (user.UID == ActiveUser.UID) : false;
		/// <summary>
		/// Determines whether the active user is the owner of the specified model.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <returns></returns>
		public static bool IsOwner(Model model) => (AccountManager.ActiveUID != Guid.Empty && model != null) && (AccountManager.ActiveUID == (model?.CreatorUID ?? Guid.Empty));
		/// <summary>
		/// Determines whether the active user can edit the specified model.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <returns></returns>
		public static bool CanEdit(Model model) => (model != null) && (IsOwner(model) || AccountManager.ActiveType >= UserTypes.Admin);
		/// <summary>
		/// Determines whether the specified user is admin.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public static bool IsAdmin(UserModel user) => (user.UserTypeFlag == UserTypes.Admin);
	}
}
