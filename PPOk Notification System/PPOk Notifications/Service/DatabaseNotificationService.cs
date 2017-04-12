using System.Collections.Generic;
using System.Linq;
using Dapper;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {
	public static class DatabaseNotificationService {

		#region Enable/Disable Operations
		public static void Notification_Enable(long notification_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["notification_enable"], new { notification_id = notification_id });
			}
		}
		public static void Notification_Disable(long notification_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["notification_disable"], new { notification_id = notification_id });
			}
		}
		#endregion

		#region Get all
		public static List<Notification> GetNotifications() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getall"]).AsList();
			}
		}
		public static List<Notification> GetNotificationsActive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getall_active"]).AsList();
			}
		}
		public static List<Notification> GetNotificationsInactive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getall_inactive"]).AsList();
			}
		}
		public static List<Notification> GetNotificationsToday() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getall_today"]).AsList();
			}
		}
		public static List<Notification> GetNotificationsFutureDate() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getall_future_date"]).AsList();
			}
		}
		public static List<Notification> GetNotificationsFutureTime() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getall_future_time"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public static Notification GetNotificationById(long notification_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getbyid"], new { notification_id = notification_id }).FirstOrDefault();
			}
		}
		public static Notification GetNotificationByIdActive(long notification_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getbyid_active"], new { notification_id = notification_id }).FirstOrDefault();
			}
		}
		public static Notification GetNotificationByIdInactive(long notification_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getbyid_inactive"], new { notification_id = notification_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by patient id
		public static List<Notification> GetNotificationsByPatientId(long patient_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getbypatientid"], new { patient_id = patient_id }).AsList();
			}
		}
		public static Notification GetNotificationByPatientIdActive(long patient_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getbypatientid_active"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		public static Notification GetNotificationByPatientIdInactive(long patient_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getbypatientid_inactive"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public static long NotificationInsert(Notification notification) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<long>(ScriptService.Scripts["notification_insert"], notification).Single();
			}
		}

		public static void NotificationInsertOrUpdate(Notification notification) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				db.Execute(ScriptService.Scripts["notification_insert_or_update"], notification);
			}
		}
		#endregion

		#region Update
		public static void NotificationUpdate(Notification notification) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				db.Execute(ScriptService.Scripts["notification_update"], notification);
			}
		}
		public static void NotificationUpdateActive(Notification notification) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				db.Execute(ScriptService.Scripts["notification_update_active"], notification);
			}
		}
		public static void NotificationUpdateInactive(Notification notification) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				db.Execute(ScriptService.Scripts["notification_update_inactive"], notification);
			}
		}
		#endregion
	}
}