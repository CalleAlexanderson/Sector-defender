namespace SectorInvader
{
    using Raylib_cs;

    public class Seeker : Enemy
    {
        public Seeker(int startPosition, bool dir)
        {
            alive = true;
            difficultyPoints = 5;
            Hitpoints = 30;
            speedY = (float)Random.Shared.Next(62, 70) / 60;
            speedX = (float)Random.Shared.Next(146, 154) / 60;
            shipPositionY = -100;
            shipPositionX = startPosition;
            directionRight = dir;
            directionChangeTime = (float)Random.Shared.NextDouble() + Random.Shared.Next(2, 5);
            damage = 6;
            hitbox = new Rectangle(shipPositionY, shipPositionY, 62, 64);
            waveStart = 11;
        }

        public override void LoadTexture()
        {
            sprite = Raylib.LoadTexture(@"pictures/seeker_enemy_8.png"); //skapar en texture för skeppet
        }
    }
}