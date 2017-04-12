using System.Collections.Generic;
using System.Linq;
using Dapper;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {
	public static class DatabaseLoginService {

		#region Enable/Disable Operations
		public static void Enable(long login_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["login_enable"], new { login_id = login_id });
			}
		}
		public static void Disable(long login_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["login_disable"], new { login_id = login_id });
			}
		}
		#endregion

		#region Get all
		public static List<Login> GetAll() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getall"]).AsList();
			}
		}
		public static List<Login> GetAllActive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getall_active"]).AsList();
			}
		}
		public static List<Login> GetAllInactive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public static Login GetById(long login_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyid"], new { login_id = login_id }).FirstOrDefault();
			}
		}
		public static Login GetByIdActive(long login_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyid_active"], new { login_id = login_id }).FirstOrDefault();
			}
		}
		public static Login GetByIdInactive(long login_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyid_inactive"], new { login_id = login_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by user id
		public static Login GetByUserId(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyuserid"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public static Login GetByUserIdActive(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyuserid_active"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public static Login GetByUserIdInactive(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyuserid_inactive"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public static long Insert(Login login) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<long>(ScriptService.Scripts["login_insert"], login).Single();
			}
		}
		public static void InsertOrUpdate(Login login) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				db.Execute(ScriptService.Scripts["login_insert_or_update"], login);
			}
		}
		#endregion

		#region Update
		public static void Update(Login login) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				db.Execute(ScriptService.Scripts["login_update"], login);
			}
		}
		public static void UpdateActive(Login login) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				db.Execute(ScriptService.Scripts["login_update_active"], login);
			}
		}
		public static void UpdateInactive(Login login) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				db.Execute(ScriptService.Scripts["login_update_inactive"], login);
			}
		}
		#endregion

	}
}