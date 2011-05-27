using System;
using De.Wellenblau.Inferfaces;
using UnityEngine;

public class PluginComponent
{
	private GuiDisplayObject _pluginList;

	private bool showList;

	public const string TAG = "PluginComponent";
	private Logger _logger = new Logger (TAG);

	public PluginComponent ()
	{
		_pluginList = new GuiGroup (new Rect (Screen.width + 10, 10, 0, 0));
		_pluginList.Name = "PluginList";
		
		GuiManager.AddChild (_pluginList);
		
		switch (Application.platform) {
		case RuntimePlatform.Android:
			InputManager.I.Subscribe (new GuiKeyEvent.EventHandler (ToggleList), GuiEventType.KEYDOWN, Tools.Hash ("key", KeyCode.Menu));
			break;
		default:
			InputManager.I.Subscribe (new GuiKeyEvent.EventHandler (ToggleList), GuiEventType.KEYDOWN, Tools.Hash ("key", KeyCode.F3));
			break;
		}
		
	}

	public void AddPlugin (int count, IPlugin plugin)
	{
		_logger.Trace ("Plugin hinzugefuegt:" + plugin.ToString ());
		
		Button btn = new Button (new Rect (0, 10 + (count) * 50, 200, 40), plugin.ToString ());
		btn.Subscribe (new GuiEvent.EventHandler (ButtonClicked), GuiEventType.CLICK, null);
		_pluginList.AddChild (btn);
	}

	public delegate void PluginSelected ();
	public event PluginSelected PluginSelectedHandler;

	public void ButtonClicked (GuiEvent e)
	{
		PluginSelectedHandler ();
	}

	public void ToggleList (GuiEvent e)
	{
		//toggle plugin list
		showList = !showList;
		if (showList == true)
			iTween.MoveTo (_pluginList.ToGameObject (), new Vector3 (-220, 0, 0), 1);
		else
			iTween.MoveTo (_pluginList.ToGameObject (), new Vector3 (0, 0, 0), 1);
	}
}
