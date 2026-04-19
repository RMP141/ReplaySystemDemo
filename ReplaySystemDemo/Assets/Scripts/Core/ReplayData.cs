using System.Collections.Generic;

[System.Serializable]
public class ReplayData
{
    public int seed;
    public float tickRate;
    public List<CommandData> commands = new List<CommandData>();
}