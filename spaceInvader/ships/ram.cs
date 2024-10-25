namespace SectorInvader
{
    using Raylib_cs;

    public class Ram : Enemy
    {
        public Ram(int startPosition, bool dir)
        {
            alive = true;
            difficultyPoints = 3;
            Hitpoints = 20;
            speedY = (float)Random.Shared.Next(36, 44) / 60;
            speedX = (float)Random.Shared.Next(64, 82) / 60;
            shipPositionY = -100;
            shipPositionX = startPosition;
            directionRight = dir;
            directionChangeTime = (float)Random.Shared.NextDouble() + Random.Shared.Next(1, 3);
            damage = 5;
            hitbox = new Rectangle(shipPositionY, shipPositionY, 62, 46);
            waveStart = 2;
        }

        public override void LoadTexture()
        {
            sprite = Raylib.LoadTexture(@"pictures/ram_enemy_2.png"); //skapar en texture för skeppet
        }
    }
}