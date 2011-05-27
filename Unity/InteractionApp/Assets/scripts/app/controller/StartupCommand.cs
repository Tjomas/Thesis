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
	
		AppConsole.AddMessage("Startup");
	
		Facade.RegisterProxy(new PluginProxy());
		Facade.RegisterMediator(new PluginMediator());

	}
}

