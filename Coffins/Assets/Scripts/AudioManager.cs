using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //static AudioManager instance;


    private void Awake()
    {
        //this.GetComponent<AudioSource>().

       
        DontDestroyOnLoad(this.gameObject);
        Application.LoadLevel("MainScene");

    }

}
