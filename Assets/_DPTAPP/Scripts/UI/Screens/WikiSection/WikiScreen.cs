using UnityEngine;
using DG.Tweening;
using Scripts.Resources;
namespace Scripts.UI.Screens
{
    public class WikiScreen : MonoBehaviour
    {
        [SerializeField] private Transform topic1Content;
        [SerializeField] private Transform topic2Content;
        [SerializeField] private Transform topic3Content;
        [SerializeField] private Transform topic4Content;
        [SerializeField] private Transform topic1Btn;
        [SerializeField] private Transform topic2Btn;
        [SerializeField] private Transform topic3Btn;
        [SerializeField] private Transform topic4Btn;
        [SerializeField] private Transform wikiScreen;
        private Vector2 originalPositionBtn1;
        private Vector2 originalPositionBtn2;
        private Vector2 originalPositionBtn3;
        private Vector2 originalPositionBtn4;
        private Sequence sequence;
        void OnEnable()
        {
            wikiScreen.gameObject.SetActive(true);
            PlayAnimation();
        }
        void PlayAnimation()
        {
            originalPositionBtn1 = topic1Btn.localPosition;
            originalPositionBtn2 = topic2Btn.localPosition;
            originalPositionBtn3 = topic3Btn.localPosition;
            originalPositionBtn4 = topic4Btn.localPosition;
            float screenHeight = Screen.height;
            topic1Btn.localPosition = new Vector2(topic1Btn.localPosition.x, -screenHeight);
            topic2Btn.localPosition = new Vector2(topic2Btn.localPosition.x, -screenHeight);
            topic3Btn.localPosition = new Vector2(topic3Btn.localPosition.x, -screenHeight);
            topic4Btn.localPosition = new Vector2(topic4Btn.localPosition.x, -screenHeight);

            sequence = DOTween.Sequence();
            sequence.Append(topic1Btn.DOLocalMoveY(originalPositionBtn1.y, 0.2f).SetEase(Ease.InCubic));
            sequence.AppendInterval(0.1f);
            sequence.Append(topic2Btn.DOLocalMoveY(originalPositionBtn2.y, 0.2f).SetEase(Ease.InCubic));
            sequence.AppendInterval(0.1f);
            sequence.Append(topic3Btn.DOLocalMoveY(originalPositionBtn3.y, 0.2f).SetEase(Ease.InCubic));
            sequence.AppendInterval(0.1f);
            sequence.Append(topic4Btn.DOLocalMoveY(originalPositionBtn4.y, 0.2f).SetEase(Ease.InCubic));
        }
        private void Start()
        {
            topic1Content.gameObject.SetActive(false);
            topic2Content.gameObject.SetActive(false);
            topic3Content.gameObject.SetActive(false);
            topic4Content.gameObject.SetActive(false);
        }
        public void OnTopic1Click()
        {
            topic1Content.gameObject.SetActive(true);
            wikiScreen.gameObject.SetActive(false);
        }
        public void OnTopic2Click()
        {
            topic2Content.gameObject.SetActive(true);
            wikiScreen.gameObject.SetActive(false);
        }
        public void OnTopic3Click()
        {
            topic3Content.gameObject.SetActive(true);
            wikiScreen.gameObject.SetActive(false);
        }
        public void OnTopic4Click()
        {
            topic4Content.gameObject.SetActive(true);
            wikiScreen.gameObject.SetActive(false);
        }
        public void OnCloseBtnClick()
        {
            topic1Content.gameObject.SetActive(false);
            topic2Content.gameObject.SetActive(false);
            topic3Content.gameObject.SetActive(false);
            topic4Content.gameObject.SetActive(false);
            wikiScreen.gameObject.SetActive(true);
            PlayAnimation();
        }
        public void CloseWikiScreen()
        {
            ScreenManager.Instance.ActivateScreen(ScreenTypes.MainMenu);
        }
    }
}