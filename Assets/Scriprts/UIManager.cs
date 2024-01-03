using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject characterSelectionPanel;
    public GameObject MainMenu;
    //public GameObject multiplayerPanel;
    public GameObject SettingsMenu;
    public GameObject player;

    private void Start()
    {
        // Ensure that the initial state is set correctly
        SetInitialState();
    }

    private void SetInitialState()
    {
        // Disable UI panels initially
        characterSelectionPanel.SetActive(false);
        MainMenu.SetActive(true);
        //multiplayerPanel.SetActive(true);

        // Hide the cursor
        //Cursor.visible = false;
    }


    public void OnPlayButtonPressed()
    {
        // Enable the character selection panel and disable others
        characterSelectionPanel.SetActive(true);
        player.SetActive(true);
        MainMenu.SetActive(false);
        // Add more panels as needed
    }

    public void OnSettingsButtonPressed()
    {
        // Enable the settings panel and disable others
        characterSelectionPanel.SetActive(false);
        SettingsMenu.SetActive(true);
        // Add more panels as needed
    }

    public void OnExitButtonPressed()
    {
        // Exit the game
        Application.Quit();
    }

    public void OnBackButtonPressed()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }

    
}
