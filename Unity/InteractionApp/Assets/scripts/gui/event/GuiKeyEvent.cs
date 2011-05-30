using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;

public class GuiKeyEvent : GuiEvent
{
    public GuiKeyEvent(GuiDisplayObject sender, GuiEventType type, SortedList paremater) : base(sender, type, paremater)
    {
    }
	
	public new void Invoke(GuiEventType type){
		//invalid button
		if(_params.ContainsKey("button")){
			if(!checkKey(Type,"GetButton",new object[] {_params["button"]})) return; 
		}
		
		//invalid key
		if(_params.ContainsKey("key")){
			if(!checkKey(Type,"GetKey",new object[] {_params["key"]})) return; 
		}
		
		base.Invoke(type);
	}
	
	private bool checkKey(GuiEventType type,string methodName, object[] args){
		
		//convert the args
		Type[] types = new Type[args.Length];
        for (int i = 0; i < args.Length; i++)
        {
            types[i] = args[i].GetType();
        }
		
		//Find the right method
		MethodInfo method = null; 
		if(type == GuiEventType.KEY) method = typeof (Input).GetMethod(methodName, types);
		else if(type == GuiEventType.KEYDOWN) method = typeof (Input).GetMethod(methodName + "Down", types);
		
		//invoke method
		if(method != null) return (bool)method.Invoke(null,args);
			
		//worng parameters
		return false;
	}
}