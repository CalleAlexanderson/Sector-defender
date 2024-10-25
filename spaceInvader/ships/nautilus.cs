namespace SectorInvader
{
    using Raylib_cs;

    public class Nautilus : Enemy
    {
        public Nautilus(int startPosition, bool dir)
        {
            alive = true;
            difficultyPoints = 5;
            Hitpoints = 85;
            speedY = (float)Random.Shared.Next(16, 22) / 60;
            speedX = (float)Random.Shared.Next(22, 28) / 60;
            shipPositionY = -100;
            shipPositionX = startPosition;
            directionRight = dir;
            directionChangeTime = (float)Random.Shared.NextDouble() + Random.Shared.Next(4, 8);
            damage = 10;
            hitbox = new Rectangle(shipPositionY, shipPositionY, 88, 50);
            waveStart = 4;
        }

        public override void LoadTexture()
        {
            sprite = Raylib.LoadTexture(@"pictures/nautlius_enemy_3.png"); //skapar en texture för skeppet
        }
    }
}