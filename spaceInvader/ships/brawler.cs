namespace SectorInvader
{
    using Raylib_cs;

    public class Brawler : Enemy
    {
        public Brawler(int startPosition, bool dir)
        {
            alive = true;
            difficultyPoints = 10;
            Hitpoints = 80;
            speedY = (float)Random.Shared.Next(26, 32) / 60;
            speedX = (float)Random.Shared.Next(46, 62) / 60;
            shipPositionY = -100;
            shipPositionX = startPosition;
            directionRight = dir;
            directionChangeTime = (float)Random.Shared.NextDouble() + Random.Shared.Next(3, 5);
            damage = 15;
            hitbox = new Rectangle(shipPositionY, shipPositionY, 100, 66);
            waveStart = 13;
        }

        public override void LoadTexture()
        {
            sprite = Raylib.LoadTexture(@"pictures/brawler_enemy_9.png"); //skapar en texture för skeppet
        }
    }
}