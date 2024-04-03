﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using _Assets.Scripts.Services.Datas.GameConfigs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using VContainer;
using Random = UnityEngine.Random;

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
        private CancellationTokenSource _cancellationSource = new();
        public event Action OnSongChanged;

        public string GetSongName()
        {
            var fileName =
                Path.GetFileNameWithoutExtension(_configLoader.CurrentConfig.SuikaAudios[_lastSongIndex].Path);
            return fileName;
        }

        public bool IsMusicPlaying => musicSource.isPlaying;

        public int LastSongIndex => _lastSongIndex;

        public void ChangeMusicVolume(float volume)
        {
            _audioSettingsLoader.ChangeMusicVolume(volume);
            musicSource.volume = volume;
        }

        public void ChangeSoundVolume(float volume)
        {
            _audioSettingsLoader.ChangeSoundVolume(volume);
            mergeSource.volume = volume;
        }

        public void PauseMusic() => musicSource.Stop();

        public async UniTask PlaySong(int index)
        {
            if (_audioSettingsLoader.AudioData.MusicVolume <= 0)
            {
                Debug.LogWarning("Music is disabled");
                return;
            }

            _lastSongIndex = index;
            var audioData = _configLoader.CurrentConfig.SuikaAudios[index];
            var extension = Path.GetExtension(audioData.Path);

            _cancellationSource?.Cancel();
            _cancellationSource = new CancellationTokenSource();

            try
            {
                switch (extension)
                {
                    case ".mp3":
                        await DownloadAndPlaySong(audioData.Path, audioData.Volume, AudioType.MPEG,
                            _cancellationSource.Token).SuppressCancellationThrow();
                        break;
                    case ".ogg":
                        await DownloadAndPlaySong(audioData.Path, audioData.Volume, AudioType.OGGVORBIS,
                            _cancellationSource.Token).SuppressCancellationThrow();
                        break;
                    case ".wav":
                        await DownloadAndPlaySong(audioData.Path, audioData.Volume, AudioType.WAV,
                            _cancellationSource.Token).SuppressCancellationThrow();
                        break;
                }
            }
            catch (OperationCanceledException)
            {
                Debug.LogWarning("Loading cancelled");
            }
        }

        public async UniTask PlayRandomSong()
        {
            if (_audioSettingsLoader.AudioData.MusicVolume <= 0)
            {
                Debug.LogWarning("Music is disabled");
                return;
            }

            var index = Random.Range(0, _configLoader.CurrentConfig.SuikaAudios.Length);
            await PlaySong(index);
        }

        public async UniTask PlaySelectedSong()
        {
            if (_audioSettingsLoader.AudioData.MusicVolume <= 0)
            {
                Debug.LogWarning("Music is disabled");
                return;
            }

            await PlaySong(_lastSongIndex);
        }

        public async UniTask PlayNextSong()
        {
            _lastSongIndex = (LastSongIndex + 1) % _configLoader.CurrentConfig.SuikaAudios.Length;
            await PlaySong(_lastSongIndex);
        }

        public async UniTask PlayPreviousSong()
        {
            _lastSongIndex = (LastSongIndex - 1 + _configLoader.CurrentConfig.SuikaAudios.Length) %
                             _configLoader.CurrentConfig.SuikaAudios.Length;
            await PlaySong(_lastSongIndex);
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
                OnSongChanged?.Invoke();
                webRequest.Dispose();
            }
            else
            {
                if (musicSource.clip.name == path && musicSource.isPlaying)
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
                    OnSongChanged?.Invoke();
                    webRequest.Dispose();
                }
            }
        }
    }
}