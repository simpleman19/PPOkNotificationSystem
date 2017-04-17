using System.Collections.Generic;
using System.Linq;
using Dapper;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {
	public static class DatabaseRefillService {

		#region Enable/Disable Operations
		public static void Enable(long refill_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["refill_enable"], new { refill_id = refill_id });
			}
		}
		public static void Disable(long refill_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["refill_disable"], new { refill_id = refill_id });
			}
		}
		#endregion

		#region Get all
		public static List<Refill> GetAll() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getall"]).AsList();
			}
		}
		public static List<Refill> GetAll(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getallbypharmacyid"], new { pharmacy_id = pharmacy_id }).AsList();
			}
		}
		public static List<Refill> GetAllActive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getall_active"]).AsList();
			}
		}
		public static List<Refill> GetAllActive(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getallbypharmacyid_active"], new { pharmacy_id = pharmacy_id }).AsList();
			}
		}
		public static List<Refill> GetAllInactive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getall_inactive"]).AsList();
			}
		}
		public static List<Refill> GetAllInactive(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getallbypharmacyid_inactive"], new { pharmacy_id = pharmacy_id }).AsList();
			}
		}
		#endregion

		#region Get by id
		public static Refill GetById(long refill_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getbyid"], new { refill_id = refill_id }).FirstOrDefault();
			}
		}
		public static Refill GetByIdActive(long refill_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getbyid_active"], new { refill_id = refill_id }).FirstOrDefault();
			}
		}
		public static Refill GetByIdInactive(long refill_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getbyid_inactive"], new { refill_id = refill_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by prescription id
		public static Refill GetByPrescriptionId(long prescription_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getbyprescriptionid"], new { prescription_id = prescription_id }).FirstOrDefault();
			}
		}
		public static Refill GetByPrescriptionIdActive(long prescription_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getbyprescriptionid_active"], new { prescription_id = prescription_id }).FirstOrDefault();
			}
		}
		public static Refill GetByPrescriptionIdInactive(long prescription_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getbyprescriptionid_inactive"], new { prescription_id = prescription_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public static long Insert(Refill refill) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<long>(ScriptService.Scripts["refill_insert"], refill).Single();
			}
		}
		public static void InsertOrUpdate(Refill refill) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				db.Execute(ScriptService.Scripts["refill_insert_or_update"], refill);
			}
		}
		#endregion

		#region Update
		public static void Update(Refill refill) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				db.Execute(ScriptService.Scripts["refill_update"], refill);
			}
		}
		public static void UpdateActive(Refill refill) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				db.Execute(ScriptService.Scripts["refill_update_active"], refill);
			}
		}
		public static void UpdateInactive(Refill refill) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				db.Execute(ScriptService.Scripts["refill_update_inactive"], refill);
			}
		}
		#endregion
	}
}