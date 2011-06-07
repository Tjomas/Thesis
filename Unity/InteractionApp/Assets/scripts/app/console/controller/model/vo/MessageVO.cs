using System;

public class MessageVO
{
	private string _msg;
	
	public MessageVO(string msg){
		_msg = msg;
	}
	
	public string Msg{
		get{
			return _msg;
		}
	}
}

