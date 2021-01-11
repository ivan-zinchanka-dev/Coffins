using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private void Awake()
    {       
        DontDestroyOnLoad(this.gameObject);
        Application.LoadLevel("MainScene");
    }

}
