using System.Collections.Generic;
using System.Linq;
using Dapper;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {

	/**
	 * Establishes all SQL squery methods for the named model.
	 * Handles all dapper interaction and attribute mapping.
	 */
	public static class DatabasePrescriptionService {

		#region Enable/Disable Operations
		public static void PEnable(long prescription_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["prescription_enable"], new { prescription_id = prescription_id });
			}
		}
		public static void Disable(long prescription_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["prescription_disable"], new { prescription_id = prescription_id });
			}
		}
		#endregion

		#region Get all
		public static List<Prescription> GetAll() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getall"]).AsList();
			}
		}
		public static List<Prescription> GetAllActive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getall_active"]).AsList();
			}
		}
		public static List<Prescription> GetAllInactive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getall_inactive"]).AsList();
			}
		}
		public static Dictionary<Refill,Prescription> GetAllWithRefill(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
                var dict = new Dictionary<Refill, Prescription>();
				db.Query<Refill, Prescription, Prescription>(ScriptService.Scripts["prescription_getallwithrefills"],
					(r, p) => {
						dict.Add(r, p);
					    r.PrescriptionId = p.PrescriptionId;
						return p;
					},
				    new { pharmacy_id = pharmacy_id },
                    splitOn: "refill_id, prescription_id"
                );
			    return dict;
			}
		}
		#endregion

		#region Get by id
		public static Prescription GetById(long prescription_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getbyid"], new { prescription_id = prescription_id }).FirstOrDefault();
			}
		}
		public static Prescription GetByIdActive(long prescription_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getbyid_active"], new { prescription_id = prescription_id }).FirstOrDefault();
			}
		}
		public static Prescription GetByIdInactive(long prescription_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getbyid_inactive"], new { prescription_id = prescription_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by patient id
		public static Prescription GetByPatientId(long patient_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getbypatientid"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		public static Prescription GetByPatientIdActive(long patient_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getbypatientid_active"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		public static Prescription GetByPatientIdInactive(long patient_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getbypatientid_inactive"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public static long Insert(Prescription prescription) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<long>(ScriptService.Scripts["prescription_insert"], prescription).Single();
			}
		}
		public static void InsertOrUpdate(Prescription prescription) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				db.Execute(ScriptService.Scripts["prescription_insert_or_update"], prescription);
			}
		}
		#endregion

		#region Update
		public static void Update(Prescription prescription) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				db.Execute(ScriptService.Scripts["prescription_update"], prescription);
			}
		}
		public static void UpdateActive(Prescription prescription) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				db.Execute(ScriptService.Scripts["prescription_update_active"], prescription);
			}
		}
		public static void UpdateInactive(Prescription prescription) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				db.Execute(ScriptService.Scripts["prescription_update_inactive"], prescription);
			}
		}
		#endregion
	}
}