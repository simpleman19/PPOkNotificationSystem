using System.Collections.Generic;
using System.Linq;
using Dapper;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {
	public static class DatabaseRefillService {

		#region Enable/Disable Operations
		public static void Refill_Enable(long refill_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["refill_enable"], new { refill_id = refill_id });
			}
		}
		public static void Refill_Disable(long refill_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["refill_disable"], new { refill_id = refill_id });
			}
		}
		#endregion

		#region Get all
		public static List<Refill> GetRefills() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getall"]).AsList();
			}
		}
		public static List<Refill> GetRefillsActive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getall_active"]).AsList();
			}
		}
		public static List<Refill> GetRefillsInactive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public static Refill GetRefillById(long refill_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getbyid"], new { refill_id = refill_id }).FirstOrDefault();
			}
		}
		public static Refill GetRefillByIdActive(long refill_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getbyid_active"], new { refill_id = refill_id }).FirstOrDefault();
			}
		}
		public static Refill GetRefillByIdInactive(long refill_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getbyid_inactive"], new { refill_id = refill_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by prescription id
		public static Refill GetRefillByPrescriptionId(long prescription_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getbyprescriptionid"], new { prescription_id = prescription_id }).FirstOrDefault();
			}
		}
		public static Refill GetRefillByPrescriptionIdActive(long prescription_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getbyprescriptionid_active"], new { prescription_id = prescription_id }).FirstOrDefault();
			}
		}
		public static Refill GetRefillByPrescriptionIdInactive(long prescription_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getbyprescriptionid_inactive"], new { prescription_id = prescription_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public static long RefillInsert(Refill refill) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<long>(ScriptService.Scripts["refill_insert"], refill).Single();
			}
		}
		public static void RefillInsertOrUpdate(Refill refill) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				db.Execute(ScriptService.Scripts["refill_insert_or_update"], refill);
			}
		}
		#endregion

		#region Update
		public static void RefillUpdate(Refill refill) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				db.Execute(ScriptService.Scripts["refill_update"], refill);
			}
		}
		public static void RefillUpdateActive(Refill refill) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				db.Execute(ScriptService.Scripts["refill_update_active"], refill);
			}
		}
		public static void RefillUpdateInactive(Refill refill) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				db.Execute(ScriptService.Scripts["refill_update_inactive"], refill);
			}
		}
		#endregion
	}
}