using System;
using System.Threading;

namespace MatrixDigitalRain.Core
{
    class MatrixQuotes
    {
        char[] Quote { get; set; }

        internal void ShowIntroQuote(byte countQuotes)
        {
            Console.Clear();
            Console.CursorVisible = true;
            for (int i = 0; i < countQuotes; i++)
            {
                Quote = GetQuote(i);
                for (int j = 0; j < Quote.Length; j++)
                {
                    Console.SetCursorPosition(15 + j, 10);
                    Thread.Sleep(100);
                    Console.ForegroundColor = Matrix.ForegroundColor;
                    Console.Write(Quote[j]);
                }
                Thread.Sleep(2000);
            }
            Console.CursorVisible = false;
        }

        internal void ShowRandomQuote()
        {
            if (Matrix.Counter == Matrix.ShowQuoteAfter && Matrix.ShowQuoteAfter > 0)
            {
                Quote = GetQuote(Matrix.random.Next(0, 8));
                Console.SetCursorPosition(Matrix.random.Next(Console.WindowWidth),
                                          Matrix.random.Next(Console.WindowHeight));
                Console.Write(' ');
                for (int j = 0; j < Quote.Length; j++)
                {
                    Thread.Sleep(70);
                    Console.ForegroundColor = Matrix.QuoteColor;
                    Console.Write(Quote[j]);
                }
                Console.Write(' ');
            }
        }

        char[] GetQuote(int value)
        {
            char[] _quote = new char[0];

            switch (value)
            {
                case 0:
                    _quote = new char[] { 'W', 'a', 'k', 'e', ' ', 'u', 'p', ',', ' ', 'N', 'e', 'o', '.', '.', '.' };
                    break;
                case 1:
                    _quote = new char[] { 'T', 'h', 'e', ' ', 'M', 'a', 't', 'r', 'i', 'x', ' ', 'h', 'a', 's', ' ', 'y', 'o', 'u', '.', '.', '.' };
                    break;
                case 2:
                    _quote = new char[] { 'F', 'o', 'l', 'l', 'o', 'w', ' ', 't', 'h', 'e', ' ', 'w', 'h', 'i', 't', 'e', ' ', 'r', 'a', 'b', 'b', 'i', 't', '.' };
                    break;
                case 3:
                    _quote = new char[] { 'K', 'n', 'o', 'c', 'k', ',', ' ', 'k', 'n', 'o', 'c', 'k', ',', ' ', 'N', 'e', 'o', '.', ' ', ' ', ' ', ' ' };
                    break;
                case 4:
                    _quote = new char[] { 'T', 'i', 'm', 'e', ' ', 'i', 's', ' ', 'a', 'l', 'w', 'a', 'y', 's', ' ', 'a', 'g', 'a', 'i', 'n', 's', 't', ' ', 'u', 's', '.' };
                    break;
                case 5:
                    _quote = new char[] { 'W', 'e', 'l', 'c', 'o', 'm', 'e', ' ', 't', 'o', ' ', 't', 'h', 'e', ' ', 'r', 'e', 'a', 'l', ' ', 'w', 'o', 'r', 'l', 'd', '.' };
                    break;
                case 6:
                    _quote = new char[] { 'W', 'h', 'a', 't', ' ', 'i', 's', ' ', 't', 'h', 'e', ' ', 'M', 'a', 't', 'r', 'i', 'x', '?', ' ', 'C', 'o', 'n', 't', 'r', 'o', 'l', '.' };
                    break;
                case 7:
                    _quote = new char[] { 'Y', 'o', 'u', 'r', ' ', 'm', 'i', 'n', 'd', ' ', 'm', 'a', 'k', 'e', 's', ' ', 'i', 't', ' ', 'r', 'e', 'a', 'l', '.' };
                    break;
            }
            return _quote;
        }
    }
}