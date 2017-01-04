using System.Reflection;
using System.Threading;

namespace RD.Service.Class
{
    public static class Config
    {
        public static bool SingleRun(string sSingleApp)
        {
            //=====创建互斥体法：=====
            bool blnIsRunning;
            Mutex mutexApp = new Mutex(false, Assembly.GetExecutingAssembly().FullName, out   blnIsRunning);
            if (!blnIsRunning && sSingleApp.Equals("true"))
            {
                return false;
            }

            //保证同时只有一个客户端在运行   
            //System.Threading.Mutex mutexMyapplication = new System.Threading.Mutex(false, "OnePorcess.exe");
            //if (!mutexMyapplication.WaitOne(100, false))
            //{
            //    MessageBox.Show("程序" + Application.ProductName + "已经运行！", Application.ProductName,
            //    MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //=====判断进程法：(修改程序名字后依然能执行)=====
            //Process current = Process.GetCurrentProcess();
            //Process[] processes = Process.GetProcessesByName(current.ProcessName);
            //foreach (Process process in processes)
            //{
            //    if (process.Id != current.Id)
            //    {
            //        if (process.MainModule.FileName
            //        == current.MainModule.FileName)
            //        {
            //            MessageBox.Show("程序已经运行！", Application.ProductName,
            //            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            return;
            //        }
            //    }
            //}    
            return true;
        }
    }
}
