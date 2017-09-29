using System;
using System.IO;
using log4net;
using log4net.Core;
using log4net.Config;
namespace RD.LoggingService
{
 
    //%c �����־��Ϣ���������ȫ�� 
    //%d �����־ʱ�������ڻ�ʱ�䣬Ĭ�ϸ�ʽΪISO8601��Ҳ���������ָ����ʽ�����磺%d{yyy-MM-dd HH:mm:ss }��������ƣ�2002-10-18- 22��10��28 
    //%f �����־��Ϣ������������� 
    //%l �����־�¼��ķ���λ�ã��������־��Ϣ����䴦�������ڵ���ĵڼ��� 
    //%m ���������ָ������Ϣ����log(message)�е�message 
    //%n ���һ���س����з���Windowsƽ̨Ϊ��rn����Unixƽ̨Ϊ��n�� 
    //%p ������ȼ�����DEBUG��INFO��WARN��ERROR��FATAL������ǵ���debug()����ģ���ΪDEBUG���������� 
    //%r �����Ӧ���������������־��Ϣ���ķѵĺ����� 
    //%t �����������־�¼����߳���

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

        #region �¼�
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
