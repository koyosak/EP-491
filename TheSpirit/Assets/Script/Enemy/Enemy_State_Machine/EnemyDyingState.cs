using UnityEngine;

public class EnemyDyingState : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine enemy)
    {
        if (enemy.ReactiveTarget != null)
        {
            enemy.ReactiveTarget.ReactToHit();
            NotifyDefeat();

            // Start the color and particle effect change, then destroy the enemy after 2 seconds
            enemy.StartCoroutine(DyingEffect(enemy));
        }
        else
        {
            Debug.LogWarning("ReactiveTarget component is missing on the enemy!");
        }

        Debug.Log("ENEMY DYING state - Enter");
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        // No ongoing behavior needed for this state
    }

    public override void ExitState(EnemyStateMachine enemy)
    {
        Debug.Log("ENEMY DYING state - Exit");
    }

    private void NotifyDefeat()
    {
        // Find the RayShooter script to update defeated count
        RayShooter rayShooter = Object.FindObjectOfType<RayShooter>();
        if (rayShooter != null)
        {
            rayShooter.IncrementDefeatedEnemies();
        }
        else
        {
            Debug.LogWarning("RayShooter script not found. Ensure it is in the scene.");
        }
    }

    private System.Collections.IEnumerator DyingEffect(EnemyStateMachine enemy)
    {
        Renderer enemyRenderer = enemy.GetComponent<Renderer>();
        ParticleSystem particleSystem = enemy.GetComponentInChildren<ParticleSystem>();

        if (enemyRenderer != null)
        {
            // Change the color of the enemy
            Color originalColor = enemyRenderer.material.color;
            enemyRenderer.material.color = Color.white; // Change to red to indicate "dying"
        }

        if (particleSystem != null)
        {
            var mainModule = particleSystem.main;
            mainModule.startColor = Color.white; // Change the particle system's color
            particleSystem.Play(); // Ensure the particle system is playing
        }

        // Wait for 2 seconds
        yield return new WaitForSeconds(3f);

        // Destroy the enemy GameObject
        if (particleSystem != null)
        {
            particleSystem.Stop(); // Stop particle effects before destroying
        }

        Object.Destroy(enemy.gameObject);
    }
}


