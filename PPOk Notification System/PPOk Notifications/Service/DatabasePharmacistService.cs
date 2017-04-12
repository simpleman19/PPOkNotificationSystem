using System.Collections.Generic;
using System.Linq;
using Dapper;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {
	public static class DatabasePharmacistService {

		#region Enable/Disable Operations
		public static void Pharmacist_Enable(long pharmacist_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["pharmacist_enable"], new { pharmacist_id = pharmacist_id });
			}
		}
		public static void Pharmacist_Disable(long pharmacist_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["pharmacist_disable"], new { pharmacist_id = pharmacist_id });
			}
		}
		#endregion

		#region Get all
		public static List<Pharmacist> GetPharmacists() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getall"]).AsList();
			}
		}
		public static List<Pharmacist> GetPharmacistsActive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getall_active"]).AsList();
			}
		}
		public static List<Pharmacist> GetPharmacistsInactive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public static Pharmacist GetPharmacistById(long pharmacist_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyid"], new { pharmacist_id = pharmacist_id }).FirstOrDefault();
			}
		}
		public static Pharmacist GetPharmacistByIdActive(long pharmacist_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyid_active"], new { pharmacist_id = pharmacist_id }).FirstOrDefault();
			}
		}
		public static Pharmacist GetPharmacistByIdInactive(long pharmacist_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyid_inactive"], new { pharmacist_id = pharmacist_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by user id
		public static Pharmacist GetPharmacistByUserId(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyuserid"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public static Pharmacist GetPharmacistByUserIdActive(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyuserid_active"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public static Pharmacist GetPharmacistByUserIdInactive(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyuserid_inactive"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public static long PharmacistInsert(Pharmacist pharmacist) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<long>(ScriptService.Scripts["pharmacist_insert"], pharmacist).Single();
			}
		}
		public static void PharmacistInsertOrUpdate(Pharmacist pharmacist) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				db.Execute(ScriptService.Scripts["pharmacist_insert_or_update"], pharmacist);
			}
		}
		#endregion

		#region Update
		public static void PharmacistUpdate(Pharmacist pharmacist) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				db.Execute(ScriptService.Scripts["pharmacist_update"], pharmacist);
			}
		}
		public static void PharmacistUpdateActive(Pharmacist pharmacist) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				db.Execute(ScriptService.Scripts["pharmacist_update_active"], pharmacist);
			}
		}
		public static void PharmacistUpdateInactive(Pharmacist pharmacist) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				db.Execute(ScriptService.Scripts["pharmacist_update_inactive"], pharmacist);
			}
		}
		#endregion
	}
}