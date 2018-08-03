using System;
using System.Collections.Generic;
using System.Linq;

namespace PiorJogoDoMundo.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            var jogadorX = 1;
            var jogadorY = 1;

            var matriz = new char[10, 20] {
                {'x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x'},
                {'x',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','x'},
                {'x',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','x'},
                {'x',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','x'},
                {'x',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','x'},
                {'x',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','x'},
                {'x',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','x'},
                {'x',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','x'},
                {'x',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','x'},
                {'x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x','x'}
            };

            matriz.SetValue('o', jogadorY, jogadorX);

            DesenharMatriz(matriz);

            ConsoleKey inputUsuario;

            do
            {
                inputUsuario = Console.ReadKey(true).Key;

                matriz.SetValue(' ', jogadorY, jogadorX);

                if (inputUsuario == ConsoleKey.DownArrow) jogadorY += 1;
                if (inputUsuario == ConsoleKey.UpArrow) jogadorY -= 1;
                if (inputUsuario == ConsoleKey.RightArrow) jogadorX += 1;
                if (inputUsuario == ConsoleKey.LeftArrow) jogadorX -= 1;

                jogadorX = Math.Clamp(jogadorX, 1, 18);
                jogadorY = Math.Clamp(jogadorY, 1, 8);

                matriz.SetValue('o', jogadorY, jogadorX);

                Console.SetCursorPosition(0, Console.CursorTop - 10);
                DesenharMatriz(matriz);

            } while (inputUsuario != ConsoleKey.Escape);
        }

        private static void DesenharMatriz(char[,] matriz)
        {
            for (var y = 0; y < 10; y++)
            {
                for (var x = 0; x < 20; x++)
                    Console.Write(matriz[y, x]);
                Console.WriteLine();
            }
        }
    }
}