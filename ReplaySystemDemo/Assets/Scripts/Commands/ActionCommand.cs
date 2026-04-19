using UnityEngine;
public class ActionCommand : ICommand
{
    public int Tick { get; private set; }
    public CommandType Type => CommandType.Action;
    public string Payload { get; private set; }

    private readonly Renderer targetRenderer;
    private readonly Color newColor;

    public ActionCommand(int tick, Renderer renderer, Color color)
    {
        Tick = tick;
        targetRenderer = renderer;
        newColor = color;
        Payload = $"{color.r},{color.g},{color.b},{color.a}";
    }

    public void Execute()
    {
        if (targetRenderer != null)
            targetRenderer.material.color = newColor;
    }
}