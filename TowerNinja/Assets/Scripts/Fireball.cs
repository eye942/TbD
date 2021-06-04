using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fireball : MonoBehaviour
{
    private Vector2 initialPosition;
    private Rigidbody2D physics;
    // spring constant
    private float k;
    
    private float forceX = 0f;
    private int health;

    private void Start()
    {
        physics = GetComponent<Rigidbody2D>();
        // TODO revise spring constant based on difficulty
        k = Random.Range(1,5);

        var vX = Random.Range(1,5);
        // v = sqrt(2 / m * (-1/2) k x^2)
        var vY = -Mathf.Sqrt(1 / physics.mass * k * 5 * 5);
        physics.velocity = new Vector2(vX, vY);
        initialPosition = transform.position;
        physics.AddForce(new Vector2(forceX,0),ForceMode2D.Force);

        // TODO revise max and min based on difficulty
        health = Random.Range(1, 3);
    }

    private void Update()
    {
        var dy = physics.position.y - initialPosition.y;

        // Modify k -- spring constant
        // k *= Random.Range(1.0f, 1.3f);
        // k = k <= 0.01 ? k : 0.01f;
        // f_k = -k * dX
        var forceY = - k * dy;
        forceX *= Random.Range(0.001f, 0.005f);
        Debug.Log($"{dy},{forceY}");

        physics.AddForce(new Vector2(forceX,forceY),ForceMode2D.Force);
    }

    // Click on fireball to decrease health
    private void OnMouseDown()
    {
        Debug.Log("Click event");
        health -= 1;
        if (health <= 0)
        {
            Die();
        }
    }

    private void OnMouseDrag()
    {
        Debug.Log("Drag event");
        health -= 1;
        if (health <= 0)
        {
            Die();
        }
    }

    private void OnMouseOver()
    {
        Debug.Log("Mouse Over");
        
    }

    private void Die()
    {
        Debug.Log("Killed enemy");
        Destroy(this);
    }
}
