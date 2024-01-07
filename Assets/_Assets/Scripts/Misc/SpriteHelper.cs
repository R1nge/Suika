using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace _Assets.Scripts.Misc
{
    public static class SpriteHelper
    {
        public static async UniTask<Sprite> CreateSprite(string relativePath)
        {
            var imageBytes = await File.ReadAllBytesAsync(relativePath);

            var texture = new Texture2D(1, 1);
            texture.LoadImage(imageBytes);
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 256);
        }


        public static async UniTask<Sprite> CreateSpriteFromStreamingAssests(string relativePath)
        {
            using (var webRequest = UnityWebRequestTexture.GetTexture(relativePath))
            {
                await webRequest.SendWebRequest();
                var imageTexture = DownloadHandlerTexture.GetContent(webRequest);
                return Sprite.Create(imageTexture, new Rect(0, 0, imageTexture.width, imageTexture.height), new Vector2(0.5f, 0.5f), 256);
            }
        }
    }
}