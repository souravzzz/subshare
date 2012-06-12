using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace subsharelib
{
	public class subshareRemoteObject : MarshalByRefObject 
	{
		private List<fdata> allfiles;
		
		public subshareRemoteObject() {
			allfiles = new List<fdata>();
		}
		
		public fdata[] getFileList(FileInfo[] clientfiles, IPEndPoint clientaddr) {
			lock(allfiles) {	//so that some other client's request does not mess up the list
				allfiles.RemoveAll(delegate(fdata f) { return f.Seeder.Equals(clientaddr); });	//remove the client's own files
				fdata[] result = allfiles.ToArray();	//the client does not need its own files, so send it only what others have uploaded
				Array.ForEach(clientfiles, delegate(FileInfo fi) { allfiles.Add(new fdata(fi.Name,fi.Length,clientaddr)); }); //add to the list the client's own files, others will need these
				
				return result;
			}
		}
		
		public void clientExit(IPEndPoint clientaddr) {
			lock(allfiles) {
				allfiles.RemoveAll(delegate(fdata f) { return f.Seeder.Equals(clientaddr); });	//remove dead client's files
			}
		}
		
	}
}