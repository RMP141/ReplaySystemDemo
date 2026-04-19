using UnityEngine;

public class MoveCommand : ICommand
{
    public int Tick { get; private set; }
    public CommandType Type => CommandType.Move;
    public string Payload { get; private set; }

    private readonly Vector3 direction;
    private readonly Transform target;
    private readonly float speed;

    public MoveCommand(int tick, Vector3 dir, Transform targetTransform, float moveSpeed = 5f)
    {
        Tick = tick;
        direction = dir.normalized;
        target = targetTransform;
        speed = moveSpeed;
        Payload = $"{dir.x},{dir.y},{dir.z}";
    }

    public void Execute()
    {
        if (target != null)
        {
            Vector3 move = direction * speed * Time.fixedDeltaTime;
            Vector3 newPos = target.position + move;
            newPos.y = 0.5f;
            target.position = newPos;
        }
    }
}