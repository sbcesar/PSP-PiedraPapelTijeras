namespace Threads;

public class MyThread
{
    private Thread hilo;
    private Action action;

    public MyThread(string name, Action action)
    {
        hilo = new Thread(new ThreadStart(action)) { Name = name };
        this.action = action;
    }

    public void Start()
    {
        hilo.Start();
    }

    public void Join()
    {
        hilo.Join();
    }
    
}