using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public AchievementDatabase database;
    public AchievementNotificationController achievementNotificationController;
    public AchievementDropdownController achievementDropdownController;
    public GameObject achievementItemPrefab;
    public Transform scrollViewContent;
    public AchievementID achievementToShow;

    [SerializeField]
    [HideInInspector]
    private List<AchievementItemController> achievementItems = new List<AchievementItemController>();

    private void Start()
    {
        if (achievementDropdownController != null)
            achievementDropdownController.onValueChanged += HandleAchievementDropdownValueChanged;

        LoadAchievementsTable();
    }

    public void ShowNotification()
    {
        // 1. Safety check: Is the database assigned in the Inspector?
        if (database == null)
        {
            Debug.LogError("Achievement Manager: Please drag your Database asset into the Inspector slot!");
            return;
        }

        Debug.Log("Triggering Animator...");

        // 2. Get the achievement safely
        int index = (int)achievementToShow;
        if (index >= 0 && index < database.achievements.Count)
        {
            Achievement achievement = database.achievements[index];

            // 3. Check if the controller exists
            if (achievementNotificationController != null)
            {
                achievementNotificationController.ShowNotification(achievement);
            }
        }
    }

    private void HandleAchievementDropdownValueChanged(AchievementID achievement)
    {
        achievementToShow = achievement;
    }

    [ContextMenu("LoadAchievementsTable()")]
    private void LoadAchievementsTable()
    {
        // Safety Check for Line 55
        if (database == null || database.achievements == null || achievementItemPrefab == null) return;

        if (achievementItems == null) achievementItems = new List<AchievementItemController>();

        // Clean up old items
        for (int i = achievementItems.Count - 1; i >= 0; i--)
        {
            if (achievementItems[i] != null)
                DestroyImmediate(achievementItems[i].gameObject);
        }
        achievementItems.Clear();

        // Create new items
        foreach (Achievement achievement in database.achievements)
        {
            GameObject obj = Instantiate(achievementItemPrefab, scrollViewContent);
            AchievementItemController item = obj.GetComponent<AchievementItemController>();

            if (item != null)
            {
                bool unlocked = PlayerPrefs.GetInt(achievement.id, 0) == 1;
                item.unlocked = unlocked;
                item.achievement = achievement;
                item.RefreshView();
                achievementItems.Add(item);
            }
        }
    }

    public void UnlockAchievement() => UnlockAchievement(achievementToShow);

    public void UnlockAchievement(AchievementID achievement)
    {
        int index = (int)achievement;
        if (achievementItems == null || index >= achievementItems.Count) return;

        AchievementItemController item = achievementItems[index];
        if (item == null || item.unlocked) return;

        ShowNotification();
        PlayerPrefs.SetInt(item.achievement.id, 1);
        item.unlocked = true;
        item.RefreshView();
    }

    public void LockAllAchievements()
    {
        foreach (Achievement achievement in database.achievements)
        {
            PlayerPrefs.DeleteKey(achievement.id);
        }
        foreach (AchievementItemController item in achievementItems)
        {
            item.unlocked = false;
            item.RefreshView();
        }
    }

}