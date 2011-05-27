using System;
using System.Collections.Generic;
using De.Wellenblau.Inferfaces;
using PureMVC.Interfaces;
using PureMVC.Patterns;

public class PluginMediator: Mediator, IMediator
{
	public new const string NAME = "PluginMediator";
	
	private PluginProxy _pluginProxy;
	
	public PluginMediator ():base(NAME)
	{
		ViewComponent = new PluginComponent();
	    View.PluginSelectedHandler += new PluginComponent.PluginSelected(PluginSelected);
	}
	
	public override void OnRegister()
	{
		base.OnRegister();
		_pluginProxy = (PluginProxy) Facade.RetrieveProxy(PluginProxy.NAME);
		
		List<IPlugin> Plugins = _pluginProxy.GetPluginList();
		for(int i = 0; i < Plugins.Count; i++){
			View.AddPlugin(i, Plugins[i]);
		}

	}
	
	private PluginComponent View{
		get{return (PluginComponent) ViewComponent;}
	}

    private void PluginSelected()
    {
        //UnityEngine.Debug.LogError("Tafa"+ UnityEngine.Random.value);
    }
}


