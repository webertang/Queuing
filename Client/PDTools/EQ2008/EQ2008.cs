using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PDTools.EQ2008
{
    public class EQCtroller
    {
        //删除所有节目
        [DllImport("EQ2008_Dll.dll", CharSet = CharSet.Ansi)]
        public static extern Boolean User_DelAllProgram(int CardNum);
        //添加节目
        [DllImport("EQ2008_Dll.dll", CharSet = CharSet.Ansi)]
        public static extern int User_AddProgram(int CardNum, Boolean bWaitToEnd, int iPlayTime);
        //添加文本区
        [DllImport("EQ2008_Dll.dll", CharSet = CharSet.Ansi)]
        public static extern int User_AddText(int CardNum, ref User_Text pText, int iProgramIndex);
        //发送数据
        [DllImport("EQ2008_Dll.dll", CharSet = CharSet.Ansi)]
        public static extern Boolean User_SendToScreen(int CardNum);

        //实时建立连接
        [DllImport("EQ2008_Dll.dll", CharSet = CharSet.Ansi)]
        public static extern Boolean User_RealtimeConnect(int CardNum);
        //实时发送文本
        [DllImport("EQ2008_Dll.dll", CharSet = CharSet.Ansi)]
        public static extern Boolean User_RealtimeSendText(int CardNum, int x, int y, int iWidth, int iHeight, string strText, ref User_FontSet pFontInfo);
        //实时关闭连接
        [DllImport("EQ2008_Dll.dll", CharSet = CharSet.Ansi)]
        public static extern Boolean User_RealtimeDisConnect(int CardNum);

        /// <summary>
        /// 向屏幕发送消息
        /// </summary>
        /// <param name="sendContent">发送内容</param>
        /// <param name="screenWidth">更新区域宽度，更新高度固定为16</param>
        ///<param name="rowId">文字所显示的行号</param>
        ///<param name="columnId">文字起始位置,从第几个字开始写入内容</param>
        /// <returns></returns>
        public string sendMessage(string[] sendContent, int screenWidth, int Y, int X,int iCardID,int iFontSize)
        {
            //连接
            if (!User_RealtimeConnect(iCardID))
            {
                return "连接实时通信失败！";
            }
            int i = 0;
            do
            {
                if (sendContent[i].Length > 0)
                {
                    //发送文本
                    int iX = X * 16;    //从屏幕最左边开始显示文字
                    int iY = (i + Y) * 16; //新行位置
                    int iW = 16 * screenWidth;   //文字所在区域宽度
                    int iH = 16;   //行高字体12号高度
                    string strText = sendContent[i];        //消息内容
                    User_FontSet FontInfo = new User_FontSet();

                    FontInfo.bFontBold = false;
                    FontInfo.bFontItaic = false;
                    FontInfo.bFontUnderline = false;
                    //if (screenWidth == 10 && i == 1)
                    if (i % 2 != 0)
                    {
                        FontInfo.colorFont = 0xFFFF;
                    }
                    else
                    {
                        FontInfo.colorFont = 0x00FF00;
                    }
                    //if ((sendContent[i].IndexOf("就诊")) != -1)
                    //    FontInfo.colorFont = 0x00FF00;
                    FontInfo.iFontSize = iFontSize;
                    FontInfo.strFontName = "宋体";
                    FontInfo.iAlignStyle = 0;
                    FontInfo.iVAlignerStyle = 0;
                    FontInfo.iRowSpace = 0;

                    if (!User_RealtimeSendText(iCardID, iX, iY, iW, iH, strText, ref FontInfo))
                    {
                        return "发送实时文本失败！";
                    }
                    System.Threading.Thread.Sleep(500);
                }
                i++;
            } while (!(i >= sendContent.Count()));

            //关闭连接
            if (!User_RealtimeDisConnect(iCardID))
            {
                return "关闭实时通信失败！";
            }
            return "成功";
        }

        /// <summary>
        /// 向屏幕发送消息内容渐变颜色
        /// </summary>
        /// <param name="sendContent">发送内容</param>
        /// <param name="screenWidth">更新区域宽度，更新高度固定为16</param>
        ///<param name="rowId">文字所显示的行号</param>
        ///<param name="columnId">文字起始位置,从第几个字开始写入内容</param>
        ///<param name="columnId">屏幕控制卡地址</param>
        /// <returns></returns>
        public string sendMessageChange(string[] sendContent, int iW, int Y, int iX, int iCardNum, int iFontSize)
        {
            try
            {
                //连接
                if (!User_RealtimeConnect(iCardNum))
                {

                    File.AppendAllText(@"Error.log", "异常数据：" + iCardNum + "号地址,连接实时通信失败！" + Environment.NewLine
                                        , Encoding.UTF8);//写入内容 // 根据路径出内容
                    return "连接实时通信失败！";
                }
                int i = 0;
                do
                {
                    if (sendContent[i].Length > 0)
                    {
                        //发送文本
                        //int iX = X * 16;    //从屏幕最左边开始显示文字
                        int iY = (i + Y) * 16; //新行位置
                        //int iW = 16 * screenWidth;   //文字所在区域宽度
                        int iH = 12;   //行高字体12号高度
                        string strText = sendContent[i];        //消息内容
                        User_FontSet FontInfo = new User_FontSet();

                        FontInfo.bFontBold = false;
                        FontInfo.bFontItaic = false;
                        FontInfo.bFontUnderline = false;
                        if (Y % 2 == 0)
                        {
                            FontInfo.colorFont = 0xFFFF;
                        }
                        else
                        {
                            FontInfo.colorFont = 0x00FF00;
                        }
                        FontInfo.iFontSize = iFontSize;
                        FontInfo.strFontName = "宋体";
                        FontInfo.iAlignStyle = 0;
                        FontInfo.iVAlignerStyle = 0;
                        FontInfo.iRowSpace = 0;

                        if (!User_RealtimeSendText(iCardNum, iX, iY + 1, iW, iH, strText, ref FontInfo))
                        {
                            return "发送实时文本失败！";
                        }
                        //System.Threading.Thread.Sleep(1000);
                    }
                    i++;
                } while (!(i >= sendContent.Count()));

                //关闭连接
                if (!User_RealtimeDisConnect(iCardNum))
                {
                    return "关闭实时通信失败！";
                }
                return "成功";
            }
            catch (Exception)
            {
                return "错误";

            }
        }

        public string scrollMessage(string sendContent, int screenWidth, int beginRow)
        {

            int iProgramIndex, iCardNum = 2;//节目号,卡地址(屏幕配置编号)
            //1.删除历史节目
            User_DelAllProgram(iCardNum);
            //2.新增节目
            iProgramIndex = User_AddProgram(iCardNum, false, 10);

            //3.添加文本
            User_Text Text = new User_Text();

            Text.BkColor = 0;
            Text.chContent = sendContent;

            Text.PartInfo.FrameColor = 0;
            Text.PartInfo.iFrameMode = 0;
            Text.PartInfo.iHeight = 16;
            Text.PartInfo.iWidth = screenWidth;//屏幕宽度
            Text.PartInfo.iX = 0;
            Text.PartInfo.iY = beginRow * 16;//滚动字幕，文字显示所在行

            Text.FontInfo.bFontBold = false;
            Text.FontInfo.bFontItaic = false;
            Text.FontInfo.bFontUnderline = false;
            Text.FontInfo.colorFont = 0xFF;
            Text.FontInfo.iFontSize = 12;
            Text.FontInfo.strFontName = "宋体";
            Text.FontInfo.iAlignStyle = 1;
            Text.FontInfo.iVAlignerStyle = 0;
            Text.FontInfo.iRowSpace = 0;

            Text.MoveSet.bClear = false;
            Text.MoveSet.iActionSpeed = 9;
            Text.MoveSet.iActionType = 3;
            Text.MoveSet.iHoldTime = 0;
            Text.MoveSet.iClearActionType = 0;
            Text.MoveSet.iClearSpeed = 4;
            Text.MoveSet.iFrameTime = 20;

            if (-1 == User_AddText(iCardNum, ref Text, iProgramIndex))
            {
                return "添加文本失败！";
            }

            //4.发送数据
            if (User_SendToScreen(iCardNum) == false)
            {
                return "发送节目失败！";
            }
            return "成功";
        }
    }
}
