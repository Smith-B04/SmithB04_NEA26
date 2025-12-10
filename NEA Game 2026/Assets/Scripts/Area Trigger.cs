//Created: Sprint 3
//Last Edited: Sprint 3
//Purpose: Activate enemies when the player enters area.

using System.Linq;
using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    public GameObject parent;
    private bool triggered;
    private RangedEnemyMovement[] rangedEnemies;
    private MeleeEnemyMovement[] meleeEnemies;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        triggered = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "PlayerCharacter" && !triggered)
        {
            rangedEnemies = this.transform.parent.GetComponentsInChildren<RangedEnemyMovement>();
            meleeEnemies = this.transform.parent.GetComponentsInChildren<MeleeEnemyMovement>();
            for (int i = 0; i < rangedEnemies.Count(); i++)
            {
                rangedEnemies[i].GetComponent<RangedEnemyMovement>().active = true;
            }
            for (int i = 0; i < meleeEnemies.Count(); i++)
            {
                meleeEnemies[i].GetComponent<MeleeEnemyMovement>().active = true;
            }
            audioSource.Play();
            triggered = true;
        }
    }
}
