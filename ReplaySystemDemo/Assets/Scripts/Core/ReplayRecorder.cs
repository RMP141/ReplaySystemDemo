using System.Collections.Generic;
using UnityEngine;

public class ReplayRecorder : MonoBehaviour
{
    public bool IsRecording { get; private set; }

    [SerializeField] private DeterministicRNG rng;
    private List<CommandData> recordedCommands = new List<CommandData>();
    private int initialSeed;

    public void StartRecording()
    {
        IsRecording = true;
        recordedCommands.Clear();

        TickManager.Instance.ResetTicks();

        initialSeed = Random.Range(int.MinValue, int.MaxValue);
        rng.InitState(initialSeed);

        Debug.Log($"Recording started at tick 0 with seed {initialSeed}");
    }

    public void StopRecording()
    {
        IsRecording = false;
        Debug.Log($"Recording stopped. Commands: {recordedCommands.Count}");
    }

    public void RecordCommand(ICommand command)
    {
        if (!IsRecording) return;

        CommandData data = new CommandData
        {
            tick = TickManager.Instance.CurrentTick,
            type = command.Type,
            payload = command.Payload
        };
        recordedCommands.Add(data);
    }

    public ReplayData GetReplayData()
    {
        return new ReplayData
        {
            seed = initialSeed,
            tickRate = 1f / Time.fixedDeltaTime,
            commands = new List<CommandData>(recordedCommands)
        };
    }

    public void SaveToFile(string filename = "replay.json")
    {
        var data = GetReplayData();
        string json = JsonUtility.ToJson(data, true);
        string path = System.IO.Path.Combine(Application.persistentDataPath, filename);
        System.IO.File.WriteAllText(path, json);
        Debug.Log($"Replay saved to: {path}");
    }

    public ReplayData LoadFromFile(string filename = "replay.json")
    {
        string path = System.IO.Path.Combine(Application.persistentDataPath, filename);
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            return JsonUtility.FromJson<ReplayData>(json);
        }
        Debug.LogError($"File not found: {path}");
        return null;
    }
}