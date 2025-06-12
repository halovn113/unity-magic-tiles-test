using UnityEngine;

public class GameRunner : MonoBehaviour
{
    [Header("Data")]
    public TextAsset musicData;
    public MusicInfoSO musicInfoSO;

    [Header("Manager")]
    public ScoreBehaviour scoreBehaviour;

    [Header("Game Objects")]
    public Transform topPoint, touchPoint;

    [Header("Setting")]
    [SerializeField]
    public AudioSource audioSource;
    [SerializeField]
    private SongInfo currentSongInfo;
    [SerializeField]
    private bool isPlaying = false;
    [SerializeField]
    private int currentNoteIndex = 0;

    // ========================= //
    private ListSongInfo listSongInfo;
    private float interval;
    private TileManager tileManager;
    private UIManager uiManager;

    public void OnAwake(TileManager tileManager, UIManager uiManager, ObserverManager observerManager)
    {
        this.tileManager = tileManager;
        this.uiManager = uiManager;
        scoreBehaviour = FindObjectOfType<ScoreBehaviour>();
        scoreBehaviour.OnAwake(observerManager, uiManager, touchPoint.transform.position);
        ReadData();
    }

    public void OnStart()
    {
        StartGame("0");
    }

    public void OnUpdate(float smoothDeltaTime)
    {
        if (isPlaying)
        {
            if (interval >= 0)
            {
                if (audioSource.isPlaying == false)
                {
                    audioSource.Play();
                }
                interval = audioSource.time;
            }
            else
            {
                interval += smoothDeltaTime;
            }

            float currentNoteTime = currentSongInfo.noteTimes[currentNoteIndex] - currentSongInfo.tempo;
            if (interval >= currentNoteTime)
            {
                SpawnTile();
                if (currentNoteIndex < currentSongInfo.noteTimes.Length - 1)
                {
                    currentNoteIndex++;
                }
                else
                {
                    isPlaying = false;
                }
            }
        }
    }

    public void ReadData()
    {
        listSongInfo = JsonUtility.FromJson<ListSongInfo>(musicData.text);
    }

    public void StartGame(string songID)
    {
        currentNoteIndex = 0;
        audioSource.clip = musicInfoSO.GetAudioClip(songID);
        currentSongInfo = listSongInfo.GetSongInfo(songID);
        if (currentSongInfo == null)
        {
            Debug.LogWarning("Song not found!");
            return;
        }
        if (currentSongInfo.noteTimes[0] < currentSongInfo.tempo)
        {
            interval = currentSongInfo.noteTimes[0] - currentSongInfo.tempo;
        }
        tileManager.fallSpeed = (touchPoint.position.y - topPoint.position.y) / currentSongInfo.tempo;
        isPlaying = true;
    }

    public void SpawnTile()
    {
        tileManager.SpawnTile();
    }

    public void PlaySong()
    {
        audioSource.Play();
    }
}

