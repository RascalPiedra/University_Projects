using System;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

class Program
{
    static int gameTime = 30;
    static DateTime nowTime = DateTime.Now;
    static int screenWidth = Console.WindowWidth, screenHeight = Console.WindowHeight;
    static string[] log = new string[4];
    static char player = '@';
    static int playerX = screenWidth/2, playerY = screenHeight-10;
    static int itemX, itemY=0;
    static char item = ' ';
    static int score = 0;
    static Random random = new Random();
    static int Main()
    {
        Console.CursorVisible = false;
        bool gameRunning = true;

        while (gameRunning && (DateTime.Now - nowTime).Seconds < gameTime)
        {
            log[3] = gameTime - (DateTime.Now - nowTime).Seconds+" seconds remained!";
            input();
            spawnItem();
            checkCollision();
            draw();
            writeLog(log);

            Thread.Sleep(100);
            Console.Clear();
        }

        Console.SetCursorPosition(screenWidth/2-5, screenHeight/2-5);
        Console.Write($"GAME OVER! Score: {score}");

        return 0;
    }

    static void input()
    {
        while(Console.KeyAvailable)
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            
            if(key == ConsoleKey.RightArrow && playerX < screenWidth - 1){
                playerX++;
            }
            if(key == ConsoleKey.LeftArrow && playerX > 0){
                playerX--;
            }
            if (key == ConsoleKey.UpArrow && playerY > 0){
                playerY--;
            }
            if (key == ConsoleKey.DownArrow && playerY < screenHeight - 6){
                playerY++;
            }

            log[0] = $"Input --> Key={key} playerX={playerX} playerY={playerY}";
        }
    }

    static void spawnItem()
    {
        if(itemY > screenHeight - 6)
        {
            item = ' ';
        }
        if(item == ' '){
            item = random.Next(1,3)%2==0 ? '*' : '0';
            itemY = 0;
            itemX = random.Next(0, screenWidth); 
            log[1] = $"Item --> x={itemX} y={itemY}";
        }
    }

    static void draw()
    {
        Console.SetCursorPosition(playerX, playerY);            //Player Spawning
        Console.Write(player);    
    }

    static void checkCollision()
    {
        itemY += 1;
        log[1] = $"Item --> x={itemX} y={itemY}";
        if(!(itemX == playerX && itemY == playerY))
        {
            Console.SetCursorPosition(itemX, itemY);
            Console.Write(item);
        }
        else
        {
            item = ' ';
            score++;
        }
        log[2] = $"Collision --> score={score}";
    }

    static void writeLog(string[] log)
    {
        for(int i = 0; i < log.Length; i++)
        {
            Console.SetCursorPosition(0, screenHeight - 5 + i);
            Console.Write(log[i]);
        }
    }
}