using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    // Start is called before the first frame update

   
    LookAtGiven lookingObject;
	public Camera Camera1; 
	public Camera Camera2;
   


   
   
public void CameraSwitcher(int player)
{


	if (player == 1)
	{
		Camera1.enabled = true;
		Camera2.enabled = false;
		
	}
if (player == 2)
	{
		Camera1.enabled = false;
		Camera2.enabled = true;
		
	}
if(player == -1 || player == -2)
{
		Camera1.enabled = false;
		Camera2.enabled = false;
		Camera.main.enabled = true;
}

}

   
   
    

    

}
