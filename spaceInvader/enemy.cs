namespace SectorInvader
{
    using Raylib_cs;

    public class Enemy
    {
        private int hitpoints; //totala hitpoints
        protected bool alive;
        protected bool directionRight; // håller koll på nuvarande håll skeppet åker i
        protected float speedY; //hur snabbt skeppter rör sig mot spelaren
        protected float speedX; // hur snabbt skeppet åker i sidled
        protected float shipPositionY; // skeppets position i Y axeln
        protected float shipPositionX; // skeppets position i X axeln
        protected float directionChangeTimer = 0; // timer som räknar tid för at byta håll
        protected float directionChangeTime; // när timer når detta byter skeppet håll
        protected Rectangle hitbox; //hitbox för skeppet 
        protected Texture2D sprite; //bild för skeppet
        protected int damage; // hur mycket skada som görs om skeppet når spelarens border
        protected int waveStart; // vilken wave skeppet först kan spawna
        protected int difficultyPoints; // används för att beräkna storleken på olika waves
        protected int Hitpoints // nuvarande hitpoints
        {
            get
            {
                return hitpoints;
            }
            set
            {
                hitpoints = value;
            }
        }

        public virtual void DrawShip(Player player)
        {
            if (alive)
            {
                Raylib.DrawRectangleRec(hitbox, Color.Blank); // används för hitbox, color blank för att den inte ska synas
                Raylib.DrawTexture(sprite, (int)shipPositionX, (int)shipPositionY, Color.White); //lägger ut bild för skepp
                Movement(player);
                CheckForShot(player);
            }
        }

        public int GetDifficultyPoints()
        {
            return difficultyPoints;
        }

        public int GetWaveStart()
        {
            return waveStart;
        }

        public virtual void LoadTexture()
        {
        }

        protected void CheckForShot(Player playerShip)
        {
            for (int i = 0; i < playerShip.bullets.Count; i++) // kollar varje skott spelaren har
            {
                if (Raylib.CheckCollisionRecs(playerShip.bullets[i].shot, hitbox)) //om ett skott kolliderar med hitbox tar skeppet skada och skottet kör funktionen hit som tar bort skottet
                {
                    Hitpoints -= playerShip.GetDamage();
                    Raylib.DrawTexture(sprite, (int)shipPositionX, (int)shipPositionY, Color.Red); //lägger ut bild för skepp med röd färg när den tar skada
                    playerShip.bullets[i].Hit();
                }
            }
            if (Hitpoints <= 0) // när hitpoints sjunkit till 0 eller under 0 dör skeppet
            {
                alive = false;
                Arena.RaiseScore(difficultyPoints * Wave_system.waveSizeMultiplier);
                Wave_system.waveSizeTracker -= difficultyPoints; // tar bort skeppet från waven
            }
        }

        protected void Movement(Player playerShip)
        {
            if (!Raylib.CheckCollisionRecs(hitbox, Arena.homeBorder)) // kollar om skeppet nått homeborder
            {
                directionChangeTimer += Raylib.GetFrameTime();
                if (directionChangeTimer >= directionChangeTime) //byter direction på en timer
                {
                    directionChangeTimer = 0;
                    directionRight = !directionRight;
                }

                if (directionRight) // kollar vilket håll skeppet åker mot
                {
                    if (!Raylib.CheckCollisionRecs(hitbox, Arena.borderEast)) //om skeppet krockar i en borderEast byter den direction
                    {
                        shipPositionX += speedX;
                    }
                    else
                    {
                        directionRight = !directionRight;
                    }
                }
                else
                {

                    if (!Raylib.CheckCollisionRecs(hitbox, Arena.borderWest)) //om skeppet krockar i en borderWest byter den direction
                    {
                        shipPositionX -= speedX;
                    }
                    else
                    {
                        directionRight = !directionRight;
                    }
                }
                shipPositionY += speedY;
                hitbox.Y = shipPositionY;
                hitbox.X = shipPositionX;
            }
            else // har skeppet nått homeborder "dör" det och player health går ner
            {
                alive = false;
                playerShip.ChangeHealth(damage);
                Wave_system.waveSizeTracker -= difficultyPoints; // tar bort skeppet från waven
            }
        }
    }
}