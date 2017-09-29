using System;
using System.Drawing;
using System.Windows.Forms;

namespace RDTools.Common
{
    public partial class FrmBaseOutChild : BaseFormCls
    {
        public FrmBaseOutChild()
        {
            InitializeComponent();
            this.barPublic = bar1;
        }

        public BaseFormCls ChildFrm { get; set; }

        private void FrmBaseOutChild_Load(object sender, EventArgs e)
        {        
            ChildFrm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            panelControl1.Size = new System.Drawing.Size(ChildFrm.Size.Width + 3, ChildFrm.Size.Height + 3);
            panelControl1.Location = new Point(
                (Screen.PrimaryScreen.WorkingArea.Width - panelControl1.Width) /2,
                (Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Height ) - panelControl1.Height) / 2);
            ChildFrm.TopLevel = false;
            ChildFrm.Dock = DockStyle.Fill;
            panelControl1.Controls.Add((Control)ChildFrm);
            panelControl1.Controls[0].Show();
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}
