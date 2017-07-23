using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour {

	[System.Serializable]
	class Users {
		public User[] users;
	}

	public static UserManager instance;

	private User _currentUser = null;

	private Users users;

	void Awake () {
		if (instance == null) {
			instance = this;

			LoadUsers ();
		}
		else if (instance != this)
			Destroy(gameObject);
	}

	public List<User> GetUsers() {
		List<User> usersToReturn = new List<User> ();

		foreach (User user in users.users) {
			usersToReturn.Add (user);
		}

		return usersToReturn;
	}

	public void NewUser() {
		// new user
		Save();
	}

	public void DeleteUser() {
		// delete user
		Save();
	}

	public User currentUser {
		get {
			return _currentUser;
		}

		set {
			_currentUser = value;
		}
	}

	public void Save() {
		// save
	}

	private void LoadUsers() {
		string jsonString = 
			"{\"users\":[" +
			"   {\"id\":1, \"name\":\"Pekka\", \"level\":1}," +
			"   {\"id\":2, \"name\":\"Janne\", \"level\":2}," +
			"   {\"id\":3, \"name\":\"Heikka\", \"level\":3}" +
			"]}";
		
		users = JsonUtility.FromJson<Users> (jsonString);
	}
}
