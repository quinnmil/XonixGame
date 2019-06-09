using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keep_audio : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
