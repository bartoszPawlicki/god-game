using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    public bool isInGame = false;

    void Start()
    {
        if (isInGame)
            gameObject.SetActive(false);
    }

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

    void OnDisable()
    {
        Time.timeScale = 1;
    }

    void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}