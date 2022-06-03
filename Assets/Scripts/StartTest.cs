using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class StartTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame){
            NextSceneLoad(); 
        }
    }

    private void NextSceneLoad(){
        SceneManager.LoadScene(1);
    }
}
