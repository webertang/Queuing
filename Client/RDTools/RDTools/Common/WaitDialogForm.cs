using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System;
using System.ComponentModel;
using System.Configuration;

namespace RDTools.Common
{
	public class WaitDialogForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
        private string m_Caption = string.Empty;
        private PictureBox pictureBox1;
        private Label label6;
		private string m_Title = string.Empty;

		public WaitDialogForm()
		{
			m_Caption = string.Empty;
			m_Title = (m_Title == string.Empty) ? "正在组织数据，请稍候...." : m_Title;
			InitializeComponent();
			this.ClientSize = new Size(260,50);
            //this.pic.Location = new Point(8, (base.ClientSize.Height / 2) - 0x10);
			this.Show();
			this.Refresh();
		}
		
		public WaitDialogForm(string caption)
		{
            InitializeComponent();
            //this.ClientSize = new Size(260,50);
            //this.pic.Location = new Point(8, (base.ClientSize.Height / 2) - 0x10);
            //this.Show();
            //this.Refresh();

            m_Caption = caption;
            m_Title = (m_Title == string.Empty) ? "数据加载中，请稍候..." : m_Title;

            this.Show();
            this.Refresh();
            pictureBox1.ImageLocation = Application.StartupPath + "\\" + ConfigurationManager.AppSettings["Logo"];
            string version = ConfigurationManager.AppSettings["version"];
            if (version != null && version != string.Empty)
            {
                label6.Text = version;
            }
		}

		public WaitDialogForm(string caption, Size size)
		{
			m_Caption = caption;
			m_Title = (m_Title == string.Empty) ? "正在组织数据，请稍候...." : m_Title;
			InitializeComponent();
			this.ClientSize = size;
            //this.pic.Location = new Point(8, (base.ClientSize.Height / 2) - 0x10);
			this.Show();
			this.Refresh();
		}
		public WaitDialogForm(string caption, string title)
		{
			m_Caption = caption;
			m_Title = title;
			m_Title = (m_Title == string.Empty) ? "正在组织数据，请稍候...." : m_Title;
			InitializeComponent();
			
			this.ClientSize = new Size(260,50);
            //this.pic.Location = new Point(8, (base.ClientSize.Height / 2) - 0x10);
			this.Show();
			this.Refresh();
		}
		public WaitDialogForm(string caption, string title, Size size)
		{
			m_Caption = caption;
			m_Title = title;
			m_Title = (m_Title == string.Empty) ? "正在组织数据，请稍候...." : m_Title;
			InitializeComponent();
			ClientSize = size;
            //this.pic.Location = new Point(8, (base.ClientSize.Height / 2) - 0x10);
			this.Show();
			this.Refresh();
		}


		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(2, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(111, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Gray;
            this.label6.Location = new System.Drawing.Point(2, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(271, 14);
            this.label6.TabIndex = 10;
            this.label6.Text = "Copyright ©2013 RDHIS. All rights reserved.";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WaitDialogForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(273, 154);
            this.ControlBox = false;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "WaitDialogForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.WaitDialogForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.WaitDialogPaint);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		public string Caption
		{
			get
			{
				return m_Caption;
			}
            set
            {
                m_Caption = value;

                this.Refresh();
            }
		}

		private void WaitDialogPaint(object sender, PaintEventArgs e)
		{
			StringAlignment alignment1;

            Rectangle rectangle1 = e.ClipRectangle;
            rectangle1.Inflate(-1, -1);
            StringFormat format1 = new StringFormat();
            format1.LineAlignment = alignment1 = StringAlignment.Center;
            format1.Alignment = alignment1;
            ControlPaint.DrawBorder3D(e.Graphics, rectangle1, Border3DStyle.RaisedInner);
            rectangle1.X += 10;
            rectangle1.Width -= 20;
            rectangle1.Height /= 3;
            rectangle1.Y += 50;// (rectangle1.Height / 2);
            e.Graphics.DrawString(m_Title, new Font("宋体", 11f, FontStyle.Bold), SystemBrushes.WindowText, (RectangleF)rectangle1, format1);
            rectangle1.Y += 30;// rectangle1.Height;
            e.Graphics.DrawString(m_Caption, new Font("宋体", 10f), SystemBrushes.WindowText, (RectangleF)rectangle1, format1);
		}

        private void WaitDialogForm_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = Application.StartupPath + "\\" + ConfigurationManager.AppSettings["Logo"];
            label6.Text = ConfigurationManager.AppSettings["version"];
        }
 

	}
}
