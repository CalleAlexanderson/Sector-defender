namespace SectorInvader
{
    using Raylib_cs;
    class Program
    {
        static void Main()
        {
            // att göra:
            // rectangel som spelaren kontrollerar, skjuter på någon knapp :: klart
            // skott försvinner om det har kontakt med fiende :: klart
            // skapa en klass för fiender som individuella fiender ärver ifrån :: klart
            // skapa en timer upp i vänstra hörnet :: klart
            // skapa ett system som lägger in fiender i en array som varje wave är, när array är tom skapas en ny :: klart
            // gör så att varje fiende har ett visst antal poäng och varje wave kan bara bestå av ett visst antal :: klart
            // skapat ett scoreboard till spelet

            // saker som lades till under skapande:
            // uppgradera spelarens skepp efter varje wave :: klart
            // gör så att en elite kommer in var fjärde wave  :: klart
            // wave counter :: klart
            // score system :: klart

            Raylib.InitWindow(1200, 850, "Game");
            Raylib.SetTargetFPS(60);
            Player player = new Player();
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                switch (Screen.currentScreen)
                {
                    case "MainMenu":
                        Screen.MainMenuScreen();
                        break;
                    case "Win":
                        Screen.WinScreen(player);
                        break;
                    case "Death":
                        Screen.DeathScreen(player);
                        break;
                    case "Controls":
                        Screen.ControlsScreen();
                        break;
                    default:
                        Arena.Reset();
                        player.DrawPlayer();
                        Wave_system.PrepareWave(player);
                        break;
                }
                Raylib.EndDrawing();
            }
        }
    }
}