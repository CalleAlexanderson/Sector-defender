namespace SectorInvader
{
    using Raylib_cs;

    static public class Arena
    {
        public static Rectangle homeBorder = new Rectangle(1, 700, 1200, 5); //där fienderna åker till, om de kolliderar tar bordern skada
        public static Rectangle borderWest = new Rectangle(1, -110, 5, 960); //-110 för att ta hand om där skepp spawnar
        public static Rectangle borderSouth = new Rectangle(1, 850, 1200, 0);
        public static Rectangle borderEast = new Rectangle(1200, -110, 5, 960); //-110 för att ta hand om där skepp spawnar
        public static Rectangle BulletBorder = new Rectangle(1, 1, 1200, 5); //höjden 5 så att den fångar upp skott även med öka hastighet
        public static Texture2D backgroundSprite = Raylib.LoadTexture(@"pictures/space.png"); //skapar en texture av bilden space för bakgrund
        public static float timerSeconds = 0;
        public static int timerMinutes = 0;
        public static int score = 0;

        public static void ResetGame()
        {  //återställer arena till de värden som spelet börjar med
            timerSeconds = 0;
            timerMinutes = 0;
            score = 0;
        }

        public static void Reset()
        {
            Raylib.ClearBackground(Color.Black);
            Raylib.DrawTexture(backgroundSprite, -10, -10, Color.White); //lägger ut bakgrundsbilden, -10 på positioner för att ta bort watermark
            Raylib.DrawRectangleRec(homeBorder, Color.White);
            Raylib.DrawText($"score: {score}", 10, 10, 20, Color.White); // skriver ut score
            Raylib.DrawText($"wave: {Wave_system.GetWaveNr() - 1}", 10, 70, 20, Color.White); // skriver ut score
            timerSeconds += Raylib.GetFrameTime();

            if ((int)timerSeconds == 60) // när det gått 60 sekunder ökar minuttimer och sekundtimer resetas
            {
                timerSeconds = 0;
                timerMinutes++;
            }

            // gör så att timer alltid skrivs i formatet 00:00 även när sekund och minut är under 10
            if ((int)timerSeconds < 10 && timerMinutes < 10)
            {
                Raylib.DrawText($"0{timerMinutes}:0{(int)timerSeconds}", 10, 40, 20, Color.White); // skriver ut timer
            }
            else if (timerMinutes < 10)
            {
                Raylib.DrawText($"0{timerMinutes}:{(int)timerSeconds}", 10, 40, 20, Color.White); // skriver ut timer
            }
            else if ((int)timerSeconds < 10)
            {
                Raylib.DrawText($"{timerMinutes}:0{(int)timerSeconds}", 10, 40, 20, Color.White); // skriver ut timer
            }
            else
            {
                Raylib.DrawText($"{timerMinutes}:{(int)timerSeconds}", 10, 40, 20, Color.White); // skriver ut timer
            }
        }

        public static void RaiseScore(int scoreUp)
        {
            score += scoreUp;
        }

        public static void BorderCollisions(Bullet bullet)
        { // när en kula skjuts kollas det om den träffar bulletBorder
            if (Raylib.CheckCollisionRecs(BulletBorder, bullet.shot))
            {
                bullet.Hit();
            }
        }
    }
}