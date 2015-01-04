using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity_Story : ScriptableObject
{	
	public List<Param> param = new List<Param> ();

	[System.SerializableAttribute]
	public class Param
	{
		
		public int id;
		public string story_1;
		public string story_2;
		public string story_3;
	}
}