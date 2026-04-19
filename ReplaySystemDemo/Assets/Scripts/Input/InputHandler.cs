using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private ReplayRecorder recorder;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Renderer playerRenderer;
    [SerializeField] private float moveSpeed = 5f;

    private Vector3 moveDirection;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        CommandFactory.PlayerTransform = playerTransform;
        CommandFactory.PlayerRenderer = playerRenderer;
        CommandFactory.MoveSpeed = moveSpeed;
    }

    private void Update()
    {
        if (recorder.IsRecording)
        {
            HandleMovementInput();
            HandleActionInput();
            HandleSelectInput();
        }
    }

    private void HandleMovementInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(h, 0, v).normalized;

        if (moveDirection != Vector3.zero)
        {
            var cmd = new MoveCommand(TickManager.Instance.CurrentTick, moveDirection, playerTransform, moveSpeed);
            recorder.RecordCommand(cmd);
            cmd.Execute();
        }
    }

    private void HandleActionInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Color randomColor = new Color(Random.value, Random.value, Random.value);
            var cmd = new ActionCommand(TickManager.Instance.CurrentTick, playerRenderer, randomColor);
            recorder.RecordCommand(cmd);
            cmd.Execute();
        }
    }

    private void HandleSelectInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                var cmd = new SelectCommand(TickManager.Instance.CurrentTick, hit.collider.gameObject);
                recorder.RecordCommand(cmd);
                cmd.Execute();
            }
        }
    }
}