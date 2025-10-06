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
    public enum GrowthRate
    {
        Fast,
        Medeaum,
        Slow
    }
    public bool Wild = false;
    public Aura1 type1;
    public Aura2 type2;
    public GrowthRate growth;
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
    public List<GameObject> Moves;
    [Range(1, 100)] private int LV;
    private int expNeeded=100;
    public int expHas;
    Canvas canvas;
    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        Show();
        LevelCalc();
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
    private void LevelCalc()
    {
        HPMax = (int)Mathf.Round(HP * 0.0314f * LV);
        OffenceCurrent = (int)Mathf.Round(Offence * 0.0314f * LV);
        DefenceCurrent = (int)Mathf.Round(Defence * 0.0314f * LV);
        SpeedCurrent = (int)Mathf.Round(Speed * 0.0314f * LV);
    }
    public int GiveExp()
    {
        int rate = (int)(HP * 0.01f);
        int expGive = expHas / LV;
        return expGive* rate;
    }
    public void ReciveExp(int giftedExp)
    {
        expHas = (int)(giftedExp / LV);

        if (expHas > expNeeded&&LV!=100)
        {
            LV++;
            LevelUp();
            LevelCalc();
        }
    }
    private void LevelUp()
    {
        switch (growth)
        {
            case GrowthRate.Fast: expNeeded = (int)(100 * LV * 3.14f * 1.0f);
                break;
            case GrowthRate.Medeaum: expNeeded = (int)(100 * LV * 3.14f * 1.3f);
                break;
            case GrowthRate.Slow: expNeeded = (int)(100 * LV * 3.14f * 1.5f);
                break;
            default:
                break;
        }
    }
}
