using System.Collections.Generic;
using System.Linq;
using Dapper;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {

	/**
	 * Establishes all SQL squery methods for the named model.
	 * Handles all dapper interaction and attribute mapping.
	 */
	public static class DatabasePharmacistService {

		#region Enable/Disable Operations
		public static void Enable(long pharmacist_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["pharmacist_enable"], new { pharmacist_id = pharmacist_id });
			}
		}
		public static void Disable(long pharmacist_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["pharmacist_disable"], new { pharmacist_id = pharmacist_id });
			}
		}
		#endregion

		#region Get all
		public static List<Pharmacist> GetAll() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getall"]).AsList();
			}
		}
		public static List<Pharmacist> GetAllActive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getall_active"]).AsList();
			}
		}
		public static List<Pharmacist> GetAllInactive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by all by pharmacy id
		public static List<Pharmacist> GetAllByPharmacyId(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getallbypharmacyid"], new { pharmacy_id = pharmacy_id }).AsList();
			}
		}
		public static List<Pharmacist> GetAllByPharmacyIdActive(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getallbypharmacyid_active"], new { pharmacy_id = pharmacy_id }).AsList();
			}
		}
		public static List<Pharmacist> GetAllByPharmacyIdInactive(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getallbypharmacyid_inactive"], new { pharmacy_id = pharmacy_id }).AsList();
			}
		}
		#endregion

		#region Get by id
		public static Pharmacist GetById(long pharmacist_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyid"], new { pharmacist_id = pharmacist_id }).FirstOrDefault();
			}
		}
		public static Pharmacist GetByIdActive(long pharmacist_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyid_active"], new { pharmacist_id = pharmacist_id }).FirstOrDefault();
			}
		}
		public static Pharmacist GetByIdInactive(long pharmacist_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyid_inactive"], new { pharmacist_id = pharmacist_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by user id
		public static Pharmacist GetByUserId(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyuserid"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public static Pharmacist GetByUserIdActive(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyuserid_active"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public static Pharmacist GetByUserIdInactive(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyuserid_inactive"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public static long Insert(Pharmacist pharmacist) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<long>(ScriptService.Scripts["pharmacist_insert"], pharmacist).Single();
			}
		}
		public static void InsertOrUpdate(Pharmacist pharmacist) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				db.Execute(ScriptService.Scripts["pharmacist_insert_or_update"], pharmacist);
			}
		}
		#endregion

		#region Update
		public static void Update(Pharmacist pharmacist) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				db.Execute(ScriptService.Scripts["pharmacist_update"], pharmacist);
			}
		}
		public static void UpdateActive(Pharmacist pharmacist) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				db.Execute(ScriptService.Scripts["pharmacist_update_active"], pharmacist);
			}
		}
		public static void UpdateInactive(Pharmacist pharmacist) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				db.Execute(ScriptService.Scripts["pharmacist_update_inactive"], pharmacist);
			}
		}
		#endregion
	}
}