using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviourPunCallbacks
{
    public float speed = 5f;

    Vector3 movement;

    public float whipRate = 0.7F;
    private float nextWhip = 0.0F;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!photonView.IsMine)
        {
            return;
        }

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if(Input.GetMouseButton(0) && Time.time > nextWhip)
        {
            nextWhip = Time.time + whipRate;
            photonView.RPC("Whip", RpcTarget.All);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            photonView.RPC("Nigger", RpcTarget.All);
        }
    }

    private void FixedUpdate()
    {
        if(!photonView.IsMine)
        {
            return;
        }

        transform.position += movement * speed * Time.fixedDeltaTime;
    }

    [PunRPC]
    public void Whip()
    {
        GetComponent<AudioSource>().Play();
        GetComponentInChildren<Animator>().SetTrigger("Whip");
    }

    [PunRPC]
    public void Nigger()
    {
        foreach(Transform t in transform)
        {
            if(t.tag == "Nigger")
            {
                t.GetComponent<AudioSource>().Play();
            }
        }
    }
}
