using System;
using System.Configuration;
using System.Collections.Generic;

namespace WebDAV {
	public class Settings {
		//private QueryElementCollection queries;
		private List<QueryElement> queries;
		private string username;
		private string password;
		private string exchangeServer;
		private string dateTimeFormat;
		private bool isTest = false;
		private bool tryToParseDates;

		public Settings(string[] args) {
			//Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			//QuerySettings querySettings = config.GetSection("querySettings") as QuerySettings;

			//if (querySettings == null) {
			//    throw new Exception("Looks like there are no settings defined, or they are defined incorrectly.");
			//}

			this.exchangeServer = string.Empty; //querySettings.ExchangeServer;
			this.queries = new List<QueryElement>();// string.Empty; //querySettings.Queries;
			//this.tryToParseDates = querySettings.TryToParseDates;
			this.dateTimeFormat = string.Empty; //querySettings.DateTimeFormat;

			//Common.ArgumentParser argumentParser = new Common.ArgumentParser(args);

			//ConnectionSettings connectionSettings = config.GetSection("connectionSettings") as ConnectionSettings;

			/*
			if (connectionSettings == null &&
				(argumentParser.NonSwitchArguments.Count < 2)) {
				if (!argumentParser.NonSwitchArguments[0].Equals("-e")) {
					throw new ArgumentException("Please specify a username and password in your arguments or in the config file.");
				}
			}

			if (argumentParser.NonSwitchArguments.Count > 0) {
				if (argumentParser.NonSwitchArguments.Contains("-e")) {
					if (connectionSettings != null &&
						!connectionSettings.IsReadOnly() &&
						!connectionSettings.SectionInformation.IsProtected &&
						!connectionSettings.SectionInformation.IsLocked &&
						connectionSettings.SectionInformation.IsDeclared) {
						Console.WriteLine("Trying to encrypt the connectionString section.");
						connectionSettings.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
						connectionSettings.SectionInformation.ForceSave = true;
						config.Save();
						Console.WriteLine("The section has now been encrypted.");
					}
				} else if (argumentParser.NonSwitchArguments.Contains("-t")) {
					this.isTest = true;
				}
			}

			if (argumentParser.NonSwitchArguments.Count >= 2) {
				this.username = argumentParser.NonSwitchArguments[0];
				this.password = argumentParser.NonSwitchArguments[1];
			} else if (connectionSettings != null) {
				this.username = connectionSettings.Connection.Username;
				this.password = connectionSettings.Connection.Password;
			}
			 * */
		}

		public bool IsTest {
			get { return this.isTest; }
		}

		public string Username {
			get { return username; }
		}

		public string Password {
			get { return password; }
		}

		public List<QueryElement> Queries {
			get { return queries; }
		}

		public string ExchangeServer {
			get { return exchangeServer; }
		}

		public bool TryToParseDates {
			get { return tryToParseDates; }
		}

		public string DateTimeFormat {
			get { return dateTimeFormat; }
		}
	}
}