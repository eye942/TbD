using UnityEngine;

public class Arrow : MonoBehaviour
{
    private static readonly float ForceX = 0.0f;
    private static readonly float ForceY = 0.0f;
    private static readonly float VelocityX = -7.0f;
    private static readonly float VelocityY = 4.0f;
    
    private Rigidbody2D _rigidBody;
    private Vector2 _initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _initialPosition = transform.position;
        transform.localEulerAngles = new Vector3(0, 0, -142);

        float randomOffsetVelocityX =  Random.Range(-1, 2);
        float randomOffsetVelocityY = Random.Range(-2, 2);

        _rigidBody.velocity = new Vector2(VelocityX + randomOffsetVelocityX, VelocityY + randomOffsetVelocityY);
        _rigidBody.AddForce(new Vector2(ForceX, ForceY), ForceMode2D.Force);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -4.5) Die();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            var managerObject = GameObject.Find("ResourceManager");
            var manager = managerObject.GetComponent<ResourceManager>();
            Debug.LogError("Enemy Collision");
            Debug.Log(collision.gameObject);
            var parent = collision.gameObject.transform.parent;
            parent.GetComponent<enemyDmgHitbox>()?.DamageEnemy(BalanceManager.ArrowDamage);
            manager.UpdateProjectileDamage(this.GetType().Name, BalanceManager.ArrowDamage);
            Die();
        }
    }

    private void Die()
    {
        // Debug.Log("Arrow - Die()");
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
