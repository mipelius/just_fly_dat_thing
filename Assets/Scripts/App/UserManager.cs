using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UserManager : MonoBehaviour {

	[System.Serializable]
	class RawUserData {
		public User[] users;
	}

	public static UserManager instance;

	private User _currentUser = null;
	private List<User> users;
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

		foreach (User user in users) {
			usersToReturn.Add (user);
		}

		return usersToReturn;
	}

	public User CreateUser(string name, int level = 1) {		
		User user = new User (
			            GetFreeUserId (),
			            name,
			            level
		            );

		users.Add (user);

		Save ();

		return user;
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

	public User GetUser(string name) {
		foreach (User user in users) {
			if (user.name.Equals (name)) {
				return user;
			}
		}
		return null;
	}

	public User GetUser(int id) {
		foreach (User user in users) {
			if (user.id.Equals (id)) {
				return user;
			}
		}
		return null;
	}

	private void LoadUsers() {
		users = new List<User> ();

		string filePath = GetFilePath ();

		if (File.Exists (filePath)) {
			string jsonString = File.ReadAllText (filePath);				
			RawUserData userData = JsonUtility.FromJson<RawUserData> (jsonString);			

			foreach (User user in userData.users) {
				users.Add (user);
			}

		} 
		// else { just use the empty list of users }
	}

	private int GetFreeUserId() {
		int freeUserId = 0;
		foreach (User user in users) {
			if (user.id >= freeUserId) {
				freeUserId = user.id + 1;
			}
		}
		
		return freeUserId;
	}

	private void Save() {
		User[] usersToSave = users.ToArray ();
		RawUserData userData = new RawUserData();
		userData.users = usersToSave;
		string userDataJsonString = JsonUtility.ToJson (userData);
		string filePath = GetFilePath ();
		File.WriteAllText(filePath, userDataJsonString);
	}

	private string GetFilePath() {
		return Path.Combine (Application.streamingAssetsPath, userDataFilePath);				
	}
}
