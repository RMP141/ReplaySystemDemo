public interface ICommand
{
    int Tick { get; }
    CommandType Type { get; }
    string Payload { get; }
    void Execute();
}