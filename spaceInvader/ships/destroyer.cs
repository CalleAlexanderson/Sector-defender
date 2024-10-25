namespace SectorInvader
{
    using Raylib_cs;

    public class Destroyer : Enemy
    {
        public Destroyer(int startPosition, bool dir)
        {
            alive = true;
            difficultyPoints = 30;
            Hitpoints = 180;
            speedY = (float)Random.Shared.Next(28, 32) / 60;
            speedX = (float)Random.Shared.Next(78, 86) / 60;
            shipPositionY = -100;
            shipPositionX = startPosition;
            directionRight = dir;
            directionChangeTime = (float)Random.Shared.NextDouble() + Random.Shared.Next(3, 4);
            damage = 24;
            hitbox = new Rectangle(shipPositionY, shipPositionY, 82, 84);
            waveStart = 8;
        }

        public override void LoadTexture()
        {
            sprite = Raylib.LoadTexture(@"pictures/destroyer_elite_2.png"); //skapar en texture för skeppet
        }
    }
}