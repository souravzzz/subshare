using System;
using System.Net;

namespace subsharelib
{
	[Serializable]
	public class fdata {
		public string File_Name {get;set;}
		public long File_Size {get;set;}
		public IPEndPoint Seeder {get;set;}
		
		public fdata(string fn, long fsz, IPEndPoint fsd) {
			File_Name = fn;
			File_Size = fsz;
			Seeder = fsd;
		}
	}
}
