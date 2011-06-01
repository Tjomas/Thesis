using System;
using UnityEngine;

public class ConsoleComponent: GuiSimple
{
	private bool showList;
	
	public ConsoleComponent ():base("Label", Tools.List("rect", new Rect(0, -(Screen.height >> 1), Screen.width, Screen.height >> 1), "text", "Text"))
	{
		this.Name="Console";
		
		//Add sensorgui
		AddChild(new SensorComponent());
		
		//link console into the 2d scenegraph
		GuiManager.AddChild(this);
		
		//subscribe for keyevents
        InputManager.I.Subscribe(new GuiKeyEvent.EventHandler(Toggle), GuiEventType.KEYDOWN, Tools.List("key", KeyCode.F2));
	}
	
	private void Toggle(GuiEvent e)
    {
        //toggle plugin list
        showList = !showList;
        if (showList == true) iTween.MoveTo(this.ToGameObject(), new Vector3(0, Screen.height >> 1, 0), 1);
        else iTween.MoveTo(this.ToGameObject(), new Vector3(0, 0, 0), 1);
    }
}


