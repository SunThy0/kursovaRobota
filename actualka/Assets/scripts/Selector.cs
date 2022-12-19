using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    // Start is called before the first frame update

    //camera script
    CamScript camS;
    //game manager
    GameMang gameS;

    void Start()
    {
        gameS = GameObject.FindGameObjectWithTag("GameMang").GetComponent<GameMang>();
        camS = GameObject.Find("Main Camera").GetComponent<CamScript>();
    }

   

    public void onclick()
    {
        
        gameS.clickObject = gameObject;
        gameS.click += 1;
    }


    public void p_enter()
    {
        gameS.overObject = gameObject;
       
    }
   
    public void p_exit()
    {
        gameS.overObject = null;
    }
}
