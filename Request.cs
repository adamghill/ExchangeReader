using System;
using System.IO;
using System.Net;
using System.Xml;
using Common;

namespace WebDAV {
	/// <summary>
	/// This was, basically, ripped from the excellent article on accessing WebDAV shares here (Marc Charbonneau):
	/// http://blog.downtownsoftwarehouse.com/2006/10/26/using-net-and-webdav-to-access-an-exchange-server/
	/// And, this article on how to query the exchange inbox here (Lee Derbyshire): 
	/// http://www.msexchange.org/articles/Access-Exchange-2000-2003-Mailbox-WebDAV.html.
	/// And, querying the calendar:
	/// http://msdn2.microsoft.com/en-us/library/aa123570.aspx.
	/// </summary>
	public class Request 
    {
        public Request(CookieContainer authenticationCookies, string exchangeServer, string path)
        {
            ExchangeServer = exchangeServer;
            Path = path;
            AuthenticationCookies = authenticationCookies;
        }

		/// <summary>
		/// Single-threaded approach to execute queries.
		/// </summary>
		/// <param name="queries">Queries to execute.</param>
		public string ExecuteQuery(QueryElement query) {
			XmlDocument resultsDocument = executeQuery(query);
			string output = resultsDocument.OuterXml;
			return output;
		}

		/// <summary>
		/// Execute the query on the WebDAV server.
		/// </summary>
		/// <param name="queryElement">Query information.</param>
		/// <returns>Results from the query.</returns>
		private XmlDocument executeQuery(QueryElement queryElement) {
			// Document to hold the query information.
			XmlDocument queryDocument = new XmlDocument();

			try {
				queryDocument.LoadXml(queryElement.Xml);			
			} catch (XmlException ex) {
				throw new ApplicationException("The Xml specified is not valid.", ex);
			}

			// Document to hold the resulting Xml.
			return runQuery(queryDocument.OuterXml);
		}

		/// <summary>
		/// Run the query on the WebDAV server.
		/// </summary>
		private XmlDocument runQuery(string query) {
			// Document to store the response.
			XmlDocument document = new XmlDocument();

			// Create the Uri that will be used.
			Uri = ExchangeServer + Path;

			// Replace all of our custom variables in the Xml to actual values.
			string body = query;

			using (HttpWebResponse response = HttpUtility.SendRequestAndGetResponse(Uri, body, "SEARCH", "text/xml", 
                AuthenticationCookies)) {
				using (Stream responseStream = response.GetResponseStream()) {
					document.Load(responseStream);
				}
			}

			return document;
		}

		public string Uri { get; private set; }
        public string Path { get; private set; }
        public CookieContainer AuthenticationCookies { get; private set; }
        public string ExchangeServer { get; private set; }
	}
}