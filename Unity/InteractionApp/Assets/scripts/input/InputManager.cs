using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using UnityEngine;

public class InputManager
{
	private List<GuiKeyEvent> _events = null;
	
    #region Singelton

    private static InputManager _instance = null;

    public static InputManager I
    {
        get
        {
            if (_instance == null) _instance = new InputManager();
            return _instance;
        }
    }

    private InputManager()
    {
       
    }

    #endregion
	
	public void Update(){
		//InputCheck für das EventSystem
		
		//AnyKey
		if(Input.anyKey){
			foreach(GuiKeyEvent e in _events){
				e.Invoke(GuiEventType.KEY);
			}

           
		}
		//AnyKeyDown
		if(Input.anyKeyDown){
			foreach(GuiKeyEvent e in _events){
				e.Invoke(GuiEventType.KEYDOWN);
			}

		    //Array lstProvider = System.Enum.GetValues( typeof(KeyCode)); 
            //foreach (KeyCode enProvider in lstProvider)
            //{
            //    if (Input.GetKey(enProvider)) AppConsole.AddMessage(enProvider);
            //}      
		}
	}
	
	public void Subscribe(GuiEvent.EventHandler callback,GuiEventType type){
		Subscribe(callback,type,null);
	}
	
	public void Subscribe(GuiEvent.EventHandler callback,GuiEventType type, SortedList parameter){
		GuiKeyEvent e = new GuiKeyEvent(null ,type, parameter);
		e.Handler += callback;
		//
		if (_events == null) _events = new List<GuiKeyEvent>();
        _events.Add(e);
	}
}


