using UnityEngine;

public class Tester : MonoBehaviour
{
    public AchievementManager achievementManager;

    public void MilestoneReached()
    {
        // Ensure AchievementID.GoodBye exists in your Enum
        achievementManager.UnlockAchievement(AchievementID.GoodBye);
    }
}