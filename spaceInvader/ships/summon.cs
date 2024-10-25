namespace SectorInvader
{
    using Raylib_cs;

    public class Summon : Enemy
    {
        public bool inPosition = false;
        private float positionDiff;
        public Summon(int startPosition, int pos, bool dir)
        {
            alive = true;
            difficultyPoints = 0;
            Hitpoints = 12;
            speedY = (float)Random.Shared.Next(26, 34) / 60;
            speedX = (float)Random.Shared.Next(142, 158) / 60;
            shipPositionY = 40;
            shipPositionX = startPosition;
            directionRight = dir;
            positionDiff = pos - startPosition;
            directionChangeTime = (float)Random.Shared.NextDouble() + Random.Shared.Next(2, 3);
            damage = 2;
            hitbox = new Rectangle(shipPositionY, shipPositionY, 48, 46);
        }

        public void MoveToStartPosition()
        { // gör så skeppet åker ut från hangaren till en annan position

            Raylib.DrawRectangleRec(hitbox, Color.Blank); // används för hitbox, color blank för att den inte ska synas
            Raylib.DrawTexture(sprite, (int)shipPositionX, (int)shipPositionY, Color.White); //lägger ut bild för skepp
            if (shipPositionY >= 0)
            {
                shipPositionX += positionDiff / 120; // på två sekunder åker skeppet till dens startposition
                shipPositionY -= 0.33334f; // två sekunder åker skeppet till y 0
            }
            else
            {
                inPosition = true;
            }
        }

        public override void LoadTexture()
        {
            sprite = Raylib.LoadTexture(@"pictures/boss_summon.png"); //skapar en texture för skeppet
        }

        public bool GetAlive()
        {
            return alive;
        }
    }
}