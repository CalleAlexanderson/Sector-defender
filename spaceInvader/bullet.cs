namespace SectorInvader
{
    using Raylib_cs;

    public class Bullet
    {
        public bool ready;
        public bool inAir;
        public Rectangle shot = new Rectangle(-1000, -1000, 5, 33); //projectilen som skjuts
        float reloadTime = 2f;
        float reloadTimer = 0; //timer som håller koll på reload time
        Texture2D bulletSprite = Raylib.LoadTexture(@"pictures/bullet.png"); //skapar en texture bullet;
        bool Rightshot;
        float velocity;

        public bool ShotReady() // kollar om skottet är redo
        {
            if (ready == true && inAir == false)
            {
                return true;
            }
            return false;
        }

        public void Fire(int x, int y, bool rightShot, float vel) // när skottet skjuts hämtas spelarens position
        {
            Rightshot = rightShot; // uppdaterar vilken blaster som skjuter skottet
            if (Rightshot) // ser till så skotten ändrar blaster
            {
                shot.X = x + 38;
            }
            else
            {
                shot.X = x + 22;
            }
            shot.Y = y - 26;
            ready = false;
            inAir = true;
            velocity = vel;
        }

        public void Hit()
        { //gör så skottet kan skjutas igen
            inAir = false;
            shot.Y = -1000; // sätter skotten out of bounds så de inte träffar skepp i arenan
            shot.X = -1000;
        }

        public void CheckReload()
        {
            if (ready == false) // ser till så reloadtimer bara ökas när skottet inte är redo
            {
                reloadTimer += Raylib.GetFrameTime();
            }

            if (reloadTimer >= reloadTime) // om reloadtimer är klar blir skottet redo
            {
                reloadTimer = 0;
                ready = true;
            }

            if (inAir)
            {
                Raylib.DrawRectangleRec(shot, Color.Blank);
                Raylib.DrawTexture(bulletSprite, (int)shot.X, (int)shot.Y, Color.White); //lägger ut bild för bullet på höger
                shot.Y -= velocity; // flyttas uppåt 10px per frame
                Arena.BorderCollisions(this);
            }
        }
    }
}