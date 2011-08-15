using UnityEngine;
using System.Collections.Generic;
using De.Wellenblau.Inferfaces;

public class FrameworkLoader : AbstractFrameworkLoader {
	
	protected override void PluginConnect ()
	{
			App.Facade.SendNotification(NoteConsts.CONSOLE_ADDMESSAGE,new MessageVO("LOL"));
			
			List<IPlugin> plugins = App.PluginList;
			
			//GameObject cube = GameObject.Find("Butterfly");
			
			foreach(IPlugin p in plugins){
				Debug.Log("Bähhhm " + p.GetID() + Random.value);
			
				p.registerGameObject(GameObject.Find("B1"));
				p.registerGameObject(GameObject.Find("B2"));
				p.registerGameObject(GameObject.Find("B3"));
				p.registerGameObject(GameObject.Find("B4"));
				p.registerGameObject(GameObject.Find("B5"));
				p.registerGameObject(GameObject.Find("B6"));
				p.registerGameObject(GameObject.Find("B7"));
				p.registerGameObject(GameObject.Find("B8"));
				p.registerGameObject(GameObject.Find("B9"));
				p.registerGameObject(GameObject.Find("B10"));
				
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
