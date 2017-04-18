using System.Collections.Generic;
using System.Linq;
using Dapper;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {

	/**
	 * Establishes all SQL squery methods for the named model.
	 * Handles all dapper interaction and attribute mapping.
	 */
	public static class DatabaseEmailOtpService {

		#region Enable/Disable Operations
		public static void Enable(long emailotp_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["emailotp_enable"], new { emailotp_id = emailotp_id });
			}
		}
		public static void Disable(long emailotp_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["emailotp_disable"], new { emailotp_id = emailotp_id });
			}
		}
		#endregion

		#region Get all
		public static List<EmailOTP> GetAll() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getall"]).AsList();
			}
		}
		public static List<EmailOTP> GetAllActive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getall_active"]).AsList();
			}
		}
		public static List<EmailOTP> GetAllInactive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public static EmailOTP GetById(long emailotp_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbyid"], new { emailotp_id = emailotp_id }).FirstOrDefault();
			}
		}
		public static EmailOTP GetByIdActive(long emailotp_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbyid_active"], new { emailotp_id = emailotp_id }).FirstOrDefault();
			}
		}
		public static EmailOTP GetByIdInactive(long emailotp_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbyid_inactive"], new { emailotp_id = emailotp_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by notification id
		public static EmailOTP GetByNotificationId(long notification_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbynotificationid"], new { notification_id = notification_id }).FirstOrDefault();
			}
		}
		public static EmailOTP GetByNotificationIdActive(long notification_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbynotificationid_active"], new { notification_id = notification_id }).FirstOrDefault();
			}
		}
		public static EmailOTP GetByNotificationIdInactive(long notification_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbynotificationid_inactive"], new { notification_id = notification_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by code
		public static EmailOTP GetByCode(string emailotp_code) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbycode"], new { emailotp_code = emailotp_code }).FirstOrDefault();
			}
		}
		public static EmailOTP GetByCodeActive(string emailotp_code) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbycode_active"], new { emailotp_code = emailotp_code }).FirstOrDefault();
			}
		}
		public static EmailOTP GetByCodeInactive(string emailotp_code) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbycode_inactive"], new { emailotp_code = emailotp_code }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public static long Insert(EmailOTP emailotp) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<long>(ScriptService.Scripts["emailotp_insert"], emailotp).Single();
			}
		}
		public static void InsertOrUpdate(EmailOTP emailotp) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				db.Execute(ScriptService.Scripts["emailotp_insert_or_update"], emailotp);
			}
		}
		#endregion

		#region Update
		public static void Update(EmailOTP emailotp) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				db.Execute(ScriptService.Scripts["emailotp_update"], emailotp);
			}
		}
		public static void UpdateActive(EmailOTP emailotp) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				db.Execute(ScriptService.Scripts["emailotp_update_active"], emailotp);
			}
		}
		public static void UpdateInactive(EmailOTP emailotp) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				db.Execute(ScriptService.Scripts["emailotp_update_inactive"], emailotp);
			}
		}
		#endregion
	}
}