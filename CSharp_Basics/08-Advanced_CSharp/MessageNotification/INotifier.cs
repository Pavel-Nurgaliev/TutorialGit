namespace MessageNotification
{
    //interface is contract that inherited classes obligates to implement. Compare to abstract class, inherited classes can implement many interfaces. Interface shares with inherited classes its members and doesn't consists any logic or method implementation
    public interface INotifier
    {
        void Send(string message);
    }
}
