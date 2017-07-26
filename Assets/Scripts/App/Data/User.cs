using System;
using UnityEngine;

[System.Serializable]
public class User
{
	public int id;
	public string name;
	public int level;
	public bool isActive;

	public static User CreateFromJSON(string jsonString)
	{		
		return JsonUtility.FromJson<User>(jsonString);
	}

	public void ToJson() {
		//return JsonUtility.ToJson().ToString ();
	}

	public User(int id, string name, int level, bool isActive = true) {
		this.id = id;
		this.name = name;
		this.level = level;
		this.isActive = isActive;
	}
}


