using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text rowsInput;
    public Text columnsInput;
    public Dropdown algorithmInput;
    public Button playButton;
    public GameObject warningPopup;

    public float secondToShowWarning;

    private int rows;
    private int columns;

    private MazeBuilder mazeBuilder;

    private bool isPlayable = false;
    private bool isBuildable = false;

    private GameObject player;

    private void Awake()
    {
        mazeBuilder = FindObjectOfType<MazeBuilder>();
    }

    private void Update()
    {
        playButton.interactable = isPlayable;
    }

    public void OnBuildMaze()
    {
        //if there is a player in the scene we destroy it
        if (player != null)
        {
            Destroy(player.gameObject);
        }

        //get the input from the UI
        if (int.TryParse(rowsInput.text, out int result1) && int.TryParse(columnsInput.text, out int result2))
        {
            if (result1 > 0 && result2 > 0)
            {
                rows = result1;
                columns = result2;
                isBuildable = true;
            }
            else
            {
                StartCoroutine(WarningPanel());
            }
        }
        else
        {
            StartCoroutine(WarningPanel());
        }

        if (isBuildable)
        {
            mazeBuilder.BuildMaze(rows, columns, algorithmInput.value);
            isPlayable = true;
        }
    }


    public void OnPlayMaze(GameObject player)
    {
        this.player = Instantiate(player);
        isPlayable = false;
    }


    IEnumerator WarningPanel()
    {
        warningPopup.SetActive(true);
        yield return new WaitForSeconds(secondToShowWarning);
        warningPopup.SetActive(false);
    }
}
