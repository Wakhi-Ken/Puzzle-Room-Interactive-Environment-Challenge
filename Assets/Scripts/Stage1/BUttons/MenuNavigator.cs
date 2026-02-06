using UnityEngine;
using UnityEngine.UI;

public class MenuNavigator : MonoBehaviour
{
    public Button[] menuButtons;   // Assign buttons in Inspector
    private int selectedIndex = 0;

    void Start()
    {
        HighlightButton();
    }

    void Update()
    {
        // Navigate up
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex--;
            if (selectedIndex < 0)
                selectedIndex = menuButtons.Length - 1;
            HighlightButton();
        }

        // Navigate down
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex++;
            if (selectedIndex >= menuButtons.Length)
                selectedIndex = 0;
            HighlightButton();
        }

        // Activate selected button
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            menuButtons[selectedIndex].onClick.Invoke();
        }
    }

    void HighlightButton()
    {
        for (int i = 0; i < menuButtons.Length; i++)
        {
            ColorBlock colors = menuButtons[i].colors;
            if (i == selectedIndex)
            {
                colors.normalColor = Color.yellow;  // highlight selected
            }
            else
            {
                colors.normalColor = Color.white;   // default
            }
            menuButtons[i].colors = colors;
        }
    }
}
