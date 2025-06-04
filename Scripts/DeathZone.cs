using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public float shrinkDuration = 120f;
    public Vector2 targetXZScale = Vector2.zero;

    private Vector3 _initialScale;
    private float _elapsedTime;

    private void Start()
    {
        _initialScale = transform.localScale;
    }

    private void Update()
    {
        if (_elapsedTime < shrinkDuration)
        {
            _elapsedTime += Time.deltaTime;
            var t = _elapsedTime / shrinkDuration;
            var newX = Mathf.Lerp(_initialScale.x, targetXZScale.x, t);
            var newZ = Mathf.Lerp(_initialScale.z, targetXZScale.y, t);
            transform.localScale = new Vector3(newX, _initialScale.y, newZ);
        }
        else
        {
            transform.localScale = new Vector3(targetXZScale.x, _initialScale.y, targetXZScale.y);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy") || other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }
    }
}