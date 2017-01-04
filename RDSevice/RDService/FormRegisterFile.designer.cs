namespace RD.Service
{
    partial class FormRegisterFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRegisterFile));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnCreate = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnCheck = new DevExpress.XtraEditors.SimpleButton();
            this.txtDataBaseName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtLocalIP = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtLocalName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.saveFile = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataBaseName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLocalIP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLocalName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnCreate);
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.btnCheck);
            this.panelControl1.Controls.Add(this.txtDataBaseName);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.txtLocalIP);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.txtLocalName);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.txtName);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.LookAndFeel.SkinName = "Office 2007 Green";
            this.panelControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(341, 221);
            this.panelControl1.TabIndex = 0;
            // 
            // btnCreate
            // 
            this.btnCreate.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Appearance.Options.UseFont = true;
            this.btnCreate.Location = new System.Drawing.Point(35, 165);
            this.btnCreate.LookAndFeel.SkinName = "Office 2007 Green";
            this.btnCreate.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(78, 25);
            this.btnCreate.TabIndex = 15;
            this.btnCreate.Text = "序列号(&C)";
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(237, 165);
            this.btnClose.LookAndFeel.SkinName = "Office 2007 Green";
            this.btnClose.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 25);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "退  出(&X)";
            // 
            // btnCheck
            // 
            this.btnCheck.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.Appearance.Options.UseFont = true;
            this.btnCheck.Location = new System.Drawing.Point(152, 165);
            this.btnCheck.LookAndFeel.SkinName = "Office 2007 Green";
            this.btnCheck.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(78, 25);
            this.btnCheck.TabIndex = 16;
            this.btnCheck.Text = "注  册(&R)";
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // txtDataBaseName
            // 
            this.txtDataBaseName.Enabled = false;
            this.txtDataBaseName.Location = new System.Drawing.Point(89, 123);
            this.txtDataBaseName.Name = "txtDataBaseName";
            this.txtDataBaseName.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtDataBaseName.Properties.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDataBaseName.Properties.Appearance.Options.UseBackColor = true;
            this.txtDataBaseName.Properties.Appearance.Options.UseForeColor = true;
            this.txtDataBaseName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.txtDataBaseName.Properties.LookAndFeel.SkinName = "Office 2007 Green";
            this.txtDataBaseName.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txtDataBaseName.Size = new System.Drawing.Size(226, 21);
            this.txtDataBaseName.TabIndex = 13;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(35, 128);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 12);
            this.labelControl5.TabIndex = 19;
            this.labelControl5.Text = "数据库名";
            // 
            // txtLocalIP
            // 
            this.txtLocalIP.Enabled = false;
            this.txtLocalIP.Location = new System.Drawing.Point(89, 89);
            this.txtLocalIP.Name = "txtLocalIP";
            this.txtLocalIP.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtLocalIP.Properties.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtLocalIP.Properties.Appearance.Options.UseBackColor = true;
            this.txtLocalIP.Properties.Appearance.Options.UseForeColor = true;
            this.txtLocalIP.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.txtLocalIP.Properties.LookAndFeel.SkinName = "Office 2007 Green";
            this.txtLocalIP.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txtLocalIP.Size = new System.Drawing.Size(226, 21);
            this.txtLocalIP.TabIndex = 12;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(35, 94);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 12);
            this.labelControl4.TabIndex = 18;
            this.labelControl4.Text = "本机  IP";
            // 
            // txtLocalName
            // 
            this.txtLocalName.Enabled = false;
            this.txtLocalName.Location = new System.Drawing.Point(89, 55);
            this.txtLocalName.Name = "txtLocalName";
            this.txtLocalName.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtLocalName.Properties.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtLocalName.Properties.Appearance.Options.UseBackColor = true;
            this.txtLocalName.Properties.Appearance.Options.UseForeColor = true;
            this.txtLocalName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.txtLocalName.Properties.LookAndFeel.SkinName = "Office 2007 Green";
            this.txtLocalName.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txtLocalName.Size = new System.Drawing.Size(226, 21);
            this.txtLocalName.TabIndex = 10;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(35, 60);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 12);
            this.labelControl3.TabIndex = 14;
            this.labelControl3.Text = "本机名称";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(35, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 12);
            this.labelControl1.TabIndex = 11;
            this.labelControl1.Text = "用 户 名";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(89, 21);
            this.txtName.Name = "txtName";
            this.txtName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.txtName.Properties.LookAndFeel.SkinName = "Office 2007 Green";
            this.txtName.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txtName.Size = new System.Drawing.Size(226, 21);
            this.txtName.TabIndex = 9;
            // 
            // openFile
            // 
            this.openFile.DefaultExt = "key";
            this.openFile.Filter = "秘钥|*.key";
            // 
            // saveFile
            // 
            this.saveFile.Filter = "序列号|*.ser";
            this.saveFile.Title = "选择保存位置";
            // 
            // FormRegisterFile
            // 
            this.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(341, 221);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Office 2007 Green";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRegisterFile";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "注 册";
            this.Load += new System.EventHandler(this.FormRegisterFile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataBaseName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLocalIP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLocalName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.SaveFileDialog saveFile;
        private DevExpress.XtraEditors.SimpleButton btnCreate;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnCheck;
        private DevExpress.XtraEditors.TextEdit txtDataBaseName;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtLocalIP;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtLocalName;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtName;
    }
}