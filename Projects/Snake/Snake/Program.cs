using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace Arcade
{
    class Pixel
    {
        private string[] shades = { "██", "▓▓", "▒▒", "░░", "  " };
        private String[] floorShades = { "##", "xx", "--", "..", "  " };
        public ConsoleColor color;
        public string pixel;
        public Pixel(int shade = 0, ConsoleColor color = ConsoleColor.White, bool floor = false)
        {   
            if (!floor)
                pixel = shades[(shade <= 4) ? shade : 4];
            else
                pixel = floorShades[(shade <= 4) ? shade : 4];
            this.color = color;
        }
    }

    class Snake
    {
        const int gridSize = 16;
        int vertical = gridSize + 1;
        Pixel[,] grid = new Pixel[gridSize, gridSize];

        int snakeX = gridSize / 2;
        int snakeY = gridSize / 2;
        int dir = 0;
        int length = 4 + 10;
        List<int[]> snakeTail = new List<int[]>();
        bool alive = true;
        bool visible = true;
        public bool gameEnded = false;
        int deathType = 0;
        public int score = 0;

        Random r = new Random();
        int appleX;
        int appleY;

        public void Start()
        {
            Console.Title = "Snake";
            Console.CursorVisible = false;
            Console.SetWindowSize(gridSize * 2 + 10, gridSize + 5);
            Console.SetBufferSize(gridSize * 2 + 10, gridSize + 5);
            generateGrid();
            spawnApple();
        }

        public void Update()
        {
            getInputs();

            if (alive)
                moveSnake();
            else
                removeSnake();

            generateGrid();
            updateDisplay();
        }

        void spawnApple()
        {
            appleX = r.Next(0, gridSize);
            appleY = r.Next(0, gridSize);

            if (appleX == snakeX && appleY == snakeY)
                spawnApple();

            foreach (int[] pos in snakeTail)
                if (appleX == pos[0] && appleY == pos[1])
                    spawnApple();
        }

        void moveSnake()
        {
            snakeTail.Add(new int[2] { snakeX, snakeY });

            switch (dir)
            {
                case 0: if (snakeY > 0) snakeY--; else alive = false; break;
                case 1: if (snakeX > 0) snakeX--; else alive = false; break;
                case 2: if (snakeY < gridSize - 1) snakeY++; else alive = false; break;
                case 3: if (snakeX < gridSize - 1) snakeX++; else alive = false; break;
            }

            foreach (int[] pos in snakeTail)
                if (snakeX == pos[0] && snakeY == pos[1])
                    alive = false;

            if (alive && snakeTail.Count > length)
                snakeTail.RemoveAt(0);

            if (snakeX == appleX && snakeY == appleY)
            {
                spawnApple();
                length += 2;
                score++;
            }
        }

        void removeSnake()
        {
            if (deathType == 0)
                deathType = r.Next(1, 3);

            if (deathType == 1)
            {
                if (snakeTail.Count > 0)
                    snakeTail.RemoveAt(0);
                if (snakeTail.Count > 0)
                    snakeTail.RemoveAt(snakeTail.Count - 1);
            } else if (deathType == 2)
            {
                if (snakeTail.Count > 0)
                    snakeTail.RemoveAt(r.Next(0, snakeTail.Count));
                if (snakeTail.Count > 0)
                    snakeTail.RemoveAt(r.Next(0, snakeTail.Count));
            }
        }

        void getInputs()
        {
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.W: if (dir != 2) dir = 0; break;
                    case ConsoleKey.A: if (dir != 3) dir = 1; break;
                    case ConsoleKey.S: if (dir != 0) dir = 2; break;
                    case ConsoleKey.D: if (dir != 1) dir = 3; break;
                }
            }
        }

        void generateGrid()
        {
            for (int x = 0; x < gridSize; x++)
                for (int y = 0; y < gridSize; y++)
                        grid[x, y] = new Pixel(4);

            if (alive)
                grid[snakeX, snakeY] = new Pixel(0, ConsoleColor.Blue);

            foreach (int[] pos in snakeTail)
                grid[pos[0], pos[1]] = new Pixel(0, ConsoleColor.Blue);

            grid[appleX, appleY] = new Pixel(0, ConsoleColor.Red);
        }

        void updateDisplay()
        {
            if (!alive)
            {
                //const string gameOverText = "Game Over!";
                //for (int x = 0; x < gameOverText.Length; x += 2)
                    //grid[x / 2 + gridSize / 2 - gameOverText.Length / 4, gridSize / 2] = gameOverText[x].ToString() + gameOverText[x + 1].ToString();
            }

            if (visible)
            {
                for (int y = -1; y <= gridSize; y++)
                {
                    for (int x = -1; x <= gridSize; x++)
                    {
                        Console.SetCursorPosition((x + 1) * 2, y + 1);
                        Console.ForegroundColor = ConsoleColor.White;

                        if (x == -1 || x == gridSize || y == -1 || y == gridSize)
                            Console.Write("██");
                        else
                        {
                            Console.ForegroundColor = grid[x, y].color;
                            Console.Write(grid[x, y].pixel);
                        }  
                    }
                }
                if (snakeTail.Count <= 0)
                    visible = false;
            } else
            {
                for (int x = -1; x <= (gridSize + 1) * 2; x++)
                {
                    Console.SetCursorPosition(x + 1, vertical);
                    Console.Write(" ");
                }
                if (vertical > 0)
                    vertical--;
                else
                    gameEnded = true;
            }
        }
    }
    
    class Doom
    {
        int screenHeight = 30;
        int screenWidth = 60;
        int margin = 4;
        string renderMode = "mono"; // "mono", "colour"
        float fps;
        List<float> fpsList = new List<float>();
        Stopwatch updateTime = new Stopwatch();
        Stopwatch renderTime = new Stopwatch();
        Stopwatch calcTime = new Stopwatch();
        float deltaTime;

        int sideBarWidth = 8;
        int sideBarHeight = 5;

        Pixel[,] screen;

        float playerX = 4;
        float playerY = 4;
        float playerA = 0;

        float playerVX = 0;
        float playerVY = 0;

        float Asmooth = 0.1f;
        float playerTA = 0;

        float sFOV = 60;
        float FOV;
        float depth = 20;

        int mapHeight = 16;
        int mapWidth = 16;
        float sWallHeight = 25;
        float wallHeight;

        string[,] map =
        {
            {"X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X"},
            {"X", " ", " ", " ", " ", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", "X", "X", "X", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", "X", " ", "X", " ", " ", "X", "X", " ", " ", "X", "X", " ", " ", "X"},
            {"X", " ", "X", " ", " ", " ", " ", "X", "X", " ", " ", "X", "X", " ", " ", "X"},
            {"X", " ", "X", "X", "X", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X"}
        };

        public void Start()
        {
            Console.Title = "Doom";
            Console.SetWindowSize(screenWidth * 2 + sideBarWidth + margin * 2, screenHeight + margin);
            Console.SetBufferSize(screenWidth * 2 + sideBarWidth + margin * 2, screenHeight + margin);
            Console.CursorVisible = false;

            FOV = screenWidth / sFOV;
            wallHeight = sWallHeight / screenHeight;

            updateTime.Start();
            calcTime.Start();
            renderTime.Start();
        }

        public void Update()
        {
            calcTime.Restart();
            screen = new Pixel[screenWidth, screenHeight];

            playerVX *= 1 - 0.1f * deltaTime;
            playerVY *= 1 - 0.1f * deltaTime;

            if (Console.KeyAvailable)
            {
                float forwardX = MathF.Sin(playerA) * 0.1f;
                float forwardY = MathF.Cos(playerA) * 0.1f;

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.W: playerVY += forwardY; playerVX += forwardX; break;
                    case ConsoleKey.A: playerTA -= MathF.PI / 4; break;
                    case ConsoleKey.S: playerVY -= forwardY; playerVX -= forwardX; break;
                    case ConsoleKey.D: playerTA += MathF.PI / 4; break;
                    case ConsoleKey.Add: FOV += 0.1f; wallHeight -= 0.1f; break;
                    case ConsoleKey.Subtract: FOV -= 0.1f; wallHeight += 0.1f; break;
                    case ConsoleKey.M: wallHeight += 0.1f; break;
                    case ConsoleKey.N: wallHeight -= 0.1f; break;
                }
            }

            if (map[(int)(playerX + playerVX * deltaTime), (int)playerY] == " ")
                playerX += playerVX * deltaTime;
            else
                playerVX = 0;
            if (map[(int)playerX, (int)(playerY + playerVY * deltaTime)] == " ")
                playerY += playerVY * deltaTime;
            else
                playerVY = 0;

            playerA = playerA * (1 - Asmooth) + playerTA * Asmooth;

            for (int x = 0; x < screenWidth; x++)
            {
                float rayAngle = (playerA - FOV / 2) + (x / (float)screenWidth) * FOV;
                
                float distanceToWall = 0;
                bool hitWall = false;

                float eyeX = MathF.Sin(rayAngle);
                float eyeY = MathF.Cos(rayAngle);

                while(!hitWall && distanceToWall < depth)
                {
                    distanceToWall += 0.1f;

                    int testX = (int)(playerX + eyeX * distanceToWall);
                    int testY = (int)(playerY + eyeY * distanceToWall);

                    if (testX < 0 || testX > mapWidth || testY < 0 || testY > mapHeight)
                    {
                        hitWall = true;
                        distanceToWall = depth;
                    } else
                    {
                        if (map[testX, testY] == "X")
                        {
                            hitWall = true;
                        }
                    }
                }

                float ceiling = screenHeight / 2 - screenHeight / distanceToWall * wallHeight;
                float floor = screenHeight - ceiling;
                int shade = (int)(distanceToWall / 3.5f + 0.5);
                int shadeAA = (int)(ceiling % 1 * 4 + shade);

                for (int y = 0; y < screenHeight; y++)
                {
                    if (y < ceiling)
                        screen[x, y] = new Pixel(4, ConsoleColor.Blue);
                    else if (y > ceiling + 1 && y < floor - 1)
                        screen[x, y] = new Pixel(shade, ConsoleColor.DarkRed);
                    else if (y > ceiling && y < floor - 1 && shadeAA < 4 && y != 0)
                        screen[x, y] = new Pixel(shadeAA, ConsoleColor.DarkRed);
                    else if (y > ceiling - 1 && y < floor && shadeAA < 4 && y != 0)
                        screen[x, y] = new Pixel(shadeAA, ConsoleColor.DarkRed);
                    else
                    {
                        float b = 1 - (((float)y - screenHeight / 2) / ((float)screenHeight / 2));
                        screen[x, y] = new Pixel((int)(b * 4.9), ConsoleColor.Gray, true);
                    }
                }
            }
            calcTime.Stop();

            renderTime.Restart();
            switch (renderMode)
            {
                case "colour":
                    {
                        // Render one pixel at a time, allows colour but slower, a lot slower
                        for (int y = 0; y < screenHeight; y++)
                        {
                            for (int x = 0; x < screenWidth; x++)
                            {
                                if (screen[x, y].pixel == "  ")
                                    Console.BackgroundColor = screen[x, y].color;
                                else
                                {
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.ForegroundColor = screen[x, y].color;
                                }
                                Console.SetCursorPosition(x * 2 + sideBarWidth, y);
                                Console.Write(screen[x, y].pixel);
                            }
                        }
                    } break;
                default:
                    {
                        // Render whole screen, no colour but faster
                        string display = "";
                        for (int y = 0; y < screenHeight; y++)
                        {
                            for (int x = 0; x < screenWidth; x++)
                                display += screen[x, y].pixel;
                            display += "\n\t"; // change the amount of \t's if you chnage the sidebar width, 1 \t for every 8 spaces
                        }
                        Console.SetCursorPosition(sideBarWidth, 0);
                        Console.Write(display);
                    }
                    break;
            }
            renderTime.Stop();

            string sideBar = "";
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < sideBarHeight; y++)
            {
                for (int x = 0; x < sideBarWidth; x++)
                    sideBar += " ";
                sideBar += "\n";
            }
            Console.Write(sideBar);
            if (updateTime.ElapsedMilliseconds != 0)
            fpsList.Add(1000 / updateTime.ElapsedMilliseconds);
            if (fpsList.Count > 60)
                fpsList.RemoveAt(0);
            float totalFps = 0;
            foreach (float var in fpsList)
                totalFps += var;
            fps = totalFps / fpsList.Count;

            Console.SetCursorPosition(0, 0);
            Console.WriteLine("FPS: " + (int)fps);
            if (updateTime.ElapsedMilliseconds != 0)
                Console.WriteLine("UPS: " +  1000 / updateTime.ElapsedMilliseconds);
            Console.WriteLine("Upd: " + updateTime.ElapsedMilliseconds);
            Console.WriteLine("Clc: " + calcTime.ElapsedMilliseconds);
            Console.WriteLine("Rnd: " + renderTime.ElapsedMilliseconds);
            deltaTime = (float)(updateTime.ElapsedMilliseconds * 0.06); // (timeInMiliseconds * 60) / 1000

            int newWidth = (int)((Console.WindowWidth - sideBarWidth) - margin * 2) / 2;
            int newHeight = (int)Console.WindowHeight - margin;
            if (screenWidth != newWidth || screenHeight != newHeight)
            {
                screenWidth = newWidth;
                screenHeight = newHeight;
                Console.SetWindowSize(screenWidth * 2 + sideBarWidth + margin * 2, screenHeight + margin);
                Console.CursorVisible = false;
                Console.Clear();
                FOV = screenWidth / sFOV;
                wallHeight = sWallHeight / screenHeight;
            }

            updateTime.Restart();
        }
    }

    class Program
    {
        static void Main()
        {
            ConsoleKey gameChoice;
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to the arcade");
                Console.WriteLine("Choose a game!\n");
                Console.WriteLine("1 - Snake");
                Console.WriteLine("2 - Doom");
                Console.WriteLine("\nEscape - Exit");
                gameChoice = Console.ReadKey().Key;

                if (gameChoice == ConsoleKey.D1) {
                    do
                    {
                        int highscore = 0;
                        if (File.Exists("Highscore.TXT"))
                        {
                            using (StreamReader sw = new StreamReader("Highscore.TXT"))
                                highscore = Convert.ToInt16(sw.ReadLine());
                        } else
                        {
                            using (StreamWriter sw = new StreamWriter("Highscore.TXT"))
                                sw.WriteLine(highscore);
                        }

                        Console.Clear();
                        Snake snake = new Snake();
                        snake.Start();
                        do
                        {
                            snake.Update();
                            System.Threading.Thread.Sleep(1000 / 16);
                        } while (snake.gameEnded == false);
                        Console.Clear();
                        Console.WriteLine("Game Over!");
                        if (snake.score > highscore)
                        {
                            highscore = snake.score;
                            Console.WriteLine("New Highscore!");

                            using (StreamWriter sw = new StreamWriter("Highscore.TXT"))
                                sw.WriteLine(highscore);
                        }
                        Console.WriteLine("\nScore: " + snake.score);
                        Console.WriteLine("Highscore: " + highscore);
                        Console.WriteLine("\nPress Enter to play again");
                    } while (Console.ReadKey().Key == ConsoleKey.Enter);
                }

                if (gameChoice == ConsoleKey.D2)
                {
                    do
                    {
                        Console.Clear();
                        Doom doom = new Doom();
                        doom.Start();
                        do
                        {
                            doom.Update();
                            //System.Threading.Thread.Sleep(1000 / 60);
                        } while (true);
                    } while (Console.ReadKey().Key == ConsoleKey.Enter);
                }
            } while (gameChoice != ConsoleKey.Escape);
            
        }
    }
}
