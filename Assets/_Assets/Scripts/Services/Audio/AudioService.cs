using System.IO;
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
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource mergeSource;
        [Inject] private IConfigLoader _configLoader;
        [Inject] private IAudioSettingsLoader _audioSettingsLoader;
        private int _lastSongIndex;

        public async UniTask PlaySong(int index)
        {
            if (!_audioSettingsLoader.AudioData.IsMusicEnabled)
            {
                return;
            }

            if (_lastSongIndex < index)
            {
                _lastSongIndex = index;
                var audioData = _configLoader.CurrentConfig.SuikaAudios[index];
                var extension = Path.GetExtension(audioData.Path);

                switch (extension)
                {
                    case ".mp3":
                        await DownloadAndPlaySong(audioData.Path, audioData.Volume, AudioType.MPEG,
                            this.GetCancellationTokenOnDestroy());
                        break;
                    case ".ogg":
                        await DownloadAndPlaySong(audioData.Path, audioData.Volume, AudioType.OGGVORBIS,
                            this.GetCancellationTokenOnDestroy());
                        break;
                    case ".wav":
                        await DownloadAndPlaySong(audioData.Path, audioData.Volume, AudioType.WAV,
                            this.GetCancellationTokenOnDestroy());
                        break;
                }
            }
            else
            {
                Debug.LogWarning("The song index is smaller than the last played song index, nothing to do");
            }
        }

        public async UniTask PlayMerge(int index)
        {
            if (!_audioSettingsLoader.AudioData.IsSoundEnabled)
            {
                return;
            }

            var audioData = _configLoader.CurrentConfig.MergeSoundsAudios[index];
            var extension = Path.GetExtension(audioData.Path);

            switch (extension)
            {
                case ".mp3":
                    await DownloadAndPlayMergeSound(audioData.Path, audioData.Volume, AudioType.MPEG,
                        this.GetCancellationTokenOnDestroy());
                    break;
                case ".ogg":
                    await DownloadAndPlayMergeSound(audioData.Path, audioData.Volume, AudioType.OGGVORBIS,
                        this.GetCancellationTokenOnDestroy());
                    break;
                case ".wav":
                    await DownloadAndPlayMergeSound(audioData.Path, audioData.Volume, AudioType.WAV,
                        this.GetCancellationTokenOnDestroy());
                    break;
            }
        }

        private async UniTask DownloadAndPlayMergeSound(string path, float volume, AudioType audioType,
            CancellationToken cancellationToken)
        {
            var webRequest = new UnityWebRequest(path, "GET", new DownloadHandlerAudioClip(path, audioType), null);
            await webRequest.SendWebRequest().WithCancellation(cancellationToken);
            ((DownloadHandlerAudioClip)webRequest.downloadHandler).streamAudio = true;
            var sound = DownloadHandlerAudioClip.GetContent(webRequest);
            mergeSource.clip = sound;
            mergeSource.clip.name = path;
            mergeSource.volume = volume;
            mergeSource.Play();
            webRequest.Dispose();
        }

        private async UniTask DownloadAndPlaySong(string path, float volume, AudioType audioType,
            CancellationToken cancellationToken)
        {
            if (musicSource.clip == null)
            {
                var webRequest = new UnityWebRequest(path, "GET", new DownloadHandlerAudioClip(path, audioType), null);
                await webRequest.SendWebRequest().WithCancellation(cancellationToken);
                ((DownloadHandlerAudioClip)webRequest.downloadHandler).streamAudio = true;
                var song = DownloadHandlerAudioClip.GetContent(webRequest);
                musicSource.clip = song;
                musicSource.clip.name = path;
                musicSource.volume = volume;
                musicSource.Play();
                webRequest.Dispose();
            }
            else
            {
                if (musicSource.clip.name == path)
                {
                    Debug.LogWarning("The same song is playing already, nothing to do");
                }
                else
                {
                    var webRequest =
                        new UnityWebRequest(path, "GET", new DownloadHandlerAudioClip(path, audioType), null);
                    await webRequest.SendWebRequest().WithCancellation(cancellationToken);
                    ((DownloadHandlerAudioClip)webRequest.downloadHandler).streamAudio = true;
                    var song = DownloadHandlerAudioClip.GetContent(webRequest);
                    musicSource.clip = song;
                    musicSource.clip.name = path;
                    musicSource.volume = volume;
                    musicSource.Play();
                    webRequest.Dispose();
                }
            }
        }
    }
}