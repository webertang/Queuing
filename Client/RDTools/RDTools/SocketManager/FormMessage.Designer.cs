namespace SystemFramework.SocketManager
{
    partial class FormMessage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMessage));
            this.listMessage = new System.Windows.Forms.ListBox();
            this.messageItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.labClose = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.labHistoryMessage = new System.Windows.Forms.LinkLabel();
            this.timerClose = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.messageItemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // listMessage
            // 
            this.listMessage.DataSource = this.messageItemBindingSource;
            this.listMessage.DisplayMember = "Message";
            this.listMessage.Font = new System.Drawing.Font("宋体", 10F);
            this.listMessage.FormattingEnabled = true;
            this.listMessage.HorizontalScrollbar = true;
            this.listMessage.Location = new System.Drawing.Point(11, 22);
            this.listMessage.Name = "listMessage";
            this.listMessage.Size = new System.Drawing.Size(292, 108);
            this.listMessage.TabIndex = 0;
            this.listMessage.ValueMember = "ID";
            // 
            // messageItemBindingSource
            // 
            this.messageItemBindingSource.DataSource = typeof(SystemFramework.SocketManager.MessageItem);
            // 
            // labClose
            // 
            this.labClose.AutoSize = true;
            this.labClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labClose.Location = new System.Drawing.Point(299, 4);
            this.labClose.Name = "labClose";
            this.labClose.Size = new System.Drawing.Size(11, 12);
            this.labClose.TabIndex = 1;
            this.labClose.Text = "X";
            this.labClose.Click += new System.EventHandler(this.label1_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // labHistoryMessage
            // 
            this.labHistoryMessage.AutoSize = true;
            this.labHistoryMessage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labHistoryMessage.Location = new System.Drawing.Point(11, 6);
            this.labHistoryMessage.Name = "labHistoryMessage";
            this.labHistoryMessage.Size = new System.Drawing.Size(53, 12);
            this.labHistoryMessage.TabIndex = 2;
            this.labHistoryMessage.TabStop = true;
            this.labHistoryMessage.Text = "历史消息";
            this.labHistoryMessage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.labHistoryMessage_LinkClicked);
            // 
            // timerClose
            // 
            this.timerClose.Interval = 1000;
            this.timerClose.Tick += new System.EventHandler(this.timerClose_Tick);
            // 
            // FormMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 138);
            this.Controls.Add(this.listMessage);
            this.Controls.Add(this.labClose);
            this.Controls.Add(this.labHistoryMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMessage";
            this.Opacity = 0.9;
            this.ShowInTaskbar = false;
            this.Text = "FormMessage";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormMessage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.messageItemBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labClose;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.LinkLabel labHistoryMessage;
        private System.Windows.Forms.Timer timerClose;
        internal System.Windows.Forms.ListBox listMessage;
        private System.Windows.Forms.BindingSource messageItemBindingSource;
    }
}