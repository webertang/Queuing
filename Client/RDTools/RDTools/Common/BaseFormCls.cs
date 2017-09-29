using System.Windows.Forms;

namespace RDTools.Common
{
    /// <summary>
    /// 各子系统主窗体基类
    /// </summary>
    public class BaseFormCls : Form
    {
        public BaseFormCls()
        {

        }

        private DevExpress.XtraBars.Bar _bar;
        public DevExpress.XtraBars.Bar barPublic
        {
            get
            {
                //_bar.Visible = false;
                return _bar;
            }
            set { _bar = value; }
        }

        public string TabText { get; set; }

        private bool isOutFrm=false;
        public bool IsOutFrm
        {
            get { return isOutFrm; }
            set { isOutFrm = value; }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseFormCls
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "BaseFormCls";
            this.ResumeLayout(false);

        }

        protected Form SelectFromMain(Form form1, Form form2, Form form3)
        {

            if (form1 != null)
            {
                if (form1.Name == "FrmMain")
                {
                    return form1;
                }
                else
                {
                    return SelectFromMain(form1.MdiParent as Form, form1.Parent as Form, form1.Owner as Form);
                }
            }
            if (form2 != null)
            {
                if (form2.Name == "FrmMain")
                {
                    return form2;
                }
                else
                {
                    return SelectFromMain(form1.MdiParent as Form, form1.Parent as Form, form1.Owner as Form);
                }
            }
            if (form3 != null)
            {
                if (form3.Name == "FrmMain")
                {
                    return form3;
                }
                else
                {
                    return SelectFromMain(form1.MdiParent as Form, form1.Parent as Form, form1.Owner as Form);
                }
            }
            return null;

        }

        protected void FormMainSend(NewSocketManager.NewMessage mse)
        {
            Form f = SelectFromMain(this.Parent as Form, this.MdiParent as Form, this.Owner as Form);
            if (f != null)
            {
                f.GetType().GetMethod("SendMes").Invoke(f, new object[] { mse });
            }
        }

        protected void FormMainSend1(NewSocketManager.NewMessage mse)
        {
            Form f = SelectFromMain(this.Parent as Form, this.MdiParent as Form, this.Owner as Form);
            if (f != null)
            {
                f.GetType().GetMethod("SendMes1").Invoke(f, new object[] { mse });
            }
        }



        private void SetIDShow()
        {
     
        }
    }
}
