using System;
using De.Wellenblau.Inferfaces;

namespace De.Wellenblau.Interactions
{
	public class InteractionOne : AbstractPlugin
	{
		public InteractionOne(){
			meta = new PluginMeta();
			meta.id ="Meine ID ist InteractionOne";
			meta.desc ="Dies ist meine Beschreibung";
			meta.port = PluginPort.Scale;
		}
	}
}

