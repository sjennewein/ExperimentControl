namespace ColdNetworkStack
{
    public enum StateType
    {
        Unused,
        Connect,
        Disconnect,
        Data,
        Receive,        
        Send,
        Wait,
        Trigger,
        Error
    }

    public class ConnectionStates
    {
        public StateType Status = StateType.Unused;        
    }
}
