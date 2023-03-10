using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Level")
        {
            SceneManager.LoadScene(target.gameObject.name);
        }
    }
}
