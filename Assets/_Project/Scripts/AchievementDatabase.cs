using System.Collections.Generic;
using UnityEngine;

// This line allows you to right-click in Project view to create the file
[CreateAssetMenu(fileName = "AchievementDatabase", menuName = "Achievements/Database")]
public class AchievementDatabase : ScriptableObject
{
    public List<Achievement> achievements;
}