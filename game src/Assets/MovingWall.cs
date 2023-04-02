using UnityEngine;

public class MovingWall : MonoBehaviour
{
    [SerializeField] private float speed = 30f;
    [SerializeField] private float duration = 5f;

    private float timer = 0f;
    public bool isFacingRight;

    private void Start()
    {
        isFacingRight = FindObjectOfType<PlayerMovement>().isFacingRight;
        Destroy(gameObject, duration); // destroy wall object after specified duration
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        float dir = isFacingRight ? speed : -speed; // move right if isFacingRight is true, left otherwise
        pos.x += dir * Time.deltaTime; // Remove the extra speed multiplication here
        transform.position = pos;
    }
}
