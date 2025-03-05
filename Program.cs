using System.Drawing;
using System.Runtime.CompilerServices;

double treat_cooldown = 2.0;

int horizontal_space = 0;
int vertical_space = 0;

int player_point = 0;

List<Tuple<int, int>> treats = new List<Tuple<int, int>>();

Random rnd = new Random();

DateTime last_updt_time = DateTime.Now;
DateTime last_keypress_time = DateTime.Now;

while (true) {
    Console.CursorVisible = false;

    if ((DateTime.Now - last_updt_time).TotalSeconds >= 1) {
        treat_cooldown--;
        if (treat_cooldown <= 0) {
            spawnTreats(rnd.Next(1, 3));
            treat_cooldown = rnd.Next(5 / 10, 5);
        }
        last_updt_time = DateTime.Now;
    }

    for (var i = 0; i < treats.Count; i++) {
        if (horizontal_space == treats[i].Item1 && vertical_space == treats[i].Item2) {
            player_point++;
            treats.RemoveAt(i);
            break;
        }
    }

    Console.Clear();
    Console.Title = $"{player_point}";

    for (int i = 0; i < horizontal_space; i++) {
        Console.Write(" ");
    }
    for (int i = 0; i < vertical_space; i++) {
        Console.WriteLine();
    }
    Console.SetCursorPosition(horizontal_space, vertical_space);
    Console.Write("O");

    foreach (var treat in treats) {
        Console.SetCursorPosition(treat.Item1, treat.Item2);
        Console.Write("•");
    }

    if (Console.KeyAvailable) {
        ConsoleKeyInfo key_pressed = Console.ReadKey();

        switch (key_pressed.Key) {
            case ConsoleKey.A:
                if (horizontal_space == 0)
                    horizontal_space = Console.WindowWidth - 1;
                horizontal_space--;
                break;
            case ConsoleKey.D:
                if (horizontal_space == Console.WindowWidth - 1)
                    horizontal_space = 0;
                horizontal_space++;
                break;
            case ConsoleKey.W:
                if (vertical_space == 0)
                    vertical_space = Console.WindowHeight - 1;
                vertical_space--;
                break;
            case ConsoleKey.S:
                if (vertical_space == Console.WindowHeight - 1)
                    vertical_space = 0;
                vertical_space++;
                break;
        }
    }
    Thread.Sleep(10);
}

void spawnTreats(int count) {
    for (int i = 0; i < count; i++) {
        int x = rnd.Next(0, Console.WindowWidth);
        int y = rnd.Next(0, Console.WindowHeight);
        treats.Add(new Tuple<int, int>(x, y));
    }
}