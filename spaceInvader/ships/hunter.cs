namespace SectorInvader
{
    using Raylib_cs;

    public class Hunter : Enemy
    {
        public Hunter(int startPosition, bool dir)
        {
            alive = true;
            difficultyPoints = 3;
            Hitpoints = 15;
            speedY = (float)Random.Shared.Next(44, 50) / 60;
            speedX = (float)Random.Shared.Next(74, 96) / 60;
            shipPositionY = -100;
            shipPositionX = startPosition;
            directionRight = dir;
            directionChangeTime = (float)Random.Shared.NextDouble() + Random.Shared.Next(4, 5);
            damage = 10;
            hitbox = new Rectangle(shipPositionY, shipPositionY, 74, 46);
            waveStart = 7;
        }

        public override void LoadTexture()
        {
            sprite = Raylib.LoadTexture(@"pictures/hunter_enemy_6.png"); //skapar en texture för skeppet
        }
    }
}