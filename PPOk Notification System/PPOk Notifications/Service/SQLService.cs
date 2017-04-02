using System.Collections.Generic;
using System.Configuration;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
		public void User_Enable(long  user_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["user_enable"], user_id);
			}
		}
		public void User_Disable(long  user_id) {
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
		public User GetUserById(long user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyid"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public User GetUserByIdActive(long user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyid_active"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public User GetUserByIdInactive(long user_id) {
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

		#region Get by phone number
		public User GetUserByPhone(string user_phone) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyphone"], new { user_phone = user_phone }).FirstOrDefault();
			}
		}
		public User GetUserByPhoneActive(string user_phone) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyphone_active"], new { user_phone = user_phone }).FirstOrDefault();
			}
		}
		public User GetUserByPhoneInactive(string user_phone) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<User>(ScriptService.Scripts["user_getbyphone_inactive"], new { user_phone = user_phone }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public long UserInsert(User user) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				return db.Query<long>(ScriptService.Scripts["user_insert"], user).Single();
			}
		}
		public void UserInsertOrUpdate(User user) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				db.Execute(ScriptService.Scripts["user_insert_or_update"], user);
			}
		}
		#endregion

		#region Update
		public void UserUpdate(User user) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				db.Execute(ScriptService.Scripts["user_update"], user);
			}
		}
		public void UserUpdateActive(User user) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
				db.Execute(ScriptService.Scripts["user_update_active"], user);
			}
		}
		public void UserUpdateInactive(User user) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(User), new ColumnAttributeTypeMapper<User>());
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
		public void Login_Enable(long  login_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["login_enable"], new { login_id = login_id });
			}
		}
		public void Login_Disable(long  login_id) {
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
		public Login GetLoginById(long login_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyid"], new { login_id = login_id }).FirstOrDefault();
			}
		}
		public Login GetLoginByIdActive(long login_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyid_active"], new { login_id = login_id }).FirstOrDefault();
			}
		}
		public Login GetLoginByIdInactive(long login_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyid_inactive"], new { login_id = login_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by user id
		public Login GetLoginByUserId(long  user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyuserid"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public Login GetLoginByUserIdActive(long  user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyuserid_active"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public Login GetLoginByUserIdInactive(long  user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<Login>(ScriptService.Scripts["login_getbyuserid_inactive"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public long LoginInsert(Login login) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				return db.Query<long>(ScriptService.Scripts["login_insert"], login).Single();
			}
		}
		public void LoginInsertOrUpdate(Login login) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				db.Execute(ScriptService.Scripts["login_insert_or_update"], login);
			}
		}
		#endregion

		#region Update
		public void LoginUpdate(Login login) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				db.Execute(ScriptService.Scripts["login_update"], login);
			}
		}
		public void LoginUpdateActive(Login login) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
				db.Execute(ScriptService.Scripts["login_update_active"], login);
			}
		}
		public void LoginUpdateInactive(Login login) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Login), new ColumnAttributeTypeMapper<Login>());
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
		public void Pharmacy_Enable(long  pharmacy_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["pharmacy_enable"], new { pharmacy_id = pharmacy_id });
			}
		}
		public void Pharmacy_Disable(long  pharmacy_id) {
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
		public Pharmacy GetPharmacyById(long  pharmacy_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<Pharmacy>(ScriptService.Scripts["pharmacy_getbyid"], new { pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		public Pharmacy GetPharmacyByIdActive(long  pharmacy_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<Pharmacy>(ScriptService.Scripts["pharmacy_getbyid_active"], new { pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		public Pharmacy GetPharmacyByIdInactive(long  pharmacy_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<Pharmacy>(ScriptService.Scripts["pharmacy_getbyid_inactive"], new { pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public long PharmacyInsert(Pharmacy pharmacy) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				return db.Query<long>(ScriptService.Scripts["pharmacy_insert"], pharmacy).Single();
			}
		}
		public void PharmacyInsertOrUpdate(Pharmacy pharmacy) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				db.Execute(ScriptService.Scripts["pharmacy_insert_or_update"], pharmacy);
			}
		}
		#endregion

		#region Update
		public void PharmacyUpdate(Pharmacy pharmacy) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				db.Execute(ScriptService.Scripts["pharmacy_update"], pharmacy);
			}
		}
		public void PharmacyUpdateActive(Pharmacy pharmacy) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
				db.Execute(ScriptService.Scripts["pharmacy_update_active"], pharmacy);
			}
		}
		public void PharmacyUpdateInactive(Pharmacy pharmacy) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacy), new ColumnAttributeTypeMapper<Pharmacy>());
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
		public void Pharmacist_Enable(long  pharmacist_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["pharmacist_enable"], new { pharmacist_id = pharmacist_id });
			}
		}
		public void Pharmacist_Disable(long  pharmacist_id) {
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
		public Pharmacist GetPharmacistById(long  pharmacist_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyid"], new { pharmacist_id = pharmacist_id }).FirstOrDefault();
			}
		}
		public Pharmacist GetPharmacistByIdActive(long  pharmacist_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyid_active"], new { pharmacist_id = pharmacist_id }).FirstOrDefault();
			}
		}
		public Pharmacist GetPharmacistByIdInactive(long  pharmacist_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyid_inactive"], new { pharmacist_id = pharmacist_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by user id
		public Pharmacist GetPharmacistByUserId(long  user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyuserid"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public Pharmacist GetPharmacistByUserIdActive(long  user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyuserid_active"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public Pharmacist GetPharmacistByUserIdInactive(long  user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<Pharmacist>(ScriptService.Scripts["pharmacist_getbyuserid_inactive"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public long PharmacistInsert(Pharmacist pharmacist) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				return db.Query<long>(ScriptService.Scripts["pharmacist_insert"], pharmacist).Single();
			}
		}
		public void PharmacistInsertOrUpdate(Pharmacist pharmacist) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				db.Execute(ScriptService.Scripts["pharmacist_insert_or_update"], pharmacist);
			}
		}
		#endregion

		#region Update
		public void PharmacistUpdate(Pharmacist pharmacist) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				db.Execute(ScriptService.Scripts["pharmacist_update"], pharmacist);
			}
		}
		public void PharmacistUpdateActive(Pharmacist pharmacist) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
				db.Execute(ScriptService.Scripts["pharmacist_update_active"], pharmacist);
			}
		}
		public void PharmacistUpdateInactive(Pharmacist pharmacist) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Pharmacist), new ColumnAttributeTypeMapper<Pharmacist>());
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
		public void Template_Enable(long  template_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["template_enable"], new { template_id = template_id });
			}
		}
		public void Template_Disable(long  template_id) {
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
		public Template GetTemplateById(long  template_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getbyid"], new { template_id = template_id }).FirstOrDefault();
			}
		}
		public Template GetTemplateByIdActive(long  template_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getbyid_active"], new { template_id = template_id }).FirstOrDefault();
			}
		}
		public Template GetTemplateByIdInactive(long  template_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getbyid_inactive"], new { template_id = template_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by pharmacy id
		public Template GetTemplateByUserId(long  pharmacy_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getbypharmacyid"], new { pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		public Template GetTemplateByUserIdActive(long  pharmacy_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getbypharmacyid_active"], new { pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		public Template GetTemplateByUserIdInactive(long  pharmacy_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<Template>(ScriptService.Scripts["template_getbypharmacyid_inactive"], new { pharmacy_id = pharmacy_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public long TemplateInsert(Template template) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				return db.Query<long>(ScriptService.Scripts["template_insert"], template).Single();
			}
		}
		public void TemplateInsertOrUpdate(Template template) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				db.Execute(ScriptService.Scripts["template_insert_or_update"], template);
			}
		}
		#endregion

		#region Update
		public void TemplateUpdate(Template template) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				db.Execute(ScriptService.Scripts["template_update"], template);
			}
		}
		public void TemplateUpdateActive(Template template) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
				db.Execute(ScriptService.Scripts["template_update_active"], template);
			}
		}
		public void TemplateUpdateInactive(Template template) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Template), new ColumnAttributeTypeMapper<Template>());
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
		public void Patient_Enable(long  patient_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["patient_enable"], new { patient_id = patient_id });
			}
		}
		public void Patient_Disable(long  patient_id) {
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
		public Patient GetPatientById(long patient_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyid"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		public Patient GetPatientByIdActive(long patient_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyid_active"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		public Patient GetPatientByIdInactive(long  patient_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyid_inactive"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by user id
		public Patient GetPatientByUserId(long  user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyuserid"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public Patient GetPatientByUserIdActive(long  user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyuserid_active"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public Patient GetPatientByUserIdInactive(long  user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<Patient>(ScriptService.Scripts["patient_getbyuserid_inactive"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public long PatientInsert(Patient patient) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				return db.Query<long>(ScriptService.Scripts["patient_insert"], patient).Single();
			}
		}

		public void PatientInsertOrUpdate(Patient patient) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				db.Execute(ScriptService.Scripts["patient_insert_or_update"], patient);
			}
		}
		#endregion

		#region Update
		public void PatientUpdate(Patient patient) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				db.Execute(ScriptService.Scripts["patient_update"], patient);
			}
		}
		public void PatientUpdateActive(Patient patient) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
				db.Execute(ScriptService.Scripts["patient_update_active"], patient);
			}
		}
		public void PatientUpdateInactive(Patient patient) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Patient), new ColumnAttributeTypeMapper<Patient>());
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

		#region Enable/Disable Operations
		public void Prescription_Enable(long  prescription_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["prescription_enable"], new { prescription_id = prescription_id });
			}
		}
		public void Prescription_Disable(long  prescription_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["prescription_disable"], new { prescription_id = prescription_id });
			}
		}
		#endregion

		#region Get all
		public List<Prescription> GetPrescriptions() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getall"]).AsList();
			}
		}
		public List<Prescription> GetPrescriptionsActive() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getall_active"]).AsList();
			}
		}
		public List<Prescription> GetPrescriptionsInactive() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public Prescription GetPrescriptionById(long prescription_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getbyid"], new { prescription_id = prescription_id }).FirstOrDefault();
			}
		}
		public Prescription GetPrescriptionByIdActive(long prescription_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getbyid_active"], new { prescription_id = prescription_id }).FirstOrDefault();
			}
		}
		public Prescription GetPrescriptionByIdInactive(long prescription_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getbyid_inactive"], new { prescription_id = prescription_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by patient id
		public Prescription GetPrescriptionByPatientId(long  patient_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getbypatientid"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		public Prescription GetPrescriptionByPatientIdActive(long  patient_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getbypatientid_active"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		public Prescription GetPrescriptionByPatientIdInactive(long  patient_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<Prescription>(ScriptService.Scripts["prescription_getbypatientid_inactive"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public long PrescriptionInsert(Prescription prescription) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				return db.Query<long>(ScriptService.Scripts["prescription_insert"], prescription).Single();
			}
		}
		public void PrescriptionInsertOrUpdate(Prescription prescription) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				db.Execute(ScriptService.Scripts["prescription_insert_or_update"], prescription);
			}
		}
		#endregion

		#region Update
		public void PrescriptionUpdate(Prescription prescription) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				db.Execute(ScriptService.Scripts["prescription_update"], prescription);
			}
		}
		public void PrescriptionUpdateActive(Prescription prescription) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				db.Execute(ScriptService.Scripts["prescription_update_active"], prescription);
			}
		}
		public void PrescriptionUpdateInactive(Prescription prescription) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Prescription), new ColumnAttributeTypeMapper<Prescription>());
				db.Execute(ScriptService.Scripts["prescription_update_inactive"], prescription);
			}
		}
		#endregion

		#endregion

		/*
		 * =======================================
		 *     Refill Operations
		 */
		#region Refill Operations

		#region Enable/Disable Operations
		public void Refill_Enable(long refill_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["refill_enable"], new { refill_id = refill_id });
			}
		}
		public void Refill_Disable(long  refill_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["refill_disable"], new { refill_id = refill_id });
			}
		}
		#endregion

		#region Get all
		public List<Refill> GetRefills() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getall"]).AsList();
			}
		}
		public List<Refill> GetRefillsActive() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getall_active"]).AsList();
			}
		}
		public List<Refill> GetRefillsInactive() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public Refill GetRefillById(long  refill_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getbyid"], new { refill_id = refill_id }).FirstOrDefault();
			}
		}
		public Refill GetRefillByIdActive(long  refill_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getbyid_active"], new { refill_id = refill_id }).FirstOrDefault();
			}
		}
		public Refill GetRefillByIdInactive(long  refill_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getbyid_inactive"], new { refill_id = refill_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by prescription id
		public Refill GetRefillByPrescriptionId(long  prescription_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getbyprescriptionid"], new { prescription_id = prescription_id }).FirstOrDefault();
			}
		}
		public Refill GetRefillByPrescriptionIdActive(long  prescription_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getbyprescriptionid_active"], new { prescription_id = prescription_id }).FirstOrDefault();
			}
		}
		public Refill GetRefillByPrescriptionIdInactive(long  prescription_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<Refill>(ScriptService.Scripts["refill_getbyprescriptionid_inactive"], new { prescription_id = prescription_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public long RefillInsert(Refill refill) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				return db.Query<long>(ScriptService.Scripts["refill_insert"], refill).Single();
			}
		}
		public void RefillInsertOrUpdate(Refill refill) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				db.Execute(ScriptService.Scripts["refill_insert_or_update"], refill);
			}
		}
		#endregion

		#region Update
		public void RefillUpdate(Refill refill) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				db.Execute(ScriptService.Scripts["refill_update"], refill);
			}
		}
		public void RefillUpdateActive(Refill refill) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				db.Execute(ScriptService.Scripts["refill_update_active"], refill);
			}
		}
		public void RefillUpdateInactive(Refill refill) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Refill), new ColumnAttributeTypeMapper<Refill>());
				db.Execute(ScriptService.Scripts["refill_update_inactive"], refill);
			}
		}
		#endregion

		#endregion

		/*
		 * =======================================
		 *     Notification Operations
		 */
		#region Notification Operations

		#region Enable/Disable Operations
		public void Notification_Enable(long notification_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["notification_enable"], new { notification_id = notification_id });
			}
		}
		public void Notification_Disable(long notification_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["notification_disable"], new { notification_id = notification_id });
			}
		}
		#endregion

		#region Get all
		public List<Notification> GetNotifications() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getall"]).AsList();
			}
		}
		public List<Notification> GetNotificationsActive() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getall_active"]).AsList();
			}
		}
		public List<Notification> GetNotificationsInactive() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getall_inactive"]).AsList();
			}
		}
		public List<Notification> GetNotificationsToday() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getall_today"]).AsList();
			}
		}
		public List<Notification> GetNotificationsFutureDate() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getall_future_date"]).AsList();
			}
		}
		public List<Notification> GetNotificationsFutureTime() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getall_future_time"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public Notification GetNotificationById(long notification_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getbyid"], new { notification_id = notification_id }).FirstOrDefault();
			}
		}
		public Notification GetNotificationByIdActive(long notification_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getbyid_active"], new { notification_id = notification_id }).FirstOrDefault();
			}
		}
		public Notification GetNotificationByIdInactive(long  notification_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getbyid_inactive"], new { notification_id = notification_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by patient id
		public Notification GetNotificationByPatientId(long patient_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getbypatientid"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		public Notification GetNotificationByPatientIdActive(long patient_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getbypatientid_active"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		public Notification GetNotificationByPatientIdInactive(long patient_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<Notification>(ScriptService.Scripts["notification_getbypatientid_inactive"], new { patient_id = patient_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public long NotificationInsert(Notification notification) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				return db.Query<long>(ScriptService.Scripts["notification_insert"], notification).Single();
			}
		}

		public void NotificationInsertOrUpdate(Notification notification) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				db.Execute(ScriptService.Scripts["notification_insert_or_update"], notification);
			}
		}
		#endregion

		#region Update
		public void NotificationUpdate(Notification notification) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				db.Execute(ScriptService.Scripts["notification_update"], notification);
			}
		}
		public void NotificationUpdateActive(Notification notification) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				db.Execute(ScriptService.Scripts["notification_update_active"], notification);
			}
		}
		public void NotificationUpdateInactive(Notification notification) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(Notification), new ColumnAttributeTypeMapper<Notification>());
				db.Execute(ScriptService.Scripts["notification_update_inactive"], notification);
			}
		}
		#endregion

		#endregion

		/*
		 * =======================================
		 *     OTP Operations
		 */
		#region OTP Operations

		#region Enable/Disable Operations
		public void OTP_Enable(long  otp_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["otp_enable"], new { otp_id = otp_id });
			}
		}
		public void OTP_Disable(long  otp_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["otp_disable"], new { otp_id = otp_id });
			}
		}
		#endregion

		#region Get all
		public List<OTP> GetOTPs() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getall"]).AsList();
			}
		}
		public List<OTP> GetOTPsActive() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getall_active"]).AsList();
			}
		}
		public List<OTP> GetOTPsInactive() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public OTP GetOTPById(long  otp_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getbyid"], new { otp_id = otp_id }).FirstOrDefault();
			}
		}
		public OTP GetOTPByIdActive(long  otp_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getbyid_active"], new { otp_id = otp_id }).FirstOrDefault();
			}
		}
		public OTP GetOTPByIdInactive(long  otp_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getbyid_inactive"], new { otp_id = otp_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by user id
		public OTP GetOTPByUserId(long  user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getbyuserid"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public OTP GetOTPByUserIdActive(long  user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getbyuserid_active"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public OTP GetOTPByUserIdInactive(long  user_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<OTP>(ScriptService.Scripts["otp_getbyuserid_inactive"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public long OTPInsert(OTP otp) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				return db.Query<long>(ScriptService.Scripts["otp_insert"], otp).Single();
			}
		}
		public void OTPInsertOrUpdate(OTP otp) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				db.Execute(ScriptService.Scripts["otp_insert_or_update"], otp);
			}
		}
		#endregion

		#region Update
		public void OTPUpdate(OTP otp) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				db.Execute(ScriptService.Scripts["otp_update"], otp);
			}
		}
		public void OTPUpdateActive(OTP otp) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				db.Execute(ScriptService.Scripts["otp_update_active"], otp);
			}
		}
		public void OTPUpdateInactive(OTP otp) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(OTP), new ColumnAttributeTypeMapper<OTP>());
				db.Execute(ScriptService.Scripts["otp_update_inactive"], otp);
			}
		}
		#endregion

		#endregion

		/*
		 * =======================================
		 *     EmailOTP Operations
		 */
		#region EmailOTP Operations

		#region Enable/Disable Operations
		public void EmailOTP_Enable(long  emailotp_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["emailotp_enable"], new { emailotp_id = emailotp_id });
			}
		}
		public void EmailOTP_Disable(long  emailotp_id) {
			using (var db = connect()) {
				db.Execute(ScriptService.Scripts["emailotp_disable"], new { emailotp_id = emailotp_id });
			}
		}
		#endregion

		#region Get all
		public List<EmailOTP> GetEmailOTPs() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getall"]).AsList();
			}
		}
		public List<EmailOTP> GetEmailOTPsActive() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getall_active"]).AsList();
			}
		}
		public List<EmailOTP> GetEmailOTPsInactive() {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public EmailOTP GetEmailOTPById(long  emailotp_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbyid"], new { emailotp_id = emailotp_id }).FirstOrDefault();
			}
		}
		public EmailOTP GetEmailOTPByIdActive(long  emailotp_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbyid_active"], new { emailotp_id = emailotp_id }).FirstOrDefault();
			}
		}
		public EmailOTP GetEmailOTPByIdInactive(long  emailotp_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbyid_inactive"], new { emailotp_id = emailotp_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by notification id
		public EmailOTP GetEmailOTPByNotificationId(long  notification_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbynotificationid"], new { notification_id = notification_id }).FirstOrDefault();
			}
		}
		public EmailOTP GetEmailOTPByNotificationIdActive(long  notification_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbynotificationid_active"], new { notification_id = notification_id }).FirstOrDefault();
			}
		}
		public EmailOTP GetEmailOTPByNotificationIdInactive(long  notification_id) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<EmailOTP>(ScriptService.Scripts["emailotp_getbynotificationid_inactive"], new { notification_id = notification_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Insert
		public long EmailOTPInsert(EmailOTP emailotp) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				return db.Query<long>(ScriptService.Scripts["emailotp_insert"], emailotp).Single();
			}
		}
		public void EmailOTPInsertOrUpdate(EmailOTP emailotp) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				db.Execute(ScriptService.Scripts["emailotp_insert_or_update"], emailotp);
			}
		}
		#endregion

		#region Update
		public void EmailOTPUpdate(EmailOTP emailotp) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				db.Execute(ScriptService.Scripts["emailotp_update"], emailotp);
			}
		}
		public void EmailOTPUpdateActive(EmailOTP emailotp) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				db.Execute(ScriptService.Scripts["emailotp_update_active"], emailotp);
			}
		}
		public void EmailOTPUpdateInactive(EmailOTP emailotp) {
			using (var db = connect()) {
				Dapper.SqlMapper.SetTypeMap(typeof(EmailOTP), new ColumnAttributeTypeMapper<EmailOTP>());
				db.Execute(ScriptService.Scripts["emailotp_update_inactive"], emailotp);
			}
		}
		#endregion

		#endregion

	}
}