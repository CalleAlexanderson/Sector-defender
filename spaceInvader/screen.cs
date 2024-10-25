namespace SectorInvader
{
    using System.Numerics;
    using System.Text.Json;
    using Raylib_cs;

    public static class Screen
    {
        public static string currentScreen = "MainMenu";
        public static string difficulty = "normal";
        private static Texture2D title = Raylib.LoadTexture(@"pictures/title.png");
        private static Texture2D death = Raylib.LoadTexture(@"pictures/death.png");
        private static Texture2D victory = Raylib.LoadTexture(@"pictures/victory.png");
        private static Texture2D startBtn = Raylib.LoadTexture(@"pictures/start_btn_1.png");
        private static Texture2D startBtnHover = Raylib.LoadTexture(@"pictures/start_btn_2.png");
        private static Texture2D controlsBtn = Raylib.LoadTexture(@"pictures/controls_btn_1.png");
        private static Texture2D controlsBtnHover = Raylib.LoadTexture(@"pictures/controls_btn_2.png");
        private static Texture2D controlsMove = Raylib.LoadTexture(@"pictures/controls_move.png");
        private static Texture2D controlsShoot = Raylib.LoadTexture(@"pictures/controls_shoot.png");
        private static Texture2D backBtn = Raylib.LoadTexture(@"pictures/back_btn_1.png");
        private static Texture2D backBtnHover = Raylib.LoadTexture(@"pictures/back_btn_2.png");
        private static Texture2D easyBtn = Raylib.LoadTexture(@"pictures/easy_btn_1.png");
        private static Texture2D easyBtnHover = Raylib.LoadTexture(@"pictures/easy_btn_2.png");
        private static Texture2D easyBtnActive = Raylib.LoadTexture(@"pictures/easy_btn_3.png");
        private static int easyBtnX = 250;
        private static Texture2D normalBtn = Raylib.LoadTexture(@"pictures/normal_btn_1.png");
        private static Texture2D normalBtnHover = Raylib.LoadTexture(@"pictures/normal_btn_2.png");
        private static Texture2D normalBtnActive = Raylib.LoadTexture(@"pictures/normal_btn_3.png");
        private static int normalBtnX = 490;
        private static Texture2D hardBtn = Raylib.LoadTexture(@"pictures/hard_btn_1.png");
        private static Texture2D hardBtnHover = Raylib.LoadTexture(@"pictures/hard_btn_2.png");
        private static Texture2D hardBtnActive = Raylib.LoadTexture(@"pictures/hard_btn_3.png");
        private static int hardBtnX = 800;
        private static Texture2D exitBtn = Raylib.LoadTexture(@"pictures/exit_btn_1.png");
        private static Texture2D exitBtnHover = Raylib.LoadTexture(@"pictures/exit_btn_2.png");
        private static Texture2D continueBtn = Raylib.LoadTexture(@"pictures/continue_btn_1.png");
        private static Texture2D continueBtnHover = Raylib.LoadTexture(@"pictures/continue_btn_2.png");
        private static Texture2D restartBtn = Raylib.LoadTexture(@"pictures/restart_btn_1.png");
        private static Texture2D restartBtnHover = Raylib.LoadTexture(@"pictures/restart_btn_2.png");
        private static Texture2D scoreboard = Raylib.LoadTexture(@"pictures/scoreboard.png");
        private static List<Score> scores = new List<Score>();

        public class Score
        {
            public required string score { get; set; }
            public required string timeMin { get; set; }
            public required string timeSec { get; set; }
            public required string wave { get; set; }
            public required string difficulty { get; set; }
        }
        
        public static void SaveToScoreBoard(int score, int wave, int min, float sec, string diff)
        {
            scores = [];
            Score newScore = new Score() //skapar ett nytt score
            {
                score = score.ToString(),
                wave = wave.ToString(),
                timeMin = min.ToString(),
                timeSec = ((int)sec).ToString(),
                difficulty = diff
            };

            if (File.Exists(@"scoreBoard.json"))
            {
                string currentScores = File.ReadAllText(@"scoreBoard.json");
                Score[] currentScoresArr = JsonSerializer.Deserialize<Score[]>(currentScores);
                for (int i = 0; i < currentScoresArr.Length; i++)
                {
                    scores.Add(currentScoresArr[i]);
                }
            }
            scores.Add(newScore);
            File.WriteAllText(@"scoreBoard.json", JsonSerializer.Serialize(scores));
        }

        private static void ShowScorebaord()
        {
            scores = [];
            if (File.Exists(@"scoreBoard.json"))
            {
                string currentScores = File.ReadAllText(@"scoreBoard.json");
                Score[] currentScoresArr = JsonSerializer.Deserialize<Score[]>(currentScores);
                for (int i = 0; i < currentScoresArr.Length; i++)
                {
                    scores.Add(currentScoresArr[i]);
                }
            }

            Score[] scoresArr = scores.ToArray();

            for (int i = 0; i < scoresArr.Length - 1; i++) //samma bubblesort vi använde i moment 2.2
            {
                for (int index = 0; index < scoresArr.Length - 1; index++)
                {
                    if (int.Parse(scoresArr[index].score) < int.Parse(scoresArr[index + 1].score))
                    {
                        (scoresArr[index], scoresArr[index + 1]) = (scoresArr[index + 1], scoresArr[index]);
                    }
                }
            }
            Raylib.DrawTexture(scoreboard, 100, 500, Color.White);

            for (int i = 0; i < scoresArr.Length; i++)
            {
                if (i == 3) { break; }
                Raylib.DrawText(scoresArr[i].wave, 120, 550 + (35 * (i + 1)), 20, Color.White);
                if (int.Parse(scoresArr[i].timeSec) < 10 && int.Parse(scoresArr[i].timeMin) < 10)
                {
                    Raylib.DrawText($"0{scoresArr[i].timeMin}:0{scoresArr[i].timeSec}", 170, 550 + (35 * (i + 1)), 20, Color.White); // skriver ut timer
                }
                else if (int.Parse(scoresArr[i].timeMin) < 10)
                {
                    Raylib.DrawText($"0{scoresArr[i].timeMin}:{scoresArr[i].timeSec}", 170, 550 + (35 * (i + 1)), 20, Color.White); // skriver ut timer
                }
                else if (int.Parse(scoresArr[i].timeSec) < 10)
                {
                    Raylib.DrawText($"{scoresArr[i].timeMin}:0{scoresArr[i].timeSec}", 170, 550 + (35 * (i + 1)), 20, Color.White); // skriver ut timer
                }
                else
                {
                    Raylib.DrawText($"{scoresArr[i].timeMin}:{scoresArr[i].timeSec}", 170, 550 + (35 * (i + 1)), 20, Color.White); // skriver ut timer
                }
                Raylib.DrawText(scoresArr[i].score, 240, 550 + (35 * (i + 1)), 20, Color.White);
                Raylib.DrawText(scoresArr[i].difficulty, 325, 550 + (35 * (i + 1)), 20, Color.White);
            }

        }
        public static void MainMenuScreen()
        {
            Vector2 mouse = Raylib.GetMousePosition();
            Raylib.DrawTexture(Arena.backgroundSprite, -10, -10, Color.White); //lägger ut bakgrundsbilden, -10 på positioner för att ta bort watermark
            Raylib.DrawTexture(title, 200, 20, Color.White);
            Rectangle start = new Rectangle(520, 500, 160, 60);
            Raylib.DrawRectangleRec(start, Color.Blank);
            Raylib.DrawTexture(startBtn, 520, 500, Color.White);
            Rectangle controls = new Rectangle(470, 600, 260, 60);
            Raylib.DrawRectangleRec(controls, Color.Blank);
            Raylib.DrawTexture(controlsBtn, 470, 600, Color.White);

            Rectangle easy = new Rectangle(easyBtnX, 720, 150, 60);
            Rectangle normal = new Rectangle(normalBtnX, 720, 220, 60);
            Rectangle hard = new Rectangle(hardBtnX, 720, 150, 60);

            switch (difficulty)
            {
                case "easy":
                    Raylib.DrawTexture(easyBtnActive, easyBtnX, 720, Color.White);
                    Raylib.DrawTexture(normalBtn, normalBtnX, 720, Color.White);
                    Raylib.DrawTexture(hardBtn, hardBtnX, 720, Color.White);
                    if (Raylib.CheckCollisionPointRec(mouse, normal))
                    {
                        Raylib.DrawTexture(normalBtnHover, normalBtnX, 720, Color.White);
                        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                        {
                            difficulty = "normal";
                        }
                    }
                    if (Raylib.CheckCollisionPointRec(mouse, hard))
                    {
                        Raylib.DrawTexture(hardBtnHover, hardBtnX, 720, Color.White);
                        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                        {
                            difficulty = "hard";
                        }
                    }
                    break;
                case "hard":
                    Raylib.DrawTexture(easyBtn, easyBtnX, 720, Color.White);
                    Raylib.DrawTexture(normalBtn, normalBtnX, 720, Color.White);
                    Raylib.DrawTexture(hardBtnActive, hardBtnX, 720, Color.White);

                    if (Raylib.CheckCollisionPointRec(mouse, easy))
                    {
                        Raylib.DrawTexture(easyBtnHover, easyBtnX, 720, Color.White);
                        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                        {
                            difficulty = "easy";
                        }
                    }
                    if (Raylib.CheckCollisionPointRec(mouse, normal))
                    {
                        Raylib.DrawTexture(normalBtnHover, normalBtnX, 720, Color.White);
                        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                        {
                            difficulty = "normal";
                        }
                    }
                    break;
                default:
                    Raylib.DrawTexture(normalBtnActive, normalBtnX, 720, Color.White);
                    Raylib.DrawTexture(easyBtn, easyBtnX, 720, Color.White);
                    Raylib.DrawTexture(hardBtn, hardBtnX, 720, Color.White);

                    if (Raylib.CheckCollisionPointRec(mouse, easy))
                    {
                        Raylib.DrawTexture(easyBtnHover, easyBtnX, 720, Color.White);
                        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                        {
                            difficulty = "easy";
                        }
                    }
                    if (Raylib.CheckCollisionPointRec(mouse, hard))
                    {
                        Raylib.DrawTexture(hardBtnHover, hardBtnX, 720, Color.White);
                        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                        {
                            difficulty = "hard";
                        }
                    }
                    break;
            }

            if (Raylib.CheckCollisionPointRec(mouse, start))
            {
                Raylib.DrawTexture(startBtnHover, 520, 500, Color.White);
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    Wave_system.waveSizeMultiplier = difficulty switch
                    {
                        "easy" => 10,
                        "hard" => 15,
                        _ => 12,
                    };
                    currentScreen = "game";
                }
            }

            if (Raylib.CheckCollisionPointRec(mouse, controls))
            {
                Raylib.DrawTexture(controlsBtnHover, 470, 600, Color.White);
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    currentScreen = "Controls";
                }
            }
            ShowScorebaord();
        }

        public static void ControlsScreen()
        {
            Vector2 mouse = Raylib.GetMousePosition();
            Raylib.DrawTexture(Arena.backgroundSprite, -10, -10, Color.White); //lägger ut bakgrundsbilden, -10 på positioner för att ta bort watermark
            Rectangle back = new Rectangle(525, 700, 150, 60);
            Raylib.DrawRectangleRec(back, Color.Blank);
            Raylib.DrawTexture(backBtn, 525, 700, Color.White);
            Raylib.DrawText("MOVE", 555, 265, 30, Color.White);
            Raylib.DrawTexture(controlsMove, 490, 300, Color.White);
            Raylib.DrawText("SHOOT", 545, 515, 30, Color.White);
            Raylib.DrawTexture(controlsShoot, 478, 550, Color.White);
            if (Raylib.CheckCollisionPointRec(mouse, back))
            {
                Raylib.DrawTexture(backBtnHover, 525, 700, Color.White);
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    currentScreen = "MainMenu";
                }
            }
        }
        public static void WinScreen(Player player)
        {
            Vector2 mouse = Raylib.GetMousePosition();
            Raylib.DrawTexture(Arena.backgroundSprite, -10, -10, Color.White); //lägger ut bakgrundsbilden, -10 på positioner för att ta bort watermark
            Raylib.DrawTexture(victory, 200, 20, Color.White);
            Rectangle continueButton = new Rectangle(470, 600, 260, 60);
            Raylib.DrawRectangleRec(continueButton, Color.Blank);
            Raylib.DrawTexture(continueBtn, 470, 600, Color.White);
            Rectangle restart = new Rectangle(490, 700, 220, 60);
            Raylib.DrawRectangleRec(restart, Color.Blank);
            Raylib.DrawTexture(restartBtn, 490, 700, Color.White);
            if (Raylib.CheckCollisionPointRec(mouse, continueButton))
            {
                Raylib.DrawTexture(continueBtnHover, 470, 600, Color.White);
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    currentScreen = "game";
                }
            }

            if (Raylib.CheckCollisionPointRec(mouse, restart))
            {
                Raylib.DrawTexture(restartBtnHover, 490, 700, Color.White);
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    player.ResetGame();
                    Wave_system.ResetGame();
                    Arena.ResetGame();
                    currentScreen = "MainMenu";
                }
            }
        }

        public static void DeathScreen(Player player)
        {
            Vector2 mouse = Raylib.GetMousePosition();
            Raylib.DrawTexture(Arena.backgroundSprite, -10, -10, Color.White); //lägger ut bakgrundsbilden, -10 på positioner för att ta bort watermark
            Raylib.DrawTexture(death, 200, 20, Color.White);
            Rectangle restart = new Rectangle(490, 640, 220, 60);
            Raylib.DrawRectangleRec(restart, Color.Blank);
            Raylib.DrawTexture(restartBtn, 490, 640, Color.White);
            if (Raylib.CheckCollisionPointRec(mouse, restart))
            {
                Raylib.DrawTexture(restartBtnHover, 490, 640, Color.White);
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    player.ResetGame();
                    Wave_system.ResetGame();
                    Arena.ResetGame();
                    currentScreen = "MainMenu";
                }
            }
        }
    }
}