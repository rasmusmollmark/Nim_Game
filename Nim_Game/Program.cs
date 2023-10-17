class program
{
    public static void Main(string[] args)
    {
        greetUser();
        while (true)
        {
            int[] noOfSticksEachRow = { 5, 5, 5 };
            int userChoice = menu();
            if (userChoice == 0)
            {
                Environment.Exit(0);
            }

            switch (userChoice)
            {
                case 1:
                    playWithComputer(noOfSticksEachRow);
                    break;
                case 2:
                    playTogether(noOfSticksEachRow);
                    break;
                default:
                    break;
            }
        }
    }

    private static void greetUser()
    {
        Console.WriteLine("Hej och välkommen till nimspelet!\n" +
            "Det kommer att finnas tre högar med fem stickor i varje.\n" +
            "En spelares tur går ut på att plocka valfritt antal stickor från \nen hög" +
            " för att till slut plocka den sista stickan.\n" +
            "Man får ta hur många stickor ur EN hög under sin tur.\n" +
            "Man vinnar som sagt genom att ta den sista stickan.\n" +
            "För att plocka stickor skriver du in \"<rad> <antal stickor>\".\nEx \"1 2\" för att plocka 2 stickor från rad 1. \n");
    }

    private static void playTogether(int[] noOfSticksEachRow)
    {
        string whichPlayerIsPlaying = "Spelare 1:s";
        bool gameOver = false;
        while (!gameOver)
        {
            createGame(noOfSticksEachRow);
            
            try
            {
                string[] userChoice = getUserChoice(whichPlayerIsPlaying);
                int tooBigOrSmallNumber = noOfSticksEachRow[int.Parse(userChoice[0]) - 1] - int.Parse(userChoice[1]);
                if (tooBigOrSmallNumber > 5 || tooBigOrSmallNumber < 0)
                {
                    Console.WriteLine("Du har tagit för många pinnar eller stoppat in för många pinnar!");
                    continue;
                }
                else
                {
                    noOfSticksEachRow[(int.Parse(userChoice[0]))-1] -= int.Parse(userChoice[1]);
                    if (noOfSticksEachRow[0] == 0 && noOfSticksEachRow[1] == 0 && noOfSticksEachRow[2] == 0)
                    {
                        gameOver = true;
                        Console.WriteLine("\n"+whichPlayerIsPlaying + " vann spelet!\n\n");
                    }
                    if (whichPlayerIsPlaying.Equals("Spelare 1:s"))
                    {
                        whichPlayerIsPlaying = "Spelare 2:s";
                    }
                    else
                    {
                        whichPlayerIsPlaying = "Spelare 1:s";
                    }
                }
               
            }
            catch
            {
                Console.WriteLine("Felinmat, endast siffror");
                continue;
            }
        }
    }

    

    private static string[] getUserChoice(string whichPlayerIsPlaying)
    {
        Console.WriteLine(whichPlayerIsPlaying + " tur");
        Console.Write("Skriv in vilken rad och hur många stickor du vill plocka enligt \"<rad> <antal>\": ");
        string[] input = Console.ReadLine().Split();
        if (int.Parse(input[0]) < 0 && int.Parse(input[0]) > 3) 
        {
            throw new Exception();
        }
        return input;
        
    }

    private static void createGame(int[] noOfSticksEachRow)
    {
        printSticks(noOfSticksEachRow);
    }

    private static void printSticks(int[] noOfSticksEachRow)
    {
        Console.WriteLine("\t*****");
        for(int i = 0; i < noOfSticksEachRow.Length; i++)
        {
            Console.Write(i+1+"\t");
            for(int j = 0; j < noOfSticksEachRow[i]; j++)
            {
                Console.Write("|");
            }
            Console.WriteLine();
        }
        Console.WriteLine("\t*****");
    }

    private static void playWithComputer(int[] noOfSticksEachRow)
    {
        bool gameOver = false;
        bool playersTurn = false;
        Console.WriteLine("\nSkriv in siffran 1 om du vill börja, annat för att datorn ska börja: ");
        if (Console.ReadLine().Equals("1"))
        {
            playersTurn = true;
        }
        while (!gameOver)
        {
            createGame(noOfSticksEachRow);
            try
            {
                if (playersTurn)
                {
                    string[] userChoice = getUserChoice("Spelares");
                    int tooBigOrSmallNumber = noOfSticksEachRow[int.Parse(userChoice[0]) - 1] - int.Parse(userChoice[1]);
                    if (tooBigOrSmallNumber > 5 || tooBigOrSmallNumber < 0)
                    {
                        Console.WriteLine("Du har tagit för många pinnar eller stoppat in för många pinnar!");
                        continue;
                    }
                    else
                    {
                        noOfSticksEachRow[(int.Parse(userChoice[0])) - 1] -= int.Parse(userChoice[1]);
                        playersTurn = false;
                    }
                }
                else
                {
                    generateComputerMove(noOfSticksEachRow);
                }

            }
            catch
            {

            }

        }
    }

    private static void generateComputerMove(int[] noOfSticksEachRow)
    {
        throw new NotImplementedException();
    }

    private static int menu()
    {
        while (true)
        {
            try
            {
                Console.Write("Välj om du vill spela med:\n" +
                    "1. Datorn\n" +
                    "2. Två spelare\n" +
                    "0. För att avsluta\n" +
                    "Ditt val: ");
                return int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Felinmat, välj mellan 1 och 2.");
                continue;

            }
        }

    }
}