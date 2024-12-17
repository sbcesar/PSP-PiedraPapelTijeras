// See https://aka.ms/new-console-template for more information


using Threads;

class Program
{
    private static String[] options = {"rock", "paper", "scissors"};
    private static String[] choices = new String[2];
    private static Action finalAction;
    private static object locker = new object();    // Sirve para que un hilo vaya despues que otro
    
    static void Main()
    {
        MyThread player1 = new MyThread("Player 1", ref finalAction, () => ChooseOption(0));
        MyThread player2 = new MyThread("Player 2", ref finalAction, () => ChooseOption(1));

        
        finalAction += () =>
        {
            ChooseWinner();
            Console.WriteLine("\nGame Over!");
        };
        
        player1.Start();
        player1.Join();
        
        player2.Start();
        player2.Join();
    }

    static void ChooseOption(int playerIndex)
    {
        lock (locker)
        {
            string? option = "";
            bool validInput = false;
    
            while (!validInput)
            {
                Console.WriteLine($"Player {playerIndex + 1}, choose an action ({string.Join(", ", options)}):");
                option = Console.ReadLine()?.ToLower();
    
                if (options.Contains(option))
                {
                    validInput = true;
                    choices[playerIndex] = option!;
                    Console.WriteLine($"Player {playerIndex + 1} chose: {option}");
                }
                else
                {
                    Console.WriteLine("Invalid option. Try again.");
                }
            }
        }
        
    }

    static void ChooseWinner()
    {
        string firstPlayerOption = choices[0];
        string secondPlayerOption = choices[1];

        // Draw
        if (firstPlayerOption == secondPlayerOption)
        {
            Console.WriteLine("It's a Draw!");
        }
        else if ((firstPlayerOption == "rock" && secondPlayerOption == "scissors") || (firstPlayerOption == "scissors" && secondPlayerOption == "paper") || (firstPlayerOption == "paper" && secondPlayerOption == "rock"))
        {
            Console.WriteLine("Player 1 wins!");
        }
        else
        {
            Console.WriteLine("Player 2 wins!");
        }
    }
}



