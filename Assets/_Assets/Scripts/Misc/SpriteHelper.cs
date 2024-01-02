using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace _Assets.Scripts.Misc
{
    public static class SpriteHelper
    {
        public static async UniTask<Sprite> CreateSprite(string relativePath, float width, float height)
        {
            var imageBytes = await File.ReadAllBytesAsync(relativePath);
            var imageTexture = new Texture2D((int)width, (int)height);
            imageTexture.LoadImage(imageBytes);
            return Sprite.Create(imageTexture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
        }

        public static async UniTask<Sprite> CreateSpriteFromStreamingAssests(string relativePath, float width, float height)
        {
            using (var webRequest = UnityWebRequestTexture.GetTexture(relativePath))
            {
                await webRequest.SendWebRequest();
                var imageTexture = DownloadHandlerTexture.GetContent(webRequest);
                return Sprite.Create(imageTexture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
            }
        }
    }
}