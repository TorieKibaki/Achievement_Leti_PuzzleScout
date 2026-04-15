using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Dropdown))]
public class AchievementDropdownController : MonoBehaviour
{
    private TMP_Dropdown m_dropdown;
    private TMP_Dropdown Dropdown => m_dropdown ??= GetComponent<TMP_Dropdown>();

    public event Action<AchievementID> OnValueChanged;

    private void Start()
    {
        UpdateOptions();
        Dropdown.onValueChanged.AddListener(HandleDropdownValueChanged);
    }

    [ContextMenu("Update Options")]
    public void UpdateOptions()
    {
        Dropdown.options.Clear();
        foreach (var name in Enum.GetNames(typeof(AchievementID)))
        {
            Dropdown.options.Add(new TMP_Dropdown.OptionData(name));
        }
        Dropdown.RefreshShownValue();
    }

    private void HandleDropdownValueChanged(int value)
    {
        OnValueChanged?.Invoke((AchievementID)value);
    }
}