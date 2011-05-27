using System;
using System.Collections.Generic;
using System.Text;

using PureMVC.Patterns;
using PureMVC.Interfaces;

public class StartupCommand : SimpleCommand, ICommand
{
	/// <summary>
	/// Register the Proxies and Mediators.
	/// 
	/// Get the View Components for the Mediators from the app,
	/// which passed a reference to itself on the notification.
	/// </summary>
	/// <param name="note"></param>
	public override void Execute(INotification note)
	{
		//Commands anlegen
		Facade.RegisterCommand(NoteConsts.CONSOLE_ADDMESSAGE,typeof(AddConsoleCommand));
	
		//Proxies anlegen
		Facade.RegisterProxy(new ConsoleProxy());
		Facade.RegisterProxy(new PluginProxy());
		
		//Mediatoren anlegen
		Facade.RegisterMediator(new PluginMediator());
		Facade.RegisterMediator(new ConsoleMediator());
		
		//StartupMessage verschicken
		SendNotification(NoteConsts.CONSOLE_ADDMESSAGE,new MessageVO("StartupCommand done!"));
	}
}

