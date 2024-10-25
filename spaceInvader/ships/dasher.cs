namespace SectorInvader
{
    using Raylib_cs;

    public class Dasher : Enemy
    {
        public Dasher(int startPosition, bool dir)
        {
            alive = true;
            difficultyPoints = 5;
            Hitpoints = 25;
            speedY = (float)Random.Shared.Next(24, 28) / 60;
            speedX = (float)Random.Shared.Next(198, 206) / 60;
            shipPositionY = -100;
            shipPositionX = startPosition;
            directionRight = dir;
            directionChangeTime = (float)Random.Shared.NextDouble() + Random.Shared.Next(2, 3);
            damage = 5;
            hitbox = new Rectangle(shipPositionY, shipPositionY, 86, 40);
            waveStart = 6;
        }

        public override void LoadTexture()
        {
            sprite = Raylib.LoadTexture(@"pictures/dasher_enemy_4.png"); //skapar en texture för skeppet
        }
    }
}