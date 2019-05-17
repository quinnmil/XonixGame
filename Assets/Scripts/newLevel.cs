using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class newLevel : MonoBehaviour

{
    public string nextLevel;

    void onTriggerStay(Collider other){
        print("collision detected");
        if (other.gameObject.tag == "Player"){
            print("next level");
            SceneManager.LoadScene(nextLevel);
        }
    }
}
