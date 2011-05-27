using System;
namespace De.Wellenblau.Inferfaces
{
	public interface IPlugin
	{
		string GetID();
		
		//bool Translate();
		//bool Scale();
		//bool Rotate();
		//bool Transform();
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