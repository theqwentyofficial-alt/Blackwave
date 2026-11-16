namespace pcdatacollector
{
    public static class Logger
    {
        public enum LVL : int
        {
            LOW = 0,
            MEDIUM = 1,
            HIGH = 2
        }
        public static void Log(LVL riskLvl, string msg)
        {
             
            switch (riskLvl) {

                case LVL.LOW:
                {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"RISK: [LOW] => {msg}");
                        Console.ForegroundColor = ConsoleColor.White;
                }
                break;
                case LVL.MEDIUM:
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"RISK: [MEDIUM] => {msg}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    break;
                case LVL.HIGH:
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"RISK: [HIGH] => {msg}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    break;

            }


        }
    }
}
