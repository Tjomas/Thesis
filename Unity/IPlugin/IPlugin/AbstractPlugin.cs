using System;
using De.Wellenblau.Inferfaces;

namespace De.Wellenblau.Interactions
{
	public abstract class AbstractPlugin : IPlugin
	{
		public PluginMeta meta;
		
		public string GetID(){
			return this.GetType().FullName;
		}
	}
}

