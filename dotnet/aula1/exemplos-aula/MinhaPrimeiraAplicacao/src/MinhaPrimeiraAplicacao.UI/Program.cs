using System;
using System.Collections.Generic;
using System.Linq;

namespace MinhaPrimeiraAplicacao.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            String helloWorld = "Hello World!";

            string helloWorld2 = helloWorld;

            var helloWorld3 = helloWorld2;

            var meuNumero = 1;

            var variavelNull = 1;

            var meuDicionario = new Dictionary<string, List<CLSCompliantAttribute>>();

            var strings = new List<string> { "www.google.com", "www.netflix.com", "www.stackoverflow.com" };

            var uris = new List<Uri>();

            //foreach(string str in strings)
            foreach (var str in strings)
            {
                uris.Add(new Uri($"http://{str}"));
            }

            var uris2 = strings.Select(str => new Uri($"http://{str}")).ToList();

            var primeiroElemento = strings[0];
            var primeiroElemento1 = strings.FirstOrDefault();

            var matriz = new char[6, 5] {

                { ' ', ' ', ' ', ' ', ' ' },
                { ' ', ' ', ' ', ' ', ' ' },
                { ' ', ' ', 'o', ' ', ' ' },
                { ' ', ' ', ' ', ' ', ' ' },
                { ' ', ' ', ' ', ' ', ' ' },
                { ' ', ' ', ' ', ' ', ' ' },
            };

            matriz.SetValue('x', 3, 3);

            foreach (var elemento in matriz)
            {
                Console.Write(elemento);
            }

            Console.WriteLine(helloWorld);

            do
            {
                var teclaPressionada = Console.ReadKey(true).Key;

                Console.SetCursorPosition(0, Console.CursorTop);

                if (teclaPressionada == ConsoleKey.UpArrow) Console.WriteLine("Você apertou a seta para cima");
                if (teclaPressionada == ConsoleKey.DownArrow) Console.Write("Você apertou a seta para baixo");
                if (teclaPressionada == ConsoleKey.LeftArrow) Console.Write("Você apertou a seta para esquerda");
                if (teclaPressionada == ConsoleKey.RightArrow) Console.Write("Você apertou a seta para direita");

            } while (true);
        }
    }
}