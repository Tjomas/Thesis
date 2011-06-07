using UnityEngine;
using System.Collections.Generic;
using De.Wellenblau.Inferfaces;

public class FrameworkLoader : AbstractFrameworkLoader {
	
	protected override void PluginConnect ()
	{
			App.Facade.SendNotification(NoteConsts.CONSOLE_ADDMESSAGE,new MessageVO("LOL"));
			
			List<IPlugin> plugins = App.PluginList;
			
			GameObject cube = GameObject.Find("Cube");
			
			foreach(IPlugin p in plugins){
				Debug.Log("Bähhhm " + p.GetID() + Random.value);
				p.registerGameObject(cube);
			}
			
			PluginConnectVO vo = new PluginConnectVO();
			
			App.Facade.SendNotification(NoteConsts.PLUGIN_CONNECT,new PluginConnectVO());
	}
	
	protected override List<InteractionInterestVO> ListInterests(){
		//new list
		List<InteractionInterestVO> lst = new List<InteractionInterestVO>();	
		
		//shake
		lst.Add(new InteractionInterestVO("shake"));
		
		//return interests
		return lst;
	}
}
