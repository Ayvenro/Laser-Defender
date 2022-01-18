using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float shotCounter;
    [SerializeField] private float minTimeBetweenShots = 0.2f;
    [SerializeField] private float maxTimeBetweenShots = 3f;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private AudioClip deathSound;
    [SerializeField, Range(0, 1)] private float deathSoundVolume = 0.25f;
    [SerializeField] private AudioClip fireSound;
    [SerializeField, Range(0, 1)] private float fireSoundVolume;
    private float durationOfExplosion = 1f;
    private int scoreValue = 50;

    private void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void Update()
    {
        CountDownAndShoot();       
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(fireSound, transform.position, fireSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)     { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound, transform.position, deathSoundVolume);
    }
}
