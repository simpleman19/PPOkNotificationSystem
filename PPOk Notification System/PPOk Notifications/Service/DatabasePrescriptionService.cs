using System.Collections.Generic;
using System.Linq;
using Dapper;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {
	public static class DatabasePrescriptionService {

		#region Enable/Disable Operations
		public static void Prescription_Enable(long prescription_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["prescription_enable"], new { prescription_id = prescription_id });
			}
		}
		public static void Prescription_Disable(long prescription_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["prescription_disable"], new { prescription_id = prescription_id });
			}
		}
		#endregion

		#region Get all
		public static List<Prescription> GetPrescriptions() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getall"]).AsList();
			}
		}
		public static List<Prescription> GetPrescriptionsActive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getall_active"]).AsList();
			}
		}
		public static List<Prescription> GetPrescriptionsInactive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public static Prescription GetPrescriptionById(long prescription_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getbyid"], new { prescription_id = prescription_id }).FirstOrDefault();
			}
		}
		public static Prescription GetPrescriptionByIdActive(long prescription_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getbyid_active"], new { prescription_id = prescription_id }).FirstOrDefault();
			}
		}
		public static Prescription GetPrescriptionByIdInactive(long prescription_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getbyid_inactive"], new { prescription_id = prescription_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by patient id
		public static Prescription GetPrescriptionByPatientId(long patient_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getbypatientid"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		public static Prescription GetPrescriptionByPatientIdActive(long patient_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getbypatientid_active"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		public static Prescription GetPrescriptionByPatientIdInactive(long patient_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getbypatientid_inactive"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public static long PrescriptionInsert(Prescription prescription) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<long>(ScriptService.Scripts["prescription_insert"], prescription).Single();
			}
		}
		public static void PrescriptionInsertOrUpdate(Prescription prescription) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				db.Execute(ScriptService.Scripts["prescription_insert_or_update"], prescription);
			}
		}
		#endregion

		#region Update
		public static void PrescriptionUpdate(Prescription prescription) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				db.Execute(ScriptService.Scripts["prescription_update"], prescription);
			}
		}
		public static void PrescriptionUpdateActive(Prescription prescription) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				db.Execute(ScriptService.Scripts["prescription_update_active"], prescription);
			}
		}
		public static void PrescriptionUpdateInactive(Prescription prescription) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				db.Execute(ScriptService.Scripts["prescription_update_inactive"], prescription);
			}
		}
		#endregion
	}
}