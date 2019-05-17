using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class newLevel : MonoBehaviour

{
    public string nextLevel;

    void onTriggerEnter(Collider other){
        print("collision detected");
        if (other.CompareTag("Player")){
            print("next level");
            SceneManager.LoadScene(nextLevel);
        }
    }
}
