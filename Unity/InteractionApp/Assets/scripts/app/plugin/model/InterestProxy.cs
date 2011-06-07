using System;
using System.Collections.Generic;
using De.Wellenblau.Inferfaces;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using Object = UnityEngine.Object;

public class InterestProxy: Proxy , IProxy
{
	public new const string NAME = "InterestProxy";
	
	private Logger _logger = new Logger (NAME);

    public InterestProxy():base(NAME)
    {
		List = new List<InteractionInterestVO>();
    }
	
	public List<InteractionInterestVO> List{
		get{return base.Data as List<InteractionInterestVO>;}
		set{base.Data = value;}
	}
	
	public void Add(InterestsListVO vo){
		foreach(InteractionInterestVO ivo in vo.List){
			_logger.Trace("New interest added: " + ivo.Name);
			List.Add(ivo);			
		}
	}

    

    public override string ToString()
    {
        string ids = "Interest-List Start\n";

        ids += "Plugin-List End";

        return ids;
    }
}