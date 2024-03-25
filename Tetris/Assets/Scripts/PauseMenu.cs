using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pauseMenu;
    //public GameObject _startMenu;

    public static bool isPause;
    void Start(){
        //pauseMenu.SetActive(false);
    }

    void Update(){
        // if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1) Time.timeScale = 0;
        // else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0) Time.timeScale = 1;
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPause){
                ResumeGame();
            }
            else{
                Pause();
            }
        }
    }

    public void PauseButton()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Pause(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPause = true;
    }
    public void ResumeGame(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
    }

    // public void OutGame(){
    //     Time.timeScale = 0;
    //     _startMenu.SetActive(true);
    // }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    // Update is called once per frame
    
}
