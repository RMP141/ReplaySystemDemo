using UnityEngine;
using UnityEngine.UI;

public class ReplayUIController : MonoBehaviour
{
    [SerializeField] private ReplayRecorder recorder;
    [SerializeField] private ReplayPlayer player;
    [SerializeField] private Button recordStartButton;
    [SerializeField] private Button recordStopButton;
    [SerializeField] private Button playLastButton;

    private ReplayData lastRecording;

    private void Start()
    {
        recordStartButton.onClick.AddListener(OnRecordStart);
        recordStopButton.onClick.AddListener(OnRecordStop);
        playLastButton.onClick.AddListener(OnPlayLast);
        playLastButton.interactable = false;
    }

    public void OnRecordStart()
    {
        recorder.StartRecording();
        recordStartButton.interactable = false;
        recordStopButton.interactable = true;
        playLastButton.interactable = false;
    }

    public void OnRecordStop()
    {
        recorder.StopRecording();
        lastRecording = recorder.GetReplayData();
        recordStartButton.interactable = true;
        recordStopButton.interactable = false;
        playLastButton.interactable = true;
    }

    public void OnPlayLast()
    {
        if (lastRecording != null)
        {
            player.StartPlayback(lastRecording);
        }
    }

    public void LoadFromFileAndPlay()
    {
        var data = recorder.LoadFromFile();
        if (data != null)
        {
            lastRecording = data;
            player.StartPlayback(lastRecording);
        }
    }
}