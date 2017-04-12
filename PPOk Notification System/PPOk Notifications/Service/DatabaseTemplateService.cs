using System.Collections.Generic;
using System.Linq;
using Dapper;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {
	public static class DatabaseTemplateService {

		#region Enable/Disable Operations
		public static void Template_Enable(long template_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["template_enable"], new { template_id = template_id });
			}
		}
		public static void Template_Disable(long template_id) {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["template_disable"], new { template_id = template_id });
			}
		}
		#endregion

		#region Get all
		public static List<Template> GetTemplates() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getall"]).AsList();
			}
		}
		public static List<Template> GetTemplatesActive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getall_active"]).AsList();
			}
		}
		public static List<Template> GetTemplatesInactive() {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public static Template GetTemplateById(long template_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getbyid"], new { template_id = template_id }).FirstOrDefault();
			}
		}
		public static Template GetTemplateByIdActive(long template_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getbyid_active"], new { template_id = template_id }).FirstOrDefault();
			}
		}
		public static Template GetTemplateByIdInactive(long template_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getbyid_inactive"], new { template_id = template_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by pharmacy id
		public static Template GetTemplateByUserId(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getbypharmacyid"], new { pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		public static Template GetTemplateByUserIdActive(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getbypharmacyid_active"], new { pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		public static Template GetTemplateByUserIdInactive(long pharmacy_id) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getbypharmacyid_inactive"], new { pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public static long TemplateInsert(Template template) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<long>(ScriptService.Scripts["template_insert"], template).Single();
			}
		}
		public static void TemplateInsertOrUpdate(Template template) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				db.Execute(ScriptService.Scripts["template_insert_or_update"], template);
			}
		}
		#endregion

		#region Update
		public static void TemplateUpdate(Template template) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				db.Execute(ScriptService.Scripts["template_update"], template);
			}
		}
		public static void TemplateUpdateActive(Template template) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				db.Execute(ScriptService.Scripts["template_update_active"], template);
			}
		}
		public static void TemplateUpdateInactive(Template template) {
			using (var db = DatabaseService.Connection) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				db.Execute(ScriptService.Scripts["template_update_inactive"], template);
			}
		}
		#endregion
	}
}