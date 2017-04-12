using System.Collections.Generic;
using System.Linq;
using Dapper;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {
	public static class DatabaseEmailOtpService {

		#region Enable/Disable Operations
		public static void EmailOTP_Enable(long emailotp_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["emailotp_enable"], new { emailotp_id = emailotp_id });
			}
		}
		public static void EmailOTP_Disable(long emailotp_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["emailotp_disable"], new { emailotp_id = emailotp_id });
			}
		}
		#endregion

		#region Get all
		public static List<EmailOTP> GetEmailOTPs() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getall"]).AsList();
			}
		}
		public static List<EmailOTP> GetEmailOTPsActive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getall_active"]).AsList();
			}
		}
		public static List<EmailOTP> GetEmailOTPsInactive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public static EmailOTP GetEmailOTPById(long emailotp_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbyid"], new { emailotp_id = emailotp_id }).FirstOrDefault();
			}
		}
		public static EmailOTP GetEmailOTPByIdActive(long emailotp_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbyid_active"], new { emailotp_id = emailotp_id }).FirstOrDefault();
			}
		}
		public static EmailOTP GetEmailOTPByIdInactive(long emailotp_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbyid_inactive"], new { emailotp_id = emailotp_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by notification id
		public static EmailOTP GetEmailOTPByNotificationId(long notification_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbynotificationid"], new { notification_id = notification_id }).FirstOrDefault();
			}
		}
		public static EmailOTP GetEmailOTPByNotificationIdActive(long notification_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbynotificationid_active"], new { notification_id = notification_id }).FirstOrDefault();
			}
		}
		public static EmailOTP GetEmailOTPByNotificationIdInactive(long notification_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbynotificationid_inactive"], new { notification_id = notification_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by code
		public static EmailOTP GetEmailOTPByCode(string emailotp_code) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbycode"], new { emailotp_code = emailotp_code }).FirstOrDefault();
			}
		}
		public static EmailOTP GetEmailOTPByCodeActive(string emailotp_code) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbycode_active"], new { emailotp_code = emailotp_code }).FirstOrDefault();
			}
		}
		public static EmailOTP GetEmailOTPByCodeInactive(string emailotp_code) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbycode_inactive"], new { emailotp_code = emailotp_code }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public static long EmailOTPInsert(EmailOTP emailotp) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<long>(ScriptService.Scripts["emailotp_insert"], emailotp).Single();
			}
		}
		public static void EmailOTPInsertOrUpdate(EmailOTP emailotp) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				db.Execute(ScriptService.Scripts["emailotp_insert_or_update"], emailotp);
			}
		}
		#endregion

		#region Update
		public static void EmailOTPUpdate(EmailOTP emailotp) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				db.Execute(ScriptService.Scripts["emailotp_update"], emailotp);
			}
		}
		public static void EmailOTPUpdateActive(EmailOTP emailotp) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				db.Execute(ScriptService.Scripts["emailotp_update_active"], emailotp);
			}
		}
		public static void EmailOTPUpdateInactive(EmailOTP emailotp) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				db.Execute(ScriptService.Scripts["emailotp_update_inactive"], emailotp);
			}
		}
		#endregion
	}
}