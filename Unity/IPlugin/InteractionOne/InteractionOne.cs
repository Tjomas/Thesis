using System;
using UnityEngine;
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
		
		public override void Update(GameObject go){
			go.transform.Translate(UnityEngine.Random.value - 0.5f,UnityEngine.Random.value - 0.5f,UnityEngine.Random.value - 0.5f);
		}
		
		public override void Start(GameObject go){}
		public override void Awake(GameObject go){}
		public override void OnGUI(GameObject go){}
	}
}

