using UnityEngine;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private MoveOption moveOption1;
    [SerializeField] private MoveOption moveOption2;
    [SerializeField] private MoveOption moveOption3;
    [SerializeField] private MoveOption moveOption4;
    [SerializeField] private GameObject Options;
    [SerializeField] private GameObject BattleOptions;
    public void PressedFight()
    {
        Options.SetActive(false);
        BattleOptions.SetActive(true);
    }
    public void PressedTamed()
    {

    }
    public void PressedCapture()
    {

    }
    public void PressedRun()
    {

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
    public void Back()
    {
        Options.SetActive(true);
        BattleOptions.SetActive(false);
    }

}
