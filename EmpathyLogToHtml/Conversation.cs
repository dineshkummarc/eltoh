using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace EmpathyLogToHtml
{
	[Serializable]
	[XmlRoot("log")]
	public class Conversation
	{
		[XmlIgnore]
		public List<Individual> people;

		[XmlElement("message")]
		public List<Message> log;

		public Conversation ()
		{
			this.people = new List<Individual> ();
			this.log = new List<Message> ();
		}
		
		public void PopulateUserList()
		{
			List<Individual> users = new List<Individual>();
			bool existingUser = false;
			
			foreach (Message msg in this.log)
			{
				existingUser=false;
				foreach (Individual i in users)
				{
					if (i.ident==msg.user.ident) {existingUser=true; break;}
				}
				if (existingUser==false) {users.Add(msg.user);}
				else {msg.user=users.Find(u => u.ident==msg.user.ident);}
			}
			this.people = users;
		}

		public static Conversation LoadFromXml (string path)
		{
			if (System.IO.File.Exists(path)) {
				XmlSerializer x = new XmlSerializer(typeof(Conversation));
				TextReader r = new StreamReader(path);
				Conversation conv = (Conversation)x.Deserialize(r);
				r.Close();
				return conv;
			} else {
				throw new FileNotFoundException("Logfile not found.", path);
			}
		}
		
		public static void SaveToXml (string path, Conversation conv)
		{
			XmlSerializer x = new XmlSerializer(typeof(Conversation));
			TextWriter w = new StreamWriter(path);
			x.Serialize(w, conv);
			w.Close();
		}
		
		public static void SaveToHtml (string path, Conversation conv)
		{
			TextWriter w = new StreamWriter(path);
			w.WriteLine("<html>\n\t<head>\n\t\t<meta http-equiv=\"content-type\" content=\"text/html; charset=UTF-8\">");
			w.WriteLine("\t\t<title>Conversation log: "+conv.log[0].time.ToShortDateString()+"</title>");
			w.WriteLine("\t\t<style type=\"text/css\">");
			w.WriteLine("\t\t\tbody {\n\t\t\t\tcolor: black;\n\t\t\t\tbackground-color: white;\n\t\t\t}");
			foreach (Individual usr in conv.people) {
				w.WriteLine("\t\t\t.usr-"+conv.people.IndexOf(usr).ToString()+" span {\n\t\t\t\tcolor: "+usr.colorHtml+";\n\t\t\t\tfont-weight: bold;\n\t\t\t}");
			}
			w.WriteLine("\t\t</style>");
			w.WriteLine("\t</head>\n\t<body>");
			w.WriteLine("\t\t<h1>Conversation on "+conv.log[0].time.ToShortDateString()+"</h1>");
			w.WriteLine("\t\t<p>");
			foreach (Individual usr in conv.people) {
				w.WriteLine("\t\t\t<span class=\"usr-"+conv.people.IndexOf(usr).ToString()+"\"><span>"+usr.name+"</span></span>");
			}
			w.WriteLine("\t\t</p>");
			w.WriteLine("\t\t<p>");
			foreach (Message msg in conv.log) {
				w.WriteLine("\t\t\t<div class=\"usr-"+conv.people.IndexOf(msg.user).ToString()+"\">"+msg.time.ToLongTimeString()+": <span>"+msg.user.name+": </span>"+msg.body+"</div>");
			}
			w.WriteLine("\t\t</p>");
			w.WriteLine("\t</body>\n</html>");
			w.Close();
		}
	}
}
