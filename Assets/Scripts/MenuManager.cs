using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour{

    public string NextLevel;
    public void SceneChange(){
        SceneManager.LoadScene(NextLevel);
    }

    public void Exit(){
        Application.Quit();
    }

}
