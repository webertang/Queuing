using RDTools.SocketManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace PDTools.SocketManager
{
    public class TimeOutMessage
    {

        //MessageSender messageSender = new MessageSender();//语音消息发送
        //private void Work(object obj)
        //{
        //    MessageEventArgs e = (MessageEventArgs)obj;
        //    //叫号内容保存
        //    string strMessage = "";
        //    strMessage = e.Message + Environment.NewLine;
        //    string path = @"c:\TTSContent.txt";//写入内容文件的径
        //    File.AppendAllText(path, strMessage, Encoding.UTF8);//写入内容 // 根据路径出内容

        //    //0                 1(0       1               2                   3           4      5
        //    //IP1|IP2|IP3|……@rowID|QueueNumber+Name|Departments(科室)|Office(诊室)|Doctor|SoundCardID
        //    string[] iCardNum = e.Message.Split('@');
        //    string[] message = iCardNum[1].Split('|');
        //    //请!QueueNumber+Name到Office就诊!
        //    string sendText = "请!" + message[1] + "到" + message[3] + "就诊!";

        //    try
        //    {
        //        //发送声音
        //        messageSender.Send(Convert.ToInt32(message[5]), sendText);
        //        //发送屏幕内容


        //        PDTools.TTSInterface screenMessage = new PDTools.TTSInterface();
        //        //解决掉
        //        //screenMessage.sendMessage(message, 1, Convert.ToInt32(message[0]) - 1, Convert.ToInt32(iCardNum[0]));
        //        NewRecMessage newRecMessage = new NewRecMessage();
        //        CallWithTimeout(send, 10, newRecMessage);

        //    }
        //    catch (Exception)
        //    {
        //        File.AppendAllText(path, "异常数据：" + strMessage, Encoding.UTF8);//写入内容 // 根据路径出内容
        //    }
        //}
        //private void send(NewRecMessage newRecMessage)
        //{
        //    //PDTools.TTSInterface screenMessage = new PDTools.TTSInterface();
        //    //screenMessage.sendMessage();
        //}
        //static void CallWithTimeout(Action<NewRecMessage> action, int timeoutMilliseconds, NewRecMessage newRecMessage)
        //{
        //    Thread threadToKill = null;
        //    Action wrappedAction = () =>
        //    {
        //        threadToKill = Thread.CurrentThread;

        //        action(newRecMessage);
        //    };

        //    IAsyncResult result = wrappedAction.BeginInvoke(null, null);
        //    if (result.AsyncWaitHandle.WaitOne(timeoutMilliseconds))
        //    {
        //        wrappedAction.EndInvoke(result);
        //    }
        //    else
        //    {
        //        threadToKill.Abort();
        //    }
        //}
    }


}
