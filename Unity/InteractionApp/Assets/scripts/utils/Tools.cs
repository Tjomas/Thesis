using System;
using System.Collections;
using System.Collections.Generic;

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

	public static Dictionary<string,object> List(params object[] args){
		
		Dictionary<string,object> list = new Dictionary<string, object>(args.Length/2);
		if (args.Length %2 != 0) {
			return null;
		}else{
			int i = 0;
			while(i < args.Length - 1) {
				list.Add((string)args[i], args[i+1]);
				
				i += 2;
			}
			return list;
		}
	}

	public static string ListToString(IDictionary list){
		string tmp = "";
		foreach(DictionaryEntry item in list)
        {
           tmp += item.Key.ToString() +": "+ item.Value.ToString()+"\n";
        }
		return tmp;
	}
	
	public static string ArrayToString(object[] list){
		string tmp = "";
		for(int i = 0; i < list.Length; ++i)
        {
           tmp += list[i]+"\n";
        }
		return tmp;
	}
}


