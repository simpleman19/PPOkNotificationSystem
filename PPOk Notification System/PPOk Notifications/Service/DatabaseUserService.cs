using System.Collections.Generic;
using System.Linq;
using Dapper;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {
	public static class DatabaseUserService {

		#region Enable/Disable Operations
		public static void User_Enable(long user_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["user_enable"], user_id);
			}
		}
		public static void User_Disable(long user_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["user_disable"], new { user_id = user_id });
			}
		}
		#endregion

		#region Get all
		public static List<User> GetUsers() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getall"]).AsList();
			}
		}
		public static List<User> GetUsersActive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getall_active"]).AsList();
			}
		}
		public static List<User> GetUsersInactive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public static User GetUserById(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyid"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public static User GetUserByIdActive(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyid_active"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public static User GetUserByIdInactive(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyid_inactive"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by email
		public static User GetUserByEmail(string user_email) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyemail"], new { user_email = user_email }).FirstOrDefault();
			}
		}
		public static User GetUserByEmailActive(string user_email) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyemail_active"], new { user_email = user_email }).FirstOrDefault();
			}
		}
		public static User GetUserByEmailInactive(string user_email) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyemail_inactive"], new { user_email = user_email }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by name
		public static User GetUserByName(string user_fname, string user_lname) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyname"], new { user_fname = user_fname, user_lname = user_lname }).FirstOrDefault();
			}
		}
		public static User GetUserByNameActive(string user_fname, string user_lname) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyname_active"], new { user_fname = user_fname, user_lname = user_lname }).FirstOrDefault();
			}
		}
		public static User GetUserByNameInactive(string user_fname, string user_lname) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyname_inactive"], new { user_fname = user_fname, user_lname = user_lname }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by phone number
		public static User GetUserByPhone(string user_phone) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyphone"], new { user_phone = user_phone }).FirstOrDefault();
			}
		}
		public static User GetUserByPhoneActive(string user_phone) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyphone_active"], new { user_phone = user_phone }).FirstOrDefault();
			}
		}
		public static User GetUserByPhoneInactive(string user_phone) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyphone_inactive"], new { user_phone = user_phone }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public static long UserInsert(User user) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<long>(ScriptService.Scripts["user_insert"], user).Single();
			}
		}
		public static void UserInsertOrUpdate(User user) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				db.Execute(ScriptService.Scripts["user_insert_or_update"], user);
			}
		}
		#endregion

		#region Update
		public static void UserUpdate(User user) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				db.Execute(ScriptService.Scripts["user_update"], user);
			}
		}
		public static void UserUpdateActive(User user) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				db.Execute(ScriptService.Scripts["user_update_active"], user);
			}
		}
		public static void UserUpdateInactive(User user) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				db.Execute(ScriptService.Scripts["user_update_inactive"], user);
			}
		}
		#endregion
	}
}