using UnityEngine;
using System.Collections.Generic;
using De.Wellenblau.Inferfaces;

public class FrameworkLoader : MonoBehaviour {
	
	private AsyncOperation _asOp;
	
	// Use this for initialization
	void Start () {
		_asOp = Application.LoadLevelAdditiveAsync("main");
	}
	
	// Update is called once per frame
	void Update () {
		if(_asOp.isDone){
			App.Facade.SendNotification(NoteConsts.CONSOLE_ADDMESSAGE,new MessageVO("LOL"));
			
			List<IPlugin> plugins = App.PluginList;
			
			GameObject cube = GameObject.Find("Cube");
			
			foreach(IPlugin p in plugins){
				Debug.Log("Bähhhm " + p.GetID() + Random.value);
			}
			
			PluginConnectVO vo = new PluginConnectVO();
			
			App.Facade.SendNotification(NoteConsts.PLUGIN_CONNECT,new PluginConnectVO());
			
			Destroy(this.gameObject);
		}
	}
}
