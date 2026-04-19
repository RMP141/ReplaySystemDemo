using UnityEngine;

public class TickManager : MonoBehaviour
{
    public static TickManager Instance { get; private set; }
    public int CurrentTick { get; private set; } = 0;

    [SerializeField] private int tickRate = 50;
    public System.Action<int> OnTick;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Time.fixedDeltaTime = 1f / tickRate;
    }

    private void FixedUpdate()
    {
        CurrentTick++;
        OnTick?.Invoke(CurrentTick);
    }

    public void ResetTicks()
    {
        CurrentTick = 0;
    }
}