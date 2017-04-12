using System.Collections.Generic;
using System.Linq;
using Dapper;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {
	public static class DatabaseOtpService {

		#region Enable/Disable Operations
		public static void OTP_Enable(long otp_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["otp_enable"], new { otp_id = otp_id });
			}
		}
		public static void OTP_Disable(long otp_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["otp_disable"], new { otp_id = otp_id });
			}
		}
		#endregion

		#region Get all
		public static List<OTP> GetOTPs() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getall"]).AsList();
			}
		}
		public static List<OTP> GetOTPsActive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getall_active"]).AsList();
			}
		}
		public static List<OTP> GetOTPsInactive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public static OTP GetOTPById(long otp_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getbyid"], new { otp_id = otp_id }).FirstOrDefault();
			}
		}
		public static OTP GetOTPByIdActive(long otp_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getbyid_active"], new { otp_id = otp_id }).FirstOrDefault();
			}
		}
		public static OTP GetOTPByIdInactive(long otp_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getbyid_inactive"], new { otp_id = otp_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by user id
		public static OTP GetOTPByUserId(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getbyuserid"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public static OTP GetOTPByUserIdActive(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getbyuserid_active"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public static OTP GetOTPByUserIdInactive(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getbyuserid_inactive"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public static long OTPInsert(OTP otp) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<long>(ScriptService.Scripts["otp_insert"], otp).Single();
			}
		}
		public static void OTPInsertOrUpdate(OTP otp) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				db.Execute(ScriptService.Scripts["otp_insert_or_update"], otp);
			}
		}
		#endregion

		#region Update
		public static void OTPUpdate(OTP otp) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				db.Execute(ScriptService.Scripts["otp_update"], otp);
			}
		}
		public static void OTPUpdateActive(OTP otp) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				db.Execute(ScriptService.Scripts["otp_update_active"], otp);
			}
		}
		public static void OTPUpdateInactive(OTP otp) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				db.Execute(ScriptService.Scripts["otp_update_inactive"], otp);
			}
		}
		#endregion
	}
}