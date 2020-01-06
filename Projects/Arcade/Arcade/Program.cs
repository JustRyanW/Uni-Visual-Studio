using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace Arcade
{
    struct Pixel
    {
        private string[] shades;
        private string[] floorShades;
        public ConsoleColor color;
        public string pixel;
        public Pixel(int shade = 0, ConsoleColor color = ConsoleColor.White, bool floor = false)
        {
            shades = new string[]{ "██", "▓▓", "▒▒", "░░", "  " };
            floorShades = new string[]{ "##", "xx", "--", "..", "  " };
            if (!floor)
                pixel = shades[(shade <= 4) ? shade : 4];
            else
                pixel = floorShades[(shade <= 4) ? shade : 4];
            this.color = color;
        }

        public static bool operator ==(Pixel c1, Pixel c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(Pixel c1, Pixel c2)
        {
            return !c1.Equals(c2);
        }
    }

    struct Vector
    {
        public float x, y, z;

        public Vector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    struct Triangle
    {
        public Vector[] vectors;
        public Triangle(Vector vec1, Vector vec2, Vector vec3)
        {
            vectors = new Vector[3];
            vectors[0] = vec1;
            vectors[1] = vec2;
            vectors[2] = vec3;
        }

        public Triangle(float x1, float y1, float z1, float x2, float y2, float z2, float x3, float y3, float z3)
        {
            vectors = new Vector[3];
            vectors[0] = new Vector(x1, y1, z1);
            vectors[1] = new Vector(x2, y2, z2);
            vectors[2] = new Vector(x3, y3, z3);
        }

        public Triangle(bool empty)
        {
            vectors = new Vector[3];
            vectors[0] = new Vector(0.0f, 0.0f, 0.0f);
            vectors[1] = new Vector(0.0f, 0.0f, 0.0f);
            vectors[2] = new Vector(0.0f, 0.0f, 0.0f);
        }

        public Triangle Copy()
        {
            return new Triangle(
                vectors[0].x, vectors[0].y, vectors[0].z,
                vectors[1].x, vectors[1].y, vectors[1].z,
                vectors[2].x, vectors[2].y, vectors[2].z
                );
        }
    }

    struct mesh
    {
        public List<Triangle> triangles;
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
            }
            else if (deathType == 2)
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
                    case ConsoleKey.Escape: gameEnded = true; break;
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
            }
            else
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
        string renderMode = "colour"; // "mono", "colour"

        float fps;
        List<float> fpsList = new List<float>();
        Stopwatch updateTime = new Stopwatch();
        Stopwatch renderTime = new Stopwatch();
        Stopwatch calcTime = new Stopwatch();
        float deltaTime;

        int sideBarWidth = 8;
        int sideBarHeight = 5;

        Pixel[,] screen;
        Pixel[,] screenPrev;

        float playerX = 1.5f;
        float playerY = 1.5f;
        float playerA = MathF.PI / 4;

        float playerVX = 0;
        float playerVY = 0;

        float Asmooth = 0.1f;
        float playerTA = MathF.PI / 4;

        float sFOV = 50;
        float FOV;
        float depth = 20;

        int mapHeight = 16;
        int mapWidth = 32;
        float sWallHeight = 25;
        float wallHeight;

        string[,] map =
        {
            {"X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X"},
            {"X", " ", " ", " ", " ", " ", " ", " ", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", "X", "X", "X", " ", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", "X", " ", "X", " ", "X", " ", " ", "X", "X", " ", " ", "X"},
            {"X", " ", "X", " ", " ", " ", "X", " ", "X", " ", " ", "X", "X", " ", " ", "X"},
            {"X", " ", "X", "X", "X", "X", "X", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", "X", "X", " ", " ", " ", "X", "X", "X", "X"},
            {"X", " ", " ", " ", " ", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", "X", "X", "X", "X", " ", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X"},
            {"X", "X", "X", "X", "X", " ", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X"},
            {"X", " ", " ", " ", " ", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", "X", "X", "X", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", "X", " ", "X", " ", " ", "X", "X", " ", " ", "X", "X", " ", " ", "X"},
            {"X", " ", "X", " ", " ", " ", " ", "X", "X", " ", " ", "X", "X", " ", " ", "X"},
            {"X", " ", "X", "X", "X", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", "X", "X", " ", " ", " ", "X", "X", "X", "X"},
            {"X", " ", " ", " ", " ", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "X"},
            {"X", " ", " ", " ", " ", " ", " ", "X", "X", " ", " ", " ", " ", " ", " ", "X"},
            {"X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X"}
        };

        public bool gameEnded = false;

        public void Start()
        {
            Console.Title = "Doom";
            Console.SetWindowSize(screenWidth * 2 + sideBarWidth + margin * 2, screenHeight + margin);
            Console.SetBufferSize(screenWidth * 2 + sideBarWidth + margin * 2, screenHeight + margin);
            Console.CursorVisible = false;

            FOV = screenWidth / sFOV;
            wallHeight = sWallHeight / screenHeight;

            screenPrev = new Pixel[screenWidth, screenHeight];
            for (int x = 0; x < screenWidth; x++)
                for (int y = 0; y < screenHeight; y++)
                    screenPrev[x, y] = new Pixel();

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
                    case ConsoleKey.L: FOV += 0.1f; wallHeight -= 0.1f; break;
                    case ConsoleKey.K: FOV -= 0.1f; wallHeight += 0.1f; break;
                    case ConsoleKey.M: wallHeight += 0.1f; break;
                    case ConsoleKey.N: wallHeight -= 0.1f; break;
                    case ConsoleKey.Escape: gameEnded = true; break;
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

                while (!hitWall && distanceToWall < depth)
                {
                    distanceToWall += 0.1f;

                    int testX = (int)(playerX + eyeX * distanceToWall);
                    int testY = (int)(playerY + eyeY * distanceToWall);

                    if (testX < 0 || testX > mapWidth || testY < 0 || testY > mapHeight)
                    {
                        hitWall = true;
                        distanceToWall = depth;
                    }
                    else
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
                        screen[x, y] = new Pixel(0, ConsoleColor.Blue);
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
                        for (int y = 0; y < screenHeight; y++)
                        {
                            for (int x = 0; x < screenWidth; x++)
                            {
                                if (screenPrev[x, y].pixel != screen[x, y].pixel || screenPrev[x, y].color != screen[x, y].color) // compare .pixel and .color
                                {
                                    Console.SetCursorPosition(x * 2 + sideBarWidth, y);
                                    Console.ForegroundColor = screen[x, y].color;
                                    Console.Write(screen[x, y].pixel);
                                    screenPrev[x, y] = screen[x, y];
                                }
                            }
                        }
                    }
                    break;
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
                Console.WriteLine("UPS: " + 1000 / updateTime.ElapsedMilliseconds);
            Console.WriteLine("Upd: " + updateTime.ElapsedMilliseconds);
            Console.WriteLine("Clc: " + calcTime.ElapsedMilliseconds);
            Console.WriteLine("Rnd: " + renderTime.ElapsedMilliseconds);

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



            deltaTime = (float)(updateTime.ElapsedMilliseconds * 0.06); // (timeInMiliseconds * 60) / 1000
            updateTime.Restart();
        }
    }

    class Minecraft
    {
        struct mat4x4
        {
            public float[,] m;

            public mat4x4(float i)
            {
                m = new float[4, 4]
                {
                    {i, i, i, i},
                    {i, i, i, i},
                    {i, i, i, i},
                    {i, i, i, i}
                };
            }
        }

        public bool gameEnded = false;
        public float fTheta = 0;

        mesh meshCube;
        mat4x4 matProj = new mat4x4(0);

        int screenWidth = 120, screenHeight = 120;

        Pixel[,] screen;
        Pixel[,] screenPrev;

        public void Start()
        {
            Console.Title = "Minecraft";
            Console.Clear();

            screenPrev = new Pixel[screenWidth, screenHeight];
            for (int x = 0; x < screenWidth; x++)
                for (int y = 0; y < screenHeight; y++)
                    screenPrev[x, y] = new Pixel();

            meshCube.triangles = new List<Triangle>
            {
                // South
                new Triangle(0, 0, 0, 0, 1, 0, 1, 1, 0),
                new Triangle(0, 0, 0, 1, 1, 0, 1, 0, 0),
                // East
                new Triangle(1, 0, 0, 1, 1, 0, 1, 1, 1),
                new Triangle(1, 0, 0, 1, 1, 1, 1, 0, 1),
                // North
                new Triangle(1, 0, 1, 1, 1, 1, 0, 1, 1),
                new Triangle(1, 0, 1, 0, 1, 1, 0, 0, 1),
                // West
                new Triangle(0, 0, 1, 0, 1, 1, 0, 1, 0),
                new Triangle(0, 0, 1, 0, 1, 0, 0, 0, 0),
                // Top
                new Triangle(0, 1, 0, 0, 1, 1, 1, 1, 1),
                new Triangle(0, 1, 0, 1, 1, 1, 1, 1, 0),
                // Bottom
                new Triangle(1, 0, 1, 0, 0, 1, 0, 0, 0),
                new Triangle(1, 0, 1, 0, 0, 0, 1, 0, 0)
            };

            // Projection Matrix
            float fNear = 0.1f;
            float fFar = 1000.0f;
            float fFov = 90.0f;
            float fAspectRatio = (float)screenHeight / screenWidth;
            float fFovRad = 1.0f / MathF.Tan(fFov * 0.5f / 180.0f * MathF.PI);

            matProj.m[0, 0] = fAspectRatio * fFovRad;
            matProj.m[1, 1] = fFovRad;
            matProj.m[2, 2] = fFar / (fFar - fNear);
            matProj.m[3, 2] = (-fFar * fNear) / (fFar - fNear);
            matProj.m[2, 3] = 1;
            matProj.m[3, 3] = 0.0f;
        }

        public void Update()
        {
            if (Console.KeyAvailable)
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                    gameEnded = true;

            screen = new Pixel[screenWidth, screenHeight];
            for (int x = 0; x < screenWidth; x++)
                for (int y = 0; y < screenHeight; y++)
                    screen[x, y] = new Pixel(4);

            fTheta += 0.1f;

            mat4x4 matRotZ = new mat4x4(0);
            mat4x4 matRotX = new mat4x4(0);

            matRotZ.m[0, 0] = MathF.Cos(fTheta);
            matRotZ.m[0, 1] = MathF.Sin(fTheta);
            matRotZ.m[1, 0] = -MathF.Sin(fTheta);
            matRotZ.m[1, 1] = MathF.Cos(fTheta);
            matRotZ.m[2, 2] = 1;
            matRotZ.m[3, 3] = 1;


            matRotX.m[0, 0] = 1;
            matRotX.m[1, 1] = MathF.Cos(fTheta * 0.5f);
            matRotX.m[1, 2] = MathF.Sin(fTheta * 0.5f);
            matRotX.m[2, 1] = -MathF.Sin(fTheta * 0.5f);
            matRotX.m[2, 2] = MathF.Cos(fTheta * 0.5f);
            matRotX.m[3, 3] = 1;

            // Draw tris
            foreach (Triangle tri in meshCube.triangles)
            {
                Triangle triProjected = new Triangle(true);
                Triangle triTranslated = new Triangle(true);
                Triangle triRotatedZ = new Triangle(true);
                Triangle triRotatedZX = new Triangle(true);

                // rotating z for some reason flattens cube to plane ???????? rotating x gives some weird stretching ??


                MultiplyMatrixVector(tri.vectors[0], ref triRotatedZ.vectors[0], matRotZ);
                MultiplyMatrixVector(tri.vectors[1], ref triRotatedZ.vectors[1], matRotZ);
                MultiplyMatrixVector(tri.vectors[2], ref triRotatedZ.vectors[2], matRotZ);

                MultiplyMatrixVector(triRotatedZ.vectors[0], ref triRotatedZX.vectors[0], matRotX);
                MultiplyMatrixVector(triRotatedZ.vectors[1], ref triRotatedZX.vectors[1], matRotX);
                MultiplyMatrixVector(triRotatedZ.vectors[2], ref triRotatedZX.vectors[2], matRotX);

                triTranslated = triRotatedZX.Copy();
                triTranslated.vectors[0].z += 2.0f;
                triTranslated.vectors[1].z += 2.0f;
                triTranslated.vectors[2].z += 2.0f;


                MultiplyMatrixVector(triTranslated.vectors[0], ref triProjected.vectors[0], matProj);
                MultiplyMatrixVector(triTranslated.vectors[1], ref triProjected.vectors[1], matProj);
                MultiplyMatrixVector(triTranslated.vectors[2], ref triProjected.vectors[2], matProj);

                // Scale into view
                triProjected.vectors[0].x += 1.0f; triProjected.vectors[0].y += 1.0f;
                triProjected.vectors[1].x += 1.0f; triProjected.vectors[1].y += 1.0f;
                triProjected.vectors[2].x += 1.0f; triProjected.vectors[2].y += 1.0f;

                triProjected.vectors[0].x *= 0.5f * screenWidth; triProjected.vectors[0].y *= 0.5f * screenHeight;
                triProjected.vectors[1].x *= 0.5f * screenWidth; triProjected.vectors[1].y *= 0.5f * screenHeight;
                triProjected.vectors[2].x *= 0.5f * screenWidth; triProjected.vectors[2].y *= 0.5f * screenHeight;

                // Draw
                DrawTriangle(
                    (int)triProjected.vectors[0].x, (int)triProjected.vectors[0].y,
                    (int)triProjected.vectors[1].x, (int)triProjected.vectors[1].y,
                    (int)triProjected.vectors[2].x, (int)triProjected.vectors[2].y
                    );
            }

            switch("mono")
            {
                case "colour":
                    for (int y = 0; y < screenHeight; y++)
                    {
                        for (int x = 0; x < screenWidth; x++)
                        {
                            if (screenPrev[x, y].pixel != screen[x, y].pixel || screenPrev[x, y].color != screen[x, y].color) // compare .pixel and .color
                            {
                                Console.SetCursorPosition(x * 2, y);
                                Console.ForegroundColor = screen[x, y].color;
                                Console.Write(screen[x, y].pixel);
                                screenPrev[x, y] = screen[x, y];
                            }
                        }
                    }
                    break;
                default:
                    string displayString = "";
                    for (int y = 0; y < screenHeight; y++)
                    {
                        for (int x = 0; x < screenWidth; x++)
                        {
                            displayString += screen[x, y].pixel;
                        }
                        displayString += "\n";
                    }
                    Console.SetCursorPosition(0, 0);
                    Console.Write(displayString);
                    break;
            }
        }

        void DrawTriangle (int x0, int y0, int x1, int y1, int x2, int y2)
        {
            DrawLine(x0, y0, x1, y1);
            DrawLine(x1, y1, x2, y2);
            DrawLine(x2, y2, x0, y0);
        }

        void DrawLine(int x0, int y0, int x1, int y1)
        {
            int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2, e2;
            for (; ; )
            {
                if (x0 >= 0 && x0 < screenWidth && y0 >= 0 && y0 < screenHeight)
                screen[x0, y0] = new Pixel(0);
                if (x0 == x1 && y0 == y1) break;
                e2 = err;
                if (e2 > -dx) { err -= dy; x0 += sx; }
                if (e2 < dy) { err += dx; y0 += sy; }
            }
        }

        void MultiplyMatrixVector(Vector i, ref Vector o, mat4x4 m)
        {
            o.x = i.x * m.m[0, 0] + i.y * m.m[1, 0] + i.z * m.m[2, 0] + m.m[3, 0];
            o.y = i.x * m.m[0, 1] + i.y * m.m[1, 1] + i.z * m.m[2, 1] + m.m[3, 1];
            o.z = i.x * m.m[0, 2] + i.y * m.m[1, 2] + i.z * m.m[2, 3] + m.m[3, 2];
            float w = i.x * m.m[0, 3] + i.y * m.m[1, 3] + i.z * m.m[2, 3] + m.m[3, 3];

            if (w != 0)
            {
                o.x /= w;
                o.y /= w;
                o.z /= w;
            }
        }
    }

    /*
    class Pathfinder 
    {
        public bool gameEnded = false;

        int mapSize = 8

        int[,] map = {
            {1, 1, 1, 1, 1, 1, 1, 1},
            {1, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 1},
            {1, 1, 1, 1, 1, 1, 1, 1},
        };

        public void Start()
        {

        }

        public void Update()
        {
            for (int i = 0; i < mapSize; i++)
            {

            }
        }

    }
    */
    class Program
    {
        static void Main()
        {
            ConsoleKey gameChoice;
            do
            {
                Console.Title = "Arcade";
                Console.CursorVisible = false;
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Welcome to the arcade");
                Console.WriteLine("Choose a game!\n");
                Console.WriteLine("1 - Snake");
                Console.WriteLine("2 - Doom");
                Console.WriteLine("3 - Minecraft");
                // Console.WriteLine("4 - Pathfinder");
                Console.WriteLine("\nEscape - Exit");
                gameChoice = Console.ReadKey().Key;

                if (gameChoice == ConsoleKey.D1)
                {
                    do
                    {
                        int highscore = 0;
                        if (File.Exists("Highscore.TXT"))
                        {
                            using (StreamReader sw = new StreamReader("Highscore.TXT"))
                                highscore = Convert.ToInt16(sw.ReadLine());
                        }
                        else
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
                    Console.Clear();
                    Doom doom = new Doom();
                    doom.Start();
                    do
                    {
                        doom.Update();
                        //System.Threading.Thread.Sleep(1000 / 60);
                    } while (!doom.gameEnded);
                }
                if (gameChoice == ConsoleKey.D3)
                {
                    Console.Clear();
                    Minecraft minecraft = new Minecraft();
                    minecraft.Start();
                    do
                    {
                        minecraft.Update();
                        System.Threading.Thread.Sleep(1000 / 60);
                    } while (!minecraft.gameEnded);
                }
                /*if (gameChoice == ConsoleKey.D4)
                {
                    Console.Clear();
                    Pathfinder pathfinder = new Pathfinder();
                    pathfinder.Start();
                    do
                    {
                        pathfinder.Update();
                        //System.Threading.Thread.Sleep(1000 / 60);
                    } while (!pathfinder.gameEnded);
                }*/
            } while (gameChoice != ConsoleKey.Escape);

        }
    }
}

