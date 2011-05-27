using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;


public class AppConsole
{
    #region Singelton

    private static AppConsole _instance = null;
    private GuiDisplayObject _gui;
    private bool showList;
	private Logger _logger;


    public static AppConsole I
    {
        get
        {
            if (_instance == null) _instance = new AppConsole();
            return _instance;
        }
    }

    private AppConsole()
    {
        _gui = new GuiSimple("Label", new Rect(0, -(Screen.height >> 1), Screen.width, Screen.height >> 1), "Null");
        GuiManager.AddChild(_gui);
		
        InputManager.I.Subscribe(new GuiKeyEvent.EventHandler(ToggleConsole), GuiEventType.KEYDOWN, Tools.Hash("key", KeyCode.F2));
		
		_logger = new Logger("APPCONSOLE");
    }

    #endregion

    public void ToggleConsole(GuiEvent e)
    {
        AppConsole.AddMessage("Toogle Console");
		
        //toggle plugin list
        showList = !showList;
        if (showList == true) iTween.MoveTo(_gui.ToGameObject(), new Vector3(0, Screen.height >> 1, 0), 1);
        else iTween.MoveTo(_gui.ToGameObject(), new Vector3(0, 0, 0), 1);
    }

    private List<Object> _msg;
    private string _msgCache;

    public static void AddMessage(object msg)
    {
        I.AddMessageInternal(msg);
    }

    protected void AddMessageInternal(object msg)
    {
        //Neue Liste mit Nachrichten erzeugen
        if (_msg == null) _msg = new List<object>();
        //Neues Element aufnehmen
        _msg.Add(msg);
        //Cache erneuern
        _msgCache = GenerateCache();
        //Anzeigen
        _gui.Text = _msgCache;
		//socket-out
		_logger.Trace(msg.ToString());
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