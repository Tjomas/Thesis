using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;


public class ConsoleProxy: Proxy, IProxy
{
	public new const string NAME = "ConsoleProxy";
	
	private List<Object> _msg;
	
	public ConsoleProxy ():base(NAME)
	{
		
	}
	
	public void AddMessage(object msg)
    {
        //Neue Liste mit Nachrichten erzeugen
        if (_msg == null) _msg = new List<object>();
        //Neues Element aufnehmen
        _msg.Add(msg);
        //Cache erneuern
        Data = GenerateCache();
		//send update notification
		SendNotification(NoteConsts.CONSOLE_UPDATE);
    }
	
	public string Data{
		get{return (string) base.Data;}
		set{base.Data = value;}
	}

    protected string GenerateCache()
    {
        //Early out
        if (_msg == null) return "Empty";

        //Cache erzeugen
        string cache = "";

        //Die letzten 20 Nachrichten zusammenfassen
        for (int i = Math.Max(0, _msg.Count - 20); i < _msg.Count; i++)
        {
            cache += _msg[i].ToString() + "\n";
        }

        return cache;
    }
}


