/*
 * Created by SharpDevelop.
 * User: SOURAV
 * Date: 10-09-2010
 * Time: 18:02:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace subshare
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBoxDLDir = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.checkBoxRemember = new System.Windows.Forms.CheckBox();
			this.buttonConnect = new System.Windows.Forms.Button();
			this.textBoxDir = new System.Windows.Forms.TextBox();
			this.textBoxPort = new System.Windows.Forms.TextBox();
			this.textBoxIP = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.buttonShowAll = new System.Windows.Forms.Button();
			this.buttonSearch = new System.Windows.Forms.Button();
			this.textBoxSearch = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonRefresh = new System.Windows.Forms.Button();
			this.buttonDownload = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.DLProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBoxDLDir);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.checkBoxRemember);
			this.groupBox1.Controls.Add(this.buttonConnect);
			this.groupBox1.Controls.Add(this.textBoxDir);
			this.groupBox1.Controls.Add(this.textBoxPort);
			this.groupBox1.Controls.Add(this.textBoxIP);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(410, 116);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// textBoxDLDir
			// 
			this.textBoxDLDir.Location = new System.Drawing.Point(87, 85);
			this.textBoxDLDir.Name = "textBoxDLDir";
			this.textBoxDLDir.ReadOnly = true;
			this.textBoxDLDir.Size = new System.Drawing.Size(228, 20);
			this.textBoxDLDir.TabIndex = 4;
			this.textBoxDLDir.DoubleClick += new System.EventHandler(this.TextBoxDLDirClick);
			this.textBoxDLDir.Click += new System.EventHandler(this.TextBoxDLDirClick);
			this.textBoxDLDir.Enter += new System.EventHandler(this.TextBoxDLDirClick);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6, 85);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 23);
			this.label4.TabIndex = 8;
			this.label4.Text = "Download To";
			// 
			// checkBoxRemember
			// 
			this.checkBoxRemember.Location = new System.Drawing.Point(321, 52);
			this.checkBoxRemember.Name = "checkBoxRemember";
			this.checkBoxRemember.Size = new System.Drawing.Size(83, 56);
			this.checkBoxRemember.TabIndex = 5;
			this.checkBoxRemember.Text = "Remember Settings";
			this.checkBoxRemember.UseVisualStyleBackColor = true;
			this.checkBoxRemember.CheckedChanged += new System.EventHandler(this.CheckBoxRememberCheckedChanged);
			// 
			// buttonConnect
			// 
			this.buttonConnect.Location = new System.Drawing.Point(321, 13);
			this.buttonConnect.Name = "buttonConnect";
			this.buttonConnect.Size = new System.Drawing.Size(83, 23);
			this.buttonConnect.TabIndex = 6;
			this.buttonConnect.Text = "Connect";
			this.buttonConnect.UseVisualStyleBackColor = true;
			this.buttonConnect.Click += new System.EventHandler(this.ButtonConnectClick);
			// 
			// textBoxDir
			// 
			this.textBoxDir.Location = new System.Drawing.Point(87, 62);
			this.textBoxDir.Name = "textBoxDir";
			this.textBoxDir.ReadOnly = true;
			this.textBoxDir.Size = new System.Drawing.Size(228, 20);
			this.textBoxDir.TabIndex = 3;
			this.textBoxDir.DoubleClick += new System.EventHandler(this.TextBoxDirClick);
			this.textBoxDir.Click += new System.EventHandler(this.TextBoxDirClick);
			this.textBoxDir.Enter += new System.EventHandler(this.TextBoxDirClick);
			// 
			// textBoxPort
			// 
			this.textBoxPort.Location = new System.Drawing.Point(87, 39);
			this.textBoxPort.Name = "textBoxPort";
			this.textBoxPort.Size = new System.Drawing.Size(228, 20);
			this.textBoxPort.TabIndex = 2;
			this.textBoxPort.TextChanged += new System.EventHandler(this.TextBoxPortTextChanged);
			// 
			// textBoxIP
			// 
			this.textBoxIP.Location = new System.Drawing.Point(87, 16);
			this.textBoxIP.Name = "textBoxIP";
			this.textBoxIP.Size = new System.Drawing.Size(228, 20);
			this.textBoxIP.TabIndex = 1;
			this.textBoxIP.TextChanged += new System.EventHandler(this.TextBoxIPTextChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(6, 62);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(75, 23);
			this.label3.TabIndex = 2;
			this.label3.Text = "Shared Folder";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 39);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(75, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "Server Port";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Server IP";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.buttonShowAll);
			this.groupBox2.Controls.Add(this.buttonSearch);
			this.groupBox2.Controls.Add(this.textBoxSearch);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.dataGridView1);
			this.groupBox2.Controls.Add(this.buttonCancel);
			this.groupBox2.Controls.Add(this.buttonRefresh);
			this.groupBox2.Controls.Add(this.buttonDownload);
			this.groupBox2.Location = new System.Drawing.Point(12, 134);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(410, 303);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			// 
			// buttonShowAll
			// 
			this.buttonShowAll.Location = new System.Drawing.Point(87, 274);
			this.buttonShowAll.Name = "buttonShowAll";
			this.buttonShowAll.Size = new System.Drawing.Size(75, 23);
			this.buttonShowAll.TabIndex = 11;
			this.buttonShowAll.Text = "Show All";
			this.buttonShowAll.UseVisualStyleBackColor = true;
			this.buttonShowAll.Click += new System.EventHandler(this.ButtonShowAllClick);
			// 
			// buttonSearch
			// 
			this.buttonSearch.Location = new System.Drawing.Point(321, 14);
			this.buttonSearch.Name = "buttonSearch";
			this.buttonSearch.Size = new System.Drawing.Size(83, 23);
			this.buttonSearch.TabIndex = 8;
			this.buttonSearch.Text = "Search";
			this.buttonSearch.UseVisualStyleBackColor = true;
			this.buttonSearch.Click += new System.EventHandler(this.ButtonSearchClick);
			// 
			// textBoxSearch
			// 
			this.textBoxSearch.Location = new System.Drawing.Point(87, 17);
			this.textBoxSearch.Name = "textBoxSearch";
			this.textBoxSearch.Size = new System.Drawing.Size(228, 20);
			this.textBoxSearch.TabIndex = 7;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(7, 20);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(74, 23);
			this.label5.TabIndex = 5;
			this.label5.Text = "Search File";
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToOrderColumns = true;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(6, 46);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.ShowEditingIcon = false;
			this.dataGridView1.Size = new System.Drawing.Size(398, 222);
			this.dataGridView1.StandardTab = true;
			this.dataGridView1.TabIndex = 9;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(335, 274);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 13;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
			// 
			// buttonRefresh
			// 
			this.buttonRefresh.Location = new System.Drawing.Point(6, 274);
			this.buttonRefresh.Name = "buttonRefresh";
			this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
			this.buttonRefresh.TabIndex = 10;
			this.buttonRefresh.Text = "Refresh";
			this.buttonRefresh.UseVisualStyleBackColor = true;
			this.buttonRefresh.Click += new System.EventHandler(this.ButtonRefreshClick);
			// 
			// buttonDownload
			// 
			this.buttonDownload.Location = new System.Drawing.Point(254, 274);
			this.buttonDownload.Name = "buttonDownload";
			this.buttonDownload.Size = new System.Drawing.Size(75, 23);
			this.buttonDownload.TabIndex = 12;
			this.buttonDownload.Text = "Download";
			this.buttonDownload.UseVisualStyleBackColor = true;
			this.buttonDownload.Click += new System.EventHandler(this.ButtonDownloadClick);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.StatusLabel,
									this.DLProgressBar});
			this.statusStrip1.Location = new System.Drawing.Point(0, 440);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(434, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 3;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// StatusLabel
			// 
			this.StatusLabel.AutoSize = false;
			this.StatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(300, 17);
			this.StatusLabel.Text = "Not Connected";
			this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// DLProgressBar
			// 
			this.DLProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.DLProgressBar.AutoToolTip = true;
			this.DLProgressBar.Name = "DLProgressBar";
			this.DLProgressBar.Size = new System.Drawing.Size(100, 16);
			this.DLProgressBar.ToolTipText = "Download Progress";
			this.DLProgressBar.Visible = false;
			// 
			// folderBrowserDialog1
			// 
			this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
			this.folderBrowserDialog1.ShowNewFolderButton = false;
			// 
			// fileSystemWatcher1
			// 
			this.fileSystemWatcher1.EnableRaisingEvents = true;
			this.fileSystemWatcher1.SynchronizingObject = this;
			this.fileSystemWatcher1.Renamed += new System.IO.RenamedEventHandler(this.FileSystemWatcher1Renamed);
			this.fileSystemWatcher1.Deleted += new System.IO.FileSystemEventHandler(this.FileSystemWatcher1Changed);
			this.fileSystemWatcher1.Created += new System.IO.FileSystemEventHandler(this.FileSystemWatcher1Changed);
			this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.FileSystemWatcher1Changed);
			// 
			// MainForm
			// 
			this.AcceptButton = this.buttonConnect;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(434, 462);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(450, 500);
			this.MinimumSize = new System.Drawing.Size(450, 500);
			this.Name = "MainForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "SubShare";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.IO.FileSystemWatcher fileSystemWatcher1;
		private System.Windows.Forms.Button buttonShowAll;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxSearch;
		private System.Windows.Forms.Button buttonSearch;
		private System.Windows.Forms.TextBox textBoxDLDir;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.ToolStripProgressBar DLProgressBar;
		private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
		private System.Windows.Forms.TextBox textBoxPort;
		private System.Windows.Forms.TextBox textBoxIP;
		private System.Windows.Forms.TextBox textBoxDir;
		private System.Windows.Forms.CheckBox checkBoxRemember;
		private System.Windows.Forms.Button buttonConnect;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonDownload;
		private System.Windows.Forms.Button buttonRefresh;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}
