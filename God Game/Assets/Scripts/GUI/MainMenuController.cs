using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public void StartGame()
	{
        SceneManager.LoadScene(1);
        GameContener.IsTutorialEnable = false;
	}

    public void StartTutorial()
    {
        SceneManager.LoadScene(1);
        GameContener.IsTutorialEnable = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}