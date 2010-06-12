using System;
using System.Xml;
using System.Xml.Serialization;

namespace EmpathyLogToHtml
{
	public class Message
	{
		[XmlIgnore]
		public DateTime time = new DateTime();
		
		[XmlAttribute("time")]
		public string xmltime {
			get { return this.time.ToString("yyyyMMddTHH:mm:ss"); }
			set { this.time=DateTime.ParseExact(value, "yyyyMMddTHH:mm:ss", null); }
		}
		
		[XmlIgnore]
		public Individual user = new Individual();
		
		[XmlAttribute("id")]
		public string xmlid { get { return this.user.ident; } set { this.user.ident=value;}}
		
		[XmlAttribute("name")]
		public string xmlname { get { return this.user.name; } set {this.user.name=value;}}
		
		[XmlText]
		public string body;
		
		public Message ()
		{
		}
		
		public override string ToString ()
		{
			return string.Format("[Message: xmltime={0}, xmlid={1}, xmlname={2}, body={3}]", xmltime, xmlid, xmlname, body);
		}
	}
}
