namespace SectorInvader
{
    static public class Enemy_maker
    {
        public static Enemy CreateShip(string ship, int position)
        {
            bool[] direction = [true, false];
            return ship switch
            {
                "blitzen" => new Blitzen(position, direction[Random.Shared.Next(0, 2)]),
                "brawler" => new Brawler(position, direction[Random.Shared.Next(0, 2)]),
                "buster" => new Buster(position, direction[Random.Shared.Next(0, 2)]),
                "cataphract" => new Cataphract(position, direction[Random.Shared.Next(0, 2)]),
                "centurion" => new Centurion(position, direction[Random.Shared.Next(0, 2)]),
                "dasher" => new Dasher(position, direction[Random.Shared.Next(0, 2)]),
                "destroyer" => new Destroyer(position, direction[Random.Shared.Next(0, 2)]),
                "hunter" => new Hunter(position, direction[Random.Shared.Next(0, 2)]),
                "nautilus" => new Nautilus(position, direction[Random.Shared.Next(0, 2)]),
                "ram" => new Ram(position, direction[Random.Shared.Next(0, 2)]),
                "seeker" => new Seeker(position, direction[Random.Shared.Next(0, 2)]),
                "wasp" => new Wasp(position, direction[Random.Shared.Next(0, 2)]),
                "reaper" => new Reaper(position),
                "mantis" => new Mantis(position),
                _ => new Scrapper(position, direction[Random.Shared.Next(0, 2)]),
            };
        }
    }
}