using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

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
				return db.Query<User>(ScriptService.Scripts["user_getall"]).AsList();
			}
		}
		public List<User> GetUsersActive() {
			using (var db = connect()) {
				return db.Query<User>(ScriptService.Scripts["user_getall_active"]).AsList();
			}
		}
		public List<User> GetUsersInactive() {
			using (var db = connect()) {
				return db.Query<User>(ScriptService.Scripts["user_getall_inactive"]).AsList();
			}
		}
		#endregion

		#region Get by id
		public User GetUserById(int user_id) {
			using (var db = connect()) {
				return db.Query<User>(ScriptService.Scripts["user_getbyid"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public User GetUserByIdActive(int user_id) {
			using (var db = connect()) {
				return db.Query<User>(ScriptService.Scripts["user_getbyid_active"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		public User GetUserByIdInactive(int user_id) {
			using (var db = connect()) {
				return db.Query<User>(ScriptService.Scripts["user_getbyid_inactive"], new { user_id = user_id }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by email
		public User GetUserByEmail(string user_email) {
			using (var db = connect()) {
				return db.Query<User>(ScriptService.Scripts["user_getbyemail"], new { user_email = user_email }).FirstOrDefault();
			}
		}
		public User GetUserByEmailActive(string user_email) {
			using (var db = connect()) {
				return db.Query<User>(ScriptService.Scripts["user_getbyemail_active"], new { user_email = user_email }).FirstOrDefault();
			}
		}
		public User GetUserByEmailInactive(string user_email) {
			using (var db = connect()) {
				return db.Query<User>(ScriptService.Scripts["user_getbyemail_inactive"], new { user_email = user_email }).FirstOrDefault();
			}
		}
		#endregion

		#region Get by name
		public User GetUserByName(string user_fname, string user_lname) {
			using (var db = connect()) {
				return db.Query<User>(ScriptService.Scripts["user_getbyname"], new { user_fname = user_fname, user_lname = user_lname }).FirstOrDefault();
			}
		}
		public User GetUserByNameActive(string user_fname, string user_lname) {
			using (var db = connect()) {
				return db.Query<User>(ScriptService.Scripts["user_getbyname_active"], new { user_fname = user_fname, user_lname = user_lname }).FirstOrDefault();
			}
		}
		public User GetUserByNameInactive(string user_fname, string user_lname) {
			using (var db = connect()) {
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

		#endregion

		/*
		 * =======================================
		 *     Pharmacy Operations
		 */
		#region Pharmacy Operations

		#endregion

		/*
		 * =======================================
		 *     Pharmacist Operations
		 */
		#region Pharmacist Operations

		#endregion

		/*
		 * =======================================
		 *     Template Operations
		 */
		#region Template Operations

		#endregion

		/*
		 * =======================================
		 *     Patient Operations
		 */
		#region Patient Operations

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