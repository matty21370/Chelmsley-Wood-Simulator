using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Player p in FindObjectsOfType<Player>())
        {
            if(p.photonView.IsMine)
            {
                player = p;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -3f), new Vector3(player.transform.position.x, player.transform.position.y, -3f), 1f);
    }
}
