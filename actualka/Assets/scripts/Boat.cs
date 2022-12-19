using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    GameMang gameS;
    Canvas cv;
    public int nb_impacts;
    public int life =3;
    public bool destroyed=false;
    void Start()
    {
        gameS = GameObject.FindGameObjectWithTag("GameMang").GetComponent<GameMang>();
        cv = transform.GetComponent<Canvas>();
    }
    void Update()
    {
        if(life<=nb_impacts)
        {
            destroyed = true;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        gameS = GameObject.FindGameObjectWithTag("GameMang").GetComponent<GameMang>();
        gameS.canPlace = false;
    }
    public void OnTriggerExit(Collider other)
    {
        gameS.canPlace = true;
    }
    public void disableCanvas()
    {
        cv.enabled = false;
    }
}
