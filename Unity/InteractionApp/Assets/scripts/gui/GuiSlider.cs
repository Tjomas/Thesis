using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

public class GuiSlider: GuiDisplayObject
{
 	private MethodInfo _methode;
	private float _oldValue;
	public float VALUE{get;set;}
	
    public GuiSlider(Dictionary<string,object> args):base(args)
    {
		Name = "HorizontalSlider " + _id; 
        _methode = FindMethode("HorizontalSlider", args);
    }

    protected override void DrawImpl()
    {

        VALUE = (float) _methode.Invoke(null, GetArgs());
		
		if(VALUE != _oldValue)
        {
			_logger.Debug("new slider value:" + VALUE);
			_args["value"] = VALUE;
			MarkDirty();
            InvokeEvent(GuiEventType.VALUE_CHANGED);
        }
		
		//save the old value
		_oldValue = VALUE;
		
    }
}


