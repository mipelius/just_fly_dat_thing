using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UserManager : MonoBehaviour {

	[System.Serializable]
	class UserData {
		public User[] users;
	}

	public static UserManager instance;

	private User _currentUser = null;

	private UserData userData;

	private string userDataFilePath = "users.json"; 

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

		foreach (User user in userData.users) {
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
		string filePath = Path.Combine (Application.streamingAssetsPath, userDataFilePath);

		if (File.Exists (filePath)) {
			string jsonString = File.ReadAllText (filePath);				
			userData = JsonUtility.FromJson<UserData> (jsonString);
		} else {
			Debug.LogError ("Cannot find game data!");
		}

	}
}
