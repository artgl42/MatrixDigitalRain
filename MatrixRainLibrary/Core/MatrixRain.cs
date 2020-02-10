using System;
using System.Threading;

namespace MatrixDigitalRain.Core
{
    class MatrixRain
    {
        int[] PositionHeight { get; set; }
        byte LimitWhiteTail { get; set; }
        byte LimitGreenTail { get; set; }
        byte LimitDarkGreenTail { get; set; }
        byte WhiteTail { get; set; }
        byte GreenTail { get; set; }
        byte DarkGreenTail { get; set; }

        internal MatrixRain(byte limitWhiteTail = 1, byte limitGreenTail = 11, byte limitDarkGreenTail = 31)
        {
            PositionHeight = new int[Console.WindowWidth];
            LimitWhiteTail = limitWhiteTail;
            LimitGreenTail = limitGreenTail;
            LimitDarkGreenTail = limitDarkGreenTail;
            ResetMatrix();
        }

        internal void RunMatrixRain()
        {
            CheckConfig();

            for (int i = 0; i < Matrix.WidthConsole; i++)
            {
                Console.ForegroundColor = Matrix.FirstCharColor;
                Console.SetCursorPosition(i, PositionHeight[i]);
                Console.Write(Matrix.GetRandomChar);

                Console.ForegroundColor = Matrix.SecondCharColor;
                Console.SetCursorPosition(i, GetPositionHeight(PositionHeight[i] - WhiteTail, Matrix.HeightConsole));
                Console.Write(Matrix.GetRandomChar);

                Console.ForegroundColor = Matrix.ThirdCharColor;
                Console.SetCursorPosition(i, GetPositionHeight(PositionHeight[i] - GreenTail, Matrix.HeightConsole));
                Console.Write(Matrix.GetRandomChar);

                Console.SetCursorPosition(i, GetPositionHeight(PositionHeight[i] - DarkGreenTail, Matrix.HeightConsole));
                Console.Write(' ');

                PositionHeight[i] = GetPositionHeight(PositionHeight[i] + 1, Matrix.HeightConsole);
                Console.ForegroundColor = Matrix.ForegroundColor;
            }

            int GetPositionHeight(int positionHeight, int consoleHeight)
            {
                if (positionHeight < 0) return positionHeight + consoleHeight;
                if (positionHeight < consoleHeight) return positionHeight;
                return 0;
            }
        }

        void CheckConfig()
        {
            if (Matrix.Counter >= Matrix.ResetMatrixAfter)
            {
                if (Matrix.WidthConsole == Console.WindowWidth - 1) ResetMatrix();
                if (Matrix.RandomColor) SetRandomColor();
                Matrix.Counter = 0;
            }

            if (Matrix.ResetMatrix) ResetMatrix(); 
            if (Matrix.RestartMatrix) RestartMatrix();

            if (Matrix.StartRainSlowdown > 0)
            {
                Thread.Sleep(Matrix.StartRainSlowdown);
                Matrix.StartRainSlowdown--;
            }
            else
            {
                Thread.Sleep(Matrix.TotalRainSlowdown);
            }

            if (Matrix.WidthConsole < Console.WindowWidth - 1) Matrix.WidthConsole++;
            if (WhiteTail < LimitWhiteTail) WhiteTail++;
            if (GreenTail < LimitGreenTail) GreenTail++;
            if (DarkGreenTail < LimitDarkGreenTail) DarkGreenTail++;

            Matrix.Counter++;
        }

        void ResetMatrix()
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                PositionHeight[i] = Matrix.random.Next(Matrix.HeightConsole);
            }
            Matrix.ResetMatrix = false;
        }

        void RestartMatrix()
        {
            Matrix.WidthConsole = 0;
            Matrix.Counter = 0;
            WhiteTail = 0;
            GreenTail = 0;
            DarkGreenTail = 0;
            Matrix.RestartMatrix = false;
        }

        void SetRandomColor()
        {
            byte _randomNum = (byte)Matrix.random.Next(0, 6);
            switch (_randomNum)
            {
                case 0:
                    Matrix.SetRainColor = "green";
                    break;
                case 1:
                    Matrix.SetRainColor = "red";
                    break;
                case 2:
                    Matrix.SetRainColor = "cyan";
                    break;
                case 3:
                    Matrix.SetRainColor = "blue";
                    break;
                case 4:
                    Matrix.SetRainColor = "yellow";
                    break;
                case 5:
                    Matrix.SetRainColor = "magenta";
                    break;
            }
        }
    }
}
