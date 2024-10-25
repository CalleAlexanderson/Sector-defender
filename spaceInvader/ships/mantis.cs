namespace SectorInvader
{
    using Raylib_cs;

    public class Mantis : Boss
    {
        public Mantis(int startPosition)
        {
            coreAlive = true;
            rightwingAlive = true;
            leftWingAlive = true;
            shieldAlive = true;
            difficultyPoints = 1000;

            Hitpoints = 40 * Wave_system.waveSizeMultiplier;
            LeftWingHitpoints = 30 * Wave_system.waveSizeMultiplier;
            RightWingHitpoints = 30 * Wave_system.waveSizeMultiplier;
            shieldHitpoints = 2;

            shipPositionY = 10;
            shipPositionX = startPosition + 260;
            rightWingPositionX = startPosition + 440;
            leftWingPositionX = startPosition;

            coreHitbox = new Rectangle(shipPositionX, shipPositionY, 180, 450);

            shieldHitbox = new Rectangle(shipPositionX, 520, 180, 20); //ändra sen

            rightwingHitbox = new Rectangle(rightWingPositionX, shipPositionY, 260, 215);
            rightwingHitbox2 = new Rectangle(rightWingPositionX, shipPositionY, 260, 215);

            leftWingHitbox = new Rectangle(leftWingPositionX, shipPositionY, 260, 215);
        }

        protected override void LeftWingAbility()
        {
            leftWingAbilityTimer += Raylib.GetFrameTime();
            if (leftWingAbilityTimer >= leftWingAbilityCooldown)
            {
                SummonShip(leftWingPositionX + 180);
                leftWingAbilityTimer = 0;
            }
        }

        protected override void RightWingAbility()
        {
            rightWingAbilityTimer += Raylib.GetFrameTime();
            if (rightWingAbilityTimer >= rightWingAbilityCooldown)
            {
                SummonShip(rightWingPositionX + 60);
                rightWingAbilityTimer = 0;
            }
        }

        protected override void DrawShield()
        {
            Raylib.DrawTexture(shieldSprite, (int)shipPositionX, 380, Color.White); //lägger ut bild för shield
        }

        //gör en override här så shieldhitpoints går ner när vingen dör
        protected override void CheckForShotLeftWing(Player playerShip) // kollar om vänster vinge träffas
        {
            for (int i = 0; i < playerShip.bullets.Count; i++) // kollar varje skott spelaren har
            {
                if (Raylib.CheckCollisionRecs(playerShip.bullets[i].shot, leftWingHitbox)) //om ett skott kolliderar med hitbox tar skeppet skada och skottet kör funktionen hit som tar bort skottet
                {
                    LeftWingHitpoints -= playerShip.GetDamage();
                    Raylib.DrawTexture(leftWingsprite, leftWingPositionX, (int)shipPositionY, Color.Red); //lägger ut bild för skepp med röd färg när den tar skada
                    playerShip.bullets[i].Hit();
                }
            }
            if (LeftWingHitpoints <= 0) // när hitpoints sjunkit till 0 eller under 0 dör skeppet
            {
                leftWingAlive = false;
                shieldHitpoints--;
            }
        }
        
        public override void LoadTexture()
        {
            sprite = Raylib.LoadTexture(@"pictures/mantis_boss_2.png"); //skapar en texture för skeppet
            rightWingsprite = Raylib.LoadTexture(@"pictures/mantis_boss_2_right_wing.png"); //skapar en texture för skeppet
            leftWingsprite = Raylib.LoadTexture(@"pictures/mantis_boss_2_left_wing.png"); //skapar en texture för skeppet
            shieldSprite = Raylib.LoadTexture(@"pictures/mantis_shield.png"); //skapar en texture för skeppet
        }
    }
    
}