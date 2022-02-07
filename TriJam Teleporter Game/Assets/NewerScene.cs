using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewerScene : MonoBehaviour
{
    [SerializeField]
    string newString;
    public void Load()
    {
        SceneManager.LoadScene(newString);
    }
}
