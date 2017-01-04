namespace PDTools
{
    partial class MainFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.bgwRead = new System.ComponentModel.BackgroundWorker();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnTTS = new System.Windows.Forms.Button();
            this.bgwTTS1 = new System.ComponentModel.BackgroundWorker();
            this.mediaSlider1 = new MediaSlider.MediaSlider();
            this.btnPlay = new System.Windows.Forms.Button();
            this.lbMessage = new System.Windows.Forms.Label();
            this.bgwMessage = new System.ComponentModel.BackgroundWorker();
            this.cmbMp3Size = new System.Windows.Forms.ComboBox();
            this.myNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.MyNotifyIconcontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HideFormToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAbout = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.MyNotifyIconcontextMenuStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bgwRead
            // 
            this.bgwRead.WorkerSupportsCancellation = true;
            this.bgwRead.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwRead_DoWork);
            this.bgwRead.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwRead_RunWorkerCompleted);
            // 
            // richTextBox1
            // 
            this.richTextBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.richTextBox1.Location = new System.Drawing.Point(39, 26);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(500, 217);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.SelectionChanged += new System.EventHandler(this.richTextBox1_SelectionChanged);
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyToolStripMenuItem,
            this.PasteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(117, 48);
            // 
            // CopyToolStripMenuItem
            // 
            this.CopyToolStripMenuItem.Enabled = false;
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.CopyToolStripMenuItem.Text = "复制(&C)";
            this.CopyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // PasteToolStripMenuItem
            // 
            this.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem";
            this.PasteToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.PasteToolStripMenuItem.Text = "粘贴(&V)";
            this.PasteToolStripMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItem_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(14, 47);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 1;
            this.btnRead.Text = "朗读";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnTTS
            // 
            this.btnTTS.Location = new System.Drawing.Point(107, 47);
            this.btnTTS.Name = "btnTTS";
            this.btnTTS.Size = new System.Drawing.Size(73, 23);
            this.btnTTS.TabIndex = 2;
            this.btnTTS.Text = "转换成MP3";
            this.btnTTS.UseVisualStyleBackColor = true;
            this.btnTTS.Click += new System.EventHandler(this.btnTTS_Click);
            // 
            // bgwTTS1
            // 
            this.bgwTTS1.WorkerSupportsCancellation = true;
            this.bgwTTS1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwTTS1_DoWork);
            this.bgwTTS1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwTTS1_RunWorkerCompleted);
            // 
            // mediaSlider1
            // 
            this.mediaSlider1.Animated = false;
            this.mediaSlider1.AnimationSize = 0.2F;
            this.mediaSlider1.AnimationSpeed = MediaSlider.MediaSlider.AnimateSpeed.Normal;
            this.mediaSlider1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.mediaSlider1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.mediaSlider1.BackColor = System.Drawing.Color.DimGray;
            this.mediaSlider1.BackGroundImage = null;
            this.mediaSlider1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mediaSlider1.ButtonAccentColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.mediaSlider1.ButtonBorderColor = System.Drawing.Color.Black;
            this.mediaSlider1.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.mediaSlider1.ButtonCornerRadius = ((uint)(4u));
            this.mediaSlider1.ButtonSize = new System.Drawing.Size(14, 14);
            this.mediaSlider1.ButtonStyle = MediaSlider.MediaSlider.ButtonType.PointerDownLeft;
            this.mediaSlider1.ContextMenuStrip = null;
            this.mediaSlider1.LargeChange = 1;
            this.mediaSlider1.Location = new System.Drawing.Point(14, 17);
            this.mediaSlider1.Margin = new System.Windows.Forms.Padding(0);
            this.mediaSlider1.Maximum = 8;
            this.mediaSlider1.Minimum = -8;
            this.mediaSlider1.Name = "mediaSlider1";
            this.mediaSlider1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.mediaSlider1.ShowButtonOnHover = false;
            this.mediaSlider1.Size = new System.Drawing.Size(277, 27);
            this.mediaSlider1.SliderFlyOut = MediaSlider.MediaSlider.FlyOutStyle.None;
            this.mediaSlider1.SmallChange = 1;
            this.mediaSlider1.SmoothScrolling = false;
            this.mediaSlider1.TabIndex = 3;
            this.mediaSlider1.TickColor = System.Drawing.Color.DarkGray;
            this.mediaSlider1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.mediaSlider1.TickType = MediaSlider.MediaSlider.TickMode.Standard;
            this.mediaSlider1.TrackBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mediaSlider1.TrackDepth = 6;
            this.mediaSlider1.TrackFillColor = System.Drawing.Color.Transparent;
            this.mediaSlider1.TrackProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(101)))), ((int)(((byte)(188)))));
            this.mediaSlider1.TrackShadow = false;
            this.mediaSlider1.TrackShadowColor = System.Drawing.Color.DarkGray;
            this.mediaSlider1.TrackStyle = MediaSlider.MediaSlider.TrackType.Value;
            this.mediaSlider1.Value = 0;
            this.mediaSlider1.Scrolled += new MediaSlider.MediaSlider.ScrollDelegate(this.mediaSlider1_Scrolled);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(14, 81);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 23);
            this.btnPlay.TabIndex = 4;
            this.btnPlay.Text = "暂停";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // lbMessage
            // 
            this.lbMessage.AutoSize = true;
            this.lbMessage.Location = new System.Drawing.Point(105, 92);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(53, 12);
            this.lbMessage.TabIndex = 5;
            this.lbMessage.Text = "字数:0个";
            // 
            // bgwMessage
            // 
            this.bgwMessage.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwMessage_DoWork);
            // 
            // cmbMp3Size
            // 
            this.cmbMp3Size.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMp3Size.FormattingEnabled = true;
            this.cmbMp3Size.Items.AddRange(new object[] {
            "5000字",
            "10000字",
            "20000字",
            "50000字",
            "80000字"});
            this.cmbMp3Size.Location = new System.Drawing.Point(195, 50);
            this.cmbMp3Size.Name = "cmbMp3Size";
            this.cmbMp3Size.Size = new System.Drawing.Size(96, 20);
            this.cmbMp3Size.TabIndex = 6;
            this.cmbMp3Size.SelectedIndexChanged += new System.EventHandler(this.cmbMp3Size_SelectedIndexChanged);
            // 
            // myNotifyIcon
            // 
            this.myNotifyIcon.ContextMenuStrip = this.MyNotifyIconcontextMenuStrip;
            this.myNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("myNotifyIcon.Icon")));
            this.myNotifyIcon.Text = "TTS";
            this.myNotifyIcon.DoubleClick += new System.EventHandler(this.myNotifyIcon_DoubleClick);
            // 
            // MyNotifyIconcontextMenuStrip
            // 
            this.MyNotifyIconcontextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowFormToolStripMenuItem,
            this.HideFormToolStripMenuItem1,
            this.aboutToolStripMenuItem,
            this.ExitToolStripMenuItem2});
            this.MyNotifyIconcontextMenuStrip.Name = "MyNotifyIconcontextMenuStrip";
            this.MyNotifyIconcontextMenuStrip.Size = new System.Drawing.Size(142, 92);
            // 
            // ShowFormToolStripMenuItem
            // 
            this.ShowFormToolStripMenuItem.Name = "ShowFormToolStripMenuItem";
            this.ShowFormToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.ShowFormToolStripMenuItem.Text = "显示窗口(&S)";
            this.ShowFormToolStripMenuItem.Visible = false;
            this.ShowFormToolStripMenuItem.Click += new System.EventHandler(this.ShowFormToolStripMenuItem_Click);
            // 
            // HideFormToolStripMenuItem1
            // 
            this.HideFormToolStripMenuItem1.Name = "HideFormToolStripMenuItem1";
            this.HideFormToolStripMenuItem1.Size = new System.Drawing.Size(141, 22);
            this.HideFormToolStripMenuItem1.Text = "隐藏窗口(&H)";
            this.HideFormToolStripMenuItem1.Click += new System.EventHandler(this.HideFormToolStripMenuItem1_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.aboutToolStripMenuItem.Text = "关于(&A)";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // ExitToolStripMenuItem2
            // 
            this.ExitToolStripMenuItem2.Name = "ExitToolStripMenuItem2";
            this.ExitToolStripMenuItem2.Size = new System.Drawing.Size(141, 22);
            this.ExitToolStripMenuItem2.Text = "退出(&E)";
            this.ExitToolStripMenuItem2.Click += new System.EventHandler(this.ExitToolStripMenuItem2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAbout);
            this.groupBox1.Controls.Add(this.mediaSlider1);
            this.groupBox1.Controls.Add(this.btnRead);
            this.groupBox1.Controls.Add(this.cmbMp3Size);
            this.groupBox1.Controls.Add(this.btnPlay);
            this.groupBox1.Controls.Add(this.lbMessage);
            this.groupBox1.Controls.Add(this.btnTTS);
            this.groupBox1.Location = new System.Drawing.Point(111, 262);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(313, 128);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作区";
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(195, 80);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(75, 23);
            this.btnAbout.TabIndex = 7;
            this.btnAbout.Text = "关于";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(442, 308);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(574, 402);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.richTextBox1);
            this.MaximizeBox = false;
            this.Name = "MainFrm";
            this.Text = "TTS 文字转语音工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.MyNotifyIconcontextMenuStrip.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker  bgwRead;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnTTS;
        private System.ComponentModel.BackgroundWorker bgwTTS1;
        private MediaSlider.MediaSlider mediaSlider1;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Label lbMessage;
        private System.ComponentModel.BackgroundWorker bgwMessage;
        private System.Windows.Forms.ComboBox cmbMp3Size;
        private System.Windows.Forms.NotifyIcon myNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip MyNotifyIconcontextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ShowFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HideFormToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PasteToolStripMenuItem;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button button1;
    }
}

