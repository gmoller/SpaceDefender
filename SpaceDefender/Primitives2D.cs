using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender
{
    public static class Primitives2D
    {
        private static Texture2D _pixel; // our pixel texture we will be using to draw primitives

        public static void Initialize(GraphicsDevice graphicsDevice)
        {
            _pixel = new Texture2D(graphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });
        }

        public static void DrawPixel(this SpriteBatch spriteBatch, float x, float y, Color color)
        {
            spriteBatch.Draw(_pixel, new Vector2(x, y), color);
        }

        public static void DrawLineSegment(this SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color)
        {
            float xinc1, xinc2, yinc1, yinc2, den, num, numadd, numpixels, curpixel;

            float deltaX = Math.Abs(point2.X - point1.X);
            float deltaY = Math.Abs(point2.Y - point1.Y);
            float x = point1.X;
            float y = point1.Y;

            if (point2.X >= point1.X)                 // The x-values are increasing
            {
                xinc1 = 1;
                xinc2 = 1;
            }
            else                          // The x-values are decreasing
            {
                xinc1 = -1;
                xinc2 = -1;
            }

            if (point2.Y >= point1.Y)                 // The y-values are increasing
            {
                yinc1 = 1;
                yinc2 = 1;
            }
            else                          // The y-values are decreasing
            {
                yinc1 = -1;
                yinc2 = -1;
            }

            if (deltaX >= deltaY)         // There is at least one x-value for every y-value
            {
                xinc1 = 0;                  // Don't change the x when numerator >= denominator
                yinc2 = 0;                  // Don't change the y for every iteration
                den = deltaX;
                num = deltaX / 2;
                numadd = deltaY;
                numpixels = deltaX;         // There are more x-values than y-values
            }
            else                          // There is at least one y-value for every x-value
            {
                xinc2 = 0;                  // Don't change the x for every iteration
                yinc1 = 0;                  // Don't change the y when numerator >= denominator
                den = deltaY;
                num = deltaY / 2;
                numadd = deltaX;
                numpixels = deltaY;         // There are more y-values than x-values
            }

            for (curpixel = 0; curpixel <= numpixels; curpixel++)
            {
                spriteBatch.DrawPixel(x, y, color);
                num += numadd;              // Increase the numerator by the top of the fraction
                if (num >= den)             // Check if numerator >= denominator
                {
                    num -= den;               // Calculate the new numerator value
                    x += xinc1;               // Change the x as appropriate
                    y += yinc1;               // Change the y as appropriate
                }
                x += xinc2;                 // Change the x as appropriate
                y += yinc2;                 // Change the y as appropriate
            }
        }

        public static void DrawPolygon(this SpriteBatch spriteBatch, Vector2[] vertices, Color color)
        {
            if (vertices.Length > 0)
            {
                for (int i = 0; i < vertices.Length - 1; i++)
                {
                    spriteBatch.DrawLineSegment(vertices[i], vertices[i + 1], color);
                }
                spriteBatch.DrawLineSegment(vertices[vertices.Length - 1], vertices[0], color);
            }
        }

        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rectangle, Color color, Boolean filled)
        {
            if (filled)
            {
                spriteBatch.Draw(_pixel, rectangle, color);
            }
            else
            {
                var vertex = new Vector2[4];
                vertex[0] = new Vector2(rectangle.Left, rectangle.Top);
                vertex[1] = new Vector2(rectangle.Right, rectangle.Top);
                vertex[2] = new Vector2(rectangle.Right, rectangle.Bottom);
                vertex[3] = new Vector2(rectangle.Left, rectangle.Bottom);

                spriteBatch.DrawPolygon(vertex, color);
            }
        }

        public static void DrawCircle(this SpriteBatch spriteBatch, Vector2 center, float radius, Color color, int segments = 16)
        {
            var vertices = new Vector2[segments];

            double increment = Math.PI * 2.0 / segments;
            double theta = 0.0;

            for (int i = 0; i < segments; i++)
            {
                vertices[i] = center + radius * new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
                theta += increment;
            }

            spriteBatch.DrawPolygon(vertices, color);
        }
    }
}