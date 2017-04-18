using System.Collections.Generic;
using System.Linq;
using Dapper;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {

	/**
	 * Establishes all SQL squery methods for the named model.
	 * Handles all dapper interaction and attribute mapping.
	 */
	public static class DatabasePatientService {

		#region Enable/Disable Operations
		public static void Enable(long patient_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["patient_enable"], new { patient_id = patient_id });
			}
		}
		public static void Disable(long patient_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["patient_disable"], new { patient_id = patient_id });
			}
		}
		#endregion

		#region Get all
		public static List<Patient> GetAll() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getall"]).AsList();
			}
		}
		public static List<Patient> GetAll(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getallbypharmacyid"], new { pharmacy_id = pharmacy_id }).AsList();
			}
		}
		public static List<Patient> GetAllActive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getall_active"]).AsList();
			}
		}
		public static List<Patient> GetAllActive(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getallbypharmacyid_active"], new { pharmacy_id = pharmacy_id }).AsList();
			}
		}
		public static List<Patient> GetAllInactive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getall_inactive"]).AsList();
			}
		}
		public static List<Patient> GetAllInactive(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getallbypharmacyid_inactive"], new { pharmacy_id = pharmacy_id }).AsList();
			}
		}
		#endregion

		#region Get by id
		public static Patient GetById(long patient_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyid"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		public static Patient GettByIdActive(long patient_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyid_active"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		public static Patient GetByIdInactive(long patient_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyid_inactive"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by user id
		public static Patient GetByUserId(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyuserid"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public static Patient GetByUserIdActive(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyuserid_active"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public static Patient GetByUserIdInactive(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyuserid_inactive"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by person code
		public static Patient GetByPersonCode(string person_code, long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbypersoncode"], new { person_code = person_code, pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		public static Patient GetByUserPersonCodeActive(string person_code, long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbypersoncode_active"], new { person_code = person_code, pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		public static Patient GetByUserPersonCodeInactive(string person_code, long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbypersoncode_inactive"], new { person_code = person_code, pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public static long Insert(Patient patient) {
			using (var db = DatabaseService.Connection) {
				Patient.PatientDictInvalid = true;
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<long>(ScriptService.Scripts["patient_insert"], patient).Single();
			}
		}

		public static void InsertOrUpdate(Patient patient) {
			using (var db = DatabaseService.Connection) {
				Patient.PatientDictInvalid = true;
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				db.Execute(ScriptService.Scripts["patient_insert_or_update"], patient);
			}
		}
		#endregion

		#region Update
		public static void Update(Patient patient) {
			using (var db = DatabaseService.Connection) {
				Patient.PatientDictInvalid = true;
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				db.Execute(ScriptService.Scripts["patient_update"], patient);
			}
		}
		public static void UpdateActive(Patient patient) {
			using (var db = DatabaseService.Connection) {
				Patient.PatientDictInvalid = true;
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				db.Execute(ScriptService.Scripts["patient_update_active"], patient);
			}
		}
		public static void UpdateInactive(Patient patient) {
			using (var db = DatabaseService.Connection) {
				Patient.PatientDictInvalid = true;
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				db.Execute(ScriptService.Scripts["patient_update_inactive"], patient);
			}
		}
		#endregion
	}
}