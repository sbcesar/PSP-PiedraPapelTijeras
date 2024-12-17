// See https://aka.ms/new-console-template for more information


class Program
{
    private static String[] options = {"rock", "paper", "scissors"};
    private static String firstPlayerOption = "";
    private static String secondPlayerOption = "";
    
    static void Main()
    {
        
        Thread player1 = new Thread(() => ChooseOption(1));
        Thread player2 = new Thread(() => ChooseOption(2));
        
        player1.Start();
        player1.Join();
        
        player2.Start();
        player2.Join();
        
        ChooseWinner();
        
        Console.WriteLine("Game Over!");
    }

    static void ChooseOption(int player)
    {
        string? option = "";
        bool validInput = false;

        while (!validInput)
        {
            Console.WriteLine($"Player {player}, choose an action ({string.Join(", ", options)}):");
            option = Console.ReadLine()?.ToLower();

            if (Array.Exists(options, o => o == option))
            {
                validInput = true;
            }
            else
            {
                Console.WriteLine("Invalid option. Try again.");
            }
        }

        if (player == 1)
        {
            firstPlayerOption = option;
            Console.WriteLine($"Player 1 chose: {firstPlayerOption}");
        }
        else if (player == 2)
        {
            secondPlayerOption = option;
            Console.WriteLine($"Player 2 chose: {secondPlayerOption}");
        }
    }

    static void ChooseWinner()
    {
        // Draw
        if (firstPlayerOption == secondPlayerOption)
        {
            Console.WriteLine("It a Draw!");
        } else if (firstPlayerOption == "rock" && secondPlayerOption == "scissor" || firstPlayerOption == "scissor" && secondPlayerOption == "paper" || firstPlayerOption == "paper" && secondPlayerOption == "rock")
        {
            Console.WriteLine("Player 1 wins!");
        }
        else
        {
            Console.WriteLine("Player 2 wins!");
        }
    }
}



