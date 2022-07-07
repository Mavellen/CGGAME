using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    GameObject[] pauseObjects;

	GameObject[] creditsObjects;

	GameObject[] noneCreditsObjects;

	void Start () {
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		creditsObjects = GameObject.FindGameObjectsWithTag("ShowInCredits");
		noneCreditsObjects = GameObject.FindGameObjectsWithTag("HideInCredits");
		hidePaused();
		hideCredits();
	}

	void Update () {

		if(Input.GetKeyDown(KeyCode.P))
		{
			if(Time.timeScale == 1)
			{
				Time.timeScale = 0;
				showPaused();
			} else if (Time.timeScale == 0){
				Debug.Log ("high");
				Time.timeScale = 1;
				hidePaused();
			}
		}
	}

	public void Reload(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		//Application.LoadLevel(Application.loadedLevel);
	}

	public void pauseControl(){
			if(Time.timeScale == 1)
			{
				Time.timeScale = 0;
				showPaused();
			} else if (Time.timeScale == 0){
				Time.timeScale = 1;
				hidePaused();
			}
	}

	public void showPaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(true);
		}
	}

	public void hidePaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(false);
		}
	}

	public void LoadLevel(string level){
		SceneManager.LoadScene(level);
		//Application.LoadLevel(level);
	}

	public void quitGame(){
		Application.Quit();
	}
	
	public void showCredits(){
		foreach(GameObject g in creditsObjects){
			g.SetActive(true);
		}
		foreach(GameObject g in noneCreditsObjects){
			g.SetActive(false);
		}
	}

	public void hideCredits(){
	foreach(GameObject g in creditsObjects){
		g.SetActive(false);
	}
	foreach(GameObject g in noneCreditsObjects){
		g.SetActive(true);
	}
	}
}
