using System;
using System.Xml;
using System.Xml.Serialization;

namespace RDTools.MsgService
{
	/// <summary>
	/// Class1 的摘要说明。
	/// </summary>
	public class MsgLog
	{
		private string _msgHandler;
		private string _msgOwner;
		private string _acceptMsgType;
		private string _workOffice;
		private string _msgLengh;
		private MsgArrayList _msgArrayList;
		public MsgLog()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public string MsgHandler
		{
			get { return _msgHandler; }
			set { _msgHandler = value; }
		}
		public string MsgOwner
		{
			get { return _msgOwner; }
			set { _msgOwner = value; }
		}
		public string AcceptMsgType
		{
			get{ return _acceptMsgType; }
			set{ _acceptMsgType = value; }
		}
		public string WorkOffice
		{
			get{ return _workOffice; }
			set{ _workOffice = value; }
		}
		public string MsgLengh
		{
			get{ return _msgLengh; }
			set{ _msgLengh = value; }
		}
		[XmlElement("Message")]
		public MsgArrayList MsgArrayList
		{
			get { return _msgArrayList; }
			set { _msgArrayList = value; }
		}
	}
}
