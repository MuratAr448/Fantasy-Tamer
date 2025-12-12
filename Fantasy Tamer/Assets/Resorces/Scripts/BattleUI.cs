using UnityEngine;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private MoveOption moveOption1;
    [SerializeField] private MoveOption moveOption2;
    [SerializeField] private MoveOption moveOption3;
    [SerializeField] private MoveOption moveOption4;
    [SerializeField] private GameObject Options;
    [SerializeField] private GameObject BattleOptions;
    [SerializeField] private GameObject ChooseMonsterOptions;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private TurnSystem turnSystem;
    public void PressedFight()
    {
        Options.SetActive(false);
        BattleOptions.SetActive(true);
    }
    public void PressedTamed()
    {
        Options.SetActive(false);
        ChooseMonsterOptions.SetActive(true);
    }
    public void PickTamed(int Choose)
    {
        if (Choose != 0)
        {
            Monsters temp = player.playerMonsters[Choose];
            player.playerMonsters[Choose] = player.playerMonsters[0];
            player.playerMonsters[0] = temp;
            turnSystem.EndPlayerTurn(false);
        }
    }

    public void PressedCapture()
    {

    }
    public void PressedRun()
    {

    }
    public void BackToOptions()
    {
        Options.SetActive(true);
        BattleOptions.SetActive(false);
        ChooseMonsterOptions.SetActive(false);
    }

    public void PressedMove1()
    {
        moveOption1.Selected();
    }
    public void PressedMove2()
    {
        moveOption2.Selected();
    }
    public void PressedMove3() 
    {
        moveOption3.Selected();
    }
    public void PressedMove4()
    {
        moveOption4.Selected();
    }
}
