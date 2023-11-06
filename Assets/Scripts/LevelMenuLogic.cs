using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuLogic : MonoBehaviour {

    public static LevelMenuLogic instance;

    private void Awake()
    {
        instance = this;
    }

	public List<LevelButton> levelButtons;
	public List<Button> buttons;
	public List<GameObject> levelButtonCollectors;

	public TMP_Text pageText;

	private int currentPage = 0;

	private void Start () 
	{
		UpdateLevelButtons();
	}

	public void UpdateLevelButtons()
	{
        for (int i = 0; i < levelButtons.Count; i++)
        {
            if (SaveSystem.instance.saveData.levelsUnlocked.Contains(levelButtons[i].levelID))
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
            }
        }

        levelButtonCollectors[currentPage].gameObject.SetActive(true);
    }

	public void ScrollLevelListForward()
	{
		if (currentPage < levelButtonCollectors.Count - 1)
		{
            levelButtonCollectors[currentPage].gameObject.SetActive(false);
            currentPage++;
			levelButtonCollectors[currentPage].gameObject.SetActive(true);

            int pageNum = currentPage + 1;

            pageText.text = "Page: " + pageNum.ToString();
        }
	}

    public void ScrollLevelListBack()
    {
        if (currentPage > 0)
        {
            levelButtonCollectors[currentPage].gameObject.SetActive(false);
            currentPage--;
            levelButtonCollectors[currentPage].gameObject.SetActive(true);

			int pageNum = currentPage + 1;

            pageText.text = "Page: " + pageNum.ToString();
        }
    }
}
