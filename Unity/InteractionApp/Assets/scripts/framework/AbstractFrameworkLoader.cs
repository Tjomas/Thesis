using UnityEngine;
using System.Collections.Generic;
using De.Wellenblau.Inferfaces;

public abstract class AbstractFrameworkLoader : MonoBehaviour {
	
	private AsyncOperation _asOp;
	
	// Use this for initialization
	void Start () {
		_asOp = Application.LoadLevelAdditiveAsync("main");
	}
	
	// Update is called once per frame
	void Update () {
		if(_asOp.isDone){
			PluginConnect();
			
			InterestsListVO vo = new InterestsListVO(ListInterests());
			App.Facade.SendNotification(NoteConsts.PLUGIN_INTERESTS,vo);
			
			Destroy(this.gameObject);
		}
	}
	
	protected abstract void PluginConnect();
	
	protected abstract List<InteractionInterestVO> ListInterests();
}
