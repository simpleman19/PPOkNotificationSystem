using System.Collections.Generic;
using System.Linq;
using Dapper;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {
	public static class DatabasePatientService {

		#region Enable/Disable Operations
		public static void Patient_Enable(long patient_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["patient_enable"], new { patient_id = patient_id });
			}
		}
		public static void Patient_Disable(long patient_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["patient_disable"], new { patient_id = patient_id });
			}
		}
		#endregion

		#region Get all
		public static List<Patient> GetPatients() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getall"]).AsList();
			}
		}
		public static List<Patient> GetPatientsActive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getall_active"]).AsList();
			}
		}
		public static List<Patient> GetPatientsInactive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public static Patient GetPatientById(long patient_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyid"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		public static Patient GetPatientByIdActive(long patient_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyid_active"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		public static Patient GetPatientByIdInactive(long patient_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyid_inactive"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by user id
		public static Patient GetPatientByUserId(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyuserid"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public static Patient GetPatientByUserIdActive(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyuserid_active"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public static Patient GetPatientByUserIdInactive(long user_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyuserid_inactive"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by person code
		public static Patient GetPatientByPersonCode(string person_code) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbypersoncode"], new { person_code = person_code }).FirstOrDefault();
			}
		}
		public static Patient GetPatientByUserPersonCodeActive(string person_code) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbypersoncode_active"], new { person_code = person_code }).FirstOrDefault();
			}
		}
		public static Patient GetPatientByUserPersonCodeInactive(string person_code) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbypersoncode_inactive"], new { person_code = person_code }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public static long PatientInsert(Patient patient) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<long>(ScriptService.Scripts["patient_insert"], patient).Single();
			}
		}

		public static void PatientInsertOrUpdate(Patient patient) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				db.Execute(ScriptService.Scripts["patient_insert_or_update"], patient);
			}
		}
		#endregion

		#region Update
		public static void PatientUpdate(Patient patient) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				db.Execute(ScriptService.Scripts["patient_update"], patient);
			}
		}
		public static void PatientUpdateActive(Patient patient) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				db.Execute(ScriptService.Scripts["patient_update_active"], patient);
			}
		}
		public static void PatientUpdateInactive(Patient patient) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				db.Execute(ScriptService.Scripts["patient_update_inactive"], patient);
			}
		}
		#endregion
	}
}