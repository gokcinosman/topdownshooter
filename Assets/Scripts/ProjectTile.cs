using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
        if (other.gameObject.CompareTag("Enemy"))
        {

        }
    }
}
