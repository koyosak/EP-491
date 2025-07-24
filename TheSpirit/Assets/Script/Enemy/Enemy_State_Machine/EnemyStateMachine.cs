using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    // Reference to the currently active state
    EnemyBaseState _currentState;

    // Initialization of each possible state
    public EnemyAliveState AliveState = new();
    public EnemyDyingState DyingState = new();

    private bool _isDefeated = false;
    private bool _isAlive = true;
    public bool IsAlive { get => _isAlive; set { _isAlive = value; } }

    // Components that will handle essential behavior of the GameObject
    [HideInInspector] public ReactiveTarget ReactiveTarget;
    [HideInInspector] public WanderingAI WanderingAI;

    private void Awake()
    {
        // Retrieve components
        ReactiveTarget = GetComponent<ReactiveTarget>();
        WanderingAI = GetComponent<WanderingAI>();
    }

    private void Start()
    {
        // Initialize state
        SetState(AliveState);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    // Switch to a new state
    public void SetState(EnemyBaseState newState)
    {
        if (_currentState != null)
        {
            _currentState.ExitState(this);
        }
        _currentState = newState;
        _currentState.EnterState(this);
    }

    // This method is now used to decrement shots in the RayShooter and check if enemy is defeated
    public void ReactToHit()
    {
        if (_isDefeated) return;
        // Call the RayShooter's method to decrement shots
        RayShooter rayShooter = FindObjectOfType<RayShooter>();
        if (rayShooter != null)
        {
            rayShooter.DecreaseShotsToDefeatEnemy(); // Decrease the shots required
        }

        // If the shots reach 0, switch the enemy state to dying.
        if (IsAlive && rayShooter != null && rayShooter.ShotsLeft <= 0)
        {
            _isDefeated = true;
            SetState(DyingState); // Transition to the dying state
            IsAlive = false;

            // Increment defeated enemies in the RayShooter
            
                rayShooter.IncrementDefeatedEnemies();
            
        }
    }
}



