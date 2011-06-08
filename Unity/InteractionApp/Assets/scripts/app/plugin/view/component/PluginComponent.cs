using System;
using De.Wellenblau.Inferfaces;
using UnityEngine;

public class PluginComponent
{
	private GuiDisplayObject _pluginList;
	private GuiScaleTexture _left;

	private bool showList;

	public const string TAG = "PluginComponent";
	private Logger _logger = new Logger (TAG);

	public PluginComponent ()
	{
		_pluginList = new GuiGroup (Tools.List("rect", new Rect (Screen.width -50,0, 0, 0)));
		_pluginList.Name = "PluginList";
		
		Texture2D tex = Resources.Load("textures/menu/left") as Texture2D;
		_left = new GuiScaleTexture(Tools.List("rect",new Rect(0,0,Screen.width,Screen.height),"texture",tex,"scaleMode",ScaleMode.ScaleToFit));
		_left.Position = new Vector2(_left.Dimension.x * -1 + _left.ScaleValue(90) - Screen.width,0);	
		_pluginList.AddChild(_left); 
		
		tex = Resources.Load("textures/menu/middle") as Texture2D;
		_pluginList.AddChild(new GuiScaleTexture(Tools.List("rect",new Rect(0,0,Screen.width,Screen.height),"texture",tex,"scaleMode",ScaleMode.ScaleToFit))); 
		
		GuiManager.AddChild (_pluginList);
		
		switch (Application.platform) {
		case RuntimePlatform.Android:
			InputManager.I.Subscribe (new GuiKeyEvent.EventHandler (ToggleList), GuiEventType.KEYDOWN, Tools.List ("key", KeyCode.Menu));
			break;
		default:
			InputManager.I.Subscribe (new GuiKeyEvent.EventHandler (ToggleList), GuiEventType.KEYDOWN, Tools.List ("key", KeyCode.F3));
			break;
		}
		
	}

	public void AddPlugin (int count, IPlugin plugin)
	{
		_logger.Trace ("Plugin hinzugefuegt:" + plugin.ToString ());
		
		Button btn = new Button (Tools.List("rect",new Rect (70, 10 + (count) * 50, 200, 40),"text",plugin.ToString()));
		btn.Subscribe (new GuiEvent.EventHandler (ButtonClicked), GuiEventType.CLICK, null);
		_pluginList.AddChild (btn);
	}

	public delegate void PluginSelected ();
	public event PluginSelected PluginSelectedHandler;

	public void ButtonClicked (GuiEvent e)
	{
		PluginSelectedHandler();
	}

	public void ToggleList (GuiEvent e)
	{
		//toggle plugin list
		showList = !showList;
		if (showList == true){
			iTween.MoveTo (_pluginList.ToGameObject (), new Vector3 ((Screen.width - _left.Dimension.x) * -1, 0, 0), 1);
			iTween.MoveTo(_left.ToGameObject(), new Vector3 (_left.Dimension.x, 0, 0), 1);
		}
		else{
			iTween.MoveTo(_left.ToGameObject(), new Vector3 (0, 0, 0), 1);
			iTween.MoveTo (_pluginList.ToGameObject (), new Vector3 (0, 0, 0), 1);
		}	
	}
}
