using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private float lifeTime = 3f;
    private AudioManager audioManager;
    private Vector2 direction;
    public void SetDirection(bool faceRight)
    {
        direction = faceRight ? Vector2.right : Vector2.left;

        transform.localScale = faceRight
            ? new Vector3(1, 1, 1)
            : new Vector3(-1, 1, 1);
    }
    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
