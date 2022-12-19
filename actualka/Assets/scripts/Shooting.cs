using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject missile;
    public GameObject silo1, silo2;
    GameMang gameS;
    Transform container;
    public int p = 0;

    void Start()
    {
        gameS = GameObject.FindGameObjectWithTag("GameMang").GetComponent<GameMang>();
        container= GameObject.FindGameObjectWithTag("missiles").transform;
    }
    public void launch_player(Transform dest)
    {
        Transform origin;
        gameS.click = 0;
        gameS.shoot = true;
        if (p == 1)
        {
            origin = silo1.transform;
        }
        else
        {
            origin = silo2.transform;
        }
        GameObject m = Instantiate(missile, origin.position, Quaternion.Euler(0, 0, 0));
        m.transform.SetParent(container);
        Missile miss_Script =m.GetComponent<Missile>();
        miss_Script.dest = dest;
        miss_Script.origin = origin;
    }


}
