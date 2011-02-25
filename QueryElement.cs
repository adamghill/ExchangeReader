using System.Configuration;

namespace WebDAV {
	public class QueryElement {
		public QueryElement(string xml) {
			Xml = xml;
		}

		public string Xml { get; private set; }
	}
}