using UnityEngine;

public class ReplayPlayer : MonoBehaviour
{
    public bool IsPlaying { get; private set; }

    [SerializeField] private DeterministicRNG rng;
    [SerializeField] private InputHandler inputHandler;

    private ReplayData currentReplayData;
    private int currentReplayTick;
    private int commandIndex;

    public void StartPlayback(ReplayData data)
    {
        if (data == null || data.commands.Count == 0)
        {
            Debug.LogWarning("No replay data to play.");
            return;
        }

        currentReplayData = data;
        IsPlaying = true;
        currentReplayTick = 0;
        commandIndex = 0;

        ResetGameState();

        rng.InitState(currentReplayData.seed);
        TickManager.Instance.ResetTicks();

        if (inputHandler != null) inputHandler.enabled = false;

        TickManager.Instance.OnTick += ExecuteCommandsForCurrentTick;
        Debug.Log($"Playback started. Seed: {currentReplayData.seed}");
    }

    public void StopPlayback()
    {
        IsPlaying = false;
        TickManager.Instance.OnTick -= ExecuteCommandsForCurrentTick;
        if (inputHandler != null) inputHandler.enabled = true;
        Debug.Log("Playback stopped.");
    }

    private void ExecuteCommandsForCurrentTick(int tick)
    {
        if (!IsPlaying) return;
        currentReplayTick = tick;

        while (commandIndex < currentReplayData.commands.Count &&
               currentReplayData.commands[commandIndex].tick == currentReplayTick)
        {
            var cmdData = currentReplayData.commands[commandIndex];
            ICommand command = CommandFactory.CreateCommand(cmdData);
            command?.Execute();
            commandIndex++;
        }

        if (commandIndex >= currentReplayData.commands.Count)
        {
            StopPlayback();
        }
    }

    private void ResetGameState()
    {
        if (CommandFactory.PlayerTransform != null)
        {
            CommandFactory.PlayerTransform.position = new Vector3(0, 0.5f, 0);
        }
        if (CommandFactory.PlayerRenderer != null)
        {
            CommandFactory.PlayerRenderer.material.color = Color.white;
        }
    }
}