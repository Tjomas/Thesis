using System;
using System.Collections;

	public class Tools
	{
			public static Hashtable Hash(params object[] args){
		Hashtable hashTable = new Hashtable(args.Length/2);
		if (args.Length %2 != 0) {
			return null;
		}else{
			int i = 0;
			while(i < args.Length - 1) {
				hashTable.Add(args[i], args[i+1]);
				i += 2;
			}
			return hashTable;
		}
	}
	}


