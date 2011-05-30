using System;

public class NoteConsts
{
	
	//APP
	
	//Startup
	public const string APP_STARTUP = "app_startup";

	//LOADER
	
	//Neues Objekt laden
	public const string LOADER_LOAD = "loader_load";
	
	//CONSOLE
	
	//Im ConsolenProxy gibt es neue Informationen
	public const string CONSOLE_UPDATE = "console_update";
	
	//Eine neue Nachricht wird dem Proxy hinzugefügt. Erfolgt über einen entsprechenden Command
	public const string CONSOLE_ADDMESSAGE = "console_addmessage";
}


