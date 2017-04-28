using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace PPOk_Notifications.Service {

	/**
	 * Establishes and handles open connections to the database
	 */
	public static class DatabaseService {

		// Currently established connection to the database
		private static IDbConnection _connectionState;

		/**
		 * Public member which returns an open connection or establishes one. Allows
		 * more than one transaction to be carried over the connection and lets the SQL
		 * instance handle timeouts rather than the connection being disposed of.
		 */
		public static IDbConnection Connection {

			get {
				//If a connection does not exist, or it is closed, a new one is opened
				if (_connectionState == null || _connectionState.State != ConnectionState.Open) {
					_connectionState = new SqlConnection(ConfigurationManager.AppSettings["SqlConnectionString"]);
				}
				return _connectionState;
			}
		}

		/**
		 * Requests a connection using the default connection string
		 * 
		 * @returns - default database connection
		 */
		public static IDbConnection Connect() {
			return new SqlConnection(ConfigurationManager.AppSettings["SqlConnectionString"]);
		}

		/**
		 * Requests a connection without specifying a specific database for use 
		 * when dropping or creating the database, since no user can be connected
		 * at that time.
		 * 
		 * @returns - database connection without catalog
		 */
		public static IDbConnection ConnectLoosely() {
			return new SqlConnection(ConfigurationManager.AppSettings["SqlConnectionStringNoCatalog"]);
		}

		/**
		 * Rebuilds the database entirely by dropping the entire dbo, and creating
		 * a new one. Useful only for debugging and should never be used in pracice.
		 * It can also be used to rebuild based on an updated structure.
		 * 
		 * @returns - true if database was created
		 */
		public static bool Rebuild() {
			Drop();
			Create();
			return true;
		}

		/**
		 * Disconnects all users and drops the database object from the connected instance.
		 */
		public static void Drop() {
			using (var db = ConnectLoosely()) {
				db.Execute(ScriptService.Scripts["database_drop"]);
			}
		}

		/**
		 * Creates a new database object based on the creation script. Establishes all tables
		 * and fields. Can also be used to establish stored procedures, etc.. 
		 */
		public static void Create() {
			using (var db = ConnectLoosely()) {
				ScriptService.Execute(db, "database_create");
			}
		}
	}
}