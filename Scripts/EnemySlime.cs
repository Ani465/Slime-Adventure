using UnityEngine;
using UnityEngine.AI;

public class EnemySlime : MonoBehaviour
{
    private static readonly int Hop = Animator.StringToHash("Hop");
    public NavMeshAgent agent;
    public float range; // Range to detect the player
    public float detectionRadius = 1f; // Radius to detect nearby enemies
    public float sizeIncrement = 0.5f; // Size increase amount
    private GameObject _player;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        HandleMovement();
        HandleCollisions();
    }

    private void HandleMovement()
    {
        _animator.SetBool(Hop, agent.velocity.magnitude > 0.01f);
        if (_player != null)
        {
            var distance = Vector3.Distance(_player.transform.position, transform.position);

            if (distance < range)
            {
                agent.SetDestination(_player.transform.position);
            }
            else
            {
                SearchCoin();
            }
        }
    }

    private void SearchCoin()
    {
        var coins = GameObject.FindGameObjectsWithTag("Coin");
        GameObject nearestCoin = null;
        var shortestDistance = Mathf.Infinity;

        foreach (var coin in coins)
        {
            float distanceToCoin = Vector3.Distance(transform.position, coin.transform.position);

            if (distanceToCoin < shortestDistance)
            {
                shortestDistance = distanceToCoin;
                nearestCoin = coin;
            }
        }

        if (nearestCoin != null)
        {
            agent.SetDestination(nearestCoin.transform.position);
        }
    }

    private void HandleCollisions()
    {
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, detectionRadius);

        foreach (var obj in nearbyObjects)
        {
            if (obj.CompareTag("Enemy") && obj.gameObject != gameObject)
            {
                var otherTransform = obj.transform;

                float mySize = transform.localScale.magnitude;
                float otherSize = otherTransform.localScale.magnitude;

                if (mySize > otherSize)
                {
                    Destroy(otherTransform.gameObject);
                    IncreaseSize(sizeIncrement);
                }
                else if (mySize < otherSize)
                {
                    Destroy(gameObject);
                    otherTransform.localScale += Vector3.one * sizeIncrement;
                }
            }
        }
    }

    private void IncreaseSize(float increment)
    {
        transform.localScale += Vector3.one * increment;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
