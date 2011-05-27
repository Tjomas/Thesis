using System;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections.Generic;

public class ConsoleMediator : Mediator, IMediator
{
	public new const string NAME = "ConsoleMediator";
	
	private ConsoleProxy _proxy;
	
	public ConsoleMediator ():base(NAME)
	{
		ViewComponent = new ConsoleComponent();
	}
	
	protected ConsoleComponent View{
		get{return (ConsoleComponent) ViewComponent;}
	}
	
	public override void OnRegister(){
		
		_proxy = (ConsoleProxy) Facade.RetrieveProxy(ConsoleProxy.NAME);	
	}
	
	public override IList<string> ListNotificationInterests()
		{
			IList<string> list = new List<string>();
			list.Add(NoteConsts.CONSOLE_UPDATE);
			return list;
		}
	
	public override void HandleNotification(INotification notification){
		
		switch(notification.Name){
		case NoteConsts.CONSOLE_UPDATE: 
			
			View.Text = _proxy.Data;
			break;	
		}
	}
}


