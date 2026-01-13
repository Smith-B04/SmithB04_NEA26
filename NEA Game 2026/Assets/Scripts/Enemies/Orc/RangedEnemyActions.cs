//Created: Sprint 3
//Last Edited: Sprint 3
//Purpose: Control the actions of an enemy

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RangedEnemyActions : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject Axe;
    public bool busy;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>(); //Get the rigid body
        animator = this.GetComponent<Animator>(); //Get the animator
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<RangedEnemyMovement>().active && !animator.GetBool("Dead") && !busy)
        {
            StartCoroutine("RangedAttack");
        }
    }

    private void RangedAttack()
    {
        if (!busy)
        {
            busy = true;
            animator.SetTrigger("ThrowAttack");
            StartCoroutine(RangedAttackFinish());
        }
    }

    private IEnumerator RangedAttackFinish()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject newAxe = Instantiate(
                Axe,
                new Vector2(
                    this.transform.position.x + 2.15f * this.transform.localScale.x / Math.Abs(this.transform.localScale.x),
                    this.transform.position.y),
                Quaternion.identity); //Instantiate arrow
        newAxe.transform.localScale = new Vector3(
            this.transform.localScale.x / Math.Abs(this.transform.localScale.x) * newAxe.transform.localScale.x,
            newAxe.transform.localScale.y,
            newAxe.transform.localScale.z
            ); //Change arrows starting direction to the same as the player
        StartCoroutine(attackWait());
    }

    private IEnumerator attackWait()
    {
        yield return new WaitForSeconds((float)(UnityEngine.Random.value * 0.5) + 1f);
        busy = false;
    }
}
