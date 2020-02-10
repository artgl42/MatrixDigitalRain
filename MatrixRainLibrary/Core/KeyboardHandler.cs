using System;
using System.Threading.Tasks;

namespace MatrixDigitalRain.Core
{
    class KeyboardHandler
    {
        internal async void WaitPressedKey()
        {
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.F1:
                        DisplayTextToCenter(Matrix.TextHelp);
                        Console.ReadKey();
                        break;
                    case ConsoleKey.Spacebar:
                        DisplayTextToCenter(Matrix.TextPause);
                        Console.ReadKey();
                        break;
                    case ConsoleKey.Escape:
                        Matrix.SkipIntro = false;
                        Matrix.RestartMatrix = true;
                        break;
                    case ConsoleKey.D1:
                        Matrix.SetRainColor = "green";
                        break;
                    case ConsoleKey.D2:
                        Matrix.SetRainColor = "red";
                        break;
                    case ConsoleKey.D3:
                        Matrix.SetRainColor = "cyan";
                        break;
                    case ConsoleKey.D4:
                        Matrix.SetRainColor = "blue";
                        break;
                    case ConsoleKey.D5:
                        Matrix.SetRainColor = "yellow";
                        break;
                    case ConsoleKey.D6:
                        Matrix.SetRainColor = "magenta";
                        break;
                    case ConsoleKey.UpArrow:
                        await Task.Run(() => SetRainSlowdown(50));
                        break;
                    case ConsoleKey.DownArrow:
                        await Task.Run(() => SetRainSlowdown(-50));
                        break;
                    case ConsoleKey.Enter:
                        CommandLine();
                        break;
                }
            }

            void SetRainSlowdown(short num)
            {
                Matrix.TotalRainSlowdown += num;
                DisplayTextToCenter(Matrix.TextSpeedChange, Matrix.TotalRainSlowdown, 2);
            }
        }

        void CommandLine()
        {
            if (Matrix.WidthConsole == Console.WindowWidth - 1)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.CursorVisible = true;

                for (int i = 0; i < Matrix.CommandLine.Length; i++)
                {
                    Console.SetCursorPosition(Matrix.WidthConsole / 2 - Matrix.CommandLine[i, 0].Length / 2,
                                              Matrix.HeightConsole / 2 - Matrix.CommandLine.Length + i);
                    Console.WriteLine(Matrix.CommandLine[i, 0]);
                }

                Console.SetCursorPosition(Matrix.WidthConsole / 2 - Matrix.CommandLine[1, 0].Length / 2 + Matrix.CommandLine[1, 0].IndexOf('>') + 2,
                                          Matrix.HeightConsole / 2 - Matrix.CommandLine.Length + 1);

                string userCommand = Console.ReadLine().Trim();
                if (userCommand != "")
                {
                    GetUserCommand(userCommand);
                }
            }
        }

        void GetUserCommand(string userCommand)
        {
            string[] _commandWords = userCommand.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            short _buffer = 0;

            switch (_commandWords[0].ToLower())
            {
                case "help":
                    DisplayTextToCenter(Matrix.TextHelp);
                    Console.ReadKey();
                    break;
                case "slowdown":
                    if (_commandWords.Length == 2 && short.TryParse(_commandWords[1], out _buffer))
                    {
                        Matrix.TotalRainSlowdown = _buffer;
                    }
                    break;
                case "quote":
                    if (_commandWords.Length == 2 && short.TryParse(_commandWords[1], out _buffer))
                    {
                        Matrix.ShowQuoteAfter = _buffer;
                        Matrix.ResetMatrixAfter = _buffer + 10;
                    }
                    else if (_commandWords.Length == 2)
                    {
                        switch (_commandWords[1].ToLower())
                        {
                            case "green":
                                Matrix.QuoteColor = ConsoleColor.Green;
                                break;
                            case "darkgreen":
                                Matrix.QuoteColor = ConsoleColor.DarkGreen;
                                break;
                            case "red":
                                Matrix.QuoteColor = ConsoleColor.Red;
                                break;
                            case "darkred":
                                Matrix.QuoteColor = ConsoleColor.DarkRed;
                                break;
                            case "blue":
                                Matrix.QuoteColor = ConsoleColor.Blue;
                                break;
                            case "darkblue":
                                Matrix.QuoteColor = ConsoleColor.DarkBlue;
                                break;
                            case "yellow":
                                Matrix.QuoteColor = ConsoleColor.Yellow;
                                break;
                            case "darkyellow":
                                Matrix.QuoteColor = ConsoleColor.DarkYellow;
                                break;
                            case "white":
                                Matrix.QuoteColor = ConsoleColor.White;
                                break;
                            case "gray":
                                Matrix.QuoteColor = ConsoleColor.Gray;
                                break;
                            case "cyan":
                                Matrix.QuoteColor = ConsoleColor.Cyan;
                                break;
                            case "darkcyan":
                                Matrix.QuoteColor = ConsoleColor.DarkCyan;
                                break;
                            case "magenta":
                                Matrix.QuoteColor = ConsoleColor.Magenta;
                                break;
                            case "darkmagenta":
                                Matrix.QuoteColor = ConsoleColor.DarkMagenta;
                                break;
                        }
                    }
                    break;
                case "restart":
                    Console.Clear();
                    Matrix.RestartMatrix = true;
                    break;
                case "reset":
                    if (_commandWords.Length == 2 && short.TryParse(_commandWords[1], out _buffer))
                    {
                        Matrix.ResetMatrixAfter = _buffer;
                        Matrix.ShowQuoteAfter = _buffer - 10;
                    }
                    else
                    {
                        Console.Clear();
                        Matrix.ResetMatrix = true;
                    }
                    break;
                case "randomcolor":
                        Matrix.RandomColor = Matrix.RandomColor ? false : true;            
                    break;
                case "clear":
                    Console.Clear();
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
            }
        }

        void DisplayTextToCenter(string[,] textForDisplay, int valueForDisplay = 0, byte positionForValue = 0, ConsoleColor consoleColor = ConsoleColor.DarkRed)
        {
            if (Matrix.WidthConsole == Console.WindowWidth - 1)
            {
                Console.ForegroundColor = consoleColor;
                for (int i = 0; i < textForDisplay.Length; i++)
                {
                    if (i == positionForValue - 1)
                    {
                        Console.SetCursorPosition(Matrix.WidthConsole / 2 - textForDisplay[i, 0].Length / 2,
                                                  Matrix.HeightConsole / 2 - textForDisplay.Length + i);
                        Console.WriteLine(textForDisplay[i, 0] + Convert.ToString(valueForDisplay) + ' ');
                    }
                    else
                    {
                        Console.SetCursorPosition(Matrix.WidthConsole / 2 - textForDisplay[i, 0].Length / 2,
                                                  Matrix.HeightConsole / 2 - textForDisplay.Length + i);
                        Console.WriteLine(textForDisplay[i, 0]);
                    }
                }
            }
        }
    }


}
