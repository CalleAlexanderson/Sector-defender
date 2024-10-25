namespace SectorInvader
{
    using Raylib_cs;
    public class Boss : Enemy
    {

        protected bool coreAlive = false;
        protected bool shieldAlive = false;
        protected bool rightwingAlive = false;
        protected bool leftWingAlive = false;
        private int rightWingHitpoints; //totala hitpoints
        private int leftWingHitpoints; //totala hitpoints

        public static List<Summon> summonedShips = new List<Summon>();

        protected Rectangle coreHitbox; //hitbox för skeppets core 
        protected Rectangle shieldHitbox; //hitbox för skeppets shield 
        protected Rectangle rightwingHitbox; //hitbox för skeppets högra vinge
        protected Rectangle rightwingHitbox2; //hitbox för skeppets högra vinge
        protected Rectangle leftWingHitbox; //hitbox för skeppets vänstra vinge  
        protected Texture2D rightWingsprite; 
        protected Texture2D leftWingsprite; 
        protected Texture2D shieldSprite; // bild för shield
        protected int rightWingPositionX; // rightWing position i X axeln
        protected int leftWingPositionX; // leftWing position i X axeln
        protected int shieldPositionX; // shields position i Y axeln
        protected int shieldPositionY; // shields position i X axeln
        protected int shieldHitpoints;

        protected float rightWingAbilityCooldown = 2;
        protected float rightWingAbilityTimer = 0;
        protected float leftWingAbilityCooldown = 2;
        protected float leftWingAbilityTimer = 0;

        protected int RightWingHitpoints // nuvarande hitpoints höger vinge
        {
            get
            {
                return rightWingHitpoints;
            }
            set
            {
                rightWingHitpoints = Math.Max(value, 0);
            }
        }

        protected int LeftWingHitpoints // nuvarande hitpoints på vänster vinge
        {
            get
            {
                return leftWingHitpoints;
            }
            set
            {
                leftWingHitpoints = Math.Max(value, 0);
            }
        }


        public override void DrawShip(Player player)
        {
            if (coreAlive)
            {
                Raylib.DrawRectangleRec(coreHitbox, Color.Blank); // hitbox för skeppets core
                Raylib.DrawRectangleRec(rightwingHitbox, Color.Blank); // hitbox för skeppets högra vinge
                Raylib.DrawRectangleRec(rightwingHitbox2, Color.Blank); // hitbox för skeppets högra vinge på boss 1, på boss 2 är det samma som vanliga rightwing hitbox
                Raylib.DrawRectangleRec(leftWingHitbox, Color.Blank); // hitbox för skeppets vänstra vinge
                Raylib.DrawTexture(sprite, leftWingPositionX, (int)shipPositionY, Color.White); //lägger ut bild för skepp
                CheckForShotCore(player);
                DrawSummonedShips(player);
            }
            if (shieldAlive)
            {
                Raylib.DrawRectangleRec(shieldHitbox, Color.Blank); // hitbox för sköld som är aktiv innan core kan skjutas
                DrawShield();
                CheckForShotShield(player);
            }
            if (rightwingAlive)
            {
                RightWingAbility();
                CheckForShotRightWing(player);
            }
            if (leftWingAlive)
            {
                LeftWingAbility();
                CheckForShotLeftWing(player);
            }
        }

        protected virtual void RightWingAbility()
        {
        }
        protected virtual void LeftWingAbility()
        {
        }

        protected virtual void DrawShield()
        {
        }

        protected void SummonShip(int pos) // lägger till ett skepp till listan med summoned ships
        {
            bool[] direction = [true, false];
            Summon summon = new Summon(pos, Random.Shared.Next(200, 1000), direction[Random.Shared.Next(0, 2)]);
            summon.LoadTexture();
            summonedShips.Add(summon);
        }

        private void DrawSummonedShips(Player player) // ritar ut alla skepp i listan summoned ships
        {
            for (int i = 0; i < summonedShips.Count; i++)
            {
                if (summonedShips[i].GetAlive()) // kollar om skeppet fortfarande lever annars tas det bort från listan
                {
                    if (summonedShips[i].inPosition) // kollar om summon rört sig till sin startposition
                    {

                        summonedShips[i].DrawShip(player);
                    }
                    else
                    {
                        summonedShips[i].MoveToStartPosition();
                    }

                }
                else
                {
                    summonedShips.RemoveAt(i);
                    i--; // gör så att skepp inte skippas i summoned ships när ett skepp tas bort
                }
            }
        }

        private void CheckForShotRightWing(Player playerShip) // kollar om höger vinge träffas
        {

            for (int i = 0; i < playerShip.bullets.Count; i++) // kollar varje skott spelaren har
            {
                if (Raylib.CheckCollisionRecs(playerShip.bullets[i].shot, rightwingHitbox) || Raylib.CheckCollisionRecs(playerShip.bullets[i].shot, rightwingHitbox2)) //om ett skott kolliderar med hitbox tar skeppet skada och skottet kör funktionen hit som tar bort skottet
                {
                    RightWingHitpoints -= playerShip.GetDamage();
                    Raylib.DrawTexture(rightWingsprite, rightWingPositionX, (int)shipPositionY, Color.Red);
                    playerShip.bullets[i].Hit();
                }
            }
            if (RightWingHitpoints <= 0) // när hitpoints sjunkit till 0 eller under 0 dör skeppet
            {
                rightwingAlive = false;
                shieldHitpoints--;
            }
        }

        protected virtual void CheckForShotLeftWing(Player playerShip) // kollar om vänster vinge träffas
        {
            for (int i = 0; i < playerShip.bullets.Count; i++) // kollar varje skott spelaren har
            {
                if (Raylib.CheckCollisionRecs(playerShip.bullets[i].shot, leftWingHitbox)) //om ett skott kolliderar med hitbox tar skeppet skada och skottet kör funktionen hit som tar bort skottet
                {
                    LeftWingHitpoints -= playerShip.GetDamage();
                    Raylib.DrawTexture(leftWingsprite, leftWingPositionX, (int)shipPositionY, Color.Red); //lägger ut bild för skepp med röd färg när den tar skada
                    playerShip.bullets[i].Hit();
                }
            }
            if (LeftWingHitpoints <= 0) // när hitpoints sjunkit till 0 eller under 0 dör skeppet
            {
                leftWingAlive = false;
            }
        }

        private void CheckForShotShield(Player playerShip) // kollar om shield träffas
        {
            for (int i = 0; i < playerShip.bullets.Count; i++) // kollar varje skott spelaren har
            {
                if (Raylib.CheckCollisionRecs(playerShip.bullets[i].shot, shieldHitbox)) //om ett skott kolliderar med hitbox tar skeppet skada och skottet kör funktionen hit som tar bort skottet
                {
                    playerShip.bullets[i].Hit();
                }
            }
            if (shieldHitpoints <= 0)
            {
                shieldAlive = false;
            }
        }

        private void CheckForShotCore(Player playerShip) // kollar som core träffas
        {
            for (int i = 0; i < playerShip.bullets.Count; i++) // kollar varje skott spelaren har
            {
                if (Raylib.CheckCollisionRecs(playerShip.bullets[i].shot, coreHitbox)) //om ett skott kolliderar med hitbox tar skeppet skada och skottet kör funktionen hit som tar bort skottet
                {
                    Hitpoints -= playerShip.GetDamage();
                    Raylib.DrawTexture(sprite, leftWingPositionX, (int)shipPositionY, Color.Red); //lägger ut bild för skepp med röd färg när den tar skada
                    playerShip.bullets[i].Hit();
                }

                if ((Raylib.CheckCollisionRecs(playerShip.bullets[i].shot, rightwingHitbox) && !rightwingAlive) || (Raylib.CheckCollisionRecs(playerShip.bullets[i].shot, rightwingHitbox2) && !rightwingAlive)) // tar bort skott som kolliderar med vingar även efter de dör
                {
                    playerShip.bullets[i].Hit();
                }

                if (Raylib.CheckCollisionRecs(playerShip.bullets[i].shot, leftWingHitbox) && !leftWingAlive)
                {
                    playerShip.bullets[i].Hit();
                }
            }
            if (Hitpoints <= 0) // när hitpoints sjunkit till 0 eller under 0 dör skeppet
            {
                coreAlive = false;
                Arena.RaiseScore(difficultyPoints * Wave_system.waveSizeMultiplier);
                Wave_system.waveSizeTracker -= difficultyPoints; // tar bort skeppet från waven
            }
        }
    }
}