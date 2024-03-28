using _Assets.Scripts.Services.Audio;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs.InGame
{
    public class MusicPlayerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI songNameText;
        [SerializeField] private Button playButton;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button previousButton;
        [SerializeField] private Button shuffleButton;
        [Inject] private AudioService _audioService;

        private void Start()
        {
            playButton.onClick.AddListener(Toggle);
            pauseButton.onClick.AddListener(Toggle);
            nextButton.onClick.AddListener(Next);
            previousButton.onClick.AddListener(Previous);
            shuffleButton.onClick.AddListener(Shuffle);

            UpdatePlayButton();
            UpdateSongName();
        }

        private async void Toggle()
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
                playButton.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutSine);
                pauseButton.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutSine);
                UpdateEventSystemSelection(pauseButton.gameObject);
                var shuffleButtonNavigation = shuffleButton.navigation;
                shuffleButtonNavigation.selectOnLeft = pauseButton;
                shuffleButton.navigation = shuffleButtonNavigation;
                var previousButtonNavigation = previousButton.navigation;
                previousButtonNavigation.selectOnRight = pauseButton;
                previousButton.navigation = previousButtonNavigation;
            }
            else
            {
                playButton.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutSine);
                pauseButton.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutSine);
                UpdateEventSystemSelection(playButton.gameObject);
                var shuffleButtonNavigation = shuffleButton.navigation;
                shuffleButtonNavigation.selectOnLeft = playButton;
                shuffleButton.navigation = shuffleButtonNavigation;
                var prevButtonNavigation = previousButton.navigation;
                prevButtonNavigation.selectOnRight = playButton;
                previousButton.navigation = prevButtonNavigation;
            }
        }

        private void UpdateEventSystemSelection(GameObject gameObject)
        {
            if (EventSystem.current.currentSelectedGameObject.GetInstanceID() == playButton.gameObject.GetInstanceID() ||
                EventSystem.current.currentSelectedGameObject.GetInstanceID() == pauseButton.gameObject.GetInstanceID())
            {
                Debug.Log("Update");
                EventSystem.current.SetSelectedGameObject(gameObject);
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