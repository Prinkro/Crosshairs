using System;
using System.Numerics;
using ClickableTransparentOverlay;
using ImGuiNET;

namespace Cheat_menu
{
    class Renderer : Overlay
    {
        float sliderValue = 30f;
        float espSize = 20f;
        float crosshairSize = 6f;
        float crosshairThickness = 1.5f;
        int crosshairTypeIndex = 0;

        private bool isAimbotEnabled = false;
        private bool isESPEnabled = false;
        private bool isCrossHairEnabled = false;

        Vector4 aimbotColor = new Vector4(1, 1, 0, 1);
        Vector4 crosshairColor = new Vector4(1, 0, 0, 1);
        Vector4 espColor = new Vector4(0, 1, 0, 1);

        readonly string[] crosshairTypes = new[] { "X", "+", "Dot" };

        protected override void Render()
        {
            var io = ImGui.GetIO();
            var center = new Vector2(io.DisplaySize.X / 2f, io.DisplaySize.Y / 2f);
            var drawList = ImGui.GetBackgroundDrawList();

            ImGui.Begin("Prinkros Cheat Menu");

            

            

            // Crosshair
            ImGui.Checkbox("Crosshair", ref isCrossHairEnabled);
            if (isCrossHairEnabled)
            {
                ImGui.Combo("Crosshair Type", ref crosshairTypeIndex, crosshairTypes, crosshairTypes.Length);
                ImGui.SliderFloat("Crosshair Size", ref crosshairSize, 2, 50);
                ImGui.SliderFloat("Crosshair Thickness", ref crosshairThickness, 1, 5);
                ImGui.ColorEdit4("Crosshair Color", ref crosshairColor);
            }

            ImGui.End();

            // FOV Circle
            if (isAimbotEnabled)
            {
                uint colorFOV = ImGui.ColorConvertFloat4ToU32(aimbotColor);
                drawList.AddCircle(center, sliderValue, colorFOV, 100, 2.0f);
            }

            // ESP Box (placeholder)
            if (isESPEnabled)
            {
                uint colorESP = ImGui.ColorConvertFloat4ToU32(espColor);
                Vector2 topLeft = new Vector2(center.X - espSize / 2f, center.Y + 100 - espSize / 2f);
                Vector2 bottomRight = new Vector2(topLeft.X + espSize, topLeft.Y + espSize);
                drawList.AddRect(topLeft, bottomRight, colorESP, 3f, ImDrawFlags.None, 2f);
            }

            // Draw selected crosshair
            if (isCrossHairEnabled)
            {
                uint color = ImGui.ColorConvertFloat4ToU32(crosshairColor);
                float s = crosshairSize;
                float t = crosshairThickness;

                switch (crosshairTypes[crosshairTypeIndex])
                {
                    case "X":
                        drawList.AddLine(new Vector2(center.X - s, center.Y - s), new Vector2(center.X + s, center.Y + s), color, t);
                        drawList.AddLine(new Vector2(center.X - s, center.Y + s), new Vector2(center.X + s, center.Y - s), color, t);
                        break;

                    case "+":
                        drawList.AddLine(new Vector2(center.X - s, center.Y), new Vector2(center.X + s, center.Y), color, t);
                        drawList.AddLine(new Vector2(center.X, center.Y - s), new Vector2(center.X, center.Y + s), color, t);
                        break;

                    case "Dot":
                        drawList.AddCircleFilled(center, s / 2f, color);
                        break;

                    
                }
            }
        }
    }
}
