namespace SectorInvader
{
    using Raylib_cs;

    public class Cataphract : Enemy
    {
        public Cataphract(int startPosition, bool dir)
        {
            alive = true;
            difficultyPoints = 50;
            Hitpoints = 250;
            speedY = (float)Random.Shared.Next(18, 24) / 60;
            speedX = (float)Random.Shared.Next(16, 22) / 60;
            shipPositionY = -100;
            shipPositionX = startPosition;
            directionRight = dir;
            directionChangeTime = (float)Random.Shared.NextDouble() + Random.Shared.Next(2, 3);
            damage = 40;
            hitbox = new Rectangle(shipPositionY, shipPositionY, 118, 80);
            waveStart = 15;
        }

        public override void LoadTexture()
        {
            sprite = Raylib.LoadTexture(@"pictures/cataphract_elite_4.png"); //skapar en texture för skeppet
        }
    }
}