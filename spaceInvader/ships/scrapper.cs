namespace SectorInvader
{
    using Raylib_cs;

    public class Scrapper : Enemy
    {
        public Scrapper(int startPosition, bool dir)
        {
            alive = true;
            difficultyPoints = 1;
            Hitpoints = 5;
            speedY = (float)Random.Shared.Next(22, 28) / 60;
            speedX = (float)Random.Shared.Next(142, 158) / 60;
            shipPositionY = -100;
            shipPositionX = startPosition;
            directionRight = dir;
            directionChangeTime = (float)Random.Shared.NextDouble() + Random.Shared.Next(2, 6);
            damage = 3;
            hitbox = new Rectangle(shipPositionY, shipPositionY, 54, 46);
            waveStart = 1;
        }

        public override void LoadTexture()
        {
            sprite = Raylib.LoadTexture(@"pictures/scrapper_enemy_1.png"); //skapar en texture för skeppet
        }
    }
}