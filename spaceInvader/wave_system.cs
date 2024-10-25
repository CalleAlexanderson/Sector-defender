namespace SectorInvader
{

    static public class Wave_system
    {
        public static List<Enemy> wave = new List<Enemy>(); //lista med de skepp som nuvarande wave har i sig
        public static int waveSizeMultiplier; // på easy 10 på normal 12 på hard 15
        public static int waveSizeTracker;
        public static int waveNr = 1;
        private static int elitesInWave;
        private static readonly string[] normalShips = ["blitzen", "brawler", "dasher", "hunter", "nautilus", "ram", "seeker", "wasp", ""]; // "" är scrapper
        private static readonly string[] eliteShips = ["buster", "cataphract", "centurion", "destroyer"];
        private static bool levelUpOverlay = false;
        private static bool wavePrepared = false;
        private static bool elitesAdded = false; // kollar om elites har lagts till i wave
        public static bool gamecleared = false;

        public static void ResetGame()
        { //återställer wave system till de värden som spelet börjar med
            waveNr = 1;
            waveSizeTracker = 0;
            gamecleared = false;
            wavePrepared = false;
            elitesAdded = false;
            levelUpOverlay = false;
            wave = [];
        }
        public static void PrepareWave(Player player)
        {
            if (!levelUpOverlay) // om level up overlay är aktiv "pausas" spelet
            {
                if (!wavePrepared) // skapar en ny array med skepp för nästa wave
                {
                    int waveSize = waveSizeMultiplier + ((waveNr - 1) * waveSizeMultiplier); // beräknar wavesize
                    int elitePerWave = waveNr / 4;
                    if (waveNr == 10) // lägger till boss 1 till wave 10
                    {
                        Enemy reaper = Enemy_maker.CreateShip("reaper", 311);
                        reaper.LoadTexture();
                        wave.Add(reaper);
                        waveSizeTracker += reaper.GetDifficultyPoints();
                        waveNr++;
                        wavePrepared = true;
                    }
                    else if (waveNr == 18) // lägger till boss 2 till wave 18
                    {
                        Enemy mantis = Enemy_maker.CreateShip("mantis", 250);
                        mantis.LoadTexture();
                        wave.Add(mantis);
                        waveSizeTracker += mantis.GetDifficultyPoints();
                        waveNr++;
                        wavePrepared = true;
                    }

                    if (!wavePrepared) // gör så att inte bosswaves har extra fiender
                    {
                        if (!elitesAdded) // ser till att lägga till elites först till waven
                        {
                            Enemy eliteShip = Enemy_maker.CreateShip(eliteShips[Random.Shared.Next(eliteShips.Length)], Random.Shared.Next(100, 1100)); // lägger till ett random elite skepp med en slumpad startposition till ship listan

                            if (eliteShip.GetWaveStart() <= waveNr) // kollar om skeppet har möjlighet att komma denna wave
                            {
                                eliteShip.LoadTexture(); //laddar texture när skeppet läggs till så det inte behöver laddas för många textures
                                wave.Add(eliteShip);
                                waveSizeTracker += eliteShip.GetDifficultyPoints();
                                elitesInWave++;
                            }

                            if (waveNr == 4) { elitesAdded = true; }

                            if (elitePerWave == elitesInWave) { elitesAdded = true; }
                        }
                        else
                        {
                            Enemy ship = Enemy_maker.CreateShip(normalShips[Random.Shared.Next(normalShips.Length)], Random.Shared.Next(100, 1100)); // lägger till ett random normalt skepp med en slumpad startposition till ship listan

                            if (ship.GetWaveStart() <= waveNr) // kollar om skeppet har möjlighet att komma denna wave
                            {
                                if ((waveSize - waveSizeTracker) >= ship.GetDifficultyPoints()) // kollar om det finns plats i waven för skeppet
                                {
                                    ship.LoadTexture(); //laddar texture när skeppet läggs till så det inte behöver laddas för många textures
                                    wave.Add(ship);
                                    waveSizeTracker += ship.GetDifficultyPoints();
                                }
                            }
                        }
                    }

                    if (waveSizeTracker == waveSize) // om waveSize är max så startar waven
                    {
                        waveNr++;
                        wavePrepared = true;
                    }
                }
                else // om en wave redan är förberedd ritas waven ut
                {
                    DrawWave(player);
                }
            }
            else
            {
                WaveCleared(player);
            }

        }

        private static void DrawWave(Player player) // ritar ut alla skepp i waven
        {
            if (waveSizeTracker != 0)
            {
                for (int i = 0; i < wave.Count; i++)
                {
                    wave[i].DrawShip(player);
                }
            }
            else
            {
                levelUpOverlay = true;
                WaveCleared(player);
            }
        }

        private static void WaveCleared(Player player) // när alla skepp är döda är waven klarad
        {
            if (waveNr == 19 && !gamecleared)
            {
                Screen.currentScreen = "Win";
                gamecleared = true;
                Screen.SaveToScoreBoard(Arena.score, waveNr, Arena.timerMinutes, Arena.timerSeconds, Screen.difficulty);
            }
            player.LevelUp(player);

        }

        public static void NewWave(Player player) // start en ny wave
        {
            wavePrepared = false;
            elitesAdded = false;
            levelUpOverlay = false;
            wave = [];
            elitesInWave = 0;
            PrepareWave(player);
        }

        public static int GetWaveNr()
        {
            return waveNr;
        }
    }
}