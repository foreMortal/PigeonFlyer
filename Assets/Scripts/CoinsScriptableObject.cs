using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CoinsScriptableObject", order = 1)]
public class CoinsScriptableObject : ScriptableObject
{
    [NonSerialized] public int maxCoins = 0, maxDistance = 0, levelNumber = 0, defeatedBosses;
    [NonSerialized] public float distanceTillBoss = 0;
}
