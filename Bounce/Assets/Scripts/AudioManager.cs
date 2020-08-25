using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private int FileID;

    private void Awake()
    {
        MakeSingleton();
    }

    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set up Android Native Audio
        AndroidNativeAudio.makePool();
    }

    public void PlaySound(string filename, float volume = 1f)
    {
        //if (FileID > 0)
        //{
        //    AndroidNativeAudio.unload(FileID);
        //}

        FileID = AndroidNativeAudio.load(filename);
        int SoundID = AndroidNativeAudio.play(FileID);
        //if(volume != 1f)
        //{
        //    AndroidNativeAudio.setVolume(SoundID, volume);
        //}

    }

    void OnApplicationQuit()
    {
        // Clean up when done
        AndroidNativeAudio.unload(FileID);
        AndroidNativeAudio.releasePool();
    }
}
