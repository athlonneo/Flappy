using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    [SerializeField] private float upForce = 100;
    [SerializeField] private bool isDead;
    [SerializeField] private UnityEvent OnJump, OnDead;

    [SerializeField] private int score;
    [SerializeField] private UnityEvent OnAddPoint;

    [SerializeField] private Text scoreText;

    private Rigidbody2D rigidBody2d;
    private Animator animator;

    void Start()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDead && Input.GetMouseButtonDown(0))
        {
            Jump();
        }


    }
    public bool IsDead()
    {
        return isDead;
    }

    public void Dead()
    {
        if (!isDead && OnDead != null)
        {
            OnDead.Invoke();
        }
        isDead = true;
    }

    void Jump()
    {
        if (rigidBody2d)
        {
            rigidBody2d.velocity = Vector2.zero;
            rigidBody2d.AddForce(new Vector2(0, upForce));
        }

        if (OnJump != null)
        {
            OnJump.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.enabled = false;
    }

    public void AddScore(int value)
    {
        score += value;

        if (OnAddPoint != null)
        {
            OnAddPoint.Invoke();
        }

        scoreText.text = score.ToString();
    }
}
