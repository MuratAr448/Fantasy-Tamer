using NUnit.Framework;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class ScriptTurns : MonoBehaviour
{
    [SerializeField] private GameObject playerPlace;
    private Monsters monsterPlayer;
    [SerializeField] private GameObject opponentPlace;
    private Monsters monsterOpponent;
    public TurnSystem turnSystem;
    public List<MoveOption> moves;
    void Start()
    {
        monsterPlayer = playerPlace.GetComponentInChildren<Monsters>();
        monsterOpponent = opponentPlace.GetComponentInChildren<Monsters>();
        StartCoroutine(Loading());
    }
    IEnumerator Loading()
    {
        monsterPlayer.Begin();
        monsterOpponent.Begin();
        yield return new WaitForSeconds(0.01f);
        turnSystem.Begin();
        yield return new WaitForSeconds(0.01f);
        for (int i = 0; i < moves.Count; i++)
        {
            moves[i].Begin();
        }

    }
}
