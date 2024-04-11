using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpMenu : MonoBehaviour
{

    public static int previousScene;

    void Update()
    {
        LoadHelp();
        ExitHelp();
    }

    void LoadHelp()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            previousScene = currentSceneIndex;
            SceneManager.LoadScene("Help Screen");
        }
        
    }

    void ExitHelp()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene(previousScene);
        }
    }
}
