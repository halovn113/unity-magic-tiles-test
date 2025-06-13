using UnityEngine;
using UnityEngine.UI;

public class GameRunner : MonoBehaviour
{
    [Header("Data")]
    public TextAsset musicData;
    public MusicInfoSO musicInfoSO;

    [Header("Manager")]
    public ScoreBehaviour scoreBehaviour;

    [Header("Game Objects")]
    public Transform topPoint, touchPoint;
    public Button buttonPlay;

    [Header("Setting")]
    [SerializeField]
    public AudioSource audioSource;
    [SerializeField]
    private SongInfo currentSongInfo;
    [SerializeField]
    private bool isPlaying = false;
    [SerializeField]
    private int currentNoteIndex = 0;

    private ListSongInfo listSongInfo;
    private float interval;
    private float timer;
    private float totalTime;
    private TileManager tileManager;
    private UIManager uiManager;
    private string selectedSongID;
    private GameManager gameManager;
    private bool isWin;
    private bool canSpawn;

    public void OnAwake(TileManager tileManager, UIManager uiManager, ObserverManager observerManager, GameManager gameManager)
    {
        this.tileManager = tileManager;
        this.uiManager = uiManager;
        this.gameManager = gameManager;
        scoreBehaviour = FindObjectOfType<ScoreBehaviour>();
        scoreBehaviour.OnAwake(observerManager, uiManager, touchPoint.transform.position);
        ReadData();
        selectedSongID = listSongInfo.Data[0].songID;
        buttonPlay.gameObject.SetActive(true);
        buttonPlay.onClick.AddListener(OnClickStart);
        ResetGame();
    }

    public void OnClickStart()
    {
        StartGame(selectedSongID);
    }

    public void OnStart()
    {

    }

    public void ResetGame()
    {
        currentNoteIndex = 0;
        currentSongInfo = null;
        interval = 0;
        timer = 0;
        totalTime = 0;
        isWin = false;

        uiManager.UpdateProgress(0);
        tileManager.ResetPool();
        uiManager.ResetInfo();
        buttonPlay.gameObject.SetActive(true);
        scoreBehaviour.ResetScore();
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
                if (canSpawn)
                {
                    SpawnTile();
                    if (currentNoteIndex < currentSongInfo.noteTimes.Length - 1)
                    {
                        currentNoteIndex++;
                    }
                    else
                    {
                        canSpawn = false;
                    }
                }
            }

            timer += smoothDeltaTime;
            uiManager.UpdateProgress(timer / totalTime);
            if (timer >= totalTime)
            {
                GameOver(true);
            }
        }
    }

    public void ReadData()
    {
        listSongInfo = JsonUtility.FromJson<ListSongInfo>(musicData.text);
    }

    public void StartGame(string songID)
    {
        buttonPlay.gameObject.SetActive(false);
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
        totalTime += currentSongInfo.tempo + audioSource.clip.length;
        tileManager.fallSpeed = (touchPoint.position.y - topPoint.position.y) / currentSongInfo.tempo;
        isPlaying = true;
        canSpawn = true;
    }

    public void SpawnTile()
    {
        tileManager.SpawnTile();
    }

    public void PlaySong()
    {
        audioSource.Play();
    }

    public void GameOver(bool win)
    {
        isPlaying = false;
        isWin = win;
        audioSource.Stop();
        gameManager.ChangeState(GameState.GameOver);
    }

    public bool IsWin()
    {
        return isWin;
    }
}

