using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadScene(int sceneNum)
    {
        if(sceneNum == 1)
        {
            SceneManager.LoadScene("OpeningCutscene");
        }
        if(sceneNum == 2)
        {
            SceneManager.LoadScene("Room1");
        }
        if(sceneNum == 3)
        {
            SceneManager.LoadScene("Room1WonCutscene");
        }
        if(sceneNum == 4)
        {
            SceneManager.LoadScene("Room2");
        }
        if(sceneNum == 5)
        {
            SceneManager.LoadScene("Room2WonCutscene");//this could change idk if there's gonna be a real cutscene
        }
        if(sceneNum == 6)
        {
            SceneManager.LoadScene("CutsceneMinigame");
        }
        if(sceneNum == 7)
        {
            SceneManager.LoadScene("FinalCutscene");
        }
        if (sceneNum == 8)
        {
            SceneManager.LoadScene("credits");
        }
    }
    public string getCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name;
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
