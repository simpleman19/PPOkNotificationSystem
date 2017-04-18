using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Text.RegularExpressions;
using Dapper;


//                                 I'm pretty proud of this one...

namespace PPOk_Notifications.Service {
	
	/**
	 * Contains and hanndles text representations of all SQL scripts in order
	 * to allow running them directly through SQL's management console, and
	 * proper sytax highlighting in the IDE.
	 */
	public class ScriptService {

		/**
		 * A full dictionary of script text based on script name
		 */
		public static Dictionary<string, string> Scripts { get; private set; }

		/**
		 * Is called at runtime to establish the dictionary and load all 
		 * scripts from the application directory
		 */
		public static void Init() {
			Scripts = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + @"\Service\SQL\", "*.sql")
				.ToDictionary(f => Path.GetFileNameWithoutExtension(f).ToLower(), File.ReadAllText, StringComparer.OrdinalIgnoreCase);
		}

		/**
		 * Executes a script by splitting it based on the GO keyword. Since dapper
		 * does not support running the script in increments, this allows a single 
		 * script to wait for a state change before continuing the transaction.
		 */
		public static void Execute(IDbConnection db, string name, object thing = null) {
			var script = Scripts[name];
			if (script != null) {
				var commands = Regex.Split(script, @"^\s*GO\s*$",
					RegexOptions.Multiline | RegexOptions.IgnoreCase).ToList();
				db.Open();
				foreach (var command in commands) {
					if (command.Trim() == "") continue;
					if (thing != null) {
						db.Execute(command, thing);
					} else {
						db.Execute(command);
					}
				}
				db.Close();
			} else {
				throw new ArgumentException("SQL script was null");
			}
		}
	}
}