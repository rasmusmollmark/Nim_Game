using System.Reflection.Metadata.Ecma335;

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
        int computerDifficulty = 1;
        computerDifficulty = askUserForComputerDifficulty();
        Console.Write("\nSkriv in siffran 1 om du vill börja, annat för att datorn ska börja: ");
        if (Console.ReadLine().Equals("1"))
        {
            playersTurn = true;
        }
        while (!gameOver)
        {
            printSticks(noOfSticksEachRow);
            try
            {
                if (noOfSticksEachRow[0] == 0 && noOfSticksEachRow[1] == 0 && noOfSticksEachRow[2] == 0) //Kollar om alla stickor plockats
                {
                    gameOver = true; //Spelet är slut
                    if(playersTurn)
                    {
                        Console.WriteLine("\nDatorn vann spelet!\n");
                    }
                    else
                    {
                        Console.WriteLine("\nDu vann spelet!\n");
                    }
                    askIfGameShouldContinue(); //Fråga användaren om denna vill spela igen eller avsluta programmet
                    continue;
                }
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
                    switch (computerDifficulty)
                    {
                        case 1:
                            generateComputerMoveDif1(noOfSticksEachRow);
                            playersTurn = true;
                            break;
                        case 2:
                            generateComputerMoveDif2(noOfSticksEachRow);
                            playersTurn = true;
                            break;
                        case 3:
                            break;

                    }
                }
               

            }
            catch
            {

            }

        }
    }

    /// <summary>
    /// Frågar vilken svårighetsgrad användaren önskar spela mot.
    /// </summary>
    /// <returns> Användarval för svårighetsgrad (1-3) </returns>
    private static int askUserForComputerDifficulty()
    {
       
        int input = 0;
        while (input == 0)
        {
            try
            {
                Console.Write("Vilken svårighetsgrad vill du att datorn ska spela på?\n" +
                    "1. Enkel\n2. Medel\n3. Omöjlig\nSkriv in ditt val 1-3: ");
                input = int.Parse(Console.ReadLine());
                if(input > 3 || input < 1)
                {
                    throw new Exception();
                }
                return input;
            }
            catch 
            {
                Console.WriteLine("Du måste skriva in en siffra 1-3!");
            }
        }
        return -1;
    }

    /// <summary>
    /// Metod som skapar datorns val för nivå 2.
    /// </summary>
    ///<param name="noOfSticksEachRow">En array som innehåller antalet stickor per rad.</param>
    private static void generateComputerMoveDif2(int[] noOfSticksEachRow)
    {
        if (checkIfWinInOneMove(noOfSticksEachRow)) //Om datorn kan vinna denna nuvarande tur
        {
            int rowWithSticksLeft = checkWhichRowHasSticks(noOfSticksEachRow); // Index för rad med pinnar kvar
            printComputerMove(rowWithSticksLeft+1, noOfSticksEachRow[rowWithSticksLeft]); //Skriver ut datorns val till användare
            noOfSticksEachRow[rowWithSticksLeft] = 0; //Tar bort alla pinnar från den raden

        }
        else if (checkIfWinInTwoMoves(noOfSticksEachRow)) //Om datorn kan vinna nästa tur
        {
            if (checkIfTwoRowsHaveOneStickLeft(noOfSticksEachRow)) //Om två rader endast har en pinne kvar
            {
                int index = getIndexOfRowWithMoreThanOneStick(noOfSticksEachRow); //Index för raden med fler än en pinne
                printComputerMove(index+1, noOfSticksEachRow[index]); //Skriver ut datorns val till användare
                noOfSticksEachRow[index] = 0; //Tar bort alla pinnar från den raden
            }
            else //Om en av raderna har 0, en av raderna har 1 och sista har mer än 1 sticka
            {
                int index = getIndexOfRowWithMoreThanOneStick(noOfSticksEachRow); //Index för rad med fler än 1 sticka
                printComputerMove(index, noOfSticksEachRow[index]-1); //Skriver ut datorns val till användare
                noOfSticksEachRow[index] = 1; //Tar bort alla förutom 1 sticka från den raden
            }
        }
        else //Om det inte finns en vinst inom nästa tur
        {
            generateComputerMoveDif1(noOfSticksEachRow); //Slumpa datorns tur
        }
    }

    /// <summary>
    /// Kollar vilken rad som har mer än en sticka kvar.
    /// </summary>
    ///<param name="noOfSticksEachRow">En array som innehåller antalet stickor per rad.</param>
    /// <returns> Index för den rad med mer än en sticka.</returns>
    private static int getIndexOfRowWithMoreThanOneStick(int[] noOfSticksEachRow)
    {
        for(int i = 0; i < noOfSticksEachRow.Length; i++)
        {
            if(noOfSticksEachRow[i] > 1)  //Ifall en rad innehåller mer än en sticka
            {
                return i; 
            }
        }
        return -1;
    }

    /// <summary>
    /// Kollar om datorn kan vinna nästa tur.
    /// </summary>
    ///<param name="noOfSticksEachRow">En array som innehåller antalet stickor per rad.</param>
    /// <returns> True om datorn kan vinna på nästa tur, false om den inte kan det.</returns>
    private static bool checkIfWinInTwoMoves(int[] noOfSticksEachRow)
    {
        int noOfRowsWithSticks = noOfRowsLeftWithSticks(noOfSticksEachRow); //Sätter antalet rader som innehåller stickor
        if(noOfRowsWithSticks == 2) //Ifall endast två rader innehåller stickor
        {
            int[] index = getIndexOfNonEmptyRows(noOfSticksEachRow); //Hämta dessa raders index
            if (noOfSticksEachRow[index[0]] == 1 || noOfSticksEachRow[index[1]] == 1) //Om en av raderna med stickor bara har en sticka kvar
            {
                if (noOfSticksEachRow[index[0]] == 1 && noOfSticksEachRow[index[1]] == 1) //Om båda raderna bara har en sticka kvar
                {
                    return false;
                }
                else //Om bara en av raderna har en sticka kvar
                {
                    return true;
                }
            }

        }
        else if(noOfRowsWithSticks == 3) //Om alla rader har stickor
        {
            if (checkIfTwoRowsHaveOneStickLeft(noOfSticksEachRow)) //Om 2 rader har en sticka kvar
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Kollar om två rader båda har en sticka kvar.
    /// </summary>
    ///<param name="noOfSticksEachRow">En array som innehåller antalet stickor per rad.</param>
    /// <returns> True om datorn kan vinna på nästa tur, false om den inte kan det.</returns>
    private static bool checkIfTwoRowsHaveOneStickLeft(int[] noOfSticksEachRow)
    {
        int rowsWithOneStickLeft = 0; //Antalet rader med endast en sticka kvar
        for(int i = 0; i  < noOfSticksEachRow.Length; i++)
        {
            if (noOfSticksEachRow[i] == 1) //Om antalet stickor på raden bara har en sticka kvar
            {
                rowsWithOneStickLeft++; //Öka 
            }
        }
        if(rowsWithOneStickLeft == 2) //Om två rader har en sticka kvar
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Kollar vilka index de två raderna med stickor har.
    /// </summary>
    ///<param name="noOfSticksEachRow">En array som innehåller antalet stickor per rad.</param>
    /// <returns> En array som innehåller två index.</returns>
    private static int[] getIndexOfNonEmptyRows(int[] noOfSticksEachRow)
    {
        int[] index = new int[2];
        int counter = 0;
        for (int i = 0; i < noOfSticksEachRow.Length; i++)
        {
            if(noOfSticksEachRow[i] != 0) //Om raden inte är tom
            {
                index[counter] = i;
                counter++;
            }
        }
        return index;
    }

    /// <summary>
    /// Kollar vilken rad som har stickor kvar (anropas endast när bara en rad har stickor)
    /// </summary>
    ///<param name="noOfSticksEachRow">En array som innehåller antalet stickor per rad.</param>
    /// <returns> Index för den rad med stickor.</returns>
    private static int checkWhichRowHasSticks(int[] noOfSticksEachRow)
    {
        for (int i = 0; i < noOfSticksEachRow.Length; i++)
        {
            if (noOfSticksEachRow[i] != 0) // Om raden inte är tom
            {
                return i;
            }
        }
        return -1; 
    }
    /// <summary>
    /// Beräknar antalet rader med stickor kvar
    /// </summary>
    ///<param name="noOfSticksEachRow">En array som innehåller antalet stickor per rad.</param>
    /// <returns> Antalet rader med stickor kvar.</returns>
    private static int noOfRowsLeftWithSticks(int[] noOfSticksEachRow)
    {
        int numberOfRowsWithSticks = 3; //Integer vars värde beskriver antalet rader som har minst 1 sticka.
        for (int i = 0; i < noOfSticksEachRow.Length; i++)
        {
            if (noOfSticksEachRow[i] == 0) //Om raden är tom
            {
                numberOfRowsWithSticks--;
            }
        }
        return numberOfRowsWithSticks;
    }

    /// <summary>
    /// Kollar om datorn kan vinna på sin nuvarande tur.
    /// </summary>
    ///<param name="noOfSticksEachRow">En array som innehåller antalet stickor per rad.</param>
    /// <returns> True om datorn kan vinna på nuvarande tur, false om den inte kan det.</returns>
    private static bool checkIfWinInOneMove(int[] noOfSticksEachRow)
    {
        int numberOfRowsWithSticks = noOfRowsLeftWithSticks(noOfSticksEachRow); // Deklarerar int vars värde beskriver antalet rader som har stickor
        if (numberOfRowsWithSticks == 1) //Om det bara är en rad kvar 
        {
            return true;
        }
        return false;
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

    private static void generateComputerMoveDif1(int[] noOfSticksEachRow)
    {
        Random random = new Random();
        int heapIndex;
        int sticksToRemove;

        do
        {
            heapIndex = random.Next(0, 3); // Slumpa en rad (hög)
            sticksToRemove = random.Next(1, noOfSticksEachRow[heapIndex] + 1); // Slumpa antal stickor

        } while (sticksToRemove > 5 || sticksToRemove > noOfSticksEachRow[heapIndex]);

        noOfSticksEachRow[heapIndex] -= sticksToRemove;

        printComputerMove(heapIndex + 1, sticksToRemove); //Skriver ut datorns val till användare
    }

    private static void printComputerMove(int row, int sticksToRemove)
    {
        Console.WriteLine("Datorn valde rad " + row + " och tog bort " + sticksToRemove + " stickor.");
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