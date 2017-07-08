using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace CSharpActiveX
{

    //class TimeOutSocket
    //{
    //    private static bool IsConnectionSuccessful = false;
    //    private static Exception socketexception;
    //    private static ManualResetEvent TimeoutObject = new ManualResetEvent(false);
    //    public static TcpClient TryConnect(IPEndPoint remoteEndPoint, int timeoutMiliSecond)
    //    {
    //        TimeoutObject.Reset();
    //        socketexception = null;
    //        string serverip = Convert.ToString(remoteEndPoint.Address);
    //        int serverport = remoteEndPoint.Port;
    //        TcpClient tcpclient = new TcpClient();

    //        tcpclient.BeginConnect(serverip, serverport,
    //            new AsyncCallback(CallBackMethod), tcpclient);
    //        if (TimeoutObject.WaitOne(timeoutMiliSecond, false))
    //        {
    //            if (IsConnectionSuccessful)
    //            {
    //                return tcpclient;
    //            }
    //            else
    //            {
    //                throw socketexception;
    //            }
    //        }
    //        else
    //        {
    //            tcpclient.Close();
    //            throw new TimeoutException("TimeOut Exception");
    //        }
    //    }
    //    private static void CallBackMethod(IAsyncResult asyncresult)
    //    {
    //        try
    //        {
    //            IsConnectionSuccessful = false;
    //            TcpClient tcpclient = asyncresult.AsyncState as TcpClient;

    //            if (tcpclient.Client != null)
    //            {
    //                tcpclient.EndConnect(asyncresult);
    //                IsConnectionSuccessful = true;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            IsConnectionSuccessful = false;
    //            socketexception = ex;
    //        }
    //        finally
    //        {
    //            TimeoutObject.Set();
    //        }
    //    }
    //}
}
