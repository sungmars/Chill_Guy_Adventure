using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSkillUI : MonoBehaviour
{
    public ToggleGroup skillToggle;
    public ToggleGroup mouseToggle;
    public List<Toggle> toggles; // Toggle 리스트 (Inspector에서 할당)

    public int GetSkillOrder()
    {
        Toggle selectedToggle = skillToggle.GetFirstActiveToggle();

        if (selectedToggle != null)
        {
            int selectedIndex = toggles.IndexOf(selectedToggle);
            return selectedIndex;
        }
        return -1;
    }

    public int GetMouseOrder()
    {
        Toggle selectedToggle = mouseToggle.GetFirstActiveToggle();

        if (selectedToggle != null)
        {
            int selectedIndex = toggles.IndexOf(selectedToggle);
            return selectedIndex;
        }
        return -1;
    }


}
