using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;

namespace PPOk_Notifications.Service {
	public static class DatabaseService {

		private static IDbConnection _connectionState;

		public static IDbConnection Connection {
			get {
				if (_connectionState == null || _connectionState.State != ConnectionState.Open) {
					_connectionState = new SqlConnection(ConfigurationManager.AppSettings["SqlConnectionString"]);
				}
				return _connectionState;
			}
		}

		public static IDbConnection Connect() {
			return new SqlConnection(ConfigurationManager.AppSettings["SqlConnectionString"]);
		}

		public static IDbConnection ConnectLoosely() {
			return new SqlConnection(ConfigurationManager.AppSettings["SqlConnectionStringNoCatalog"]);
		}

		public static bool Rebuild() {
			try {
				Drop();
				Create();
			} catch (Exception) {
				return false;
			}
			return true;
		}

		public static  void Drop() {
			using (var db = DatabaseService.Connection) {
				db.Execute(ScriptService.Scripts["database_drop"]);
			}
		}

		public static void Create() {
			using (var db = DatabaseService.Connection) {
				ScriptService.Execute(db, "database_create");
			}
		}
	}
}