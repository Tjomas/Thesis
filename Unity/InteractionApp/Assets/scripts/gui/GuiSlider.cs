using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

public class GuiSlider: GuiDisplayObject
{
 	private MethodInfo _methode;
	private float _oldValue;
	private static Logger _logger = new Logger("GuiSlider");
	
    public GuiSlider(Dictionary<string,object> args):base(args)
    {
		Name = "HorizontalSlider " + _id; 
		_logger.Error(Tools.ListToString(args));
        _methode = FindMethode("HorizontalSlider", args);
    }

    protected override void DrawImpl()
    {
		_methode.Invoke(null, GetArgs());
		
        
        float newValue = (float) _methode.Invoke(null, GetArgs());
		
		//Warum zum Teufel geht das nicht		
		if(newValue != _oldValue)
        {
			_args["value"] = newValue;
			MarkDirty();
			
            InvokeEvent(GuiEventType.VALUE_CHANGED);
        }
		_oldValue = newValue;
		
    }
}


