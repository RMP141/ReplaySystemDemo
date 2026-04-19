using UnityEngine;

public class SelectCommand : ICommand
{
    public int Tick { get; private set; }
    public CommandType Type => CommandType.Select;
    public string Payload { get; private set; }

    private readonly GameObject selectedObject;

    public SelectCommand(int tick, GameObject obj)
    {
        Tick = tick;
        selectedObject = obj;
        Payload = obj != null ? obj.name : "null";
    }

    public void Execute()
    {
        if (selectedObject != null)
        {
            var renderer = selectedObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.yellow;
            }
            Debug.Log($"Selected: {selectedObject.name} at tick {Tick}");
        }
    }
}