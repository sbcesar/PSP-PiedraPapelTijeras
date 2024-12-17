// See https://aka.ms/new-console-template for more information


using Threads;

class Program
{
    private static String[] options = {"rock", "paper", "scissors"};
    private static readonly List<string> currentRound = new List<string>();
    private static readonly List<string> nextRound = new List<string>();
    private static Object locker = new object();
    
    static void Main()
    {
        
        MyThread[] players = new MyThread[16];
        
        for (int i = 0; i < 16; i++)
        {
            int playerIndex = i;
            players[i] = new MyThread($"Player {playerIndex + 1}", () => ChooseOption(playerIndex));
        }
        
        List<string> currentRound = new List<string>();
        for (int i = 0; i < 16; i++)
        {
            currentRound.Add($"Player {i + 1}");
        }

        while (currentRound.Count > 1)
        {
            Console.WriteLine($"\n--- Round with {currentRound.Count} players ---");
            nextRound.Clear(); // Preparar la siguiente ronda

            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < currentRound.Count; i += 2)
            {
                int index = i; // Necesario para capturar el índice correctamente
                Thread matchThread = new Thread(() =>
                {
                    PlayMatch(currentRound[index], currentRound[index + 1]);
                });
                threads.Add(matchThread);
                matchThread.Start();
            }

            // Esperar a que terminen todos los partidos
            foreach (var thread in threads)
            {
                thread.Join();
            }

            // Preparar la siguiente ronda
            currentRound.Clear();
            currentRound.AddRange(nextRound);
        }
        Console.WriteLine($"\nThe winner is: {currentRound[0]}!");
    }
    
    static void PlayMatch(string player1, string player2)
    {
        string choice1 = ChooseOption(player1);
        string choice2 = ChooseOption(player2);

        lock (locker)
        {
            string winner = ChooseWinner(player1, player2, choice1, choice2);
            nextRound.Add(winner);
        }
    }

    static void ChooseOption(string player)
    {
        lock (locker)
        {
            string? option = "";
            bool validInput = false;

            while (!validInput)
            {
                Console.WriteLine($"{player}, choose an action ({string.Join(", ", options)}):");
                option = Console.ReadLine()?.ToLower();

                if (options.Contains(option))
                {
                    validInput = true;
                    Console.WriteLine($"{player} chose: {option}");
                }
                else
                {
                    Console.WriteLine("Invalid option. Try again.");
                }
            }

            return option;
        }
    }

    static string ChooseWinner(string player1, string player2)
    {}
}



