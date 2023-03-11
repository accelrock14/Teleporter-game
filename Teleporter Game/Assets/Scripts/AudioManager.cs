using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager bgInstance;

    public AudioSource bgMusic;

    // Start is called before the first frame update
    private void Awake()
    {
        if(bgInstance != null && bgInstance != this) {
            Destroy(this.gameObject);
            return;
        }
        bgInstance = this;
        DontDestroyOnLoad(this);
    }
}
