using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum Type { Default, Shotgun, SMG, Sniper }
    public Type type = Type.Default;
    [SerializeField] float gravityForce = 1;
    [SerializeField] float persistence = 30;
    public int damage = 1;
    public int startAmmo = 1;
    public float force = 1;
    public float fireDelay = 1;
    Planet planet;
    Rigidbody rigidbody;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        planet = FindObjectOfType<Planet>();
        //Destroy(gameObject, persistence);
        rigidbody.AddForce(transform.forward * force, ForceMode.VelocityChange);
    }

    private void FixedUpdate()
    {
        Vector3 direction = (planet.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.FromToRotation(transform.up, -direction) * transform.rotation;
        rigidbody.AddForce(direction * gravityForce, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerStats playerStatus;
        if (playerStatus = collision.collider.GetComponent<PlayerStats>())
        {
            playerStatus.Damage(damage);
        }

        Destroy(gameObject);
    }
}
