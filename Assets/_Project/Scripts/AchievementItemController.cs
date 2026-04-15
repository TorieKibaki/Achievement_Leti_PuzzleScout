using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementItemController : MonoBehaviour
{
    [SerializeField] private Image unlockedIcon;
    [SerializeField] private Image lockedIcon;
    [SerializeField] private TextMeshProUGUI titleLabel;
    [SerializeField] private TextMeshProUGUI descriptionLabel;

    public bool unlocked;
    public Achievement achievement;

    public void RefreshView()
    {
        if (achievement == null) return;

        titleLabel.text = achievement.title;
        descriptionLabel.text = achievement.description;

        unlockedIcon.enabled = unlocked;
        lockedIcon.enabled = !unlocked;
    }

    private void OnValidate() => RefreshView();
}