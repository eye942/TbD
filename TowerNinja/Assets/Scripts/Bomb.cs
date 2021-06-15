using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private static readonly float ForceX = 0.0f;
    private static readonly float ForceY = 0.0f;
    private static readonly float VelocityX = -3.0f;
    private static readonly float VelocityY = 0.0f;
    private Rigidbody2D _rigidBody;
    private Vector2 _initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _initialPosition = transform.position;

        _rigidBody.velocity = new Vector2(VelocityX, VelocityY);
        _rigidBody.AddForce(new Vector2(ForceX, ForceY), ForceMode2D.Force);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -4.5) Die();
    }

    private void Die()
    {
        Debug.Log("Arrow - Die()");
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
