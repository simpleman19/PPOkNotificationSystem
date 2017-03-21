using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using PPOk_Notifications.Models;

namespace PPOk_Notifications.Service {
	public class SQLService {

		/*
		 * =======================================
		 *     Primary Database Handling
		 */
		#region Connection
		private readonly string _connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=PPOK;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
		private readonly string _noDatabaseConnection = @"Data Source=.\SQLEXPRESS;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

		private IDbConnection connect() => new SqlConnection(_connectionString);

		private IDbConnection noDatabaseConnect() => new SqlConnection(_noDatabaseConnection);


		public SQLService(string connectionString) {
			_connectionString = connectionString;
		}
		#endregion


		public SQLService() { }

        /*
		 * =======================================
		 *     Database Operations
		 */

        /// commented out sql services
        /// FIXME: errors because models not set up correctly yet


        #region Database Operations
        public string Rebuild() {
			//Resets entire database with fresh empty model
			Drop();
			Create();
			return "<span style='color:green'>success</span>";
		}
		public void Drop() {
			using (var db = noDatabaseConnect()) {
				db.Execute(ScriptService.Scripts["database_drop"]);
			}
		}
		public void Create() {
			using (var db = noDatabaseConnect()) {
				ScriptService.Execute(db, "database_create");
			}
		}
		#endregion

		/*
		 * =======================================
		 *     User Operations
		 */
		#region User Operations

		#region Enable/Disable Operations
		public void User_Enable(int user_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["user_enable"], user_id);
			}
		}
		public void User_Disable(int user_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["user_disable"], new {user_id = user_id} );
			}
		}
		#endregion

		#region Get all
		public List<User> GetUsers() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getall"]).AsList();
			}
		}
		public List<User> GetUsersActive() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getall_active"]).AsList();
			}
		}
		public List<User> GetUsersInactive() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public User GetUserById(int user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyid"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public User GetUserByIdActive(int user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyid_active"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public User GetUserByIdInactive(int user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyid_inactive"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by email
		public User GetUserByEmail(string user_email) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyemail"], new { user_email = user_email }).FirstOrDefault();
			}
		}
		public User GetUserByEmailActive(string user_email) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyemail_active"], new { user_email = user_email }).FirstOrDefault();
			}
		}
		public User GetUserByEmailInactive(string user_email) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyemail_inactive"], new { user_email = user_email }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by name
		public User GetUserByName(string user_fname, string user_lname) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyname"], new { user_fname = user_fname, user_lname = user_lname }).FirstOrDefault();
			}
		}
		public User GetUserByNameActive(string user_fname, string user_lname) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyname_active"], new { user_fname = user_fname, user_lname = user_lname }).FirstOrDefault();
			}
		}
		public User GetUserByNameInactive(string user_fname, string user_lname) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyname_inactive"], new { user_fname = user_fname, user_lname = user_lname }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public void UserInsert(User user) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["user_insert"], user);
			}
		}
		public void UserInsertOrUpdate(User user) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["user_insert_or_update"], user);
			}
		}
		#endregion

		#region Update
		public void UserUpdate(User user) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["user_update"], user);
			}
		}
		public void UserUpdateActive(User user) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["user_update_active"], user);
			}
		}
		public void UserUpdateInactive(User user) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["user_update_inactive"], user);
			}
		}
		#endregion

		#endregion

		/*
		 * =======================================
		 *     Login Operations
		 */
		#region Login Operations

		#region Enable/Disable Operations
		public void Login_Enable(int login_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["login_enable"], new { login_id = login_id });
			}
		}
		public void Login_Disable(int login_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["login_disable"], new { login_id = login_id });
			}
		}
		#endregion

		#region Get all
		public List<Login> GetLogins() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getall"]).AsList();
			}
		}
		public List<Login> GetLoginsActive() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getall_active"]).AsList();
			}
		}
		public List<Login> GetLoginsInactive() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public Login GetLoginById(int login_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyid"], new { login_id = login_id }).FirstOrDefault();
			}
		}
		public Login GetLoginByIdActive(int login_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyid_active"], new { login_id = login_id }).FirstOrDefault();
			}
		}
		public Login GetLoginByIdInactive(int login_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyid_inactive"], new { login_id = login_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by user id
		public Login GetLoginByUserId(int user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyuserid"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public Login GetLoginByUserIdActive(int user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyuserid_active"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public Login GetLoginByUserIdInactive(int user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyuserid_inactive"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public void LoginInsert(Login login) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["login_insert"], login);
			}
		}
		public void LoginInsertOrUpdate(Login login) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["login_insert_or_update"], login);
			}
		}
		#endregion

		#region Update
		public void LoginUpdate(Login login) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["login_update"], login);
			}
		}
		public void LoginUpdateActive(Login login) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["login_update_active"], login);
			}
		}
		public void LoginUpdateInactive(Login login) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["login_update_inactive"], login);
			}
		}
		#endregion

		#endregion

		/*
		 * =======================================
		 *     Pharmacy Operations
		 */
		#region Pharmacy Operations

		#region Enable/Disable Operations
		public void Pharmacy_Enable(int pharmacy_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["pharmacy_enable"], new { pharmacy_id = pharmacy_id });
			}
		}
		public void Pharmacy_Disable(int pharmacy_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["pharmacy_disable"], new { pharmacy_id = pharmacy_id });
			}
		}
		#endregion

		#region Get all
		public List<Pharmacy> GetPharmacies() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<Pharmacy>(ScriptService.Scripts["pharmacy_getall"]).AsList();
			}
		}
		public List<Pharmacy> GetPharmaciesActive() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<Pharmacy>(ScriptService.Scripts["pharmacy_getall_active"]).AsList();
			}
		}
		public List<Pharmacy> GetPharmaciesInactive() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<Pharmacy>(ScriptService.Scripts["pharmacy_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public Pharmacy GetPharmacyById(int pharmacy_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<Pharmacy>(ScriptService.Scripts["pharmacy_getbyid"], new { pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		public Pharmacy GetPharmacyByIdActive(int pharmacy_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<Pharmacy>(ScriptService.Scripts["pharmacy_getbyid_active"], new { pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		public Pharmacy GetPharmacyByIdInactive(int pharmacy_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<Pharmacy>(ScriptService.Scripts["pharmacy_getbyid_inactive"], new { pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public void PharmacyInsert(Pharmacy pharmacy) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["pharmacy_insert"], pharmacy);
			}
		}
		public void PharmacyInsertOrUpdate(Pharmacy pharmacy) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["pharmacy_insert_or_update"], pharmacy);
			}
		}
		#endregion

		#region Update
		public void PharmacyUpdate(Pharmacy pharmacy) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["pharmacy_update"], pharmacy);
			}
		}
		public void PharmacyUpdateActive(Pharmacy pharmacy) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["pharmacy_update_active"], pharmacy);
			}
		}
		public void PharmacyUpdateInactive(Pharmacy pharmacy) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["pharmacy_update_inactive"], pharmacy);
			}
		}
		#endregion

		#endregion

		/*
		 * =======================================
		 *     Pharmacist Operations
		 */
		#region Pharmacist Operations

		#region Enable/Disable Operations
		public void Pharmacist_Enable(int pharmacist_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["pharmacist_enable"], new { pharmacist_id = pharmacist_id });
			}
		}
		public void Pharmacist_Disable(int pharmacist_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["pharmacist_disable"], new { pharmacist_id = pharmacist_id });
			}
		}
		#endregion

		#region Get all
		public List<Pharmacist> GetPharmacists() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getall"]).AsList();
			}
		}
		public List<Pharmacist> GetPharmacistsActive() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getall_active"]).AsList();
			}
		}
		public List<Pharmacist> GetPharmacistsInactive() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public Pharmacist GetPharmacistById(int pharmacist_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyid"], new { pharmacist_id = pharmacist_id }).FirstOrDefault();
			}
		}
		public Pharmacist GetPharmacistByIdActive(int pharmacist_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyid_active"], new { pharmacist_id = pharmacist_id }).FirstOrDefault();
			}
		}
		public Pharmacist GetPharmacistByIdInactive(int pharmacist_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyid_inactive"], new { pharmacist_id = pharmacist_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by user id
		public Pharmacist GetPharmacistByUserId(int user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyuserid"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public Pharmacist GetPharmacistByUserIdActive(int user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyuserid_active"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public Pharmacist GetPharmacistByUserIdInactive(int user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyuserid_inactive"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public void PharmacistInsert(Pharmacist pharmacist) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["pharmacist_insert"], pharmacist);
			}
		}
		public void PharmacistInsertOrUpdate(Pharmacist pharmacist) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["pharmacist_insert_or_update"], pharmacist);
			}
		}
		#endregion

		#region Update
		public void PharmacistUpdate(Pharmacist pharmacist) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["pharmacist_update"], pharmacist);
			}
		}
		public void PharmacistUpdateActive(Pharmacist pharmacist) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["pharmacist_update_active"], pharmacist);
			}
		}
		public void PharmacistUpdateInactive(Pharmacist pharmacist) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["pharmacist_update_inactive"], pharmacist);
			}
		}
		#endregion

		#endregion

		/*
		 * =======================================
		 *     Template Operations
		 */
		#region Template Operations

		#region Enable/Disable Operations
		public void Template_Enable(int template_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["template_enable"], new { template_id = template_id });
			}
		}
		public void Template_Disable(int template_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["template_disable"], new { template_id = template_id });
			}
		}
		#endregion

		#region Get all
		public List<Template> GetTemplates() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getall"]).AsList();
			}
		}
		public List<Template> GetTemplatesActive() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getall_active"]).AsList();
			}
		}
		public List<Template> GetTemplatesInactive() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public Template GetTemplateById(int template_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getbyid"], new { template_id = template_id }).FirstOrDefault();
			}
		}
		public Template GetTemplateByIdActive(int template_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getbyid_active"], new { template_id = template_id }).FirstOrDefault();
			}
		}
		public Template GetTemplateByIdInactive(int template_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getbyid_inactive"], new { template_id = template_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by pharmacy id
		public Template GetTemplateByUserId(int pharmacy_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getbypharmacyid"], new { pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		public Template GetTemplateByUserIdActive(int pharmacy_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getbypharmacyid_active"], new { pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		public Template GetTemplateByUserIdInactive(int pharmacy_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getbypharmacyid_inactive"], new { pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public void TemplateInsert(Template template) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["template_insert"], template);
			}
		}
		public void TemplateInsertOrUpdate(Template template) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["template_insert_or_update"], template);
			}
		}
		#endregion

		#region Update
		public void TemplateUpdate(Template template) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["template_update"], template);
			}
		}
		public void TemplateUpdateActive(Template template) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["template_update_active"], template);
			}
		}
		public void TemplateUpdateInactive(Template template) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["template_update_inactive"], template);
			}
		}
		#endregion

		#endregion

		/*
		 * =======================================
		 *     Patient Operations
		 */
		#region Patient Operations

		#region Enable/Disable Operations
		public void Patient_Enable(int patient_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["patient_enable"], new { patient_id = patient_id });
			}
		}
		public void Patient_Disable(int patient_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["patient_disable"], new { patient_id = patient_id });
			}
		}
		#endregion

		#region Get all
		public List<Patient> GetPatients() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getall"]).AsList();
			}
		}
		public List<Patient> GetPatientsActive() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getall_active"]).AsList();
			}
		}
		public List<Patient> GetPatientsInactive() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public Patient GetPatientById(int patient_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyid"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		public Patient GetPatientByIdActive(int patient_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyid_active"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		public Patient GetPatientByIdInactive(int patient_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyid_inactive"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by user id
		public Patient GetPatientByUserId(int user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyuserid"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public Patient GetPatientByUserIdActive(int user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyuserid_active"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public Patient GetPatientByUserIdInactive(int user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyuserid_inactive"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public void PatientInsert(Patient patient) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["patient_insert"], patient);
			}
		}
		public void PatientInsertOrUpdate(Patient patient) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["patient_insert_or_update"], patient);
			}
		}
		#endregion

		#region Update
		public void PatientUpdate(Patient patient) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["patient_update"], patient);
			}
		}
		public void PatientUpdateActive(Patient patient) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["patient_update_active"], patient);
			}
		}
		public void PatientUpdateInactive(Patient patient) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["patient_update_inactive"], patient);
			}
		}
		#endregion

		#endregion

		/*
		 * =======================================
		 *     Prescription Operations
		 */
		#region Prescription Operations

		#endregion

		/*
		 * =======================================
		 *     Refill Operations
		 */
		#region Refill Operations

		#endregion

		/*
		 * =======================================
		 *     Notification Operations
		 */
		#region Notification Operations

		#endregion

		/*
		 * =======================================
		 *     OTP Operations
		 */
		#region OTP Operations

		#endregion

		/*
		 * =======================================
		 *     EmailOTP Operations
		 */
		#region EmailOTP Operations

		#endregion

	}
}