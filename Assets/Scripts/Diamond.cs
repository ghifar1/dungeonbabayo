using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public int gems = 1;
    //OnTriggerEnter to Collect
    private void OnTriggerEnter2D(Collider2D other)
    {
        //check for the player
        if (other.tag == "Player")
        {
            //add the value of the diamond to the player & collect
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                player.AddGems(gems);
                //player.diamonds += gems;
                Destroy(this.gameObject);
            }
            
        }
    }
}
