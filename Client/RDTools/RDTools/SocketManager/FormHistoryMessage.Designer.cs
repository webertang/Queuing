namespace SystemFramework.SocketManager
{
    partial class FormHistoryMessage
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
            this.listMessage = new System.Windows.Forms.ListBox();
            this.dateQuery = new System.Windows.Forms.DateTimePicker();
            this.btnQuery = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listMessage
            // 
            this.listMessage.FormattingEnabled = true;
            this.listMessage.HorizontalScrollbar = true;
            this.listMessage.ItemHeight = 12;
            this.listMessage.Location = new System.Drawing.Point(12, 42);
            this.listMessage.Name = "listMessage";
            this.listMessage.Size = new System.Drawing.Size(474, 256);
            this.listMessage.TabIndex = 0;
            // 
            // dateQuery
            // 
            this.dateQuery.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateQuery.Location = new System.Drawing.Point(13, 13);
            this.dateQuery.Name = "dateQuery";
            this.dateQuery.Size = new System.Drawing.Size(87, 21);
            this.dateQuery.TabIndex = 1;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(411, 11);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 2;
            this.btnQuery.Text = "查  看";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // FormHistoryMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 313);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.dateQuery);
            this.Controls.Add(this.listMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormHistoryMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "历史消息";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listMessage;
        private System.Windows.Forms.DateTimePicker dateQuery;
        private System.Windows.Forms.Button btnQuery;
    }
}