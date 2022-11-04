public interface IState
{
    void OnStateEnter();
    void OnStateLeave();
    void OnStateOverride();
    void OnStateResume();
}
