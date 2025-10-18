using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject Shot;
    public float projectileSpeed = 20f;
    public Transform firePoint;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    public float initialDelay = 0f;
    public AudioClip shootSound;
    public int shotsPerFire = 1;

    public float minDistance = 2f;
    public float rotationSpeed = 5f;
    public float sprintSpeed = 6f;
    public float cornerInset = 2f;

    private Transform player;
    private Vector3 targetCorner;
    private bool reachedCorner = false;

    void Start()
    {
        if (fireRate <= 0f) fireRate = 0.5f;
        nextFireTime = Time.time + initialDelay;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        PickRandomCorner();
    }

    void PickRandomCorner()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        Vector3 bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 topRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.nearClipPlane));

        Vector3[] corners = new Vector3[4];
        corners[0] = new Vector3(bottomLeft.x + cornerInset, bottomLeft.y + cornerInset, 0);
        corners[1] = new Vector3(bottomLeft.x + cornerInset, topRight.y - cornerInset, 0);
        corners[2] = new Vector3(topRight.x - cornerInset, bottomLeft.y + cornerInset, 0);
        corners[3] = new Vector3(topRight.x - cornerInset, topRight.y - cornerInset, 0);

        Vector3 newCorner;
        do
        {
            newCorner = corners[Random.Range(0, 4)];
        } while (newCorner == targetCorner);

        targetCorner = newCorner;
        reachedCorner = false;
    }

    void Update()
    {
        if (player == null) return;

        Vector2 direction = player.position - transform.position;
        float distance = direction.magnitude;

        // if player gets too close, pick a new corner
        if (distance < minDistance && reachedCorner)
        {
            PickRandomCorner();
        }

        // move toward target corner
        if (!reachedCorner)
        {
            float step = sprintSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetCorner, step);
            if (Vector2.Distance(transform.position, targetCorner) < 0.1f)
            {
                reachedCorner = true;
            }
        }

        // always face player
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        float angle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // only shoot when stationary
        if (reachedCorner && Time.time >= nextFireTime)
        {
            Vector2 fireDir = firePoint.up;
            float angleDiff = Vector2.Angle(fireDir, direction.normalized);
            if (angleDiff < 10f)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        nextFireTime = Time.time + fireRate;
        if (Shot == null || firePoint == null) return;

        if (shootSound != null)
        {
            AudioSource.PlayClipAtPoint(shootSound, firePoint.position);
        }

        int count = Mathf.Max(1, shotsPerFire);
        for (int i = 0; i < count; i++)
        {
            GameObject projectile = Instantiate(Shot, firePoint.position, firePoint.rotation);
            Rigidbody2D rb2d = projectile.GetComponent<Rigidbody2D>();
            if (rb2d != null)
            {
                rb2d.gravityScale = 0;
                rb2d.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);
            }
        }
    }
}
