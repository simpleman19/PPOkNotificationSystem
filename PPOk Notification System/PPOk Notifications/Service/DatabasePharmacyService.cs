using System.Collections.Generic;
using System.Linq;
using Dapper;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {
	public static class DatabasePharmacyService {

		#region Enable/Disable Operations
		public static void Pharmacy_Enable(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["pharmacy_enable"], new { pharmacy_id = pharmacy_id });
			}
		}
		public static void Pharmacy_Disable(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["pharmacy_disable"], new { pharmacy_id = pharmacy_id });
			}
		}
		#endregion

		#region Get all
		public static List<Pharmacy> GetPharmacies() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<Pharmacy>(ScriptService.Scripts["pharmacy_getall"]).AsList();
			}
		}
		public static List<Pharmacy> GetPharmaciesActive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<Pharmacy>(ScriptService.Scripts["pharmacy_getall_active"]).AsList();
			}
		}
		public static List<Pharmacy> GetPharmaciesInactive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<Pharmacy>(ScriptService.Scripts["pharmacy_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public static Pharmacy GetPharmacyById(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<Pharmacy>(ScriptService.Scripts["pharmacy_getbyid"], new { pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		public static Pharmacy GetPharmacyByIdActive(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<Pharmacy>(ScriptService.Scripts["pharmacy_getbyid_active"], new { pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		public static Pharmacy GetPharmacyByIdInactive(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<Pharmacy>(ScriptService.Scripts["pharmacy_getbyid_inactive"], new { pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public static long PharmacyInsert(Pharmacy pharmacy) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<long>(ScriptService.Scripts["pharmacy_insert"], pharmacy).Single();
			}
		}
		public static void PharmacyInsertOrUpdate(Pharmacy pharmacy) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				db.Execute(ScriptService.Scripts["pharmacy_insert_or_update"], pharmacy);
			}
		}
		#endregion

		#region Update
		public static void PharmacyUpdate(Pharmacy pharmacy) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				db.Execute(ScriptService.Scripts["pharmacy_update"], pharmacy);
			}
		}
		public static void PharmacyUpdateActive(Pharmacy pharmacy) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				db.Execute(ScriptService.Scripts["pharmacy_update_active"], pharmacy);
			}
		}
		public static void PharmacyUpdateInactive(Pharmacy pharmacy) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				db.Execute(ScriptService.Scripts["pharmacy_update_inactive"], pharmacy);
			}
		}
		#endregion
	}
}