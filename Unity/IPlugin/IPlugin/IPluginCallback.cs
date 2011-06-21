using System;
using UnityEngine;
using De.Wellenblau.Inferfaces;

namespace De.Wellenblau.Interactions
{
	public class IPluginCallback:MonoBehaviour
	{
		private IPlugin _plugin;
		public bool _init = false;
		
		public IPlugin Plugin{
			set{
				_init = true;
				_plugin = value;
			}
		}
		
		public void Unregister(){
			Destroy(this);
		}
		
		public bool isIPlugin(IPlugin plugin){
			return _plugin.Equals(plugin);	
		}
		
		public void Start(){
			if(SelfDestroy()) return;
			_plugin.Start(gameObject);
		}
		public void Awake(){
			if(SelfDestroy()) return;
			_plugin.Awake(gameObject);
		}
		public void Update(){
			if(SelfDestroy()) return;
			_plugin.Update(gameObject);
		}
		public void OnGUI(){
			if(SelfDestroy()) return;
			_plugin.OnGUI(gameObject);
		}
		
		private bool SelfDestroy(){
			if(_plugin == null){
				if(_init)Destroy(this);
				return true;
			}
			return false;
		}
	}
}
