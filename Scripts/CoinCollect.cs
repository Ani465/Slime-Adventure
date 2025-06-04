using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            var playerSize = other.transform;
            playerSize.localScale += Vector3.one * 0.1f;
        }
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            var enemySize = other.transform;
            enemySize.localScale += Vector3.one * 0.1f;
        }
    }
}
