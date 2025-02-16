using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public static List<GameState> savedStates = new List<GameState>();

    private void Start()
    {
        SaveGameState();
    }

    public void GeriAl()
    {
        UndoMove();
    }

    public static void SaveGameState()
    {
        savedStates.Add(GameState.GetCurrentState());
    }

    public static void UndoMove()
    {
        if (savedStates.Count <= 1)
        {
            Debug.Log("no moves");
        }
        else
        {
            savedStates[savedStates.Count -2 ].LoadGameState();
            savedStates.RemoveAt(savedStates.Count-1);
        }
    }
}
