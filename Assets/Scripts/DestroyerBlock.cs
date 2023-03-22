using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerBlock : MonoBehaviour
{
    public GameObject destroyedBlocks;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.contacts.Length > 0)
            {
                ContactPoint2D contactPoint = collision.contacts[0];

                if (Vector2.Dot(contactPoint.normal, Vector2.up) >= 0.9f)
                {
                    Destroy(destroyedBlocks);
                    Destroy(gameObject);
                }
            }
        }
    }
}