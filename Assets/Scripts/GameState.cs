using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class GameState : MonoBehaviour
{
    #region dataSave
    public Vector3 clothPos;
    public List<Vector3> clothsPos;
    #endregion

    public static GameState GetCurrentState()
    {
        GameState gameStateToSave = new GameState();
        SavedElement[] savedElements = GameObject.FindObjectsOfType<SavedElement>();
        gameStateToSave.clothsPos = new List<Vector3>();
        foreach (SavedElement element in savedElements)
        {
            if (element.type == SavedElement.Type.Cloth)
            {
                gameStateToSave.clothPos = element.transform.position;
            }
        }

        return gameStateToSave;
    }

    public void LoadGameState()
    {
        SavedElement[] elementstoScene = GameObject.FindObjectsOfType<SavedElement>();
        List<Vector3> remainingPos = new List<Vector3>(clothsPos);
        foreach (SavedElement elementtoLoad in elementstoScene)
        {
            if (elementtoLoad.type == SavedElement.Type.Cloth)
            {
                elementtoLoad.transform.position = clothPos;
            }
        }
    }
}
