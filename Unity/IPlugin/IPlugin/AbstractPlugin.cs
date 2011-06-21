using System;
using UnityEngine;
using De.Wellenblau.Inferfaces;

namespace De.Wellenblau.Interactions
{
	public abstract class AbstractPlugin: IPlugin
	{
		public PluginMeta meta;
		
		public string GetID(){
			return this.GetType().FullName;
		}
		
		public void registerGameObject(GameObject go){
			
			IPluginCallback[] callbacks = go.GetComponents<IPluginCallback>();
			//IPluginCallback[] callbacks = go.AddComponent("IPluginCallback");

			if(callbacks != null){
				for(int i = 0; i < callbacks.Length; ++i){
					if(callbacks[i].isIPlugin(this)) return;	
				}
			}
			
			go.AddComponent<IPluginCallback>().Plugin = this;
			//((IPluginCallback)go.AddComponent("IPluginCallback>")).Plugin = this;
			//go.AddComponent(
		}
		 
		public void unregisterGameObject(GameObject go){
			IPluginCallback[] callbacks = go.GetComponents<IPluginCallback>();
			//IPluginCallback[] callbacks = (IPluginCallback)go.GetComponents("IPluginCallback");
			
			for(int i = 0; i < callbacks.Length; ++i){
				if(callbacks[i].isIPlugin(this)) callbacks[i].Unregister();
			}
		}
		
		public abstract void Start(GameObject go);
		public abstract void Awake(GameObject go);
		public abstract void Update(GameObject go);
		public abstract void OnGUI(GameObject go);
	}
}

