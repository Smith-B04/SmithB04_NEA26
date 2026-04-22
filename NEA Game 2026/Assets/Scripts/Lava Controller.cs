//Created: Sprint 6
//Last Edited: Sprint 6
//Purpose: Control the effect of lava on player and enemies

using UnityEngine;

public class LavaController : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        // find entities health script and subtract over time
        CharacterHealth player = other.GetComponent<CharacterHealth>();
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (player != null)
        {
            player.TakeDamage(Time.deltaTime * 10, "fire");
        }
        else if (enemy != null) 
        {
            enemy.TakeDamage(Time.deltaTime * 10, "fire");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // find entities are enemies or a player
        CharacterHealth player = other.GetComponent<CharacterHealth>();
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        // change players variables
        if (player != null)
        {
            other.GetComponent<CharacterMovement>().speedModifier = 0.05f;
            other.GetComponent<CharacterMovement>().velocityCap = 3f;
            other.GetComponent<CharacterMovement>().jumpModifier = 250f;
        }
        // find which enemy type and edit variables
        else if (enemy != null)
        {
            MeleeEnemyMovement melee = other.GetComponent<MeleeEnemyMovement>();
            RangedEnemyMovement ranged = other.GetComponent<RangedEnemyMovement>();

            if (melee != null)
            {
                melee.speedModifier = 0.05f;
                melee.jumpModifier = 233f;
            }
            else if (ranged != null)
            {
                ranged.speedModifier = 0.05f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // removes all effects and returns variables to normal
        CharacterHealth player = other.GetComponent<CharacterHealth>();
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (player != null)
        {
            other.GetComponent<CharacterMovement>().speedModifier = 0.1f;
            other.GetComponent<CharacterMovement>().velocityCap = 5f;
            other.GetComponent<CharacterMovement>().jumpModifier = 350f;
        }
        else if (enemy != null)
        {
            MeleeEnemyMovement melee = other.GetComponent<MeleeEnemyMovement>();
            RangedEnemyMovement ranged = other.GetComponent<RangedEnemyMovement>();

            if (melee != null)
            {
                melee.speedModifier = 0.1f;
                melee.jumpModifier = 475f;
            }
            else if (ranged != null)
            {
                ranged.speedModifier = 0.1f;
            }
        }
    }
}
