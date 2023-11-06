using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuLogic : MonoBehaviour {

    public string sandBoxLevel;

    public GameObject Title, Options, sandbox, levelSelect, character;

    public void OpenOptions()
    {
        if (Title.gameObject.activeSelf == true)
        {
            Title.gameObject.SetActive(false);
            Options.gameObject.SetActive(true);
        }
        else
        {
            Title.gameObject.SetActive(true);
            Options.gameObject.SetActive(false);
        }
    }

    public void OpenCharacter()
    {
        if (Title.gameObject.activeSelf == true)
        {
            Title.gameObject.SetActive(false);
            character.gameObject.SetActive(true);
        }
        else
        {
            Title.gameObject.SetActive(true);
            character.gameObject.SetActive(false);
        }
    }

    public void OpenSandbox()
    {
        if (Title.gameObject.activeSelf == true)
        {
            Title.gameObject.SetActive(false);
            sandbox.gameObject.SetActive(true);
        }
        else
        {
            Title.gameObject.SetActive(true);
            sandbox.gameObject.SetActive(false);
        }
    }

    public void OpenLevelSelect()
    {
        if (Title.gameObject.activeSelf == true)
        {
            Title.gameObject.SetActive(false);
            levelSelect.gameObject.SetActive(true);
        }
        else
        {
            Title.gameObject.SetActive(true);
            levelSelect.gameObject.SetActive(false);
        }
    }

    public void DeleteSave()
    {
        SaveSystem.instance.ClearSave();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
