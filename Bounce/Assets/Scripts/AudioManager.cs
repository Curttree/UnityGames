using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    Dictionary<string, int> fileIDs = new Dictionary<string, int>();

    private bool androidAudio;

    // Start is called before the first frame update
    void Start()
    {
        AndroidNativeAudio.makePool(5);
    }

    public void LoadSounds(string flap, string point, string death, string noBounce)
    {
        var fileID = AndroidNativeAudio.load(flap);
        fileIDs.Add(flap, fileID);

        var fileID2 = AndroidNativeAudio.load(point);
        fileIDs.Add(point, fileID2);

        var fileID3 = AndroidNativeAudio.load(death);
        fileIDs.Add(death, fileID3);

        var fileID4 = AndroidNativeAudio.load(noBounce);
        fileIDs.Add(noBounce, fileID4);
    }

    public void PlaySound(string fileName, float volume = 1f)
    {
        int SoundID = AndroidNativeAudio.play(fileIDs[fileName]);
        AndroidNativeAudio.setVolume(SoundID, volume);
    }

    void OnApplicationQuit()
    {
        if (androidAudio)
        {
            foreach (string x in fileIDs.Keys)
            {
                AndroidNativeAudio.unload(fileIDs[x]);
            }
            AndroidNativeAudio.releasePool();
        }
    }
}
