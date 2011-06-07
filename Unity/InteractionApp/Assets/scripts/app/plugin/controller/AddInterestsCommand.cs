using System;
using PureMVC.Interfaces;
using PureMVC.Patterns;

public class AddInterestsCommand: SimpleCommand, ICommand
{
	public override void Execute (INotification notification)
	{
		//early out
		if(!notification.Body.GetType().Equals(typeof(InterestsListVO))) return;
		InterestsListVO vo = notification.Body as InterestsListVO;
		
		//find proxy
		InterestProxy proxy = Facade.RetrieveProxy(InterestProxy.NAME) as InterestProxy;
		if(proxy != null) proxy.Add(vo);
	}
}


