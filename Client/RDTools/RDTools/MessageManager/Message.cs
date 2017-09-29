using System;
using System.Xml;
using System.Xml.Serialization;

namespace RDTools.MsgService
{
	/// <summary>
	/// Message 的摘要说明。
	/// </summary>
	public class Msg
	{
		private string _msgID;
		private string _validMsg;
		private string _msgType;
		private string _executeOffice;
		private string _msgDate;
		private string _msgFrom;
		private string _msgTo;
		private string _msgText;
		public Msg()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		[XmlAttribute()]
		public string MsgID
		{
			get { return _msgID; }
			set { _msgID = value; }
		}
		[XmlAttribute()]
		public string MsgType
		{
			get { return _msgType; }
			set { _msgType = value; }
		}
		[XmlAttribute()]
		public string MsgDate
		{
			get { return _msgDate; }
			set { _msgDate = value; }
		}
		[XmlAttribute()]
		public string ValidMsg
		{
			get { return _validMsg; }
			set { _validMsg = value; }
		}
		public string From
		{
			get { return _msgFrom; }
			set { _msgFrom = value; }
		}
		public string To
		{
			get { return _msgTo; }
			set { _msgTo = value; }
		}
		public string ExecuteOffice
		{
			get { return _executeOffice; }
			set { _executeOffice = value; }
		}
		public string MsgText
		{
			get { return _msgText; }
			set { _msgText = value; }
		}
		
	}
}
