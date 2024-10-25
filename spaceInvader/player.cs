namespace SectorInvader
{
    using System.Numerics;
    using Raylib_cs;

    public class Player
    {
        int playerPositionX;
        int playerPositionY;
        Rectangle player;
        Texture2D playerSprite = Raylib.LoadTexture(@"pictures/player_ship.png"); //skapar en texture för spelaren skepp;
        bool rightBlasterShot = true;
        int magazineSize = 4; // max antal skott spelaren kan ha i magasinet, ska gå att uppgradera efter varje wave
        public List<Bullet> bullets = new List<Bullet>(); //lista med de skott spelaren kan skjuta
        int hitpoints = 100; // ska gå att uppgradera efter varje wave
        int currentHitpoints;
        int playerSpeed = 4; // ska gå att uppgradera efter varje wave
        int damage = 5; // ska gå att uppgradera efter varje wave
        float bulletVelocity = 5f; // ska gå att uppgradera efter varje wave
        Texture2D health = Raylib.LoadTexture(@"pictures/health1.png");
        Texture2D healthHover = Raylib.LoadTexture(@"pictures/health2.png");
        Texture2D ammo = Raylib.LoadTexture(@"pictures/ammo1.png");
        Texture2D ammoHover = Raylib.LoadTexture(@"pictures/ammo2.png");
        Texture2D ammoMax = Raylib.LoadTexture(@"pictures/ammo3.png");
        Texture2D dmg = Raylib.LoadTexture(@"pictures/damage1.png");
        Texture2D dmgHover = Raylib.LoadTexture(@"pictures/damage2.png");
        Texture2D dmgMax = Raylib.LoadTexture(@"pictures/damage3.png");
        Texture2D speed = Raylib.LoadTexture(@"pictures/speed1.png");
        Texture2D speedHover = Raylib.LoadTexture(@"pictures/speed2.png");
        Texture2D speedMax = Raylib.LoadTexture(@"pictures/speed3.png");

        public Player()
        {
            currentHitpoints = hitpoints;
            playerPositionX = 568;
            playerPositionY = 750;
            player = new Rectangle(playerPositionX, playerPositionY, 64, 86);

            for (int i = 0; i < magazineSize; i++) //skapar ett magasin med de skott spelaren kan skjuta
            {
                Bullet newBullet = new Bullet()
                {
                    ready = true,
                    inAir = false
                };
                bullets.Add(newBullet);
            }
        }

        public void ResetGame()
        { //återställer spelaren till de värden som spelet börjar med
            currentHitpoints = hitpoints;
            playerPositionX = 568;
            playerPositionY = 750;
            magazineSize = 4;
            damage = 5;
            bulletVelocity = 5f;
            playerSpeed = 4;
            bullets = [];

            for (int i = 0; i < magazineSize; i++) //skapar ett magasin med de skott spelaren kan skjuta
            {
                Bullet newBullet = new Bullet()
                {
                    ready = true,
                    inAir = false
                };
                bullets.Add(newBullet);
            }
        }

        public void DrawPlayer()
        {
            Raylib.DrawRectangleRec(player, Color.Blank); // används för hitbox, color blank för att den inte ska synas
            Raylib.DrawTexture(playerSprite, playerPositionX, playerPositionY, Color.White); //lägger ut bild för spelarens skepp
            player.X = playerPositionX;
            player.Y = playerPositionY;

            //hur man rör på sig
            if (Raylib.IsKeyDown(KeyboardKey.W) && !Raylib.CheckCollisionRecs(player, Arena.homeBorder))
            {
                playerPositionY -= playerSpeed;
            }

            if (Raylib.IsKeyDown(KeyboardKey.A) && !Raylib.CheckCollisionRecs(player, Arena.borderWest))
            {
                playerPositionX -= playerSpeed;
            }

            if (Raylib.IsKeyDown(KeyboardKey.S) && !Raylib.CheckCollisionRecs(player, Arena.borderSouth))
            {
                playerPositionY += playerSpeed;
            }

            if (Raylib.IsKeyDown(KeyboardKey.D) && !Raylib.CheckCollisionRecs(player, Arena.borderEast))
            {
                playerPositionX += playerSpeed;
            }

            if (Raylib.IsKeyPressed(KeyboardKey.Space)) //skjuter ett skott om knappen spacce trycks
            {
                Shoot();
            }
            ShowMagazine();
            TrackHealth();
        }

        public void ChangeHealth(int hitpointsChange)
        {
            currentHitpoints -= hitpointsChange;
        }

        public int GetDamage()
        {
            return damage;
        }

        private void Shoot()
        {
            for (int i = 0; i < bullets.Count; i++) //kollar om alla skott är redo
            {
                if (bullets[i].ShotReady()) //skjuter bara första skottet som är redo
                {
                    bullets[i].Fire(playerPositionX, playerPositionY, rightBlasterShot, bulletVelocity);
                    rightBlasterShot = !rightBlasterShot; // byter blaster som skottet skjuts från efter varje skott
                    break;
                }
            }
        }

        private void TrackHealth()
        {
            if (currentHitpoints <= 0)
            {
                if (Wave_system.gamecleared == false)
                {
                    Screen.SaveToScoreBoard(Arena.score, Wave_system.waveNr, Arena.timerMinutes, Arena.timerSeconds, Screen.difficulty);
                }
                Screen.currentScreen = "Death";
            }
            string healthText = $"{currentHitpoints}/{hitpoints} Health";
            Raylib.DrawText(healthText, 10, 675, 20, Color.Red); // skriver ut health
        }

        private void ShowMagazine()
        {

            int ammoCount = 0; // mängden skott som är laddade
            for (int i = 0; i < bullets.Count; i++) // kollar hur många skott som är redo
            {
                if (bullets[i].ShotReady())
                {
                    ammoCount++;
                }
            }

            Raylib.DrawText("Bullets:", 1060, 585, 20, Color.Orange); // skriver ut magasinet
            Raylib.DrawText($"{ammoCount}/{bullets.Count} ammo", 1060, 615, 20, Color.Orange); // skriver ut magasinet
            Raylib.DrawText($"{damage} damage", 1060, 645, 20, Color.Orange); // skriver ut magasinet
            Raylib.DrawText($"{bulletVelocity} velocity", 1060, 675, 20, Color.Orange); // skriver ut magasinet

            for (int i = 0; i < bullets.Count; i++) // körs får att hålla koll på reload av varje skott
            {
                bullets[i].CheckReload();
            }
        }

        public void LevelUp(Player player) // efter varje wave körs denna och du kan välja mellan uppgraderingar
        {
            // ett interface som låter spelaren välja mellan 4 olika uppgraderingar
            Vector2 mouse = Raylib.GetMousePosition();

            Rectangle healthChoice = new Rectangle(32, 300, 260, 260);
            Raylib.DrawRectangleRec(healthChoice, Color.Blank);
            Raylib.DrawTexture(health, 32, 300, Color.White);

            Rectangle magazineChoice = new Rectangle(324, 300, 260, 260);
            Raylib.DrawRectangleRec(magazineChoice, Color.Blank);
            Raylib.DrawTexture(ammo, 324, 300, Color.White);

            Rectangle damageChoice = new Rectangle(616, 300, 260, 260);
            Raylib.DrawRectangleRec(damageChoice, Color.Blank);
            Raylib.DrawTexture(dmg, 616, 300, Color.White);

            Rectangle velocityChoice = new Rectangle(908, 300, 260, 260);
            Raylib.DrawRectangleRec(velocityChoice, Color.Blank);
            Raylib.DrawTexture(speed, 908, 300, Color.White);

            if (Raylib.CheckCollisionPointRec(mouse, healthChoice))
            {
                Raylib.DrawTexture(healthHover, 32, 300, Color.White);
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    hitpoints += 20;
                    currentHitpoints += 20;
                    Wave_system.NewWave(player);
                }
            }

            if (magazineSize < 6)
            {
                if (Raylib.CheckCollisionPointRec(mouse, magazineChoice))
                {
                    Raylib.DrawTexture(ammoHover, 324, 300, Color.White);

                    if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                    {
                        Bullet newBullet = new Bullet()
                        {
                            ready = true,
                            inAir = false
                        };
                        bullets.Add(newBullet);
                        magazineSize++;
                        Wave_system.NewWave(player);
                    }
                }
            }
            else
            {
                Raylib.DrawTexture(ammoMax, 324, 300, Color.White);
            }

            if (damage != 15)
            {
                if (Raylib.CheckCollisionPointRec(mouse, damageChoice))
                {
                    Raylib.DrawTexture(dmgHover, 616, 300, Color.White);

                    if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                    {
                        damage += 2;
                        Wave_system.NewWave(player);
                    }
                }
            }
            else
            {
                Raylib.DrawTexture(dmgMax, 616, 300, Color.White);
            }

            if (bulletVelocity != 15)
            {
                if (Raylib.CheckCollisionPointRec(mouse, velocityChoice))
                {
                    Raylib.DrawTexture(speedHover, 908, 300, Color.White);

                    if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                    {
                        playerSpeed += 1;
                        bulletVelocity += 2.5f;
                        Wave_system.NewWave(player);
                    }
                }
            }
            else
            {
                Raylib.DrawTexture(speedMax, 908, 300, Color.White);
            }
        }
    }
}