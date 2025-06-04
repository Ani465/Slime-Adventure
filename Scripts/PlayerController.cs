using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class PlayerController : MonoBehaviour
{
    private static readonly int Hop = Animator.StringToHash("Hop");
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sizeIncrease = 0.5f;
    private FixedJoystick _joystick;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        _joystick = FindObjectOfType<FixedJoystick>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(_joystick.Horizontal * moveSpeed, rb.velocity.y, _joystick.Vertical * moveSpeed);

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity.normalized);
            animator.SetBool(Hop, true);
        }
        else
        {
            animator.SetBool(Hop, false);
        }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemyTransform = collision.transform;

            var playerSize = transform.localScale.magnitude;
            var enemySize = enemyTransform.localScale.magnitude;

            if (playerSize > enemySize)
            {
                Destroy(enemyTransform.gameObject);
                IncreaseSize(sizeIncrease);
            }
            else if (playerSize < enemySize)
            {
                Destroy(gameObject);
                enemyTransform.localScale += Vector3.one * sizeIncrease;
            }
        }
    }

    private void IncreaseSize(float increment)
    {
        transform.localScale += Vector3.one * increment;
    }
}