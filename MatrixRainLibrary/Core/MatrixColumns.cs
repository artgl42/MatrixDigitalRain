using System;
using System.Threading;

namespace MatrixDigitalRain.Core
{
    class MatrixColumns
    {
        internal void RunDigitalColumns()
        {
            for (int j = 0; j < Console.WindowHeight - 1; j++)
            {
                for (int i = 0; i < Console.WindowWidth - 1; i++)
                {
                    if (i % 9 == 0)
                    {
                        Console.SetCursorPosition(i, j);
                        Console.Write(' ');
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(i, j + 1);
                        Console.Write(Matrix.random.Next(10));

                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.SetCursorPosition(i, j);
                        Console.Write(Matrix.random.Next(10));
                    }
                }
            }
            Thread.Sleep(1000);
        }

        internal void RunSymbolColumns()
        {
            for (int j = 0; j < Console.WindowHeight - 1; j++)
            {
                for (int i = 0; i < Console.WindowWidth - 1; i++)
                {
                    if (i % 9 == 0)
                    {
                        Console.SetCursorPosition(i, j);
                        Console.Write(' ');
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(i, j + 1);
                        Console.Write(Matrix.GetRandomChar);

                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.SetCursorPosition(i, j);
                        Console.Write(Matrix.GetRandomChar);
                    }
                }
            }
            Thread.Sleep(1000);
        }

        internal void ShowTextAscii(string[,] textASCII)
        {
            byte _flashCount = 0;

            if (Console.WindowWidth > textASCII[0, 0].Length)
            {
                do
                {
                    for (int k = 0; k < 2; k++)
                    {
                        switch (k)
                        {
                            case 0:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                break;
                            case 1:
                                Console.ForegroundColor = ConsoleColor.Red;
                                break;
                        }
                        for (int i = 0; i < textASCII.GetLength(0); i++)
                        {
                            for (int j = 0; j < textASCII.GetLength(1); j++)
                            {
                                Console.SetCursorPosition(Console.WindowWidth / 2 - textASCII[0, 0].Length / 2, Console.WindowHeight / 2 - textASCII.GetLength(0) / 2 + i);
                                Console.WriteLine(textASCII[i, j]);
                            }
                        }
                        Thread.Sleep(500);
                    }
                    _flashCount++;
                } while (_flashCount != 3);
                _flashCount = 0;
            }
            Thread.Sleep(500);
        }
    }
}
