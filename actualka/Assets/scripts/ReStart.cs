using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ReStart : MonoBehaviour
{
    
    public void restart()
    {
        SceneManager.LoadScene("Alpha1", LoadSceneMode.Additive);
    }
}
