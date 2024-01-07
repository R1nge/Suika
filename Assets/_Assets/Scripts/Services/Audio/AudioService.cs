using System.Threading;
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

        public async UniTask PlaySong(int index)
        {
            if (_lastIndex < index)
            {
                _lastIndex = index;
                var path = _configLoader.CurrentConfig.SuikaAudioPaths[index];
                await DownloadAndPlay(path, this.GetCancellationTokenOnDestroy());
            }
            else
            {
                Debug.LogWarning("The song index is smaller than the last played song index, nothing to do");
            }
        }

        private async UniTask DownloadAndPlay(string path, CancellationToken cancellationToken)
        {
            if (audioSource.clip == null)
            {
                var webRequest = new UnityWebRequest(path, "GET", new DownloadHandlerAudioClip(path, AudioType.MPEG), null);
                await webRequest.SendWebRequest().WithCancellation(new CancellationToken());
                ((DownloadHandlerAudioClip)webRequest.downloadHandler).streamAudio = true;
                var song = DownloadHandlerAudioClip.GetContent(webRequest);
                audioSource.clip = song;
                audioSource.clip.name = path;
                audioSource.Play();
                webRequest.Dispose();
            }
            else
            {
                if (audioSource.clip.name == path)
                {
                    Debug.LogWarning("The same song is playing already, nothing to do");
                }
                else
                {
                    var webRequest = new UnityWebRequest(path, "GET", new DownloadHandlerAudioClip(path, AudioType.MPEG), null);
                    await webRequest.SendWebRequest();
                    ((DownloadHandlerAudioClip)webRequest.downloadHandler).streamAudio = true;
                    var song = DownloadHandlerAudioClip.GetContent(webRequest);
                    audioSource.clip = song;
                    audioSource.clip.name = path;
                    audioSource.Play();
                    webRequest.Dispose();
                }
            }
            
        }
    }
}