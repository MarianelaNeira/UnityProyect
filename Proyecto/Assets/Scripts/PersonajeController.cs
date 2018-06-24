using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeController : MonoBehaviour {

	private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            GetComponentInChildren<GameController>().Weaken();
        }
            

        if (collision.collider.tag == "Item")
        {
            Destroy(collision.gameObject);
            GetComponentInChildren<GameController>().GetItem();
        }

    }
}
