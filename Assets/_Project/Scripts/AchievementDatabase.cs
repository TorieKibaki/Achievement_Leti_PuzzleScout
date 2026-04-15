using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementDatabase", menuName = "ScriptableObjects/Achievement Database")]
public class AchievementDatabase : ScriptableObject
{
    // Unity's Inspector now handles reordering automatically for Lists
    public List<Achievement> achievements = new();
}