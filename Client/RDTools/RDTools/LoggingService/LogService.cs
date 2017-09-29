using System;
using System.IO;
using log4net;
using log4net.Core;
using log4net.Config;
namespace RD.LoggingService
{
 
    //%c 输出日志信息所属的类的全名 
    //%d 输出日志时间点的日期或时间，默认格式为ISO8601，也可以在其后指定格式，比如：%d{yyy-MM-dd HH:mm:ss }，输出类似：2002-10-18- 22：10：28 
    //%f 输出日志信息所属的类的类名 
    //%l 输出日志事件的发生位置，即输出日志信息的语句处于它所在的类的第几行 
    //%m 输出代码中指定的信息，如log(message)中的message 
    //%n 输出一个回车换行符，Windows平台为“rn”，Unix平台为“n” 
    //%p 输出优先级，即DEBUG，INFO，WARN，ERROR，FATAL。如果是调用debug()输出的，则为DEBUG，依此类推 
    //%r 输出自应用启动到输出该日志信息所耗费的毫秒数 
    //%t 输出产生该日志事件的线程名

	public class LogService
	{
		private static ILog log;// = ;
		private static LogService _instance=new LogService();
		private LogService()
		{
            log4net.Config.XmlConfigurator.Configure(new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));
            //log4net.Config.XmlConfigurator.Configure(new FileInfo("LogConfig.xml"));
		}

		public static LogService Createinstance(string logName)
		{
			log=LogManager.GetLogger(logName);
            return _instance;		
		}
       
		public void Debug(object message)
		{
			log.Debug(message);
		}
		
		public void DebugFormatted(string format, params object[] args)
		{
			log.DebugFormat(format, args);
		}
		
		public void Info(object message)
		{
			log.Info(message);
		}
		
		public void InfoFormatted(string format, params object[] args)
		{
			log.InfoFormat(format, args);
		}
		
		public void Warn(object message)
		{
			log.Warn(message);
		}
		
		public void Warn(object message, Exception exception)
		{
			log.Warn(message, exception);
		}
		
		public void WarnFormatted(string format, params object[] args)
		{
			log.WarnFormat(format, args);
		}
		
		public void Error(object message)
		{
			log.Error(message);
		}
		
		public void Error(object message, Exception exception)
		{
			log.Error(message, exception);
		}
		
		public void ErrorFormatted(string format, params object[] args)
		{
			log.ErrorFormat(format, args);
		}
		
		public void Fatal(object message)
		{
			log.Fatal(message);
		}
		
		public void Fatal(object message, Exception exception)
		{
			log.Fatal(message, exception);
		}
		
		public void FatalFormatted(string format, params object[] args)
		{
			log.FatalFormat(format, args);
		}
		
		public bool IsDebugEnabled 
		{
			get 
			{
				return log.IsDebugEnabled;
			}
		}
		
		public bool IsInfoEnabled 
		{
			get 
			{
				return log.IsInfoEnabled;
			}
		}
		
		public bool IsWarnEnabled 
		{
			get 
			{
				return log.IsWarnEnabled;
			}
		}
		
		public bool IsErrorEnabled 
		{
			get 
			{
				return log.IsErrorEnabled;
			}
		}
		
		public bool IsFatalEnabled 
		{
			get 
			{
				return log.IsFatalEnabled;
			}
        }

        #region 新加
        public static void GlobalDebugMessage(string msg)
        {
            LogManager.GetLogger("DebugLogger").Debug(msg);
        }

        public static void GlobalFatalMessage(string msg)
        {
            LogManager.GetLogger("FatalLogger").Fatal(msg);
        }

        public static void GlobalWarnMessage(string msg)
        {
            LogManager.GetLogger("WarnLogger").Warn(msg);
        }

        public static void GlobalErrorMessage(string msg)
        {
            LogManager.GetLogger("ErrorLogger").Error(msg);
        }

        public static void GlobalInfoMessage(string msg)
        {
            LogManager.GetLogger("InfoLogger").Info(msg);
        }
        #endregion
    }
}
