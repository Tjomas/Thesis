using UnityEngine;
using PureMVC.Patterns;
using System.Collections;

public class App: MonoBehaviour{
	
	private InputManager _inputManager;
	
	public static App _instance;

    public App()
    {
        _instance = this;

        //MobileSettings
        
		_inputManager = InputManager.I;
    }
	
	public static App GetInstance(){
		return _instance;	
	}

    public void Start ()
    {
		AppFacade facade = (AppFacade) AppFacade.Instance;
		facade.Startup(this);
    }
	
	
	public void Update(){
		_inputManager.Update();
	}

    
}