using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager Instance;

    private Coroutine LoadCorutine;

    [SerializeField] private Animator _myanimator;

    private Dictionary<int, string> SceneDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        LearnDictionary();
    }

    public void NextScene(int Value)
    {
        if (LoadCorutine != null)
        {
            StopCoroutine(LoadCorutine);
            LoadCorutine = null;
        }
        LoadCorutine = StartCoroutine(LoadCorutineFunction(Value));
    }

    IEnumerator LoadCorutineFunction(int Value)
    {
        //_myanimator.SetTrigger("Next");

        //while (true)
        //{
        //    if (_myanimator.GetCurrentAnimatorStateInfo(0).IsName("1"))
        //        break;

        //    yield return null;
        //}

        AsyncOperation loadLevel = null;

        if (SceneDictionary.ContainsKey(Value))
        {
            loadLevel = SceneManager.LoadSceneAsync(SceneDictionary[Value]);
        }
        else
            Debug.LogError("Cala Cabron no has puesto bien el valor para cambiar escena");

        while (!loadLevel.isDone)
        {
            yield return null;
        }

        //ChargeScene(Value);

        //_myanimator.SetTrigger("Next");

        //while (true)
        //{
        //    if (_myanimator.GetCurrentAnimatorStateInfo(0).IsName("0"))
        //        break;


        //    yield return null;
        //}
    }

    private void LearnDictionary()
    {
        SceneDictionary = new Dictionary<int, string>();
        SceneDictionary.Add(1, "Level1");
        SceneDictionary.Add(2, "Level2");
        SceneDictionary.Add(2, "Level3");
    }


}