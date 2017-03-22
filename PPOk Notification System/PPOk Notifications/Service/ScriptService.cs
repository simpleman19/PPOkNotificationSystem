using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Text.RegularExpressions;
using Dapper;

namespace PPOk_Notifications.Service {
	public class ScriptService {

		public static Dictionary<string, string> Scripts { get; private set; }

		public static void Init() {
			Scripts = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + @"\Service\SQL\", "*.sql")
				.ToDictionary(f => Path.GetFileNameWithoutExtension(f).ToLower(), File.ReadAllText, StringComparer.OrdinalIgnoreCase);
		}

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