using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {

	/**
	 * Establishes all SQL squery methods for the named model.
	 * Handles all dapper interaction and attribute mapping.
	 */
	public static class DatabaseNotificationService {

		#region Enable/Disable Operations
		public static void Enable(long notification_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["notification_enable"], new { notification_id = notification_id });
			}
		}
		public static void Disable(long notification_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["notification_disable"], new { notification_id = notification_id });
			}
		}
		#endregion

		#region Get all
		public static List<Notification> GetAll() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getall"]).AsList();
			}
		}
		public static List<Notification> GetAll(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getallbypharmacyid"], new { pharmacy_id = pharmacy_id }).AsList();
			}
		}
		public static List<Notification> GetAllActive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getall_active"]).AsList();
			}
		}
		public static List<Notification> GetAllActive(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getallbypharmacyid_active"], new { pharmacy_id = pharmacy_id }).AsList();
			}
		}
		public static List<Notification> GetAllInactive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getall_inactive"]).AsList();
			}
		}
		public static List<Notification> GetAllInactive(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getallbypharmacyid_inactive"], new { pharmacy_id = pharmacy_id }).AsList();
			}
		}
		public static List<Notification> GetToday() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getall_today"]).AsList();
			}
		}
		public static List<Notification> GetToday(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getallbypharmacyid_today"], new { pharmacy_id = pharmacy_id }).AsList();
			}
		}
		public static List<Notification> GetFutureDate() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getall_future_date"]).AsList();
			}
		}
		public static List<Notification> GetFutureDate(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getallbypharmacyid_future_date"], new { pharmacy_id = pharmacy_id }).AsList();
			}
		}
		public static List<Notification> GetDateRange(DateTime BeginDate, DateTime EndDate) {
            try
            {
                using (var db = DatabaseService.Connection)
                {
                    Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
                    return db.Query<Notification>(ScriptService.Scripts["notification_getall_range_date"], new { BeginDate = BeginDate, EndDate = EndDate }).AsList();
                }
            } catch(Exception e)
            {
                return null;
            }

		}
		public static List<Notification> GetDateRange(long pharmacy_id, DateTime BeginDate, DateTime EndDate) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getallbypharmacyid_range_date"], new { pharmacy_id = pharmacy_id, BeginDate = BeginDate, EndDate = EndDate }).AsList();
			}
		}
		public static List<Notification> GetFutureTime() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getall_future_time"]).AsList();
			}
		}
		public static List<Notification> GetFutureTime(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getallbypharmacyid_future_time"], new { pharmacy_id = pharmacy_id}).AsList();
			}
		}
		public static List<Notification> GetTimeRange(DateTime BeginDate, DateTime EndDate) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getall_range_time"], new { BeginDate = BeginDate , EndDate = EndDate }).AsList();
			}
		}
		public static List<Notification> GetTimeRange(long pharmacy_id, DateTime BeginDate, DateTime EndDate) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getallbypharmacyid_range_time"], new { pharmacy_id = pharmacy_id, BeginDate = BeginDate, EndDate = EndDate }).AsList();
			}
		}
		#endregion

		#region Get by id
		public static Notification GetById(long notification_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getbyid"], new { notification_id = notification_id }).FirstOrDefault();
			}
		}
		public static Notification GetByIdActive(long notification_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getbyid_active"], new { notification_id = notification_id }).FirstOrDefault();
			}
		}
		public static Notification GetByIdInactive(long notification_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getbyid_inactive"], new { notification_id = notification_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by patient id
		public static List<Notification> GetByPatientId(long patient_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getbypatientid"], new { patient_id = patient_id }).AsList();
			}
		}
		public static Notification GetByPatientIdActive(long patient_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getbypatientid_active"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		public static Notification GetByPatientIdInactive(long patient_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getbypatientid_inactive"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public static long Insert(Notification notification) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<long>(ScriptService.Scripts["notification_insert"], notification).Single();
			}
		}

		public static void InsertOrUpdate(Notification notification) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				db.Execute(ScriptService.Scripts["notification_insert_or_update"], notification);
			}
		}
		#endregion

		#region Update
		public static void Update(Notification notification) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				db.Execute(ScriptService.Scripts["notification_update"], notification);
			}
		}
		public static void UpdateActive(Notification notification) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				db.Execute(ScriptService.Scripts["notification_update_active"], notification);
			}
		}
		public static void UpdateInactive(Notification notification) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				db.Execute(ScriptService.Scripts["notification_update_inactive"], notification);
			}
		}
		#endregion
	}
}