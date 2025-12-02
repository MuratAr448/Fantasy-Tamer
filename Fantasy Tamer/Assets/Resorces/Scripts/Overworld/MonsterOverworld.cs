using UnityEngine;

public class MonsterOverworld : MonoBehaviour
{
    private PlayerMovement player;
    public GameObject monster;
    Vector3 monsterPosition;
    private Rigidbody2D RB2D;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        monsterPosition = transform.position;
        RB2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }
    void Movement()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (!player.TPing && 2 >= dist)
        {
            RB2D.MovePosition(player.transform.position + new Vector3(0.1f, 0.1f, 0) * 2.5f * Time.fixedDeltaTime);
        }
        else
        {
            RB2D.MovePosition(monsterPosition + new Vector3(0.1f, 0.1f, 0) * 2.5f * Time.fixedDeltaTime);
        }
    }
}
