using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDTools.NewSocketManager
{
    public enum MessageTypeEnum
    {
        /// <summary>
        /// 文字
        /// </summary>
        Character = 0,

        /// <summary>
        /// 图片
        /// </summary>
        Image = 1,

        /// <summary>
        /// 声音
        /// </summary>
        Voice = 2,

        /// <summary>
        /// 视频
        /// </summary>
        Video = 3,

        /// <summary>
        /// 上线
        /// </summary>
        Login = 4,

        /// <summary>
        /// 下线
        /// </summary>
        Exit = 5,


        /// <summary>
        /// 关闭系统
        /// </summary>
        ShutDown = 6,


        /// <summary>
        /// 重启
        /// </summary>
        PowerOff = 7,


        /// <summary>
        /// 重启
        /// </summary>
        Restart = 8,


        /// <summary>
        /// 提示
        /// </summary>
        Prompt = 9,

        /// <summary>
        /// 更新
        /// </summary>
        Modify = 10,


        /// <summary>
        /// 新增
        /// </summary>
        Increase = 11,

        /// <summary>
        /// 删除
        /// </summary>
        Delete = 12,


        /// <summary>
        /// 集合
        /// </summary>
        Collection = 13,


        /// <summary>
        /// 通知
        /// </summary>
        Notice = 14,


        /// <summary>
        /// 数据流
        /// </summary>
        Stream = 15,

        /// <summary>
        /// 心跳包
        /// </summary>
        Heartbeat=16,
        /// <summary>
        /// 获取下一跳数据
        /// </summary>
        Next = 17,
    }
}
