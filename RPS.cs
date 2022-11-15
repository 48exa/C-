namespace RpsTest;
using System.Linq;

//Global class to ensure all variables used in different functions can be called and edited within each other.
class Global 
{
    public static String[] moves = { "rock", "paper", "scissors" }; //All possible moves in Rock Paper Scissors.

    public static int uScore = 0; //User Score.
    public static int pScore = 0; //PC Score.
    public static int fScore = 0; //Final Score.
    public static int gamesPlayed = 1;

    public static string uMove = ""; //User Move.
    public static string pMove = ""; //PC Move.
    public static string iMove = ""; //Invalid move, all uMoves are automatically stored in here.

    //This token served a greater purpose than it does now, could be replaced by any word or string. I chose this base64 encoded string for some reason.
    //The original reason for this token was for if a uMove was invalid, the uMove would be changed to this token, and if the uMove was this token, then you'd lose the round.
    //This has been fixed, but I did not want to remove the token. Consider this an artifact or the early builds.
    public static string token = "TnVsbGVkIHN0cmluZyBkdWUgdG8gaW52YWxpZCBwYXJhbWV0ZXJzLg"; 

    public static Random random = new Random();
}

class RPS
{
    public static void Main() 
    {
        FirstGameScreen();
        Randomise();
        GetUserMove();
        Validate();
        CompareMoves();
        PrintScores();
        ContinuePlayingPrompt();
    }

    public static void FirstGameScreen() //Basic welcome text.
    {   
        if (Global.gamesPlayed == 1) {
            Console.WriteLine("Hi, welcomle to RPS.cs!");
            Console.Write("Please enter to what score you wish to play: ");
            Global.fScore = Convert.ToInt32(Console.ReadLine()); //Final score input automatically converted to an interger.
        }
    }

    public static string Randomise() //Generate the pMove using a randomised number from 0 to 2 (length of the Global.moves string).
    {
        int rIndexMoves = Global.random.Next(Global.moves.Length);
        Global.pMove = Global.moves[rIndexMoves];

        return Global.pMove;
    }

    public static string GetUserMove() //Get the uMove by changing the empty variable of uMove to an input field.
    {
        Console.WriteLine("Rock, paper, scissors:");
        Console.Write("> ");

        Global.uMove = (Console.ReadLine().ToLower());
        Global.iMove = Global.uMove; //Storing the uMove in iMove incase the move is invalid.
        Global.gamesPlayed += 1;
        return Global.uMove;
        return Global.iMove; 

    }

    public static void Validate() //Checking is the uMove is an allowed move.
    {
        //if (uMove in moves) return;
        if (Global.moves.Any(Global.uMove.Contains)) {
            return;
        } else {
            Console.WriteLine($"Invalid move. ({Global.iMove})"); //If the uMove is not in the list of allowed moves, the uMove gets converted to the token.
        }

        if (Global.uMove == Global.token) { //If the uMove is the token, can be simplified.
            Console.WriteLine($"Invalid move. ({Global.iMove})");
        }
    }

    public static void CompareMoves() //Core of the game, comparing the uMove and the pMove to see which wins.
    {
        //Boolean variables to not create a textwall inside the actual checking code. Instead of if (Global.uMove == "rock" && Global.pMove == "scissors") its now (uRock && pScissors).
        bool uRock = (Global.uMove == "rock") ? true : false;
        bool uPaper = (Global.uMove == "paper") ? true : false;
        bool uScissors = (Global.uMove == "scissors") ? true : false;
        bool pRock = (Global.pMove == "rock") ? true : false;
        bool pPaper = (Global.pMove == "paper") ? true : false;
        bool pScissors = (Global.pMove == "scissors") ? true : false;

        if (Global.uMove != Global.token) Console.WriteLine($"PC chose {Global.pMove}"); //If (uMove is not invalid) print the pMove.

        //Case for where the user wins, this could techinically also be made into a Switch statement, but for only 3 options an else-if works fine.
        if (uRock && pScissors || uPaper && pRock || uScissors && pPaper) {
            Console.WriteLine($"You win! {Global.uMove} beats {Global.pMove}");
            Global.uScore += 1;

        //Case where the user loses.
        } else if (pRock && uScissors || pPaper && uRock || pScissors && uPaper || Global.uMove == Global.token) {
            Console.WriteLine($"You lose. {Global.pMove} beats {Global.uMove}");
            Global.pScore += 1;

        //If the user does not win or lose it automatically has to be a tie.
        } else {
            Console.WriteLine("You tied.");
        }
    }

    public static void PrintScores() //This function describes itself.
    {
        //Little gimmick I added, the '>' and '<' change based on whose score is higher.
        if (Global.uScore > 0 || Global.pScore > 0) {
            if (Global.uScore > Global.pScore) {
                Console.WriteLine($"Current score: User: {Global.uScore} > PC: {Global.pScore}");
            } else {
                Console.WriteLine($"Current score: User: {Global.uScore} < PC: {Global.pScore}");
            }
        }
    }

    //This function used to give you a (y/n) prompt after every single round, but due to this slowing down the pace of the game significantly I-
    //Turned it into a check for if the fScore had been reached, and if it did, the game would end.
    //If the final score was not reached it executes Main(), thereby starting the loop over again. By putting the "loop" in the else part of this statment it-
    //Also ensures that there can't be a weird bug where the game continues after the final score being reached.
    public static void ContinuePlayingPrompt()
    {
        if (Global.uScore == Global.fScore || Global.pScore == Global.fScore)
        {
            Console.WriteLine("Final score reached!");
        
            if (Global.uScore > Global.pScore) {
                Console.WriteLine("You are the winner!");
            } else {
                Console.WriteLine("You lost!");
            }

            Console.WriteLine("Thanks for playing! Hope you enjoyed!");
            Console.Write("Press any key to quit.");
            Console.ReadLine();
            return;
        } else { 
            Main(); 
        }
    }
}
