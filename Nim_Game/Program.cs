class program
{
    /// <summary>
    /// Main-metoden.
    /// </summary>
    public static void Main(string[] args)
    {
        greetUser(); //Välkomnar användare
        while (true)
        {
            int[] noOfSticksEachRow = { 5, 5, 5 };  //Skapar array för stickorna. 3 rader, 5 stickor i varje.
            int userChoice = menu(); //Skriver ut meny samt kollar om användaren vill avsluta, spela mot någon eller mot datorn.
            if (userChoice == 0) //Väljer användaren 0 så avslutas programmet.
            {
                Environment.Exit(0);
            }

            switch (userChoice) //Kollar vad användaren valt
            {
                case 1:
                    playWithComputer(noOfSticksEachRow); //Spela med datorn
                    break;
                case 2:
                    playTogether(noOfSticksEachRow); //Spela mot varandra
                    break;
                default:
                    Console.WriteLine("\nMåste vara en siffra 0, 1 eller 2.\n"); //Om användaren skrivit in en int som inte är 0-2. 
                    break;
            }
        }
    }

    /// <summary>
    /// Välkomnar användare.
    /// </summary>
    
    private static void greetUser()
    {
        Console.WriteLine("Hej och välkommen till nimspelet!\n" +
            "Det kommer att finnas tre högar med fem stickor i varje.\n" +
            "En spelares tur går ut på att plocka valfritt antal stickor från \nen hög" +
            " för att till slut plocka den sista stickan.\n" +
            "Man får ta hur många stickor ur EN hög under sin tur.\n" +
            "Man vinnar som sagt genom att ta den sista stickan.\n" +
            "För att plocka stickor skriver du in \"<rad> <antal stickor>\".\nEx \"1 2\" för att plocka 2 stickor från rad 1. \n"); //Välkomnande
    }

    /// <summary>
    /// Metoden som sköter flerspelarläget. Här görs de större metodanropen.
    /// </summary>
    ///<param name="noOfSticksEachRow">En array som innehåller antalet stickor per rad.</param>
    private static void playTogether(int[] noOfSticksEachRow)
    {
        string[] playersName = getPlayersName().Split(); //Hämtar spelarnas namn och lagrar i en array, spelare 1 på plats 0, spelare 2 på plats 1.
        string whichPlayerIsPlaying = playersName[0]; //Sätter spelare 1 som startspelare
        bool gameOver = false; //Initierar en bool som ska se om spelet är klart eller om det ska fortsätta
        while (!gameOver) //Kollar om spelet är klart
        {
            printSticks(noOfSticksEachRow); //Skriver ut "spelplanen"
            
            try
            {
                string[] userChoice = getUserChoice(whichPlayerIsPlaying); //Hämtar användarens input
                if (isInputTooBigOrSmall(userChoice, noOfSticksEachRow)) //Kollar om input är för stor eller liten
                {
                    Console.WriteLine("Du har tagit för många pinnar eller stoppat in för många pinnar!");
                    continue; //Hoppar högst upp i while-loopen
                }
                else
                {
                    noOfSticksEachRow[(int.Parse(userChoice[0]))-1] -= int.Parse(userChoice[1]); //Tar bort stickorna 
                    if (noOfSticksEachRow[0] == 0 && noOfSticksEachRow[1] == 0 && noOfSticksEachRow[2] == 0) //Kollar om alla stickor plockats
                    {
                        gameOver = true; //Spelet är slut
                        Console.WriteLine("\n"+whichPlayerIsPlaying + " vann spelet!\n\n"); //Skriver ut vinnaren
                        askIfGameShouldContinue(); //Fråga användaren om denna vill spela igen eller avsluta programmet
                    }
                    if (whichPlayerIsPlaying.Equals(playersName[0])) //Kollar vems tur det varit och sätter nästa tur till den andra spelaren
                    {
                        whichPlayerIsPlaying = playersName[1];
                    }
                    else
                    {
                        whichPlayerIsPlaying = playersName[0];
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

    /// <summary>
    /// Frågar användaren om spelet ska fortsätta.
    /// </summary>
    private static void askIfGameShouldContinue()
    {
        while (true)  //Körs tills användaren matat in korrekt input
        {
            Console.Write("J för spela igen\nN för avsluta\nDitt val: ");
            string input = Console.ReadLine();
            if(input.Equals("j") || input.Equals("J")) //Ifall användaren matar in j så fortsätter programmet
            { 
                return;
            }
            else if(input.Equals("n") || input.Equals("N")) //Ifall användaren matar in n så avslutas programmet
            {
                Environment.Exit(0);
            }
            else //Fel inmat
            {
                Console.WriteLine("Fel inmat!\n");
            }
        }
    }

    /// <summary>
    /// Metoden som sköter flerspelarläget. Här görs de större metodanropen.
    /// </summary>
    /// <returns> En sträng med båda spelares namn. "Spelare 1" + " Spelare 2" </returns>
    private static string getPlayersName()
    {
        Console.Write("Skriv in spelare 1:s namn: ");
        string names = Console.ReadLine().Trim(); //Hämtar in spelare 1s namn och tar bort all whitespace
        Console.Write("Skriv in spelare 2:s namn: ");
        names += " "+Console.ReadLine().Trim(); //Hämtar in spelare 2s namn och tar bort all whitespace
        return names; 
    }

    /// <summary>
    /// Hämtar användarens val för vilken rad de plockar från samt hur många stickor.
    /// </summary>
    /// <param name="whichPlayerIsPlaying">En sträng innehållande en av spelarnas namn.</param>
    /// <returns> En array med användarens val: rad på plats 0 och antal på plats 1. </returns>
    private static string[] getUserChoice(string whichPlayerIsPlaying)
    {
        if (whichPlayerIsPlaying[whichPlayerIsPlaying.Length - 1].Equals('s')) //Kollar om spelarens namn slutar på 's'
        {
            Console.WriteLine(whichPlayerIsPlaying + " tur"); //Skriver ut vems tur det är utan ett 's'
        }
        else
        {
            Console.WriteLine(whichPlayerIsPlaying + "s tur"); //Skriver ut vems tur det är med ett 's'
        }
        
        Console.Write("Skriv in vilken rad och hur många stickor du vill plocka enligt \"<rad> <antal>\": ");
        string[] input = Console.ReadLine().Split(); //Hämtar spelarens input 
        if (int.Parse(input[0]) < 0 && int.Parse(input[0]) > 3) //Kollar om de skrivit in en rad som inte finns
        {
            throw new Exception(); //Kastar en exception om de skrivit in en ickeexisterande rad
        }
        return input; 
        
    }

    /// <summary>
    /// Skriver ut "spelplanen".
    /// </summary>
    ///<param name="noOfSticksEachRow">En array som innehåller antalet stickor per rad.</param>
    private static void printSticks(int[] noOfSticksEachRow)
    {
        Console.WriteLine("\t*****"); 
        for(int i = 0; i < noOfSticksEachRow.Length; i++) //För alla stickor per rad skrivs '|' ut
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

    /// <summary>
    /// Metoden som sköter datorläget. Här görs de större metodanropen.
    /// </summary>
    ///<param name="noOfSticksEachRow">En array som innehåller antalet stickor per rad.</param>
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
            printSticks(noOfSticksEachRow);
            try
            {
                if (playersTurn)
                {
                    string[] userChoice = getUserChoice("Spelares");
                   
                    
                    if (isInputTooBigOrSmall(userChoice, noOfSticksEachRow))
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

    /// <summary>
    /// Kollar om input som skickats är korrekt för hur många stickor som finns kvar.
    /// </summary>
    ///<param name="noOfSticksEachRow">En array som innehåller antalet stickor per rad.</param>
    /// <param name="userChoice"> En array som innehåller vilken rad och hur många stickor som användaren valt att plocka.</param>
    /// <returns> True om inputen är för för stor/liten och false om inputen är korrekt.</returns>
    private static bool isInputTooBigOrSmall(string[] userChoice, int[] noOfSticksEachRow)
    {
        int tooBigOrSmallNumber = noOfSticksEachRow[int.Parse(userChoice[0]) - 1] - int.Parse(userChoice[1]); //Lagrar antalet stickor - hur många stickor spelaren tar
        if ( tooBigOrSmallNumber < 0 || tooBigOrSmallNumber > 5) //Kollar om spelaren skrivit in en för stor eller för liten siffra
        {
            return true;
        }
        return false;
    }

    private static void generateComputerMove(int[] noOfSticksEachRow)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Skriver ut meny.
    /// </summary>
    /// <returns>Användarens val i menyn.</returns>
    private static int menu()
    {
        while (true) //Loopar tills användaren skrivit in en integer
        {
            try
            {
                Console.Write("\nVälj om du vill spela med:\n" +
                    "1. Datorn\n" +
                    "2. Två spelare\n" +
                    "0. För att avsluta\n" +
                    "Ditt val: ");
                return int.Parse(Console.ReadLine());
            }
            catch (FormatException) //Om användaren skrivit in annat än integers
            {
                Console.WriteLine("Felinmat, välj mellan 0, 1 och 2.");
                continue;

            }
        }

    }
}