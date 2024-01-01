using System;
using System.Collections;
using _Assets.Scripts.Services.Datas.GameConfigs;
using UnityEngine;
using UnityEngine.Networking;
using VContainer;

namespace _Assets.Scripts.Services.Audio
{
    public class AudioService : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [Inject] private IConfigLoader _configLoader;

        public void PlaySong(int index)
        {
            var path = _configLoader.CurrentConfig.SuikaAudioPaths[index];
            StartCoroutine(DownloadAndPlay(path));
        }

        IEnumerator DownloadAndPlay(string path)
        {
            using (UnityWebRequest unityWebRequest = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.MPEG))
            {
                yield return unityWebRequest.SendWebRequest();
                AudioClip clip = DownloadHandlerAudioClip.GetContent(unityWebRequest);
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }
}