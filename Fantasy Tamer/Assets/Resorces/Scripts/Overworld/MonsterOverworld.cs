using UnityEngine;

public class MonsterOverworld : MonoBehaviour
{
    private PlayerMovement player;
    public GameObject monster;
    Vector3 monsterPosition;
    private float distSee = 3;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        monsterPosition = transform.position;
    }
    void FixedUpdate()
    {
        if (!player.TPing && !player.inBattle&& !player.inMenu)
        {
            Movement();
        }
    }
    void Movement()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (distSee >= dist)
        {
            LookDiretion();
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 2.5f * Time.deltaTime);
            distSee = 5;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            transform.position = Vector3.MoveTowards(transform.position, monsterPosition, 2.5f * Time.deltaTime);
            distSee = 3;
        }
    }
    void LookDiretion()
    {
        if (player.transform.position.x<transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
