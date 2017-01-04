using RD.Service.Class;
using RDTools.Common;
using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace RD.Service
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool blnIsRunning;
            Mutex mutexApp = new Mutex(false, Assembly.GetExecutingAssembly().FullName, out   blnIsRunning);
            if (!blnIsRunning && AppConfig.GetAppSetting("SingleApp").Equals("true"))
                return;

            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
