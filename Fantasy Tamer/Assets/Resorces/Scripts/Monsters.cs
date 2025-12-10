using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Monsters : MonoBehaviour
{
    public enum Aura1
    {
        Null,
        Blaze,
        Aqua,
        Floral,
        Spark
    }
    public enum Aura2
    {
        Null,
        Blaze,
        Aqua,
        Floral,
        Spark,
        None
    }
    public enum GrowthRate
    {
        Fast,
        Medeaum,
        Slow
    }
    public bool opponent = false;
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
    public List<AcionMove> Moves;
    public AcionMove CurrentMove;
    [Range(1, 100)] public int LV;
    private int expNeeded=100;
    public int expHas;
    public string Name;
    public Image MonsterImage;
    public void Begin()
    {
        Show();
        LevelCalc();
        HPCurrent = HPMax;
    }
    public void Show()
    {
        if(opponent)
        {
            FrontSprite.SetActive(true);
            BackSprite.SetActive(false);
            gameObject.transform.localScale = new Vector3(1,1,1)*1.75f;
        }
        else
        {
            FrontSprite.SetActive(false);
            BackSprite.SetActive(true);
            gameObject.transform.localScale = new Vector3(1, 1, 1) * 2f;
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
