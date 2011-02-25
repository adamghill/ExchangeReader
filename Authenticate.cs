using System;
using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Common;

namespace ExchangeReader {
	public static class WebDAVAuthenticate {
		static WebDAVAuthenticate() {
			// Setup the event to check our certification.
			ServicePointManager.ServerCertificateValidationCallback += CheckCert;
		}

		/// <summary>
		/// Check the server certificate.
		/// </summary>
		private static bool CheckCert(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
			return true;
		}

		/// <summary>
		/// Authenticate against the Exchange server and store the authorization cookie so we can use
		/// it for future WebDAV requests.
		/// </summary>
		public static CookieContainer Authenticate(string server, string username, string password) {
			CookieContainer authenticateCookies;
			string authenticateUri = server + "/exchweb/bin/auth/owaauth.dll";

			// Create the web request body.
			string body = string.Format("destination={0}&username={1}&password={2}",
										server,
										username,
										password);

			using (HttpWebResponse response = HttpUtility.SendRequestAndGetResponse(authenticateUri, body, "POST", "application/x-www-form-urlencoded", new CookieContainer())) {
				if (response.Cookies.Count < 2) {
					throw new AuthenticationException("Login failed. Is the login / password correct?");
				}

				authenticateCookies = new CookieContainer();
				foreach (Cookie myCookie in response.Cookies) {
					authenticateCookies.Add(myCookie);
				}
			}

			return authenticateCookies;
		}
	}
}