using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 1. Added TMPro namespace

// 2. Updated Requirement to TMP_Dropdown
[RequireComponent(typeof(TMP_Dropdown))]
public class AchievementDropdownController : MonoBehaviour
{
    // 3. Changed Dropdown to TMP_Dropdown
    private TMP_Dropdown m_dropdown;
    private TMP_Dropdown Dropdown
    {
        get
        {
            if (m_dropdown == null)
            {
                m_dropdown = GetComponent<TMP_Dropdown>();
            }
            return m_dropdown;
        }
    }

    public Action<AchievementID> onValueChanged;

    private void Start()
    {
        UpdateOptions();
        Dropdown.onValueChanged.AddListener(HandleDropdownValueChanged);
    }

    [ContextMenu("UpdateOptions()")]
    public void UpdateOptions()
    {
        Dropdown.options.Clear();
        var ids = Enum.GetValues(typeof(AchievementID));
        foreach (AchievementID id in ids)
        {
            // 4. Using TMP_Dropdown.OptionData
            Dropdown.options.Add(new TMP_Dropdown.OptionData(id.ToString()));
        }
        Dropdown.RefreshShownValue();
    }

    private void HandleDropdownValueChanged(int value)
    {
        if (onValueChanged != null)
        {
            onValueChanged((AchievementID)value);
        }
    }
}