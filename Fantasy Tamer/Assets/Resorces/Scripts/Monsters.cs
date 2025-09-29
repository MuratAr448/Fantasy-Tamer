using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monsters : MonoBehaviour
{
    public enum Aura1
    {
        Blaze,
        Aqua,
        Floral,
        Spark
    }
    public enum Aura2
    {
        None,
        Blaze,
        Aqua,
        Floral,
        Spark
    }
    public bool Wild = false;
    public Aura1 type1;
    public Aura2 type2;
    public int MonsterID;
    [Range(1, 255)] public int HP;
    public int HPMax;
    public int HPCurrent;
    [Range(1, 255)] public int Offence;
    public int OffenceCurrent;
    [Range(1, 255)] public int Defence;
    public int DefenceCurrent;
    [Range(1, 255)] public int Speed;
    public int SpeedCurrent;
    [SerializeField] private GameObject FrontSprite;
    [SerializeField] private GameObject BackSprite;
    [SerializeField] private List<GameObject> Moves;
    [Range(1, 100)] private int LV;
    public int exp;
    Canvas canvas;
    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        Show();
    }
    public void Show()
    {
        if(Wild)
        {
            FrontSprite.SetActive(true);
            BackSprite.SetActive(false);
            transform.position = canvas.transform.position + new Vector3(200, 50, 0);
            gameObject.transform.localScale = new Vector3(1,1,1)*0.6f;
        }
        else
        {
            FrontSprite.SetActive(false);
            BackSprite.SetActive(true);
            transform.position = canvas.transform.position+ new Vector3(-200,-50,0);
            gameObject.transform.localScale = new Vector3(1, 1, 1) * 0.7f;
        }
        Debug.Log("Monster: " + MonsterID + " Base stat Total:" + (HP + Offence + Defence + Speed));
    }
    public void LevelCalc()
    {
        HPMax = (int)Mathf.Round(HP * 0.314f * LV);
        OffenceCurrent = (int)Mathf.Round(Offence * 0.314f * LV);
        DefenceCurrent = (int)Mathf.Round(Defence * 0.314f * LV);
        Speed = (int)Mathf.Round(Speed * 0.314f * LV);
    }
}
