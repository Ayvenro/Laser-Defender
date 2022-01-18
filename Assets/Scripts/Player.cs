using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float padding;
    [SerializeField] private int health = 100;
    [SerializeField] private AudioClip deathSound;
    [SerializeField, Range(0, 1)] private float deathSoundVolume = 0.25f;

    [Header("Projectile")]
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileFiringPeriod;
    [SerializeField] private AudioClip fireSound;
    [SerializeField, Range(0, 1)] private float fireSoundVolume;

    private Coroutine firingCoroutine;

    private float xMin;
    private float xMax;

    private float yMin;
    private float yMax;

    void Start()
    {
        SetUpMoveBoundaries();
    }

    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuosly());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            FindObjectOfType<Level>().LoadGameOver();
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(deathSound, transform.position, deathSoundVolume);
        }
    }

    private IEnumerator FireContinuosly()
    {
        while (true)
        {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        AudioSource.PlayClipAtPoint(fireSound, transform.position, fireSoundVolume);
        yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    public int GetHealth()
    {
        return health;
    }
}
