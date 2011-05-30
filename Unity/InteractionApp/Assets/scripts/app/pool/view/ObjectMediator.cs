using System;
using PureMVC.Interfaces;
using PureMVC.Patterns;

public class ObjectMediator: Mediator, IMediator
{
	public new const string NAME = "ObjectMediator";
	
	public ObjectMediator ():base(NAME)
	{
		
	}
}


