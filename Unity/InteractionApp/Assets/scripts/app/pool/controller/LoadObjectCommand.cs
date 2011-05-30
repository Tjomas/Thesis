using System;
using PureMVC.Interfaces;
using PureMVC.Patterns;

public class LoadObjectCommand: SimpleCommand, ICommand
{
	public override void Execute (INotification notification)
	{
		//only loading-requests
		if(notification.Type != NoteTypeConsts.REQUEST) return;
			
		//correct vo included	
		ObjectVO vo = (ObjectVO)notification.Body();
		if(vo == null) return;
		
		//finally call the proxy
		ObjectProxy proxy = (ObjectProxy)Facade.RetrieveProxy(ObjectProxy.NAME);
		if(proxy == null) return; //early out
		proxy.LoadScene(vo);
	}
}