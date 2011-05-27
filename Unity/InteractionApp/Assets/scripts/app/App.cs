using UnityEngine;
using PureMVC.Patterns;
using System.Collections;

public class App: MonoBehaviour{
	
    private PluginProxy _pluginManager;
	private InputManager _inputManager;
    private static App _instance;

    public static App GetInstance()
    {
        return _instance;
    }

    public App()
    {
        _instance = this;
		
		
		
        //MobileSettings
        
		_inputManager = InputManager.I;

    }

    public void Start ()
    {
		AppFacade facade = (AppFacade) AppFacade.Instance;
		facade.Startup(this);
		
        
		iPhoneSettings.screenCanDarken = false;

        _pluginManager = new PluginProxy();
        
        AppConsole.AddMessage(_pluginManager);
        AppConsole.AddMessage("Test1");
        AppConsole.AddMessage("Test2");
        
        Button b1 = new Button(new Rect(300, 0, 100, 100), "Hallo Invoker");
        //b1.AddEventListener(GuiEventType.CLICK,this,"TestCallback", "Button1");

        Button b2 = new Button(new Rect(300, 300, 100, 100), "Hallo Invoker2");
        //b2.AddEventListener(GuiEventType.CLICK, this, "TestCallback", "Button2");

        Button b3 = new Button(new Rect(0, 300, 100, 100), "Hallo Invoker3");
        //b3.AddEventListener(GuiEventType.CLICK, this, "TestCallback", "Button3");
        GuiSimple label = new GuiSimple("Label", new Rect(0,200,200,200),"Text Hallo");

        b1.AddChild(b2);
        b1.AddChild(b3);
        b1.AddChild(label);
		
        GuiManager.AddChild(b1);   
		
		b3.Subscribe(new GuiEvent.EventHandler(TestCallback), GuiEventType.CLICK, Tools.Hash("text","Button3"));	
		
		
		//_inputManager.Subscribe(new GuiEvent.EventHandler(TestCallbackKey), GuiEventType.KEY, Tools.Hash("text","KEY"));
		//_inputManager.Subscribe(new GuiEvent.EventHandler(TestCallbackKey), GuiEventType.KEYDOWN, Tools.Hash("text","KEYDOWN"));

    }
	
	
	public void Update(){
		_inputManager.Update();
	}
	

    public void TestCallback(GuiEvent e)
    {
        iTween.ShakePosition(e.Sender.ToGameObject(), new Vector3(10, 3, 0), 2);
        Debug.Log("TestCallback " + e["text"] +"   "+ Random.value);
        AppConsole.AddMessage("Invoked by "+ e.Sender.Name);
    }
	
	public void TestCallbackKey(GuiEvent e)
    {
        Debug.Log("TestCallback " + e["text"] +"   "+ Random.value);
        AppConsole.AddMessage("TestCallback " + e["text"] +"   "+ Random.value);
    }
    
}