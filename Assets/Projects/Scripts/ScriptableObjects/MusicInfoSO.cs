using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicInfo", menuName = "ScriptableObjects/MusicInfoSO")]
public class MusicInfoSO : ScriptableObject
{
    public List<SongID> listSongID;

    public AudioClip GetAudioClip(string songID)
    {
        for (int i = 0; i < listSongID.Count; i++)
        {
            if (listSongID[i].songID == songID)
            {
                return listSongID[i].audioClip;
            }
        }
        return null;
    }
}
