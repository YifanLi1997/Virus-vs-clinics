﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [Range(0f, 5f)]
    [SerializeField] float currentSpeed = 1f;
    [SerializeField] int fullHealth = 6666;

    [SerializeField] AudioClip deathSFX;
    [SerializeField] Slider healthBar;

    // for visualization
    [SerializeField] bool isActive = false;
    [SerializeField] bool isDead = false;
    [SerializeField] int currentHealth = 6666;

    private void Start()
    {
        healthBar.maxValue = fullHealth;
        healthBar.value = currentHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Defender" || collision.gameObject.tag == "Attacker")
        {
            Destroy(collision.gameObject);
        }
    }

    public void SetMovementSpeed(float speed)
    {
        currentSpeed = speed;

        if (currentSpeed > 0)
        {
            isActive = true;
            GetComponent<PolygonCollider2D>().enabled = true;
        }
    }

    public bool GetIsActive()
    {
        return isActive;
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    void Update()
    {
        transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
    }

    public void DealDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;
        if (currentHealth <= 0)
        {
            isDead = true;
            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponent<Animator>().SetTrigger("die");

            AudioSource.PlayClipAtPoint(
                deathSFX,
                Camera.main.transform.position,
                PlayerPrefsController.GetVolume());
        }
    }
}
