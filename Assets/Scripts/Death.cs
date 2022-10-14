using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{   
    private Animator anim;

    private GameObject player;
    private void Start(){
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Players");
    }
    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.CompareTag("Trap")) {
            Die();
        }
    }

    private void Die() {
        anim.SetTrigger("death");
        Rigidbody2D playerComponent = player.GetComponent<Rigidbody2D>();
        playerComponent.bodyType = RigidbodyType2D.Static;
    }
}
