using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    [SerializeField] private GameObject playerPlace;
    public Monsters monsterPlayer;
    [SerializeField] private TextMeshProUGUI ShowPlayerLV;
    [SerializeField] private Slider ShowPlayerHP;
    [SerializeField] private GameObject opponentPlace;
    public Monsters monsterOpponent;
    [SerializeField] private TextMeshProUGUI ShowOpponentLV;
    [SerializeField] private Slider ShowOpponentHP;
    public List<MoveOption> moveOptions;
    [SerializeField] private GameObject scriptTurns;
    public void SelectOption(MoveOption option)
    {
        for (int i = 0; i < moveOptions.Count; i++)
        {
            moveOptions[i].selected = false;
        }
        option.selected = true;
    }
    public void Begin()
    {
        StartTurn();
    }
    public void StartTurn()
    {
        Instantiate(monsterPlayer.gameObject,playerPlace.transform);
        ShowPlayerLV.text = "LV: " + monsterPlayer.LV;
        ShowPlayerHP.maxValue = monsterPlayer.HPMax;
        ShowPlayerHP.value = monsterPlayer.HPCurrent;
        Instantiate(monsterOpponent.gameObject, opponentPlace.transform);
        ShowOpponentLV.text = "LV: " + monsterOpponent.LV;
        ShowOpponentHP.maxValue = monsterOpponent.HPMax;
        ShowOpponentHP.value = monsterOpponent.HPCurrent;
    }
    private float STABCheck()
    {
        if ((int)monsterPlayer.type1 == (int)monsterPlayer.CurrentMove.type)
        {
            return 1.5f;
        }else if ((int)monsterPlayer.type2 == (int)monsterPlayer.CurrentMove.type)
        {
            return 1.5f;
        }
        else
        {
            return 1;
        }
    }
    private float SuperEffective(AcionMove Movetype,Monsters oponent)
    {
        float effectiveness = 1;
        switch (Movetype.type)
        {
            case AcionMove.AuraType.Null: return effectiveness;
            case AcionMove.AuraType.Blaze:
                if (oponent.type1 == Monsters.Aura1.Floral|| oponent.type2 == Monsters.Aura2.Floral)
                {
                    effectiveness *= 2;
                }
                if (oponent.type1 == Monsters.Aura1.Blaze || oponent.type2 == Monsters.Aura2.Blaze || 
                    oponent.type1 == Monsters.Aura1.Aqua || oponent.type2 == Monsters.Aura2.Aqua)
                {
                    effectiveness *= 0.5f;
                }
                return effectiveness;
            case AcionMove.AuraType.Aqua:
                if (oponent.type1 == Monsters.Aura1.Blaze || oponent.type2 == Monsters.Aura2.Blaze)
                {
                    effectiveness *= 2;
                }
                if (oponent.type1 == Monsters.Aura1.Aqua || oponent.type2 == Monsters.Aura2.Aqua || 
                    oponent.type1 == Monsters.Aura1.Floral || oponent.type2 == Monsters.Aura2.Floral)
                {
                    effectiveness *= 0.5f;
                }
                return effectiveness;
            case AcionMove.AuraType.Floral:
                if (oponent.type1 == Monsters.Aura1.Aqua || oponent.type2 == Monsters.Aura2.Aqua)
                {
                    effectiveness *= 2;
                }
                if (oponent.type1 == Monsters.Aura1.Blaze || oponent.type2 == Monsters.Aura2.Blaze || 
                    oponent.type1 == Monsters.Aura1.Floral || oponent.type2 == Monsters.Aura2.Floral ||
                    oponent.type1 == Monsters.Aura1.Spark || oponent.type2 == Monsters.Aura2.Spark)
                {
                    effectiveness *= 0.5f;
                }
                return effectiveness;
            case AcionMove.AuraType.Spark:
                if (oponent.type1 == Monsters.Aura1.Aqua || oponent.type2 == Monsters.Aura2.Aqua)
                {
                    effectiveness *= 2;
                }
                if (oponent.type1 == Monsters.Aura1.Floral || oponent.type2 == Monsters.Aura2.Floral ||
                    oponent.type1 == Monsters.Aura1.Spark || oponent.type2 == Monsters.Aura2.Spark)
                {
                    effectiveness *= 0.5f;
                }
                return effectiveness;
            default: return effectiveness;
        }
    }
    private float Crit(bool High)
    {
        float crit = 1;
        int limitCrit = 16;
        if (High)
        {
            limitCrit = (int)(limitCrit * 0.5f);
        }
        int rand = UnityEngine.Random.Range(0, limitCrit);
        if (rand >= limitCrit)
        {
            crit = crit * 1.5f;
        }
        return crit;
    }
    public void PlayerAttack() 
    {

        float times = (float)monsterPlayer.OffenceCurrent / (float)monsterOpponent.DefenceCurrent;
        times = (float)Math.Round(times,3);
        float damage = (2 * monsterPlayer.LV) * SuperEffective(monsterPlayer.CurrentMove, monsterOpponent) * times * Crit(monsterPlayer.CurrentMove.critHigh) * STABCheck();
        Debug.Log(damage);
        monsterOpponent.HPCurrent -= (int)damage;
        if((int)damage >= monsterOpponent.HPCurrent)
        {
            monsterOpponent.HPCurrent=0;
        }
        else
        {
            monsterOpponent.HPCurrent -= (int)damage;
        }
        ShowOpponentHP.value = monsterOpponent.HPCurrent;

    }
    public void EndPlayerTurn(bool Attack)
    {
        AcionMove AcionOption = null;
        int moveAmount = 0;
        for (int i = 0; i < monsterOpponent.Moves.Count; i++)
        {
            if (monsterOpponent.Moves[i] != null)
            {
                moveAmount++;
            }
            AcionOption = monsterOpponent.Moves[UnityEngine.Random.Range(0, moveAmount)];
        }
        monsterOpponent.CurrentMove = AcionOption;
        if (Attack)
        {
            SpeedCompare();
        }
        else
        {
            OpponentAttack();
        }
    }

    public void OpponentAttack()
    {
        float times = (float)monsterOpponent.OffenceCurrent / (float)monsterPlayer.DefenceCurrent;
        times = (float)Math.Round(times, 3);
        float damage = (2 * monsterOpponent.LV) * SuperEffective(monsterOpponent.CurrentMove, monsterPlayer) * times * Crit(monsterOpponent.CurrentMove.critHigh) * STABCheck();
        Debug.Log(damage+ "opponet");
        
        if ((int)damage >= monsterPlayer.HPCurrent)
        {
            monsterPlayer.HPCurrent = 0;
        }
        else
        {
            monsterPlayer.HPCurrent -= (int)damage;
        }
        ShowPlayerHP.value = monsterPlayer.HPCurrent;
    }
    public void SpeedCompare()
    {
        if (monsterPlayer.CurrentMove.Priority>monsterOpponent.CurrentMove.Priority)
        {
            StartCoroutine(PlayerFirst());
        }
        else if(monsterPlayer.CurrentMove.Priority < monsterOpponent.CurrentMove.Priority)
        {
            StartCoroutine(OponentFirst());
        }
        else
        {
            if (monsterPlayer.Speed > monsterOpponent.Speed)
            {
                StartCoroutine(PlayerFirst());
            }
            else if (monsterPlayer.Speed < monsterOpponent.Speed)
            {
                StartCoroutine(OponentFirst());
            }
            else
            {
                int rand = UnityEngine.Random.Range(0, 2);
                if (rand == 0)
                {
                    StartCoroutine(PlayerFirst());
                }
                else
                {
                    StartCoroutine(OponentFirst());
                }
            }
        }
    }
    IEnumerator PlayerFirst()
    {
        PlayerAttack();
       yield return new WaitForSeconds(1f);
        if (monsterOpponent.HPCurrent! <= 0)
        {
            OpponentAttack();
            Debug.Log("opAttack");
        }
        else
        {
            Debug.Log("Battle end");
            FindObjectOfType<PlayerMovement>().BattleEnd();
        }

    }
    IEnumerator OponentFirst()
    {
        yield return new WaitForSeconds(1f);
        if (monsterPlayer.HPCurrent!<=0)
        {
            PlayerAttack();
        }
        else
        {
            Debug.Log("Battle end");
            FindObjectOfType<PlayerMovement>().BattleEnd();
        }
    }
}
