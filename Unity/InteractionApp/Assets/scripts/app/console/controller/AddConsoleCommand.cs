using System;
using PureMVC.Interfaces;
using PureMVC.Patterns;

public class AddConsoleCommand: SimpleCommand, ICommand
{
	public override void Execute(INotification note)
	{
		if(note.Body == null) return; //Keine Nachricht enthalten
		if(note.Body.GetType().Equals(typeof(MessageVO)) == false) return; //Kein passendes VO erhalten
		                                  
		//casten in ein enstprechendes VO
		MessageVO vo = (MessageVO) note.Body;
		
		ConsoleProxy proxy = (ConsoleProxy) Facade.RetrieveProxy(ConsoleProxy.NAME);
		if(proxy == null) return; //Kein passender Proxy registriert
		
		//Hier scheint alles ok zu sein :)
		proxy.AddMessage(vo.Msg);
	}
}


