namespace SectorInvader
{
    using Raylib_cs;

    public class Buster : Enemy
    {
        public Buster(int startPosition, bool dir)
        {
            alive = true;
            difficultyPoints = 20;
            Hitpoints = 120;
            speedY = (float)Random.Shared.Next(34, 38) / 60;
            speedX = (float)Random.Shared.Next(54, 60) / 60;
            shipPositionY = -100;
            shipPositionX = startPosition;
            directionRight = dir;
            directionChangeTime = (float)Random.Shared.NextDouble() + Random.Shared.Next(1, 3);
            damage = 16;
            hitbox = new Rectangle(shipPositionY, shipPositionY, 80, 72);
            waveStart = 5;
        }

        public override void LoadTexture()
        {
            sprite = Raylib.LoadTexture(@"pictures/buster_elite_1.png"); //skapar en texture för skeppet
        }
    }
}