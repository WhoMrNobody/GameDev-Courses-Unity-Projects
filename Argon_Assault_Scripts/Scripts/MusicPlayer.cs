using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update

     void Awake()
    {
        int numberMusicPlayer = FindObjectsOfType<MusicPlayer>().Length;
        if (numberMusicPlayer > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        Invoke("LoadFirstScene", 2.5f);
    }

    void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
