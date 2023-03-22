using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isActive = false;
    public Color activatedColor;

    SpriteRenderer spriteRenderer;

    private void Update()
    {
        if(isActive)
        {
            IfActive();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isActive = true;
        }
    }

    void IfActive()
    {
            spriteRenderer.color = activatedColor;
            spriteRenderer.flipX = true;
    }
}
