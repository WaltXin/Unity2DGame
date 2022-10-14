using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{   
    private void Start() {}
    private int cherries = 0;

    [SerializeField] private Text cherriesText;
    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.CompareTag("Cherry")) {
            Destroy(coll.gameObject);
            cherries++;
            cherriesText.text = "Cherries: " + cherries;
        }
    }
}
