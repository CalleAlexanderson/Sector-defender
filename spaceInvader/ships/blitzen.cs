namespace SectorInvader
{
    using Raylib_cs;

    public class Blitzen : Enemy
    {
        public Blitzen(int startPosition, bool dir)
        {
            alive = true;
            difficultyPoints = 5;
            Hitpoints = 25;
            speedY = (float)Random.Shared.Next(50, 56) / 60;
            speedX = (float)Random.Shared.Next(98, 112) / 60;
            shipPositionY = -100;
            shipPositionX = startPosition;
            directionRight = dir;
            directionChangeTime = (float)Random.Shared.NextDouble() + Random.Shared.Next(4, 5);
            damage = 5;
            hitbox = new Rectangle(shipPositionY, shipPositionY, 86, 40);
            waveStart = 6;
        }

        public override void LoadTexture()
        {
            sprite = Raylib.LoadTexture(@"pictures/blitzen_enemy_5.png"); //skapar en texture för skeppet
        }
    }
}