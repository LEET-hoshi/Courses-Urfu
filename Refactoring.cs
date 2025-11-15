using System;
using Avalonia.Media;
using RefactorMe.Common;

namespace RefactorMe
{
    class Drawer
    {
        static float x, y;
        static IGraphics graphics;

        public static void Initialize(IGraphics newGraphics)
        {
            graphics = newGraphics;
            graphics.Clear(Colors.Black);
        }

        public static void SetPosition(float x0, float y0)
        {
            x = x0;
            y = y0;
        }

        public static void DrawLine(Pen pen, double length, double angle)
        {
            var x1 = (float)(x + length * Math.Cos(angle));
            var y1 = (float)(y + length * Math.Sin(angle));
            graphics.DrawLine(pen, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void MoveBy(double length, double angle)
        {
            x = (float)(x + length * Math.Cos(angle));
            y = (float)(y + length * Math.Sin(angle));
        }
    }

    public class ImpossibleSquare
    {
        public static void Draw(int width, int height, double rotationAngle, IGraphics graphics)
        {
            Drawer.Initialize(graphics);
            
            var (sideLength, smallSegment) = CalculateSizes(width, height);
            Pen pen = new(Brushes.Yellow);

            var (startX, startY) = CalculateStartPosition(width, height, sideLength, smallSegment, rotationAngle);
            Drawer.SetPosition(startX, startY);

            DrawAllSides(pen, sideLength, smallSegment, rotationAngle);
        }

        private static (float sideLength, float smallSegment) CalculateSizes(int width, int height)
        {
            var size = Math.Min(width, height);
            var sideLength = size * 0.375f;
            var smallSegment = size * 0.04f;
            return (sideLength, smallSegment);
        }

        private static (float x, float y) CalculateStartPosition(
    	int width, 
    	int height, 
    	float sideLength, 
    	float smallSegment, 
    	double rotationAngle)
        {
            var halfDiagonal = (sideLength + smallSegment) * Math.Sqrt(2) / 2;
            var startAngle = Math.PI + Math.PI / 4 + rotationAngle; // 225 градусов + поворот
            
            var x = (float)(halfDiagonal * Math.Cos(startAngle)) + width / 2f;
            var y = (float)(halfDiagonal * Math.Sin(startAngle)) + height / 2f;
            
            return (x, y);
        }

        private static double[] GetSideAngles(double rotationAngle)
        {
            return new double[] 
            {
                0 + rotationAngle,
                -Math.PI / 2 + rotationAngle,
                Math.PI + rotationAngle,
                Math.PI / 2 + rotationAngle
            };
        }

        private static void DrawAllSides(Pen pen, float sideLength, float smallSegment, double rotationAngle)
        {
            var angles = GetSideAngles(rotationAngle);
            
            foreach (var angle in angles)
            {
                DrawSquareSide(pen, sideLength, smallSegment, angle);
            }
        }

        private static void DrawSquareSide(Pen pen, float sideLength, float smallSegment, double baseAngle)
        {
            // Основная линия стороны
            Drawer.DrawLine(pen, sideLength, baseAngle);
            
            // Диагональный отрезок
            Drawer.DrawLine(pen, smallSegment * Math.Sqrt(2), baseAngle + Math.PI / 4);
            
            // Обратная линия
            Drawer.DrawLine(pen, sideLength, baseAngle + Math.PI);
            
            // Вертикальный/горизонтальный отрезок (зависит от угла)
            Drawer.DrawLine(pen, sideLength - smallSegment, baseAngle + Math.PI / 2);

            // Перемещения для подготовки к следующей стороне
            Drawer.MoveBy(smallSegment, baseAngle - Math.PI);
            Drawer.MoveBy(smallSegment * Math.Sqrt(2), baseAngle + 3 * Math.PI / 4);
        }
    }
}