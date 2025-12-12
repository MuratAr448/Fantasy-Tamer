using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    public List<Monsters> playerMonsters;
    public List<MonsterInfo> MonsterInfos;
    public static PlayerMovement instance;
    
    Vector2 Idle;
    Vector2 movement;
    public float moveSpeed = 5f;
    public Rigidbody2D RB2D;
    public Animator animator;
    public Camera cam;
    [SerializeField] private GameObject campos;

    public GameObject BattleScene;
    public GameObject MonsterTeam;

    public bool inMenu = false;
    public bool inBattle = false;
    public bool TPing = false;

    private int Filling;
    [SerializeField] private Image uiFill;

    [SerializeField] private GameObject scriptTurns;
    [SerializeField] private TurnSystem turnSystem;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
        cam = Camera.main;
    }
    private void Start()
    {
        for (int i = 0; i < playerMonsters.Count; i++)
        {
            if (playerMonsters[i] != null)
            {
                playerMonsters[i].HPCurrent = playerMonsters[i].HPMax;
            }
        }
    }
    void Update()
    {
        Movement();
        if (!TPing && !inBattle)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                VeiwMonsters();
            }
        }
    }
    void Movement()
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
        float Dis = Vector3.Distance(cam.transform.position, campos.transform.position);
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
        if (!TPing && !inBattle&& !inMenu)
        {
            RB2D.MovePosition(RB2D.position + movement * moveSpeed * Time.fixedDeltaTime);

        }
        else
        {
            movement = new Vector2(0, 0);
        }
            
        CamFollow();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            inBattle = true;
            StartCoroutine(CoverScreen());
            turnSystem.monsterPlayer = playerMonsters[0];
            turnSystem.monsterOpponent = collision.gameObject.GetComponent<MonsterOverworld>().monster.GetComponent<Monsters>();
        }
    }
    public void VeiwMonsters()
    {
        if (!inMenu)
        {
            Monsters();
            inMenu = true;
            MonsterTeam.SetActive(true);
        }
        else
        {
            inMenu= false;
            MonsterTeam.SetActive(false);
        }
    }
    void Monsters()
    {
        for (int i = 0; i<playerMonsters.Count; i++)
        {
            if (playerMonsters[i]!=null)
            {
                MonsterInfos[i].gameObject.SetActive(true);
                MonsterInfos[i].monsterName = playerMonsters[i].Name;
                MonsterInfos[i].LV = playerMonsters[i].LV;
                MonsterInfos[i].HPMax = playerMonsters[i].HPMax;
                MonsterInfos[i].HPCurrent = playerMonsters[i].HPCurrent;
                MonsterInfos[i].MonsterImage = playerMonsters[i].MonsterImage;
            }
            else
            {
                MonsterInfos[i].gameObject.SetActive(false);
            }
        }
    }
    private IEnumerator RevealingScreen()
    {
        BattleScene.SetActive(inBattle);
        GameObject temp = Instantiate(scriptTurns);
        temp.GetComponent<ScriptTurns>().turnSystem = turnSystem;
        temp.GetComponent<ScriptTurns>().moves = turnSystem.moveOptions;
        Destroy(temp ,1);
        
        
        Filling = 10;
        while (Filling >= 0)
        {
            uiFill.fillAmount -= 0.1f;
            Filling--;
            yield return new WaitForSeconds(0.1f);

            yield return null;
        }
    }
    private IEnumerator CoverScreen()
    {
        Filling = 0;
        while (Filling <= 10)
        {
            uiFill.fillAmount += 0.1f;
            Filling++;
            yield return new WaitForSeconds(0.1f);

            yield return null;
        }
        StartCoroutine(RevealingScreen());
    }
    public void BattleEnd()
    {
        inBattle = false;
        StartCoroutine(CoverScreen());
    }
}