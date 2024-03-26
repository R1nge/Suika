using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Assets.Scripts.Services.Configs
{
    [CreateAssetMenu(fileName = "UI Theme", menuName = "Configs/UITheme")]
    public class UITheme : ScriptableObject
    {
        [SerializeField] private Sprite volume;
        [SerializeField] private Sprite music;
        [SerializeField] private Sprite musicPlayerNext;
        [SerializeField] private Sprite musicPlayerPlay;
        [SerializeField] private Sprite musicPlayerPause;
        [SerializeField] private Sprite musicPlayerShuffle;
        [SerializeField] private Sprite vibrate;
        [SerializeField] private Color selected;
        [SerializeField] private Color unselected;
        public Color Selected => selected;
        public Color Unselected => unselected;

        private readonly Dictionary<UIElementType, Sprite> _sprites = new();

        private void OnEnable()
        {
            if (_sprites.Count == 7) return;

            _sprites.Add(UIElementType.MusicPlayerPlay, musicPlayerPlay);
            _sprites.Add(UIElementType.MusicPlayerNext, musicPlayerNext);
            _sprites.Add(UIElementType.MusicPlayerShuffle, musicPlayerShuffle);
            _sprites.Add(UIElementType.Vibrate, vibrate);
            _sprites.Add(UIElementType.Sound, volume);
            _sprites.Add(UIElementType.Music, music);
            _sprites.Add(UIElementType.MusicPlayerPause, musicPlayerPause);
        }

        public Sprite GetSprite(UIElementType type)
        {
            return _sprites[type];
        }

        public enum UIElementType
        {
            MusicPlayerPlay,
            MusicPlayerNext,
            MusicPlayerShuffle,
            Vibrate,
            Sound,
            Music,
            MusicPlayerPause
        }
    }
}