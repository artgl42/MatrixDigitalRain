using MatrixDigitalRain.Core;
using System;

namespace MatrixDigitalRain
{
    public class Matrix : IMatrix
    {
        internal static Random random { get; } = new Random();

        #region --- Color settings ---
        internal static ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;
        internal static ConsoleColor ForegroundColor { get; set; } = ConsoleColor.Green;
        internal static ConsoleColor FirstCharColor { get; set; } = ConsoleColor.White;
        internal static ConsoleColor SecondCharColor { get; set; } = ConsoleColor.Green;
        internal static ConsoleColor ThirdCharColor { get; set; } = ConsoleColor.DarkGreen;
        internal static ConsoleColor QuoteColor { get; set; } = ConsoleColor.Green;
        internal static string SetRainColor
        {
            set
            {
                switch (value)
                {
                    case "green":
                        ForegroundColor = ConsoleColor.Green;
                        FirstCharColor = ConsoleColor.White;
                        SecondCharColor = ConsoleColor.Green;
                        ThirdCharColor = ConsoleColor.DarkGreen;
                        break;
                    case "red":
                        ForegroundColor = ConsoleColor.Red;
                        FirstCharColor = ConsoleColor.White;
                        SecondCharColor = ConsoleColor.Red;
                        ThirdCharColor = ConsoleColor.DarkRed;
                        break;
                    case "cyan":
                        ForegroundColor = ConsoleColor.Cyan;
                        FirstCharColor = ConsoleColor.White;
                        SecondCharColor = ConsoleColor.Cyan;
                        ThirdCharColor = ConsoleColor.DarkCyan;
                        break;
                    case "blue":
                        ForegroundColor = ConsoleColor.Blue;
                        FirstCharColor = ConsoleColor.White;
                        SecondCharColor = ConsoleColor.Blue;
                        ThirdCharColor = ConsoleColor.DarkBlue;
                        break;
                    case "yellow":
                        ForegroundColor = ConsoleColor.Yellow;
                        FirstCharColor = ConsoleColor.White;
                        SecondCharColor = ConsoleColor.Yellow;
                        ThirdCharColor = ConsoleColor.DarkYellow;
                        break;
                    case "magenta":
                        ForegroundColor = ConsoleColor.Magenta;
                        FirstCharColor = ConsoleColor.White;
                        SecondCharColor = ConsoleColor.Magenta;
                        ThirdCharColor = ConsoleColor.DarkMagenta;
                        break;
                }
            }
        }
        #endregion

        #region --- Text settings ---
        static internal char GetRandomChar
        {
            get
            {
                return Convert.ToChar(random.Next(33, 127));
            }
        }

        static internal string[,] TextASCII { get; set; } = new string[,]{ {"---------------------------------------------------------------------------------------------------------------------------" },
                                                                           {"|  ######  ##    ##  ######  ######## ######## ##     ##    ########    ###    #### ##       ##     ## ########  ######## |" },
                                                                           {"| ##    ##  ##  ##  ##    ##    ##    ##       ###   ###    ##         ## ##    ##  ##       ##     ## ##     ## ##       |" },
                                                                           {"| ##         ####   ##          ##    ##       #### ####    ##        ##   ##   ##  ##       ##     ## ##     ## ##       |" },
                                                                           {"|  ######     ##     ######     ##    ######   ## ### ##    ######   ##     ##  ##  ##       ##     ## ########  ######   |" },
                                                                           {"|       ##    ##          ##    ##    ##       ##     ##    ##       #########  ##  ##       ##     ## ##   ##   ##       |" },
                                                                           {"| ##    ##    ##    ##    ##    ##    ##       ##     ##    ##       ##     ##  ##  ##       ##     ## ##    ##  ##       |" },
                                                                           {"|  ######     ##     ######     ##    ######## ##     ##    ##       ##     ## #### ########  #######  ##     ## ######## |" },
                                                                           {"---------------------------------------------------------------------------------------------------------------------------" }};

        static internal string[,] TextHelp { get; } = new string[,]{ {" -------------------------------------------- HELP ------------------------------------------ " },
                                                                     {" > F1: Help                                                                                   " },
                                                                     {" > 1..6 (to change the color): 1. Green (default) 2. Red 3. Cyan 4. Blue 5. Yellow 6. Magenta " },
                                                                     {" > Escape: Clear the screen                                                                   " },
                                                                     {" > UpArrow: MatrixRain slowdown UP (+50)                                                      " },
                                                                     {" > DownArrow: MatrixRain slowdown DOWN (-50)                                                  " },
                                                                     {" > Commands (press Enter): help, slowdown [number], quote [number or color], restart,         " },
                                                                     {" >           reset [number], randomcolor, clear, exit                                         " },
                                                                     {" -------------------------------------------------------------------------------------------- " }};

        static internal string[,] TextPause { get; } = new string[,]{ {" ----------------------------------- " },
                                                                      {" PAUSE. Press any key to continue... " },
                                                                      {" ----------------------------------- " }};

        static internal string[,] TextSpeedChange { get; } = new string[,]{ {" ---------- " },
                                                                            {" Slowdown = " },
                                                                            {" ---------- " }};

        static internal string[,] TextSpeedLimit { get; } = new string[,]{ {" ---------------------------- " },
                                                                           {" It is LIMIT speed MatrixRain " },
                                                                           {" ---------------------------- " }};

        static internal string[,] CommandLine = new string[,] { { "                       " },
                                                                { " >                     " },
                                                                { "                       " }};
        #endregion   

        internal static bool SkipIntro { get; set; }
        internal static bool RandomColor { get; set; }
        internal static int WidthConsole { get; set; }
        internal static int HeightConsole { get; set; }
        internal static int ShowQuoteAfter { get; set; }
        internal static int ResetMatrixAfter { get; set; }

        internal static byte StartRainSlowdown { get; set; }
        static short _totalRainSlowdown;
        internal static short TotalRainSlowdown
        {
            get
            {
                return _totalRainSlowdown;
            }
            set
            {
                if (value >= 0 && value <= 1000)
                {
                    _totalRainSlowdown = value;
                }
            }
        }

        internal static uint Counter { get; set; }
        internal static bool ResetMatrix { get; set; }
        internal static bool RestartMatrix { get; set; }

        private Matrix(Builder builder)
        {
            Console.Title = "MatrixDigitalRainApp";
            Console.WindowWidth = Console.BufferWidth = Console.LargestWindowWidth - 32;
            Console.WindowHeight = Console.BufferHeight = Console.LargestWindowHeight - 16;
            Console.WindowTop = 0;
            Console.WindowLeft = 0;
            Console.CursorVisible = false;

            WidthConsole = 0;
            HeightConsole = Console.WindowHeight;

            SkipIntro = builder.SkipIntro;
            RandomColor = builder.RandomColor;
            StartRainSlowdown = 75;
            ShowQuoteAfter = builder.ShowQuoteAfter > 0 ? builder.ShowQuoteAfter : 290;
            ResetMatrixAfter = builder.ResetMatrixAfter > 0 ? builder.ResetMatrixAfter : 300;
            TextASCII = builder.TextASCII ?? TextASCII;
        }

        public class Builder
        {
            internal bool SkipIntro { get; set; }
            internal bool RandomColor { get; set; }
            internal int ShowQuoteAfter { get; set; }
            internal int ResetMatrixAfter { get; set; }
            internal string[,] TextASCII { get; set; }

            public Builder SetSkipIntro(bool skipIntro)
            {
                SkipIntro = skipIntro;
                return this;
            }

            public Builder SetRandomColor(bool randomColor)
            {
                RandomColor = randomColor;
                return this;
            }

            public Builder SetShowQuoteAfter(int showQuoteAfter)
            {
                ShowQuoteAfter = showQuoteAfter;
                return this;
            }

            public Builder SetResetMatrixAfter(int resetMatrixAfter)
            {
                ResetMatrixAfter = resetMatrixAfter;
                return this;
            }

            public Builder SetTextASCII(string[,] textASCII)
            {
                TextASCII = textASCII;
                return this;
            }

            public Matrix Build()
            {
                return new Matrix(this);
            }
        }

        public void RunFullVersion()
        {
            MatrixColumns matrixBlocks = new MatrixColumns();
            MatrixRain matrixRain = new MatrixRain();
            MatrixQuotes matrixSpeech = new MatrixQuotes();
            KeyboardHandler matrixKeyboard = new KeyboardHandler();

            while (true)
            {
                if (SkipIntro)
                {
                    matrixRain.RunMatrixRain();
                    matrixSpeech.ShowRandomQuote();
                    matrixKeyboard.WaitPressedKey();
                }
                else
                {
                    matrixSpeech.ShowIntroQuote(3);
                    matrixBlocks.RunDigitalColumns();
                    matrixBlocks.RunSymbolColumns();
                    matrixBlocks.ShowTextAscii(TextASCII);
                    SkipIntro = true;
                }
            }
        }

        public void RunMatrixRain()
        {
            MatrixRain matrixRain = new MatrixRain();
            KeyboardHandler matrixKeyboard = new KeyboardHandler();

            while (true)
            {
                matrixRain.RunMatrixRain();
                matrixKeyboard.WaitPressedKey();
            }
        }

        public void RunMatrixColumns()
        {
            MatrixColumns matrixBlocks = new MatrixColumns();

            while (true)
            {
                matrixBlocks.RunDigitalColumns();
                matrixBlocks.ShowTextAscii(TextASCII);
                matrixBlocks.RunSymbolColumns();
            }
        }
    }
}
