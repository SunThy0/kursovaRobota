using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class LoadOutScript : MonoBehaviour
{
	public int returnValue;
 	GameMang gameS;

    void Start()
    {
	    gameS = GameObject.FindGameObjectWithTag("GameMang").GetComponent<GameMang>();
    }

    public void clickMethod0()
    {
	    gameS.decider = returnValue;
    }
	
}
