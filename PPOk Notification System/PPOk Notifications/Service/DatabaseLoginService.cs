using System.Collections.Generic;
using System.Linq;
using Dapper;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {
	public static class DatabaseLoginService {

		#region Enable/Disable Operations
		public static void Login_Enable(long login_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["login_enable"], new { login_id = login_id });
			}
		}
		public static void Login_Disable(long login_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["login_disable"], new { login_id = login_id });
			}
		}
		#endregion

		#region Get all
		public static List<Login> GetLogins() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getall"]).AsList();
			}
		}
		public static List<Login> GetLoginsActive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getall_active"]).AsList();
			}
		}
		public static List<Login> GetLoginsInactive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public static Login GetLoginById(long login_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyid"], new { login_id = login_id }).FirstOrDefault();
			}
		}
		public static Login GetLoginByIdActive(long login_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyid_active"], new { login_id = login_id }).FirstOrDefault();
			}
		}
		public static Login GetLoginByIdInactive(long login_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyid_inactive"], new { login_id = login_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by user id
		public static Login GetLoginByUserId(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyuserid"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public static Login GetLoginByUserIdActive(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyuserid_active"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public static Login GetLoginByUserIdInactive(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyuserid_inactive"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public static long LoginInsert(Login login) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<long>(ScriptService.Scripts["login_insert"], login).Single();
			}
		}
		public static void LoginInsertOrUpdate(Login login) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				db.Execute(ScriptService.Scripts["login_insert_or_update"], login);
			}
		}
		#endregion

		#region Update
		public static void LoginUpdate(Login login) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				db.Execute(ScriptService.Scripts["login_update"], login);
			}
		}
		public static void LoginUpdateActive(Login login) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				db.Execute(ScriptService.Scripts["login_update_active"], login);
			}
		}
		public static void LoginUpdateInactive(Login login) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				db.Execute(ScriptService.Scripts["login_update_inactive"], login);
			}
		}
		#endregion

	}
}