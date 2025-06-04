using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _player;
    public Vector3 offset;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        if (_player != null)
        {
            transform.position = _player.position + offset;
            transform.LookAt(_player);
        }
    }
}