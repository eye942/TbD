using UnityEngine;

public class Arrow : MonoBehaviour
{
    private static readonly float ForceX = 0.0f;
    private static readonly float ForceY = 0.0f;
    private static readonly float VelocityX = -11.0f;
    private static readonly float VelocityY = 6.0f;
    private AudioSource ArrowHit;
    
    private Rigidbody2D _rigidBody;
    private Vector2 _initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        ArrowHit   = GetComponent<AudioSource>();
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
            var parent = collision.gameObject.transform.parent;
            if(parent)
            {
                parent.GetComponent<enemyDmgHitbox>()?.DamageEnemy(BalanceManager.ArrowDamage);
            }
            if(manager)
            {
                manager.UpdateProjectileDamage(this.GetType().Name, BalanceManager.ArrowDamage);
            }
            Die();
        }
    }

    private void Die()
    {
        // Debug.Log("Arrow - Die()");
       if(transform.position.y < -4.5)
        {
             gameObject.SetActive(false);
             Destroy(gameObject );
             return;
        }
        AudioSource.PlayClipAtPoint(ArrowHit.clip, transform.position);
        gameObject.SetActive(false);

        Destroy(gameObject, ArrowHit.clip.length );
    }
}
