using _Assets.Scripts.Services.Audio;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs.InGame
{
    public class MusicPlayerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI songNameText;
        [SerializeField] private Button playButton;
        [SerializeField] private TextMeshProUGUI playButtonText;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button previousButton;
        [SerializeField] private Button shuffleButton;
        [Inject] private AudioService _audioService;

        private void Start()
        {
            playButton.onClick.AddListener(Play);
            nextButton.onClick.AddListener(Next);
            previousButton.onClick.AddListener(Previous);
            shuffleButton.onClick.AddListener(Shuffle);
            
            UpdatePlayButton();
        }

        private async void Play()
        {
            if (_audioService.IsMusicPlaying)
            {
                _audioService.PauseMusic();
            }
            else
            {
               await _audioService.PlaySelectedSong();
            }
            
            UpdatePlayButton();
        }
        
        private void UpdatePlayButton()
        {
            if (_audioService.IsMusicPlaying)
            {
                playButtonText.text = "Pause";
            }
            else
            {
                playButtonText.text = "Play";
            }
        }
        
        private void UpdateSongName()
        {
            songNameText.text = _audioService.GetSongName();
        }

        private void Next() => _audioService.PlayNextSong().Forget();

        private void Previous() => _audioService.PlayPreviousSong().Forget();

        private void Shuffle()
        {
            _audioService.PlayRandomSong().Forget();
            UpdateSongName();
        }
    }
}