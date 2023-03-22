using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;

    public float MovementSpeed;
    Vector3 direction;
    public bool isMovingToEnd = true;

    void Update()
    {
        if (EndPoint != null && StartPoint != null)
        {
            if (isMovingToEnd)
            {
                direction = EndPoint.position - transform.position;

                if (Vector2.Distance(transform.position, EndPoint.position) <= 0.25f)
                    isMovingToEnd = false;
            }
            else
            {
                direction = StartPoint.position - transform.position;

                if (Vector2.Distance(transform.position, StartPoint.position) <= 0.25f)
                    isMovingToEnd = true;
            }

            direction.Normalize();
            transform.position += direction * MovementSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
