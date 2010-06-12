using System;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace EmpathyLogToHtml
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			if (args.Length<1) {
				Console.WriteLine("Usage: eltoh LOGFILE [OUTPUT]");
			} else {
				string logfile = args[0];
				string outfile = args[0]+".html";
				if (args.Length==2) {outfile=args[1];}
				try {
					Conversation conv = Conversation.LoadFromXml(logfile);
					conv.PopulateUserList();
					Conversation.SaveToHtml(outfile, conv);
				} catch (FileNotFoundException ex) {
					Console.WriteLine("Error: Log file "+ex.FileName+" not found.");
					System.Environment.Exit(666);
				}
			}
		}
	}
}
