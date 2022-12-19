using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Missile : MonoBehaviour
{
    // Start is called before the first frame update

    //origin and destination of the parabola
    public Transform origin, dest;
    //speed of the missile
    public float speed = 0.7f;
    //max heigh
    public float ymax = 10;
    // the particle system attached to the missile
    ParticleSystem ps;
    //the impact gameobject that is instanciated when it hits water or ship
    public GameObject impact;
    //game manager
    GameMang gameS;
    //container for the missiles
    Transform container;
    //error check for first ship impact
    bool impactedShip = false;

    void Start()
    {
        ps = transform.GetChild(0).GetComponent<ParticleSystem>();
        ps.Play();
        gameS = GameObject.FindGameObjectWithTag("GameMang").GetComponent<GameMang>();
        container = GameObject.FindGameObjectWithTag("impacts").transform;
        StartCoroutine(LaunchMissile()); 
    }
    IEnumerator LaunchMissile()
    {
        Vector3 dir = dest.position - origin.transform.position;
        float xmax = dir.magnitude;
        dir = dir / dir.magnitude;
        //parabola parameters y=a*x^2+b+c
        float a = -2 * ymax / Mathf.Pow(xmax, 2);
        float b = -a * xmax;
        float t0 = Time.fixedTime;
        while ((transform.position - dest.position).magnitude > 0.5f)
        {
            float t = Time.fixedTime - t0;
            float x = Mathf.Sqrt(Mathf.Pow((transform.position - origin.position).x, 2) + Mathf.Pow((transform.position - origin.position).z, 2));
            float vy = 2 * a * x + b;
            Vector3 vel = dir * speed + new Vector3(0, vy, 0);
            transform.position = transform.position + vel;
            transform.forward = vel;
            yield return new WaitForFixedUpdate();
        }
        transform.rotation = Quaternion.Euler(90, 0, 0);
        transform.GetComponent<Rigidbody>().useGravity = true;
        Invoke("changeplayer", 3);
    }


    public void changeplayer()
    {
        gameS.changePlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if it detects water, show the blue image
        Debug.Log("Impact with = " +other.tag);
        if (other.gameObject.tag == "water")
        {
            GameObject impGo = Instantiate(impact, dest.position, Quaternion.Euler(90, 0, 0)) as GameObject;
            ps.Stop();
            transform.GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject,12);
            impGo.transform.GetChild(0).GetComponent<Image>().enabled = true;
            impGo.transform.SetParent(container);
        }
        else if (other.gameObject.tag == "ship")
        {
            if (impactedShip == false)
            {
                Boat boatS;
                boatS = other.GetComponent<Boat>();
                boatS.nb_impacts += 1;
                GameObject impGo = Instantiate(impact, dest.position, Quaternion.Euler(90, 0, 0)) as GameObject;
                impGo.transform.GetChild(1).GetComponent<Image>().enabled = true;
                impGo.transform.SetParent(container);
                impactedShip = true;
                transform.GetComponent<Rigidbody>().useGravity = false;
                transform.GetComponent<Rigidbody>().velocity = new Vector3();
            }
            else
            {
                
            }
        }
    
    }

 

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "ship")
        {
            transform.GetComponent<Rigidbody>().useGravity = false;
            transform.GetComponent<Rigidbody>().velocity = new Vector3();
            
        }
    }


    private void OnDestroy()
    {
        StopAllCoroutines();
    }

}
