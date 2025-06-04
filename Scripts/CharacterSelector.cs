using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public GameObject[] characters;
    private int _selectedCharacter = 0;

    public void LoopCharacter()
    {
        characters[_selectedCharacter].SetActive(false);
        _selectedCharacter = (_selectedCharacter + 1) % characters.Length;
        characters[_selectedCharacter].SetActive(true);
    }

    public GameObject GetSelectedCharacter()
    {
        return characters[_selectedCharacter];
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("SelectedCharacter", _selectedCharacter);
        PlayerPrefs.Save();
    }
}