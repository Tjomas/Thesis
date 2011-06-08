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
			
				p.registerGameObject(GameObject.Find("Butterfly1"));
				p.registerGameObject(GameObject.Find("Butterfly2"));
				p.registerGameObject(GameObject.Find("Butterfly3"));
				p.registerGameObject(GameObject.Find("Butterfly4"));
				p.registerGameObject(GameObject.Find("Butterfly5"));
				p.registerGameObject(GameObject.Find("Butterfly6"));
				p.registerGameObject(GameObject.Find("Butterfly7"));
				p.registerGameObject(GameObject.Find("Butterfly8"));
				p.registerGameObject(GameObject.Find("Butterfly9"));
				p.registerGameObject(GameObject.Find("Butterfly10"));
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
