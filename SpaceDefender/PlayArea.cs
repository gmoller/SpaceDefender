using System;

namespace SpaceDefender
{
    public static class PlayArea
    {
        public static int MapWidth = Convert.ToInt32(GameRoot.ScreenSize.X * 1.5); // 1920
        public static int Top = Convert.ToInt32(GameRoot.ScreenSize.Y * 0.042); // 30
        public static int Bottom = Convert.ToInt32(GameRoot.ScreenSize.Y * 0.875); // 630
        public static float UpdateInterval = 0.01f;
    }
}