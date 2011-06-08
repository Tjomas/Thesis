using UnityEngine;
using PureMVC.Patterns;
using System.Collections.Generic;
using De.Wellenblau.Inferfaces;

public class App: MonoBehaviour{
	
	private static App _instance;
	private AppFacade facade;
	
    public App()
    {
        _instance = this;
    }
	
	public static App I{
		get{return _instance;}	
	}

    public void Start ()
    {
		facade = (AppFacade) AppFacade.Instance;
		facade.Startup(this);
	}
	
	public static Facade Facade{
		get{return _instance.facade;}
	}
	
	public static List<IPlugin> PluginList{
		get{
			PluginProxy proxy =  _instance.facade.RetrieveProxy(PluginProxy.NAME) as PluginProxy;	
			if(proxy == null) return new List<IPlugin>();
			return proxy.GetPluginList();
		}		
	}
}