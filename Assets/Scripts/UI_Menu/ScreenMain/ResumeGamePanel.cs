using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeGamePanel : MonoBehaviour {

	public GameObject scrollViewContent;

	public GameObject playerButton;
	public GameObject playerButtonText;

	private float spaceBetweenButtons = 20;

	class OnClickListener
	{
		private User user;

		public OnClickListener(User user) { this.user = user; }

		public void Action() {				
			UserManager.instance.currentUser = user;
			UnityEngine.SceneManagement.SceneManager.LoadScene ("ScreenLevelSelect");
		}
	}

	void Start() {
		CreatePlayerList ();
	}

	private void CreatePlayerList() {		
		List<User> users = UserManager.instance.GetUsers ();

		RectTransform buttonRectTransform = playerButton.GetComponent<RectTransform> ();
		float height = buttonRectTransform.rect.height;

		for (int i=0; i < users.Count; i++) {
			Vector3 transition = new Vector3 (
				0, 
				-i * (height + spaceBetweenButtons) - spaceBetweenButtons, 
				0
			);

			Vector3 position = playerButton.transform.position + transition;

			// Instantiate button and text => set parent

			GameObject currentPlayerButton = 
				Instantiate(playerButton, position, Quaternion.identity) as GameObject;

			GameObject currentPlayerButtonText =
				Instantiate (playerButtonText, currentPlayerButton.transform) as GameObject;

			currentPlayerButton.transform.SetParent(scrollViewContent.transform, false);

			// Set text

			Text text = currentPlayerButtonText.GetComponent<Text> ();
			text.text = users [i].name;

			// Add action for button click

			Button button = currentPlayerButton.GetComponent<Button> ();
			OnClickListener listener = new OnClickListener (users [i]);
			button.onClick.AddListener(delegate { listener.Action(); });
		}

		// Adjust content panel size

		RectTransform contentRectTransform = scrollViewContent.GetComponent<RectTransform> ();
		contentRectTransform.offsetMin = new Vector2 (
			contentRectTransform.offsetMin.x,
			-users.Count * (height + spaceBetweenButtons) - spaceBetweenButtons
		);
		
	}
}
