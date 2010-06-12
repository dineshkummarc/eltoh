using System;
using System.Collections.Generic;
using System.Drawing;

namespace EmpathyLogToHtml
{
	public class Individual
	{
		static Color[] colorSelection = {Color.Blue, Color.Red, Color.Olive, Color.Azure, Color.Brown, Color.OrangeRed, Color.Black, Color.Magenta};
		static int colorIndex = 0;
		
		public Color color;
		
		public string colorHtml
		{
			get { return ColorTranslator.ToHtml(this.color); }
			set { this.color = ColorTranslator.FromHtml(value); }
		}
		
		public string name;
		
		public string ident;
		
		public Individual ()
		{
			this.color=colorSelection[colorIndex];
			colorIndex++;
		}
		
		public Individual (string name, string ident)
		{
			this.name = name;
			this.ident = ident;
			this.color=colorSelection[colorIndex];
			colorIndex++;
		}
		
		public override string ToString ()
		{
			return string.Format("{0} | {1} | {2}", this.name, this.ident, this.colorHtml);
		}
	}
}
