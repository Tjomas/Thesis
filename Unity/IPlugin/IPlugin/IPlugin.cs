using System;
using UnityEngine;

namespace De.Wellenblau.Inferfaces
{
	public interface IPlugin
	{
		string GetID();
		void registerGameObject(GameObject go);
		void unregisterGameObject(GameObject go);
		
		void Start(GameObject go);
		void Awake(GameObject go);
		void Update(GameObject go);
		void OnGUI(GameObject go);
	}
	
	public enum PluginPort{
		Rotation, Translation, Scale, None
	}
	
	public struct PluginMeta{
		public string id;
		public string desc;
		public PluginPort port;
	}	
}