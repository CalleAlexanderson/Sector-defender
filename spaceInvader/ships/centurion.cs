namespace SectorInvader
{
    using Raylib_cs;

    public class Centurion : Enemy
    {
        public Centurion(int startPosition, bool dir)
        {
            alive = true;
            difficultyPoints = 40;
            Hitpoints = 150;
            speedY = (float)Random.Shared.Next(42, 46) / 60;
            speedX = (float)Random.Shared.Next(112, 116) / 60;
            shipPositionY = -100;
            shipPositionX = startPosition;
            directionRight = dir;
            directionChangeTime = (float)Random.Shared.NextDouble() + Random.Shared.Next(4, 5);
            damage = 25;
            hitbox = new Rectangle(shipPositionY, shipPositionY, 76, 128);
            waveStart = 12;
        }

        public override void LoadTexture()
        {
            sprite = Raylib.LoadTexture(@"pictures/centurion_elite_3.png"); //skapar en texture för skeppet
        }
    }
}