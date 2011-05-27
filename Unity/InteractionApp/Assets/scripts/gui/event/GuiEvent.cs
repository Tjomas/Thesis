using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using UnityEngine;


public class GuiEvent
{
    public readonly GuiEventType Type;
    public readonly GuiDisplayObject Sender;
	
    public delegate void EventHandler(GuiEvent e);
	public event EventHandler Handler;
	
    protected Hashtable _params; 

	public GuiEvent(GuiDisplayObject sender,GuiEventType type, Hashtable parameter)
    {
        Sender = sender;
		Type = type;
		
		_params = parameter;
    }
	
	public void Invoke(GuiEventType type){
		//invalid type
		if(type != Type) return;
		
		//fire
		Handler(this);
	}
	
	public object this[string key]
	{
		get{return _params[key];}
		set{}
	}
    
}
