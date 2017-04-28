using System.Collections.Generic;
using System.Linq;
using Dapper;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {

	/**
	 * Establishes all SQL squery methods for the named model.
	 * Handles all dapper interaction and attribute mapping.
	 */
	public static class DatabaseOtpService {

		#region Enable/Disable Operations
		public static void Enable(long otp_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["otp_enable"], new { otp_id = otp_id });
			}
		}
		public static void Disable(long otp_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["otp_disable"], new { otp_id = otp_id });
			}
		}
		#endregion

		#region Get all
		public static List<OTP> GetAll() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getall"]).AsList();
			}
		}
		public static List<OTP> GetAllActive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getall_active"]).AsList();
			}
		}
		public static List<OTP> GetAllInactive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public static OTP GetById(long otp_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getbyid"], new { otp_id = otp_id }).FirstOrDefault();
			}
		}
		public static OTP GetByIdActive(long otp_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getbyid_active"], new { otp_id = otp_id }).FirstOrDefault();
			}
		}
		public static OTP GetByIdInactive(long otp_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getbyid_inactive"], new { otp_id = otp_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by user id
		public static OTP GetByUserId(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getbyuserid"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public static OTP GetByUserIdActive(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getbyuserid_active"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public static OTP GetByUserIdInactive(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getbyuserid_inactive"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by user id
		public static OTP GetByCode(string otp_code) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getbycode"], new { otp_code = otp_code }).FirstOrDefault();
			}
		}
		public static OTP GetByCodeActive(string otp_code) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getbycode_active"], new { otp_code = otp_code }).FirstOrDefault();
			}
		}
		public static OTP GetByCodeInactive(string otp_code) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getbycode_inactive"], new { otp_code = otp_code }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public static long Insert(OTP otp) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<long>(ScriptService.Scripts["otp_insert"], otp).Single();
			}
		}
		public static void InsertOrUpdate(OTP otp) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				db.Execute(ScriptService.Scripts["otp_insert_or_update"], otp);
			}
		}
		#endregion

		#region Update
		public static void Update(OTP otp) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				db.Execute(ScriptService.Scripts["otp_update"], otp);
			}
		}
		public static void UpdateActive(OTP otp) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				db.Execute(ScriptService.Scripts["otp_update_active"], otp);
			}
		}
		public static void UpdateInactive(OTP otp) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				db.Execute(ScriptService.Scripts["otp_update_inactive"], otp);
			}
		}
		#endregion
	}
}