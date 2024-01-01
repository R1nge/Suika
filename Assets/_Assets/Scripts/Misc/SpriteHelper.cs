using System.IO;
using UnityEngine;

namespace _Assets.Scripts.Misc
{
    public static class SpriteHelper
    {
        public static Sprite CreateSprite(string path, float width, float height)
        {
            var relativePath = Path.Combine($"{Application.dataPath}\\Mods", path);
            var imageBytes = File.ReadAllBytes(relativePath);
            var imageTexture = new Texture2D((int)width, (int)height);
            imageTexture.LoadImage(imageBytes);
            return Sprite.Create(imageTexture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
        }
    }
}