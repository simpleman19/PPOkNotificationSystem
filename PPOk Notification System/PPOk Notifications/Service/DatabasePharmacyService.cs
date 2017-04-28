using System.Collections.Generic;
using System.Linq;
using Dapper;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {

	/**
	 * Establishes all SQL squery methods for the named model.
	 * Handles all dapper interaction and attribute mapping.
	 */
	public static class DatabasePharmacyService {

		#region Enable/Disable Operations
		public static void Enable(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["pharmacy_enable"], new { pharmacy_id = pharmacy_id });
			}
		}
		public static void Disable(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["pharmacy_disable"], new { pharmacy_id = pharmacy_id });
			}
		}
		#endregion

		#region Get all
		public static List<Pharmacy> GetAll() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<Pharmacy>(ScriptService.Scripts["pharmacy_getall"]).AsList();
			}
		}
		public static List<Pharmacy> GetAllActive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<Pharmacy>(ScriptService.Scripts["pharmacy_getall_active"]).AsList();
			}
		}
		public static List<Pharmacy> GetAllInactive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<Pharmacy>(ScriptService.Scripts["pharmacy_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public static Pharmacy GetById(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<Pharmacy>(ScriptService.Scripts["pharmacy_getbyid"], new { pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		public static Pharmacy GetByIdActive(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<Pharmacy>(ScriptService.Scripts["pharmacy_getbyid_active"], new { pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		public static Pharmacy GetByIdInactive(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<Pharmacy>(ScriptService.Scripts["pharmacy_getbyid_inactive"], new { pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public static long Insert(Pharmacy pharmacy) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<long>(ScriptService.Scripts["pharmacy_insert"], pharmacy).Single();
			}
		}
		public static void InsertOrUpdate(Pharmacy pharmacy) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				db.Execute(ScriptService.Scripts["pharmacy_insert_or_update"], pharmacy);
			}
		}
		#endregion

		#region Update
		public static void Update(Pharmacy pharmacy) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				db.Execute(ScriptService.Scripts["pharmacy_update"], pharmacy);
			}
		}
		public static void UpdateActive(Pharmacy pharmacy) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				db.Execute(ScriptService.Scripts["pharmacy_update_active"], pharmacy);
			}
		}
		public static void UpdateInactive(Pharmacy pharmacy) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				db.Execute(ScriptService.Scripts["pharmacy_update_inactive"], pharmacy);
			}
		}
		#endregion
	}
}