using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public string newSceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GameAction.PlayerTags(collision.tag))
        {
            ChangeLevel();
        }
    }

    public void ChangeLevel()
    {
        StartCoroutine(ChangeLevelIE());
    }

    IEnumerator ChangeLevelIE()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(newSceneName);//Loads next scene in background asyncronously

        while(!asyncLoad.isDone)// Run this code until the next scene is done loading
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync(currentScene); //Unloads current scene
    }
}
