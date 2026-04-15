using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public AchievementDatabase database;
    public AchievementNotificationController notificationController;
    public AchievementDropdownController dropdownController;

    public GameObject achievementItemPrefab;
    public Transform scrollViewContent;

    public AchievementID achievementToShow;

    [SerializeField, HideInInspector]
    private List<AchievementItemController> achievementItems = new();

    private void Start()
    {
        dropdownController.OnValueChanged += (id) => achievementToShow = id;
        LoadAchievementsTable();
    }

    public void ShowNotification()
    {
        var achievement = database.achievements[(int)achievementToShow];
        notificationController.ShowNotification(achievement);
    }

    [ContextMenu("Load Achievements Table")]
    private void LoadAchievementsTable()
    {
        // Clean up existing items
        foreach (var item in achievementItems)
        {
            if (item != null) DestroyImmediate(item.gameObject);
        }
        achievementItems.Clear();

        // Populate from database
        foreach (var achievement in database.achievements)
        {
            var obj = Instantiate(achievementItemPrefab, scrollViewContent);
            var item = obj.GetComponent<AchievementItemController>();

            item.achievement = achievement;
            item.unlocked = PlayerPrefs.GetInt(achievement.id, 0) == 1;
            item.RefreshView();

            achievementItems.Add(item);
        }
    }

    public void UnlockAchievement() => UnlockAchievement(achievementToShow);

    public void UnlockAchievement(AchievementID achievementID)
    {
        int index = (int)achievementID;
        if (index < 0 || index >= achievementItems.Count) return;

        var item = achievementItems[index];
        if (item.unlocked) return;

        ShowNotification();
        PlayerPrefs.SetInt(item.achievement.id, 1);
        PlayerPrefs.Save(); // Explicit save for safety

        item.unlocked = true;
        item.RefreshView();
    }

    public void LockAllAchievements()
    {
        foreach (var achievement in database.achievements)
            PlayerPrefs.DeleteKey(achievement.id);

        foreach (var item in achievementItems)
        {
            item.unlocked = false;
            item.RefreshView();
        }
    }
}