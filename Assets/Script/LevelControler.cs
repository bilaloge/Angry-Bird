using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControler : MonoBehaviour
{
    Monster[] _monster;
    [SerializeField] string _nextLevelName;

    void OnEnable()
    {
        _monster = FindObjectsOfType<Monster>();
    }
    // Update is called once per frame
    void Update()
    {
        if (AllMonstersAreDead())
            GoToNextLevel();
    }

    void GoToNextLevel()
    {
        Debug.Log("Go To Level " + _nextLevelName);
        SceneManager.LoadScene(_nextLevelName);
    }

    bool AllMonstersAreDead()
    {
        foreach (Monster monster in _monster)
        {
            if (monster.gameObject.activeSelf)
                return false;
        }
        return true;
    }
}
