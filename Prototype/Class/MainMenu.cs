namespace Prototype
{
    public sealed class MainMenu
    {
        public void ShowMainMenu()
        {
            byte input = byte.MinValue;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("¤═══════════════════¤\n" +
                          "║   ¤ Main Menu ¤   ║\n" +
                          "║                   ║\n" +
                          "║    1- New Game    ║\n" +
                          "║    2- Load Game   ║\n" +
                          "║    3- Exit        ║\n" +
                          "║                   ║\n" +
                          "¤═══════════════════¤\n");
            Console.ResetColor();
            
            do { GetInput(ref input); }
            while (input < 1 || input > 3);

            switch (input)
            {
                case 1:
                    ShowNewGameMenu();
                    break;
                case 2:
                    ShowLoadGameMenu();
                    break;
                case 3:
                    Console.Clear();
                    break;
            }
        }

        public void ShowNewGameMenu()
        {
            byte input = byte.MinValue;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("¤═══════════════════¤\n" +
                          "║  ¤ Choose Class ¤ ║\n" +
                          "║                   ║\n" +
                          "║    1- Warrior     ║\n" +
                          "║    2- Archer      ║\n" +
                          "║    3- Sorcerer    ║\n" +
                          "║    4- Back        ║\n" +
                          "║                   ║\n" +
                          "¤═══════════════════¤\n");
            Console.ResetColor();

            do { GetInput(ref input); }
            while (input < 1 || input > 4);

            switch (input)
            {
                case 1:
                case 2:
                case 3: CreateNewCharacter((EClass)input); break;
                case 4:
                    ShowMainMenu();
                    break;
            }
        }

        public void ShowLoadGameMenu()
        {
            byte input = byte.MinValue;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("¤═══════════════════¤\n" +
                          "║     ¤ Load ¤      ║\n" +
                          "║                   ║\n" +
                          "║                   ║\n" +
                          "║       Empty       ║\n" +
                          "║                   ║\n" +
                          "║                   ║\n" +
                          "║     1: Back       ║\n" +
                          "║                   ║\n" +
                          "¤═══════════════════¤\n");
            Console.ResetColor();

            do { GetInput(ref input); }
            while (input < 1 || input > 1);
            // Temporary
            switch (input)
            {
                case 1:
                    ShowMainMenu();
                    break;
            }
        }

        private void GetInput(ref byte input)
        {
            Console.Write("Enter your choice: ");
            try 
            { 
                input = byte.Parse(Console.ReadLine());
            }
            catch { input = 0; }
        }

        private void CreateNewCharacter(EClass a_Class)
        {
            Console.Write("Enter character name: ");
            string name = Console.ReadLine();
            if (name == null)
                name = "Player";
            Console.Clear();

            Game.Instance?.CreateCharacter(name, a_Class);
        }
    }
}