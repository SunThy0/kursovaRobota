using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameMang : MonoBehaviour
{
    public GameObject[] boats;
    public int player = 3;
    public int selectedBoat = 0;
    public GameObject overObject, clickObject;
    public GameObject mess_player1_start, mess_player2_start, mess_player1_loadout_select,mess_player2_loadout_select,
	mess_player1, mess_player2, mess_gameOver;
    public int click = 0;
    public GameObject prev;
    public bool canPlace = true;
    CamScript cmS;
    public GameObject[] boats_player1, boats_player2;
    public GameObject shooting_square;
    public bool shoot;
    public Transform c_p1, c_p2;
    Shooting shootScript;
	LoadOutScript los;
	public int decider;
	public int temp1;	
	public int temp2;	

    void Start()
    {
		
        boats_player1 = new GameObject[boats.Length];
        boats_player2 = new GameObject[boats.Length];
        cmS = Camera.main.GetComponent<CamScript>();
        shootScript = shooting_square.GetComponent<Shooting>();
		
        //mess_player1_start.SetActive(true);
        mess_player1_loadout_select.SetActive(true);  
      	player = 0;
		
		
        
    }
    
	void loSelect1()
    {
		los = GameObject.FindGameObjectWithTag("Holder").GetComponent<LoadOutScript>();
		Debug.Log("Decider is : "+decider);
		
		if(decider == 1)
		{
			mess_player1_start.SetActive(true); 
			 player = decider;
			boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat], new Vector3(20000, 20000, 20000),
                    Quaternion.Euler(0, 0, 0)) as
                GameObject;
            boats_player1[selectedBoat].transform.SetParent(c_p1);
            cmS.CameraSwitcher(player);
			decider = 0;}
		if(decider == 21)
         {
			Debug.Log("here");
			mess_player2_start.SetActive(true); 
			player = 2;
			boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat], new Vector3(20000, 20000, 20000),
                    Quaternion.Euler(0, 0, 0)) as
                GameObject;
            boats_player2[selectedBoat].transform.SetParent(c_p2);
            cmS.CameraSwitcher(player);
			decider = 0;
		}
		if(decider == 2)
		{
			temp1 = 1;
			player = 3;
			decider = 0;
		}
		if(decider == 3)
		{
			temp1 = 2;
			player = 3;
			decider = 0;
		}
		if(decider == 4)
		{
			temp1 = 3;
			player = 3;
			decider = 0;	
		}
		if(decider == 5)
		{
			temp1 = 4;
			player = 3;
			decider = 0;
		}
		if(decider == 6)
		{
			temp1 = 5;
			player = 3;
			decider = 0;
		}	
		if(decider == 22)
		{
			temp2 = 1;
			player = 4;
			decider = 0;
		}
		if(decider == 23)
		{
			temp2 = 2;
			player = 4;
			decider = 0;
		}
		if(decider == 24)
		{
			temp2 = 3;
			player = 4;
			decider = 0;
		}
		if(decider == 25)
		{
			temp2 = 4;
			player = 4;
			decider = 0;
		}
		if(decider == 26)
		{
			temp2 = 5;
			player = 4;
			decider = 0;
		}
    }
    void FixedUpdate()
    {
		if(player == 0)
		{
			loSelect1();
		}
      	
        else if (player == 3)
        {
            SelectorPreGen2(temp1);
			
            disableBoats(boats_player1);
       		mess_player2_loadout_select.SetActive(true);
			player = 31;
			changePlayer();
           	
        }
        else if (player == 4)
        {
            SelectorPreGen(temp2);
            disableBoats(boats_player2);
			mess_player2.SetActive(true);
          player = -4;
          changePlayer();
        }
      


        else if (player == 1)
        {
 			
                if (overObject != null)
                {
			
                    if (overObject.tag == "grid_square_P1")
                    {
                        if (click == 0)
                        {
                            boats_player1[selectedBoat].transform.position = overObject.transform.position;
                        }
                        else if (click == 1)
                        {
                            Vector3 dir = overObject.transform.position - boats_player1[selectedBoat].transform.position;
                            if (Mathf.Abs(dir.z / dir.x) > 1e5 || Mathf.Abs(dir.z / dir.x) < 0.1)
                            {
                                boats_player1[selectedBoat].transform.forward = dir;
                            }
                        }
                        else if (click == 2)
                        {
                            if (canPlace)
                            {
                                boats_player1[selectedBoat].transform.GetChild(1).GetComponent<SimpleFloating>().enabled =
                                    true;
                                boats_player1[selectedBoat].transform.GetChild(0).GetComponent<Boat>().disableCanvas();
                                selectedBoat += 1;
                                if (selectedBoat >= boats.Length)
                                {	
                                    disableBoats(boats_player1);
                                    
                                  mess_player2_loadout_select.SetActive(true);

									selectedBoat = 0;
									decider = 0;
									player = 0;
                                   	
									
                                  
                                }
                                else
                                {
                                    boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                                        new Vector3(20000, 20000, 20000), Quaternion.Euler(0, 0, 0)) as GameObject;
                                    boats_player1[selectedBoat].transform.SetParent(c_p1);
                                }
    
                                click = 0;
                            }
                            else
                            {
                                click = 0;
                            }
                        }
    
                    }
    
                }
                
        }
        else if (player == 2)
        {
           
            if (overObject != null)
            {
                //only if selected a grid square of player 1
                if (overObject.tag == "grid_square_P2")
                {
                    if (click == 0)
                    {
                        boats_player2[selectedBoat].transform.position = overObject.transform.position;
                    }
                    else if (click == 1)
                    {
                        //we obtain the direction for rotation
                        Vector3 dir = overObject.transform.position - boats_player2[selectedBoat].transform.position;
                        // diagonals are not allowed
                        if (Mathf.Abs(dir.z / dir.x) > 1e5 || Mathf.Abs(dir.z / dir.x) < 0.1)
                        {
                            boats_player2[selectedBoat].transform.forward = dir;
                        }
                    }
                    else if (click == 2)
                    {
                        //only if it is possible to place the boat, create a new boat
                        if (canPlace)
                        {
                            boats_player2[selectedBoat].transform.GetChild(1).GetComponent<SimpleFloating>().enabled =
                                true;
                            boats_player2[selectedBoat].transform.GetChild(0).GetComponent<Boat>().disableCanvas();

                            //increase the boat number
                            selectedBoat += 1;

                            //when there is no more boats to place
                            if (selectedBoat >= boats.Length)
                            {
                                player = -1;

                                //go to player2 zone
                                cmS.CameraSwitcher(player);

                                mess_player2.SetActive(true);
                                //disable boats
                                disableBoats(boats_player2);


                            }
                            else
                            {
                                //instantiate boat and set parent
                                boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                                    new Vector3(20000, 20000, 20000), Quaternion.Euler(0, 0, 0)) as GameObject;
                                boats_player2[selectedBoat].transform.SetParent(c_p2);
                            }

                            click = 0;
                        }
                        else
                        {
                            click = 0;
                        }
                    }
                }
            }
        }
        else if (player == -1)
        {
            disableBoats(boats_player2);
            if (clickObject != null)
            {
                if (click > 0 && clickObject.tag == "grid_square_P2" && !shoot)
                {
                    shooting_square.SetActive(true);
                    shooting_square.transform.position = clickObject.transform.position;
                    shootScript.p = 1;
                }
                else
                {
                    shooting_square.SetActive(false);
                }
            }

            if (checkGameOver(boats_player1, boats_player2))
            {
                mess_gameOver.SetActive(true);
            }
        }
        else if (player == -2)
        {
            
            disableBoats(boats_player1);

            if (clickObject != null)
            {
                if (click > 0 && clickObject.tag == "grid_square_P1" && !shoot)
                {
                   
                    shooting_square.SetActive(true);
                    shooting_square.transform.position = clickObject.transform.position;
                    shootScript.p = 2;

                }
                else
                {
                    shooting_square.SetActive(false);
                }
            }

            if (checkGameOver(boats_player1, boats_player2))
            {
                mess_gameOver.SetActive(true);
            }
        }
		else if(player == -4)
		{
			player = -1;
			cmS.CameraSwitcher(player);
		}

    }
    public void changePlayer()
    {
		
		if(player == 31)
		{
			player = 0;
		}
        if (player == -1)
        {
            mess_player1.SetActive(true);
            player = -2;
            shoot = false;
            cmS.CameraSwitcher(player);


        }
		
        else if (player == -2)
        {
            mess_player2.SetActive(true);
            player = -1;
            shoot = false;
            cmS.CameraSwitcher(player);

        }
    }
    public void disableBoats(GameObject[] go)
    {
        for (int ii = 0; ii < go.Length; ii++)
        {
            Boat boatS;
            boatS = go[ii].transform.GetChild(0).GetComponent<Boat>();
            if (boatS.destroyed == false)
            {
                MeshRenderer[] renderers;
                renderers = go[ii].GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer renderer in renderers)
                {
                    renderer.enabled = false;
                }
            }
        }
    }
    public void enableBoats(GameObject[] go)
    {
        for (int ii = 0; ii < go.Length; ii++)
        {
            MeshRenderer[] renderers;
            renderers = go[ii].GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in renderers)
            {
                renderer.enabled = true;
            }
        }
    }
    bool checkGameOver(GameObject[] g1, GameObject[] g2)
    {
        //check if the player 1 or player 2 has been destroyed
        bool end1 = true;
        bool end2 = true;

        for (int ii = 0; ii < g1.Length; ii++)
        {
            end1 = end1 & g1[ii].transform.GetChild(0).GetComponent<Boat>().destroyed;
        }

        for (int ii = 0; ii < g2.Length; ii++)
        {
            end2 = end2 & g2[ii].transform.GetChild(0).GetComponent<Boat>().destroyed;
        }

        return (end1 || end2);
    }
    void SelectorPreGen(int n)
    {
        if (temp2 == 1)
        {
            selectedBoat = 0;
            boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)17.8975, 0, (float)61.54), Quaternion.Euler(0, 0, 0)) as GameObject;
            boats_player2[selectedBoat].transform.SetParent(c_p2);
            selectedBoat++;
            boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)1.656494, 0, (float)81.7), Quaternion.Euler(0, 0, 0)) as GameObject;
            boats_player2[selectedBoat].transform.SetParent(c_p2);

            selectedBoat++;
            boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)-6.3, 0, (float)69.6), Quaternion.Euler(0, 0, 0)) as GameObject;
            boats_player2[selectedBoat].transform.SetParent(c_p2);

            selectedBoat++;
            boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)1.9, 0, (float)93.7), Quaternion.Euler(0, 90, 0)) as GameObject;
            boats_player2[selectedBoat].transform.SetParent(c_p2);


        }

        if (temp2 == 2)
        {
            selectedBoat = 0;
            boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)9.9, 0, (float)81.6), Quaternion.Euler(0, 0, 0)) as GameObject;
            boats_player2[selectedBoat].transform.SetParent(c_p2);
            selectedBoat++;
            boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)-10.3, 0, (float)77.4), Quaternion.Euler(0, 0, 0)) as GameObject;
            boats_player2[selectedBoat].transform.SetParent(c_p2);

            selectedBoat++;
            boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)13.8, 0, (float)65.4), Quaternion.Euler(0, 270, 0)) as GameObject;
            boats_player2[selectedBoat].transform.SetParent(c_p2);

            selectedBoat++;
            boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)-2, 0, (float)93.6), Quaternion.Euler(0, 90, 0)) as GameObject;
            boats_player2[selectedBoat].transform.SetParent(c_p2);



        }

        if (temp2 == 3)
        {
            selectedBoat = 0;
            boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)9.9, 0, (float)81.6), Quaternion.Euler(0, 270, 0)) as GameObject;
            boats_player2[selectedBoat].transform.SetParent(c_p2);
            selectedBoat++;
            boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)-18.2, 0, (float)81.4), Quaternion.Euler(0, 0, 0)) as GameObject;
            boats_player2[selectedBoat].transform.SetParent(c_p2);

            selectedBoat++;
            boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)-2.2, 0, (float)65.4), Quaternion.Euler(0, 0, 0)) as GameObject;
            boats_player2[selectedBoat].transform.SetParent(c_p2);

            selectedBoat++;
            boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)-10, 0, (float)73.7), Quaternion.Euler(0, 0, 0)) as GameObject;
            boats_player2[selectedBoat].transform.SetParent(c_p2);



        }

        if (temp2 == 4)
        {
            selectedBoat = 0;
            boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)17.8, 0, (float)97.5), Quaternion.Euler(0, 270, 0)) as GameObject;
            boats_player2[selectedBoat].transform.SetParent(c_p2);
            selectedBoat++;
            boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)10.1, 0, (float)81.4), Quaternion.Euler(0, 180, 0)) as GameObject;
            boats_player2[selectedBoat].transform.SetParent(c_p2);

            selectedBoat++;
            boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)-2.2, 0, (float)81.7), Quaternion.Euler(0, 0, 0)) as GameObject;
            boats_player2[selectedBoat].transform.SetParent(c_p2);

            selectedBoat++;
            boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)18, 0, (float)73.6), Quaternion.Euler(0, 360, 0)) as GameObject;
            boats_player2[selectedBoat].transform.SetParent(c_p2);



        }

        if (temp2 == 5)
        {
            selectedBoat = 0;
            boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)2, 0, (float)77.6), Quaternion.Euler(0, 270, 0)) as GameObject;
            boats_player2[selectedBoat].transform.SetParent(c_p2);
            selectedBoat++;
            boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)-9.8, 0, (float)69.6), Quaternion.Euler(0, 90, 0)) as GameObject;
            boats_player2[selectedBoat].transform.SetParent(c_p2);

            selectedBoat++;
            boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)-10.3, 0, (float)81.5), Quaternion.Euler(0, 0, 0)) as GameObject;
            boats_player2[selectedBoat].transform.SetParent(c_p2);

            selectedBoat++;
            boats_player2[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)-17.9, 0, (float)61.8), Quaternion.Euler(0, 0, 0)) as GameObject;
            boats_player2[selectedBoat].transform.SetParent(c_p2);



        }
    } 
void SelectorPreGen2(int n)
    {
        if (temp1 == 1)
        {
            selectedBoat = 0;
            boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)17.8975, 0, (float)-18), Quaternion.Euler(0, 0, 0)) as GameObject;
            boats_player1[selectedBoat].transform.SetParent(c_p1);

            selectedBoat++;
            boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)1.656494, 0, (float)2.16), Quaternion.Euler(0, 0, 0)) as GameObject;
            boats_player1[selectedBoat].transform.SetParent(c_p1);

            selectedBoat++;
            boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)-6.3, 0, (float)5.06), Quaternion.Euler(0, 0, 0)) as GameObject;
            boats_player1[selectedBoat].transform.SetParent(c_p1);

            selectedBoat++;
            boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)-5.38, 0, (float)-18), Quaternion.Euler(0, 90, 0)) as GameObject;
            boats_player1[selectedBoat].transform.SetParent(c_p1);

		
        }

        if (temp1 == 2)
        {
            selectedBoat = 0;
            boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)18, 0, (float)2.16), Quaternion.Euler(0, 0, 0)) as GameObject;
            boats_player1[selectedBoat].transform.SetParent(c_p1);

            selectedBoat++;
            boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)1.656494, 0, (float)-18), Quaternion.Euler(0, 0, 0)) as GameObject;
            boats_player1[selectedBoat].transform.SetParent(c_p1);

            selectedBoat++;
            boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)-5.5, 0, (float)5.9), Quaternion.Euler(0, 90, 0)) as GameObject;
            boats_player1[selectedBoat].transform.SetParent(c_p1);

            selectedBoat++;
            boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)-2, 0, (float)18.1), Quaternion.Euler(0, 90, 0)) as GameObject;
            boats_player1[selectedBoat].transform.SetParent(c_p1);



        }

        if (temp1 == 3)
        {
           selectedBoat = 0;
            boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)-18, 0, (float)2.16), Quaternion.Euler(0, 180, 0)) as GameObject;
            boats_player1[selectedBoat].transform.SetParent(c_p1);

            selectedBoat++;
            boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)2.1, 0, (float)-10), Quaternion.Euler(0, 270, 0)) as GameObject;
            boats_player1[selectedBoat].transform.SetParent(c_p1);

            selectedBoat++;
            boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)18.1, 0, (float)-6.1), Quaternion.Euler(0, 0, 0)) as GameObject;
            boats_player1[selectedBoat].transform.SetParent(c_p1);

            selectedBoat++;
            boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)-2, 0, (float)18.1), Quaternion.Euler(0, 270, 0)) as GameObject;
            boats_player1[selectedBoat].transform.SetParent(c_p1);



        }

        if (temp1 == 4)
        {
            selectedBoat = 0;
            boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)-18, 0, (float)-13.9), Quaternion.Euler(0, 180, 0)) as GameObject;
            boats_player1[selectedBoat].transform.SetParent(c_p1);

            selectedBoat++;
            boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)17.9, 0, (float)17.7), Quaternion.Euler(0, 270, 0)) as GameObject;
            boats_player1[selectedBoat].transform.SetParent(c_p1);

            selectedBoat++;
            boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)-10.4, 0, (float)-6.1), Quaternion.Euler(0, 0, 0)) as GameObject;
            boats_player1[selectedBoat].transform.SetParent(c_p1);

            selectedBoat++;
            boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)9.8, 0, (float)-14.1), Quaternion.Euler(0, 0, 0)) as GameObject;
            boats_player1[selectedBoat].transform.SetParent(c_p1);


        }

        if (temp1 == 5)
        {
           selectedBoat = 0;
            boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)6.2, 0, (float)18), Quaternion.Euler(0, 180, 0)) as GameObject;
            boats_player1[selectedBoat].transform.SetParent(c_p1);

            selectedBoat++;
            boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)-2.2, 0, (float)1.9), Quaternion.Euler(0, 270, 0)) as GameObject;
            boats_player1[selectedBoat].transform.SetParent(c_p1);

            selectedBoat++;
            boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)10, 0, (float)-6.1), Quaternion.Euler(0, 0, 0)) as GameObject;
            boats_player1[selectedBoat].transform.SetParent(c_p1);

            selectedBoat++;
            boats_player1[selectedBoat] = GameObject.Instantiate(boats[selectedBoat],
                new Vector3((float)9.8, 0, (float)-14.1), Quaternion.Euler(0, 270, 0)) as GameObject;
            boats_player1[selectedBoat].transform.SetParent(c_p1);



        }
    } 
}



















