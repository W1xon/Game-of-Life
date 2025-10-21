using System;
using System.Drawing;
using GameOfLife.Model;

namespace GameOfLife.Services.ColorStrategies
{
    public class SmoothWaveStrategy : ColorStrategyBase
    {
        public override Color CalculateColor(Vector position, Vector size)
        {
            // текущее время в секундах
            double t = Environment.TickCount64 * 0.001;

            // мягкие волны по X и Y
            double waveX = Math.Sin((position.X * 0.02) + t * 0.8);
            double waveY = Math.Cos((position.Y * 0.02) + t * 0.6);

            // итоговое значение волны
            double w = (waveX + waveY) * 0.5;

            // плавная пульсация яркости
            double pulse = 0.75 + 0.25 * Math.Sin(t * 1.5);

            // цветовой переход — фиолетово-сине-зелёный диапазон
            int r = ClampColor((int)((0.4 + 0.4 * w) * 255 * pulse));
            int g = ClampColor((int)((0.6 + 0.4 * -w) * 255 * pulse));
            int b = ClampColor((int)((0.8 + 0.2 * w) * 255));

            return Color.FromArgb(255, r, g, b);
        }
    }
}