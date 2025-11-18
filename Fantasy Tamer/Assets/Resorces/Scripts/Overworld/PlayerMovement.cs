using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{

    Vector2 Idle;
    public float moveSpeed = 5f;
    public Rigidbody2D RB2D;
    public Animator animator;
    Vector2 movement;
    [SerializeField]
    Camera cam;
    [SerializeField]
    GameObject campos;
    float Dis = 0;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.magnitude);
        if (movement.magnitude > 0.1f)
        {
            Idle = movement;
            animator.SetFloat("Idle_H", Idle.x);
            animator.SetFloat("Idle_V", Idle.y);
        }




    }
    void CamFollow()
    {
        Dis = Vector3.Distance(cam.transform.position, campos.transform.position);
        if (Dis > 3 && cam.transform.position != campos.transform.position)
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, campos.transform.position, 8 * Time.deltaTime);
        }
        else
        if (cam.transform.position != campos.transform.position)
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, campos.transform.position, 3 * Time.deltaTime);
        }
    }
    private void FixedUpdate()
    {
        RB2D.MovePosition(RB2D.position + movement * moveSpeed * Time.fixedDeltaTime);
        CamFollow();
    }
}