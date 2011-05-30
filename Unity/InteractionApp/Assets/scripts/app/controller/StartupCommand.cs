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
		//Entfernen des Commands nach einmaligen Ausführen
		Facade.RemoveCommand(NoteConsts.APP_STARTUP);	
		
		//Console
		initConsole();	
		//Loader
		initLoader();
		//Plugins
		initPlugin();
		
		//StartupMessage verschicken
		SendNotification(NoteConsts.CONSOLE_ADDMESSAGE,new MessageVO("StartupCommand done!"));
	}
	
	private void initConsole(){
		//Commands anlegen
		Facade.RegisterCommand(NoteConsts.CONSOLE_ADDMESSAGE,typeof(AddConsoleCommand));
		//Proxies anlegen
		Facade.RegisterProxy(new ConsoleProxy());
		//Mediatoren anlegen
		Facade.RegisterMediator(new ConsoleMediator());
	}
	
	private void initLoader(){
		//Commands anlegen
		Facade.RegisterCommand(NoteConsts.LOADER_LOAD,typeof(LoadObjectCommand));
		//Proxies anlegen
		Facade.RegisterProxy(new ObjectProxy());
		//Mediatoren anlegen
	}
	
	private void initPlugin(){
		//Proxies anlegen
		Facade.RegisterProxy(new PluginProxy());
		//Mediatoren anlegen
		Facade.RegisterMediator(new PluginMediator());
	}
}

