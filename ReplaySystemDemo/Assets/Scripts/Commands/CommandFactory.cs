using UnityEngine;

public static class CommandFactory
{
    public static Transform PlayerTransform;
    public static Renderer PlayerRenderer;
    public static float MoveSpeed = 5f;

    public static ICommand CreateCommand(CommandData data)
    {
        switch (data.type)
        {
            case CommandType.Move:
                var dirParts = data.payload.Split(',');
                Vector3 dir = new Vector3(float.Parse(dirParts[0]), float.Parse(dirParts[1]), float.Parse(dirParts[2]));
                return new MoveCommand(data.tick, dir, PlayerTransform, MoveSpeed);

            case CommandType.Action:
                var colorParts = data.payload.Split(',');
                Color color = new Color(float.Parse(colorParts[0]), float.Parse(colorParts[1]),
                                        float.Parse(colorParts[2]), float.Parse(colorParts[3]));
                return new ActionCommand(data.tick, PlayerRenderer, color);

            case CommandType.Select:
                GameObject obj = GameObject.Find(data.payload);
                return new SelectCommand(data.tick, obj);

            default:
                Debug.LogError($"Unknown command type: {data.type}");
                return null;
        }
    }
}