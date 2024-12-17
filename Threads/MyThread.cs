namespace Threads;

public class MyThread
{
    private Thread hilo;
    private string name;
    private Action finalAction;
    private Action playerAction;

    public MyThread(string name, ref Action finalAction, Action playerAction)
    {
        this.name = name;
        this.finalAction = finalAction;
        this.playerAction = playerAction;
        hilo = new Thread(_process);
    }

    public void Start()
    {
        hilo.Start();
    }

    public void Join()
    {
        hilo.Join();
    }

    private void _process()
    {
        Console.WriteLine($"{name} is starting...");
        playerAction?.Invoke();
        finalAction?.Invoke();
        Console.WriteLine($"{name} has finished!");
    }
    
}