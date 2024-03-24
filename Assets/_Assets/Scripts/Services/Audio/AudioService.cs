using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private readonly List<int> _mergeSoundsQueue = new(10);
        private bool _queueIsPlaying;

        public int LastSongIndex => _lastSongIndex;

        public void ResetIndex() => _lastSongIndex = 0;

        public void ChangeMusicVolume(float volume)
        {
            _audioSettingsLoader.ChangeMusicVolume(volume);

            musicSource.volume = volume;

            if (volume > 0 && !musicSource.isPlaying)
            {
                musicSource.Play();
            }
            else if (volume <= 0)
            {
                musicSource.Stop();
            }
        }

        public void ChangeSoundVolume(float volume)
        {
            _audioSettingsLoader.ChangeSoundVolume(volume);
            mergeSource.volume = volume;
        }

        public void StopMusic() => musicSource.Stop();

        public async UniTask PlaySongContinue(int index)
        {
            _lastSongIndex = index;

            if (_audioSettingsLoader.AudioData.MusicVolume <= 0)
            {
                Debug.LogWarning("Music is disabled");
                return;
            }

            var audioData = _configLoader.CurrentConfig.SuikaAudios[_lastSongIndex];
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

        public async UniTask PlaySong(int index)
        {
            if (_audioSettingsLoader.AudioData.MusicVolume <= 0)
            {
                Debug.LogWarning("Music is disabled");
                return;
            }

            if (index > _lastSongIndex)
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

        public void AddToMergeSoundsQueue(int index)
        {
            if (_audioSettingsLoader.AudioData.VFXVolume <= 0)
            {
                Debug.LogWarning("Sounds are disabled");
                return;
            }

            _mergeSoundsQueue.Add(index);

            PlayMergeFromQueue().Forget();
        }

        private async UniTask PlayMergeFromQueue()
        {
            if (_queueIsPlaying)
            {
                Debug.LogWarning("The queue is already playing, nothing to do");
                return;
            }
            _queueIsPlaying = true;
            
            if (_audioSettingsLoader.AudioData.VFXVolume <= 0)
            {
                Debug.LogWarning("Sounds are disabled");
                return;
            }

            if (_mergeSoundsQueue.Count == 0)
            {
                Debug.LogWarning("The queue is empty, nothing to do");
                return;
            }

            await UniTask.WaitForSeconds(0.2f);

            var index = _mergeSoundsQueue.Max();

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

            _queueIsPlaying = false;
            _mergeSoundsQueue.Clear();
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
            mergeSource.volume = volume * _audioSettingsLoader.AudioData.VFXVolume;
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
                musicSource.volume = volume * _audioSettingsLoader.AudioData.MusicVolume;
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