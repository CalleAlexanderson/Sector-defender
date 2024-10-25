namespace SectorInvader
{
    using Raylib_cs;

    public class Reaper : Boss
    {
        public Reaper(int startPosition)
        {
            coreAlive = true;
            rightwingAlive = true;
            leftWingAlive = true;
            shieldAlive = true;
            difficultyPoints = 500;

            Hitpoints = 40 * Wave_system.waveSizeMultiplier;
            LeftWingHitpoints = 30 * Wave_system.waveSizeMultiplier;
            RightWingHitpoints = 20 * Wave_system.waveSizeMultiplier;
            shieldHitpoints = 1;

            shipPositionY = 15;
            shipPositionX = startPosition + 229;
            rightWingPositionX = startPosition + 349;
            leftWingPositionX = startPosition;

            coreHitbox = new Rectangle(shipPositionX, shipPositionY, 120, 210);

            shieldHitbox = new Rectangle(shipPositionX, 200, 120, 44); //ändra sen

            rightwingHitbox = new Rectangle(rightWingPositionX, shipPositionY, 229, 185);
            rightwingHitbox2 = new Rectangle(rightWingPositionX + 116, shipPositionY, 80, 280);

            leftWingHitbox = new Rectangle(leftWingPositionX, shipPositionY, 229, 185);
        }

        public override void LoadTexture()
        {
            sprite = Raylib.LoadTexture(@"pictures/reaper_boss_1.png"); //skapar en texture för skeppet
            rightWingsprite = Raylib.LoadTexture(@"pictures/reaper_boss_1_right_wing.png"); //skapar en texture för skeppet
            leftWingsprite = Raylib.LoadTexture(@"pictures/reaper_boss_1_left_wing.png"); //skapar en texture för skeppet
            shieldSprite = Raylib.LoadTexture(@"pictures/reaper_shield.png"); //skapar en texture för skeppet
        }

        protected override void LeftWingAbility()
        {
            leftWingAbilityTimer += Raylib.GetFrameTime();
            if (leftWingAbilityTimer >= leftWingAbilityCooldown)
            {
                SummonShip(leftWingPositionX + 120);
                leftWingAbilityTimer = 0;
            }
        }

        protected override void DrawShield()
        {
            Raylib.DrawTexture(shieldSprite, (int)shipPositionX, 200, Color.White); //lägger ut bild för shield
        }
    }
}