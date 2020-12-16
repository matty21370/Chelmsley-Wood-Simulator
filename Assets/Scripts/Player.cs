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

    public AudioSource nigger, whip;

    public float niggerRate;
    private float nextNigger = 0.0F;

    public GameObject theWhip;

    // Start is called before the first frame update
    void Start()
    {
        niggerRate = nigger.clip.length;

        if (!photonView.IsMine)
        {
            Destroy(Camera.main);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!photonView.IsMine)
        {
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(movement.x > 0.1 || movement.y > 0.1 || movement.x < -0.1 || movement.y < -0.1)
        {
            GetComponent<Animator>().SetBool("moving", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("moving", false);
        }

        if(Input.GetMouseButton(0) && Time.time > nextWhip)
        {
            nextWhip = Time.time + whipRate;
            photonView.RPC("Whip", RpcTarget.All);
        }

        if(Input.GetKey(KeyCode.Space) && Time.time > nextNigger)
        {
            nextNigger = Time.time + niggerRate;
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
        whip.Play();
        theWhip.GetComponent<Animator>().SetTrigger("Whip");
    }

    [PunRPC]
    public void Nigger()
    {
        nigger.Play();
    }
}
