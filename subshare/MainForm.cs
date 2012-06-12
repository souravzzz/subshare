using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using subsharelib;

namespace subshare
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			init_log();
			init_values();
			init_config();
		}

		private string m_config_file;
		private string m_log_file;
		private StreamWriter LOGFILE;

		private string m_server_ip_s;
		private string m_server_port_s;
		private string m_shared_dir_s;
		private string m_download_dir_s;
		private bool m_remember_b;

		private IPAddress m_server_ip;
		private int m_server_port;
		private IPEndPoint m_server_addr;
		private IPAddress m_my_ip;
		private int m_my_port;
		private IPEndPoint m_my_addr;
		private DirectoryInfo m_shared_dir;
		private FileInfo[] m_shared_files;

		private fdata[] m_filedata;
		private fdata[] m_current_match;
		private Thread listenerThread;
		private Thread downloadThread;
		private volatile bool m_continue_listening;
		private volatile bool m_continue_downloading;
		private Socket ssock, csock, dlsock;
		private int m_chunk_size;
		private string m_current_dlfname;
		private subshareRemoteObject m_SSRO;

		void init_values() {
			groupBox1.Enabled = true;
			groupBox2.Enabled = false;
			m_remember_b = false;
			m_my_ip = get_my_ip();	//hopefully i will get my desired one!
			m_my_port = (new Random()).Next(1025,65535);	//choose a random port, what if it is not free?
			m_my_addr = new IPEndPoint(m_my_ip,m_my_port);
			ssock = csock = dlsock =null;
			m_SSRO = null;
			m_continue_listening = false;
			m_continue_downloading = false;
			m_chunk_size = 1024*60; //60KB
		}

		void init_log() {
			try {
				m_log_file = "subshare"+DateTimeOffset.Now.ToFileTime()+".log";	//create a temporary unique log
				LOGFILE = new StreamWriter(m_log_file,true);
				LOGFILE.WriteLine("Subshare Log started at: "+DateTime.Now.ToString());
			} catch(Exception ex) {
				MessageBox.Show("Can't open log file. Quitting." + ex); //now thats an error i can't log!
				this.Close();	//no logfile? i wont run!
			}
		}

		void init_config() {
			m_config_file = "subshare.cfg";
			try {
			if(File.Exists(m_config_file)) {
				using (Stream input = File.OpenRead(m_config_file)) {
					BinaryFormatter bf = new BinaryFormatter();		//why serialize some strings? very LOL idea!
					m_server_ip_s = (string)bf.Deserialize(input);	//actually, i can save anything i want that way
					m_server_port_s = (string)bf.Deserialize(input);
					m_shared_dir_s = (string)bf.Deserialize(input);
					m_download_dir_s = (string)bf.Deserialize(input);
					textBoxIP.Text = m_server_ip_s;
					textBoxPort.Text = m_server_port_s;
					textBoxDir.Text = m_shared_dir_s;
					textBoxDLDir.Text = m_download_dir_s;
				}
			}
			} catch (Exception ex) {
				LOGFILE.WriteLine("Can't Read Config File" + ex);
				return;
			}
		}

		IPAddress get_my_ip() {
			IPHostEntry myIPs = Dns.GetHostEntry(Dns.GetHostName());
			foreach(IPAddress ip in myIPs.AddressList){
				if(ip.AddressFamily==AddressFamily.InterNetwork &&	//IPV4 address
				   IPAddress.IsLoopback(ip) == false) {	//not 127.0.0.1
					return ip;	//may not be desired (LAN) address, best would be to detect it in remote object itself, but the code is tooo long and hard
				}
			}
			return IPAddress.Loopback;	//better than nothing, and supresses a warning
		}

		void save_config() {	//saves me a lot of typing
			try {
				using(Stream output = File.Create(m_config_file)) {
					BinaryFormatter bf = new BinaryFormatter();
					bf.Serialize(output, m_server_ip_s);
					bf.Serialize(output, m_server_port_s);
					bf.Serialize(output, m_shared_dir_s);
					bf.Serialize(output, m_download_dir_s);
				}
			} catch(Exception ex) {
				LOGFILE.WriteLine("Can not write config file "+ex);
				return;
			}
		}

		bool connect_to_server() {	//true on success, false on failure
			string url = @"tcp://"+m_server_addr+@"/SUBSERVER";

			TcpChannel chan=null;
			try {
				BinaryServerFormatterSinkProvider serverProv = new BinaryServerFormatterSinkProvider();
				serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
				BinaryClientFormatterSinkProvider clientProv = new BinaryClientFormatterSinkProvider();
				IDictionary props = new Hashtable();
				props["port"] = 0;
				//all this shit just to prevent a stupid error!
				//i am passing custom objects through this channel and .NET 1.1 does not like it
				//so i have to set TypeFilterLevel=FULL and they dont have any stupid property or constructor for that
				
				chan = new TcpChannel(props, clientProv, serverProv);
				ChannelServices.RegisterChannel(chan,false);
				m_SSRO = (subshareRemoteObject)Activator.GetObject(typeof(subshareRemoteObject),url);	//get reference to the remote object
				if(m_SSRO==null)	//bad luck, try again!
					return false;
				populate_filedata();
			} catch(Exception ex) {
				if(chan!=null)
					ChannelServices.UnregisterChannel(chan);	//unregister so that you can try again
				chan = null;
				LOGFILE.WriteLine("CAN NOT CONNECT TO SERVER "+ex);
				return false;
			}
			return true;
		}

		void populate_filedata() {
			try {
				m_shared_files = m_shared_dir.GetFiles();	//get the files i want to share, and FILES does not mean SUBDIRECTORIES of files within them
				m_filedata = m_SSRO.getFileList(m_shared_files,m_my_addr); //calling the remote method
				m_current_match = m_filedata;	//current match holds the current display, and fildata stores them all. confusing, nah?
				dataGridView1.DataSource = m_current_match;	//the grid is populated
			} catch(Exception ex) {
				MessageBox.Show("Could not retrieve file list from the server.");
				LOGFILE.WriteLine("FILELIST UPDATE ERROR: "+ex);
			}

			if(m_current_match.Length==0)	//no use giving you a choice if you cant use it properly, so if nobody shares nothing, you can cry!
				buttonDownload.Enabled = false;
			else if(!m_continue_downloading) //NO ONGOING DOWNLOAD and something to download
				buttonDownload.Enabled = true;
		}

		void cancel_download() {	//why did you start it in the first place, prick? can't you fool make up your mind?
			m_continue_downloading = false;
			close_socket(dlsock);
			delete_unfinished_dl();	//no use keeping an unfinished file around...
		}

		void delete_unfinished_dl() {	//now i have to clean up your shit
			try {
				if(m_current_dlfname!=null && File.Exists(m_current_dlfname))	//first i will check if i have to do that
					File.Delete(m_current_dlfname);
			} catch(Exception ex) {
				LOGFILE.WriteLine("DELETE ERROR: "+ex);
			} finally {
				m_current_dlfname = null;	//so that somebody else does not trip on it
			}
		}

		void start_listening() { //yea i am all ears!
			try {
				ssock = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
				ssock.Blocking = true;	//blocking is much, much less trouble
				ssock.Bind(m_my_addr);	//hope i picked up a free port back there
				ssock.Listen(10);	//dont make too many guys wait
				while(m_continue_listening) {
					try {
						csock = ssock.Accept();

						byte[] recv_data = new byte[255];	//a filename should not be that long
						int recv_len = csock.Receive(recv_data);
						string fname = Encoding.ASCII.GetString(recv_data,0,recv_len);
						fname = m_shared_dir_s+"\\"+fname;

						if(File.Exists(fname)) {	//so you want a good piece of cake, not a rotten one
							long fsize = (new FileInfo(fname)).Length;	//was there a File.Length() method? i dont remember
							byte[] fileChunk = new byte[m_chunk_size];
							long sentBytes = 0;
							using(BinaryReader br = new BinaryReader(File.OpenRead(fname))) {
								while(sentBytes<fsize) {	//until i am done
									int read_len = br.Read(fileChunk,0,m_chunk_size); //read chunk
									int sent_len = csock.Send(fileChunk,read_len,SocketFlags.None); //send chunk
									if(sent_len==0 || sent_len!=read_len)	//and see if i did it properly
										break;		//if not, GFU
									sentBytes += sent_len;
								}
							}

						}

						csock.Close();	//it was nice doing business with you
					} catch(Exception ex) {
						LOGFILE.WriteLine("FILE SENDING ERROR: "+ex);
					}
				} //end of while
			} catch(Exception ex) {
					LOGFILE.WriteLine(ex);
			} finally {	//dont forget to close the sockets
				close_socket(ssock);
				close_socket(csock);
			}

		}

		void download_file(object fdataobj) {	//a parameterizedThreadStart() can accept only an object
			fdata fileparams = (fdata)fdataobj;
			string fname = fileparams.File_Name;
			long fsize = fileparams.File_Size;
			IPEndPoint fseed = fileparams.Seeder;

			string dlfname = m_download_dir_s+"\\"+fname;	//so there you will go, kid
			m_current_dlfname = dlfname;	//in case you change your mind and cancel the trip
			if(File.Exists(dlfname)) {	//hopefully you are not already there!!
				DialogResult result = MessageBox.Show("The file "+fname+" already exists. Do you want to overwrite?","OverWrite?",MessageBoxButtons.YesNo);
				if(result == DialogResult.No)
					return;
			}

			m_continue_downloading = true;	//in case you change your mind and cancel the trip
			bool download_success = false;	//why do i have to be so pessimistic all the time?
			try {
				dlsock = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
				dlsock.Connect(fseed);
				byte[] fileChunk = new byte[m_chunk_size];
				byte[] fnamebytes = Encoding.ASCII.GetBytes(fname);
				dlsock.Send(fnamebytes);	//first let them know who i am asking for

				long receivedBytes = 0;
				using (FileStream fs = File.Open(dlfname,FileMode.Create)) {
					using(BinaryWriter bw = new BinaryWriter(fs)) {
						while(receivedBytes<fsize && m_continue_downloading) {
							int recv_len = dlsock.Receive(fileChunk);
							if(recv_len==0)	//damn, somebody messed up something!
								break;
							bw.Write(fileChunk,0,recv_len);
							receivedBytes += recv_len;
							DLProgressBar.Value = (int)(100*receivedBytes/fsize);	//you like eye candy, dont you?
						}
					}
				}
				if(receivedBytes==fsize) {	//just size comparison? no hashcheck? what n00b!
					download_success = true;
				}
			} catch(Exception ex) {
				LOGFILE.WriteLine("Could Not Download "+fname+ex);
			} finally {
				if(download_success) {
					MessageBox.Show("Completed Downloading "+fname);
				} else {
					MessageBox.Show("Could Not Download "+fname);
					cancel_download();	//bad luck buddy
				}
				close_socket(dlsock);
				buttonDownload.Enabled = true;	//try some other things
				buttonCancel.Enabled = false;	//i already cancelled it!
				DLProgressBar.Visible = false;
				DLProgressBar.Value = 0;
				StatusLabel.Text = "Connected to " + m_server_addr.ToString();
				m_continue_downloading = false;
				m_current_dlfname = null;
			}
		}

		void close_socket(Socket s) { //better than calling Close() and repent
			if(s!=null && s.Connected) {
				s.Shutdown(SocketShutdown.Both);
				s.Close();
				s = null;
			}
		}

		void TextBoxIPTextChanged(object sender, EventArgs e)
		{
			m_server_ip_s = textBoxIP.Text;
		}

		void TextBoxPortTextChanged(object sender, EventArgs e)
		{
			m_server_port_s = textBoxPort.Text;
		}

		void TextBoxDirClick(object sender, EventArgs e)	//get the shared directory
		{
			DialogResult result = folderBrowserDialog1.ShowDialog();
			if(result==DialogResult.OK) {
				m_shared_dir_s = folderBrowserDialog1.SelectedPath;
				textBoxDir.Text = m_shared_dir_s;
				m_download_dir_s = m_shared_dir_s;	//by default, the download and the shared directory are the same
				textBoxDLDir.Text = m_download_dir_s;
			}
		}

		void TextBoxDLDirClick(object sender, EventArgs e)	//set the download directory manually, if needed
		{
			DialogResult result = folderBrowserDialog1.ShowDialog();
			if(result==DialogResult.OK) {
				m_download_dir_s = folderBrowserDialog1.SelectedPath;
				textBoxDLDir.Text = m_download_dir_s;
			}
		}

		void CheckBoxRememberCheckedChanged(object sender, EventArgs e)	//to remember or not to remember
		{
			m_remember_b = checkBoxRemember.Checked;
		}

		void ButtonConnectClick(object sender, EventArgs e)	//connect to the server
		{
			bool valid_input = false;

			m_server_ip_s = textBoxIP.Text;
			if(m_server_ip_s!=null)
				valid_input = IPAddress.TryParse(m_server_ip_s, out m_server_ip);
			else
				valid_input = false;
			if(!valid_input)
				MessageBox.Show("Enter Valid IP Address!");

			m_server_port_s = textBoxPort.Text;
			if(m_server_port_s!=null)
				valid_input = Int32.TryParse(m_server_port_s, out m_server_port);
			else
				valid_input = false;
			if(!valid_input)
				MessageBox.Show("Enter Valid Port Number!");

			m_shared_dir_s = textBoxDir.Text;
			m_download_dir_s = textBoxDLDir.Text;
			if(m_shared_dir_s==null || m_download_dir_s==null)
				valid_input = false;

			if(valid_input) {	//if all input seems valid
				m_server_addr = new IPEndPoint(m_server_ip, m_server_port);
				m_shared_dir = new DirectoryInfo(m_shared_dir_s);
				m_shared_files = m_shared_dir.GetFiles();

				bool connect_status = connect_to_server();	//check connection successful or not
				if(connect_status) {
					m_continue_listening = true;
					ThreadStart listen = new ThreadStart(start_listening);
					listenerThread = new Thread(listen);
					listenerThread.IsBackground = true;	//so that other tasks may continue
					listenerThread.Start();	//start the listener(uploading) thread
					StatusLabel.Text = "Connected to " + m_server_addr.ToString();
					groupBox1.Enabled = false;
					groupBox2.Enabled = true;
					buttonCancel.Enabled = false;
					this.AcceptButton = buttonSearch;
					if(m_remember_b)
						save_config();	//save input
					fileSystemWatcher1.Path = m_shared_dir_s; //monitor the shared directory for changes
				}
				else {
					MessageBox.Show("Can Not Connect to "+m_server_addr.ToString());
				}
			}

		}

		void ButtonRefreshClick(object sender, EventArgs e)	//get updated filelist
		{
			populate_filedata();
		}

		void ButtonDownloadClick(object sender, EventArgs e)
		{
			int row = dataGridView1.CurrentRow.Index;	//the file user clicked on
			if(row>=0) {
				fdata fileparams = m_current_match[row];	//get info about file 2 be downloaded
				buttonDownload.Enabled = false;
				buttonCancel.Enabled = true;
				DLProgressBar.Visible = true;
				DLProgressBar.Value = 0;
				StatusLabel.Text = "Downloading " + fileparams.File_Name;
				downloadThread = new Thread(new ParameterizedThreadStart(download_file));	//start the downloading thread
				downloadThread.IsBackground = true;
				downloadThread.Start(fileparams);
			}
		}

		void ButtonCancelClick(object sender, EventArgs e)	//user cancells the download
		{
			DialogResult res = MessageBox.Show("Do you really want to Cancel the Download?","Cancel Download",MessageBoxButtons.YesNo);
			if(res==DialogResult.Yes) {	//make sure it is not accidentally clicked
				cancel_download();
				buttonDownload.Enabled = true;
				buttonCancel.Enabled = false;
				DLProgressBar.Visible = false;
				DLProgressBar.Value = 0;
				StatusLabel.Text = "Connected to " + m_server_addr.ToString();
			}

		}

		void MainFormFormClosing(object sender, FormClosingEventArgs e)	//before exiting the client
		{
			DialogResult res = MessageBox.Show("Do you really want to exit? Any unfinished download will be cancelled.","Confirm Exit",MessageBoxButtons.YesNo);
			if(res== DialogResult.No) {
				e.Cancel = true;	//if user closed it accidentally, cancel the closing event
			} else {	//no, this guy really wants to quit
				try {
					cancel_download();	//cancel unfinished download
					m_continue_listening = false;	//stop the listener thread gracefully
					if(m_SSRO!=null) m_SSRO.clientExit(m_my_addr);	//notify the server about quitting	
				} catch(Exception ex) {
					LOGFILE.WriteLine("EXIT ERROR "+ex);
				} finally {
					LOGFILE.Close();
					File.AppendAllText("subshare.log",File.ReadAllText(m_log_file));	//dump the temporary logs content
					File.Delete(m_log_file);	//delete the temporary log
				}
			}
		}

		void ButtonSearchClick(object sender, EventArgs e)
		{
			string search_str = textBoxSearch.Text;

			m_current_match = Array.FindAll(m_filedata, delegate(fdata f) { return f.File_Name.Contains(search_str); });

			if(m_current_match.Length==0) {	//if no file matches the search
				MessageBox.Show("No files matched Search. Try Again.");
				m_current_match = m_filedata;
			} else {
				dataGridView1.DataSource = m_current_match;	//show only the files that matches the search
			}
		}

		void ButtonShowAllClick(object sender, EventArgs e)	//forget searching, i want them all
		{
			m_current_match = m_filedata;	//show all files
			dataGridView1.DataSource = m_current_match;
			textBoxSearch.Text = "";
		}

		void FileSystemWatcher1Changed(object sender, FileSystemEventArgs e)	//if something changes in the shared directory
		{
			populate_filedata();
		}

		void FileSystemWatcher1Renamed(object sender, RenamedEventArgs e)	//notify the server immediately
		{
			populate_filedata();
		}
	}

}

