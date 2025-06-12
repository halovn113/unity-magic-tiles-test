using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SongInfo
{
    public string songID = "";
    public string songName = "Untitled";
    public int tempo = 120;
    public float[] noteTimes;
}

[Serializable]
public class ListSongInfo
{
    public List<SongInfo> Data = new List<SongInfo>();
    public SongInfo GetSongInfo(string songID)
    {
        for (int i = 0; i < Data.Count; i++)
        {
            if (Data[i].songID == songID)
            {
                return Data[i];
            }
        }
        return null;
    }
}

[Serializable]
public class SongID
{
    public string songID = "";
    public AudioClip audioClip;
}