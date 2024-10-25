namespace SectorInvader
{
    using Raylib_cs;

    public class Wasp : Enemy
    {
        public Wasp(int startPosition, bool dir)
        {
            alive = true;
            difficultyPoints = 3;
            Hitpoints = 12;
            speedY = (float)Random.Shared.Next(78, 82) / 60;
            speedX = (float)Random.Shared.Next(122, 130) / 60;
            shipPositionY = -100;
            shipPositionX = startPosition;
            directionRight = dir;
            directionChangeTime = (float)Random.Shared.NextDouble() + Random.Shared.Next(3, 5);
            damage = 4;
            hitbox = new Rectangle(shipPositionY, shipPositionY, 45, 50);
            waveStart = 9;
        }

        public override void LoadTexture()
        {
            sprite = Raylib.LoadTexture(@"pictures/wasp_enemy_7.png"); //skapar en texture för skeppet
        }
    }
}