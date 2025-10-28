using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class MoveOption : MonoBehaviour
{
    [SerializeField] private int movePlace;
    AcionMove move;
    TurnSystem turnSystem;
    [SerializeField] private TextMeshProUGUI moveName;
    [SerializeField] private TextMeshProUGUI moveDiscription;
    public bool selected = false;
    public void Begin()
    {
        turnSystem = FindObjectOfType<TurnSystem>();
        if (turnSystem.monsterPlayer.Moves[movePlace]!=null)
        {
            GetComponent<Button>().enabled = true;
            move = turnSystem.monsterPlayer.Moves[movePlace];
            ChoseColor();
            moveName.text = move.name;
        }else
        {
            GetComponent<Button>().enabled = false;
            Debug.Log("asodonh");
        }
        
       
    }
    private void ChoseColor()
    {
        Image image = GetComponent<Image>();
        switch (move.type)
        {
            case AcionMove.AuraType.Null:
                image.color = Color.white;
                break;
            case AcionMove.AuraType.Blaze:
                image.color = new Color(1,0.333f,0);
                break;
            case AcionMove.AuraType.Aqua:
                image.color = new Color(0, 0.5f, 1);
                break;
            case AcionMove.AuraType.Floral:
                image.color = new Color(0.2f, 0.8f, 0);
                break;
            case AcionMove.AuraType.Spark:
                image.color = new Color(1, 1, 0.1f);
                break;
            default:
                image.color = Color.white;
                break;
        }
    }
    public void Selected()
    {
        
        if (selected)
        {
            Using();
        }else
        {
            turnSystem.SelectOption(this);
        }
        if(turnSystem.monsterPlayer.Moves[movePlace] != null)
        {
            moveDiscription.text = move.discription + Environment.NewLine +
    "Type: " + move.type.ToString() +
    " Power: " + move.basePower;
        }
    }
    public void Using()
    {
        turnSystem.monsterPlayer.CurrentMove = move;
        turnSystem.PlayerAttack();
    }
    private void Update()
    {

    }
}
