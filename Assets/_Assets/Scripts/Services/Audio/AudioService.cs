using System.IO;
using _Assets.Scripts.Services.Datas.GameConfigs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using VContainer;

namespace _Assets.Scripts.Services.Audio
{
    public class AudioService : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [Inject] private IConfigLoader _configLoader;
        private int _lastIndex;

        public async void PlaySong(int index)
        {
            if (_lastIndex < index)
            {
                _lastIndex = index;
                var path = _configLoader.CurrentConfig.SuikaAudioPaths[index];
                await DownloadAndPlay(path);
            }
            else
            {
                Debug.LogWarning("Trying to play already played song");
            }
        }

        private async UniTask DownloadAndPlay(string path)
        {
#if UNITY_ANDROID
            path = "file://" + path;
            Debug.LogError("Android path: " + path);
#endif
            var webRequest = new UnityWebRequest(path, "GET", new DownloadHandlerAudioClip(path,  AudioType.MPEG), null);
            await webRequest.SendWebRequest();
            ((DownloadHandlerAudioClip)webRequest.downloadHandler).streamAudio = true;
            var song = DownloadHandlerAudioClip.GetContent(webRequest);
            audioSource.clip = song;
            audioSource.Play();
            webRequest.Dispose();
        }
    }
}