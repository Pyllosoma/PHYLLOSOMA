using Runtime.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

namespace Runtime.Managers
{
    #region Enums
    public enum NextSceneNames
    {
        MainScene,
        PlayerMovement,
    }

    public enum LoadTypes
    {
        New,
        Continue,
        Load
    }
    #endregion

    public class LoadManager : Singleton<LoadManager>
    {
        private string _nextSceneName = "PlayerMovement";
        public float _loadingProgress = 0f;

        [Header("Canvas Group--------------------------")]
        public GameObject fadeInOutGroup;
        public GameObject imageGroup;
        public GameObject videoGroup;

        [Header("Screen Group--------------------------")]
        public Image fadeImage;
        public RawImage imageScreen;
        public RawImage videoScreen;

        [Header("Video Player--------------------------")]
        public VideoPlayer videoPlayer;
        public List<VideoClip> videoClips = new List<VideoClip>();

        [Header("Image List--------------------------")]
        public List<Texture> images = new List<Texture>();

        [Header("Text Group--------------------------")]
        public TextMeshProUGUI fadeText;
        public TextMeshProUGUI imagedText_Progress;
        public TextMeshProUGUI imagedText_Done;
        public TextMeshProUGUI videoText_Progress;
        public TextMeshProUGUI videoText_Done;
        public Image videoProgressImage;

        [Header("Audio Source--------------------------")]
        public AudioSource audioPlayer;
        public List<AudioClip> audioClips = new List<AudioClip>();

        [Header("Settings--------------------------")]
        public float waitTime = 5f;
        public float fadeTime = 1f;
        public float fadeGroupSetDelay = 0.1f;

        [ContextMenu("Reset Fade Set")]
        public void ResetFadeSettings()
        {
            fadeImage.color = new Color(1f, 1f, 1f, 1f);
            fadeInOutGroup.SetActive(false);        
        }


        public void LoadNewGame(NextSceneNames nextScene)
        {
            OnVideoLoading();
        }

        public void LoadContinueGame(NextSceneNames nextScene)
        {
            OnDie();
        }

        public void LoadSavedGame(NextSceneNames nextScene)
        {
            OnImageLoading();
        }

        //이벤트 함수
        public void OnDie()
        {
            PlayAudio("Fade");
            StartCoroutine(FadeInOutCoroutineWithText(waitTime));
        }

        public void OnImageLoading()
        {
            PlayAudio("Image");
            StartCoroutine(ImageInOutCoroutineWithText(waitTime));
        }


        public void OnVideoLoading()
        {
            PlayAudio("Video");
            StartCoroutine(VideoInOutCoroutine(waitTime));
        }

        private void PlayAudio(string type)
        {
            switch (type)
            {
                case "Fade":
                    audioPlayer.clip = audioClips[Random.Range(0, audioClips.Count)];
                    audioPlayer.Play();
                    break;
                case "Image":
                    audioPlayer.clip = audioClips[Random.Range(0, audioClips.Count)];
                    audioPlayer.Play();
                    break;
                case "Video":
                    audioPlayer.clip = audioClips[Random.Range(0, audioClips.Count)];
                    audioPlayer.Play();
                    break;
            }
        }

        #region FadeInOut
        // 페이드 인 함수
        private void FadeIn()
        {
            StartCoroutine(FadeInCoroutine(waitTime));
        }

        // 페이드 아웃 함수
        private void FadeOut()
        {
            StartCoroutine(FadeOutCoroutine(waitTime));
        }

        // 페이드 인과 페이드 아웃 함수를 함께 호출
        private void FadeInOut()
        {
            StartCoroutine(FadeInOutCoroutine(waitTime));
        }

        // Image의 부모 GameObject를 활성화하는 함수
        private void EnableFadeGroup()
        {
            fadeInOutGroup.SetActive(true);
        }

        // Image의 부모 GameObject를 비활성화하는 함수
        private void DisableFadeGroup()
        {
            fadeInOutGroup.SetActive(false);
        }

        IEnumerator FadeInCoroutine(float waitTime)
        {
            // 맨 첫 줄에 EnableCanvas 호출 전에 활성화 여부를 확인, 색상 확인
            if (!fadeInOutGroup.activeSelf) EnableFadeGroup();
            fadeImage.color = new Color(1f, 1f, 1f, 1f);


            float targetAlpha = 0f;
            float startAlpha = 1f;
            float elapsedTime = 0f;


            while (elapsedTime < waitTime)
            {
                elapsedTime += Time.deltaTime * fadeTime;
                float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime);
                fadeImage.color = new Color(0f, 0f, 0f, newAlpha);
                yield return null;
            }

            // 페이드 인이 끝나면 최종 색상을 설정
            fadeImage.color = new Color(0f, 0f, 0f, targetAlpha);

            // 약간의 지연시간 후 비활성화
            yield return new WaitForSeconds(fadeGroupSetDelay);

            // DisableCanvas 호출 여부 검사 및 호출
            if (fadeInOutGroup.activeSelf)
            {
                DisableFadeGroup();
            }
        }

        IEnumerator FadeOutCoroutine(float waitTime)
        {
            // 맨 첫 줄에 EnableCanvas 호출 전에 비활성화 여부를 확인, 색상 확인
            if (!fadeInOutGroup.activeSelf) EnableFadeGroup();
            fadeImage.color = new Color(1f, 1f, 1f, 0f);

            float targetAlpha = 1f;
            float startAlpha = 0f;
            float elapsedTime = 0f;

            while (elapsedTime < waitTime)
            {
                elapsedTime += Time.deltaTime * fadeTime;
                float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime);
                fadeImage.color = new Color(0f, 0f, 0f, newAlpha);
                yield return null;
            }

            // 페이드 아웃이 끝나면 최종 색상을 설정
            fadeImage.color = new Color(0f, 0f, 0f, targetAlpha);

            // 약간의 지연시간 후 비활성화
            yield return new WaitForSeconds(fadeGroupSetDelay);

            // DisableCanvas 호출 여부 검사 및 호출
            if (fadeInOutGroup.activeSelf)
            {
                DisableFadeGroup();
            }
        }

        IEnumerator FadeInOutCoroutine(float waitTime)
        {
            yield return StartCoroutine(FadeOutCoroutine(waitTime));
            yield return StartCoroutine(FadeInCoroutine(waitTime));
        }

        IEnumerator FadeOutCoroutineWithText(float waitTime)
        {
            // 맨 첫 줄에 EnableCanvas 호출 전에 비활성화 여부를 확인, 색상 확인
            if (!fadeInOutGroup.activeSelf) EnableFadeGroup();
            if (!fadeText.gameObject.activeSelf) fadeText.gameObject.SetActive(true);
            fadeImage.color = new Color(1f, 1f, 1f, 0f);
            fadeText.color = new Color(1f, 1f, 1f, 0f);

            float targetAlpha = 1f;
            float startAlpha = 0f;
            float elapsedTime = 0f;

            while (elapsedTime < waitTime)
            {
                elapsedTime += Time.deltaTime * fadeTime;
                float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime);
                fadeImage.color = new Color(0f, 0f, 0f, newAlpha);
                fadeText.color = new Color(1f, 1f, 1f, newAlpha * 1f); //나중에 코루틴으로 글자만 지연시간 후 출력되게 바꾸기
                yield return null;
            }

            // 페이드 아웃이 끝나면 최종 색상을 설정
            fadeImage.color = new Color(0f, 0f, 0f, targetAlpha);
            fadeText.color = new Color(1f, 1f, 1f, targetAlpha);

            // 약간의 지연시간 후 비활성화
            yield return new WaitForSeconds(fadeGroupSetDelay);

            // DisableCanvas 호출 여부 검사 및 호출
            if (fadeInOutGroup.activeSelf)
            {
                DisableFadeGroup();
            }
        }

        IEnumerator FadeInCoroutineWithText(float waitTime)
        {
            // 맨 첫 줄에 EnableCanvas 호출 전에 활성화 여부를 확인, 색상 확인
            if (!fadeInOutGroup.activeSelf) EnableFadeGroup();
            if (!fadeText.gameObject.activeSelf) fadeText.gameObject.SetActive(true);
            fadeImage.color = new Color(1f, 1f, 1f, 1f);
            fadeText.color = new Color(1f, 1f, 1f, 1f);

            float targetAlpha = 0f;
            float startAlpha = 1f;
            float elapsedTime = 0f;


            while (elapsedTime < waitTime)
            {
                elapsedTime += Time.deltaTime * fadeTime;
                float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime);
                fadeImage.color = new Color(0f, 0f, 0f, newAlpha);
                fadeText.color = new Color(1f, 1f, 1f, newAlpha * 1f);
                yield return null;
            }

            // 페이드 인이 끝나면 최종 색상을 설정
            fadeImage.color = new Color(1f, 1f, 1f, targetAlpha);

            // 약간의 지연시간 후 비활성화
            yield return new WaitForSeconds(fadeGroupSetDelay);

            // DisableCanvas 호출 여부 검사 및 호출
            if (fadeInOutGroup.activeSelf)
            {
                DisableFadeGroup();
            }
        }

        IEnumerator FadeInOutCoroutineWithText(float waitTime)
        {
            yield return StartCoroutine(FadeOutCoroutineWithText(waitTime));
            yield return StartCoroutine(FadeInCoroutineWithText(waitTime));
        }
        #endregion

        #region LoadingImage
        private void DisableImageGroup()
        { 
            imageGroup.SetActive(false);
        }

        private void EnableImageGroup()
        {
            imageGroup.SetActive(true);
        }

        IEnumerator ImageOutCoroutineWithText(float waitTime)
        {
            // 맨 첫 줄에 EnableCanvas 호출 전에 비활성화 여부를 확인, 색상 확인
            if (!imageGroup.activeSelf) EnableImageGroup();
            imageScreen.color = new Color(1f, 1f, 1f, 0f);
            imagedText_Progress.color = new Color(1f, 1f, 1f, 0f);

            float targetAlpha = 1f;
            float startAlpha = 0f;
            float elapsedTime = 0f;

            while (elapsedTime < waitTime)
            {
                elapsedTime += Time.deltaTime * fadeTime;
                float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime);
                imageScreen.color = new Color(1f, 1f, 1f, newAlpha);
                imagedText_Progress.color = new Color(1f, 1f, 1f, newAlpha * 1f); //나중에 코루틴으로 글자만 지연시간 후 출력되게 바꾸기
                yield return null;
            }

            // 페이드 아웃이 끝나면 최종 색상을 설정
            imageScreen.color = new Color(1f, 1f, 1f, targetAlpha);
            imagedText_Progress.color = new Color(1f, 1f, 1f, targetAlpha);

            // 약간의 지연시간 후 비활성화
            yield return new WaitForSeconds(fadeGroupSetDelay);

            // DisableCanvas 호출 여부 검사 및 호출
            if (imageGroup.activeSelf)
            {
                DisableImageGroup();
            }
        }

        IEnumerator ImageInCoroutineWithText(float waitTime)
        {
            // 맨 첫 줄에 EnableCanvas 호출 전에 활성화 여부를 확인, 색상 확인
            if (!imageGroup.activeSelf) EnableImageGroup();
            imageScreen.color = new Color(1f, 1f, 1f, 1f);
            imagedText_Progress.color = new Color(1f, 1f, 1f, 1f);

            float targetAlpha = 0f;
            float startAlpha = 1f;
            float elapsedTime = 0f;


            while (elapsedTime < waitTime)
            {
                elapsedTime += Time.deltaTime * fadeTime;
                float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime);
                imageScreen.color = new Color(1f, 1f, 1f, newAlpha);
                imagedText_Progress.color = new Color(1f, 1f, 1f, newAlpha * 1f);
                yield return null;
            }

            // 페이드 인이 끝나면 최종 색상을 설정
            imageScreen.color = new Color(1f, 1f, 1f, targetAlpha);
            imagedText_Progress.color = new Color(1f, 1f, 1f, targetAlpha);

            // 약간의 지연시간 후 비활성화
            yield return new WaitForSeconds(fadeGroupSetDelay);

            // DisableCanvas 호출 여부 검사 및 호출
            if (imageGroup.activeSelf)
            {
                DisableImageGroup();
            }
        }

        IEnumerator ImageInOutCoroutineWithText(float waitTime)
        {
            yield return StartCoroutine(ImageOutCoroutineWithText(waitTime));
            yield return StartCoroutine(ImageInCoroutineWithText(waitTime));
        }
        #endregion

        #region LoadingVideo
        private void DisableVideoGroup()
        {
            videoGroup.SetActive(false);
        }

        private void EnableVideoGroup()
        {
            videoGroup.SetActive(true);
        }

        IEnumerator PlayVideoCoroutine(float waitTime)
        {
            yield return null;

            // 맨 첫 줄에 EnableCanvas 호출 전에 비활성화 여부를 확인, 색상 확인
            if (!videoGroup.activeSelf) EnableVideoGroup();

            videoPlayer.Play();
            StartCoroutine(AsyncLoadSceneProgress());
            StartCoroutine(LoadingBarAnim());
        }

        IEnumerator LoadingBarAnim()
        {
            yield return new WaitForSeconds(0.2f);
            while (_loadingProgress < 1f)
            {
                videoProgressImage.fillAmount = _loadingProgress;
                yield return null;
            }
            videoProgressImage.fillAmount = _loadingProgress;

            // 알파값 왔다갔다하는 애니메이션
            float alphaSpeed = 0.7f; // 알파값 변화 속도
            float minAlpha = 0.2f;
            float maxAlpha = 1.0f;
            bool increasing = true;
            while (videoProgressImage.fillAmount >= 1)
            {
                // 알파값을 왔다갔다하는 애니메이션
                float newAlpha = videoProgressImage.color.a + (increasing ? alphaSpeed : -alphaSpeed) * Time.deltaTime;
                newAlpha = Mathf.Clamp(newAlpha, minAlpha, maxAlpha);
                videoProgressImage.color = new Color(videoProgressImage.color.r, videoProgressImage.color.g, videoProgressImage.color.b, newAlpha);

                // 알파값이 최솟값 또는 최댓값에 도달하면 방향을 변경
                if (newAlpha <= minAlpha || newAlpha >= maxAlpha)
                {
                    increasing = !increasing;
                }

                yield return null;
            }

        }

        IEnumerator AsyncLoadSceneProgress()
        {
            yield return null;

            AsyncOperation op = SceneManager.LoadSceneAsync(_nextSceneName);
            op.allowSceneActivation = false;

            float timer = 0f;
            float duration = 3f;
            while (!op.isDone)
            {
                if (op.progress < 0.9f)
                {
                    _loadingProgress = op.progress;
                }
                else
                {
                    timer += Time.unscaledDeltaTime;
                    _loadingProgress = Mathf.Lerp(0.91f, 1f, timer / duration);
                    if (_loadingProgress >= 1f)
                    {
                        Debug.Log("Done!!!!!!");
                    }
                    if (_loadingProgress >= 1f & Input.anyKeyDown)
                    {
                        op.allowSceneActivation = true;
                        StartCoroutine(FadeInOutCoroutine(waitTime));
                        yield return new WaitForSeconds(1f);
                        if (videoPlayer.frame > 0) videoPlayer.Stop();
                        DisableVideoGroup();
                        yield break;
                    }

                }
                yield return null;
            }

        }

        IEnumerator VideoInOutCoroutine(float waitTime)
        {
            yield return StartCoroutine(FadeOutCoroutine(waitTime));
            yield return StartCoroutine(PlayVideoCoroutine(waitTime));
        }
        #endregion

        /*
        public void ScreenFadeInOut()
        {
            InitializeFadeGroup();
            PlayEffectSound();
            StartCoroutine(ScreenFadeRoutine());
        }


        public void PlayEffectSound()
        {
            int ranClip = Random.Range(0, AudioClips.Count);
            AudioPlayer.clip = AudioClips[ranClip];
            AudioPlayer.Play();
        }
        `
        private void InitializeFadeGroup()
        {
            TurnOffAllGroups();
            FadeInOutGroup.SetActive(true);
            if (FadeScreen.gameObject.activeSelf == false) FadeScreen.gameObject.SetActive(true);
            if (FadeText.gameObject.activeSelf == true) FadeText.gameObject.SetActive(false);
        }

        private void TurnOffAllGroups()
        {
            if (FadeInOutGroup.activeSelf) FadeInOutGroup.SetActive(false);
            if (ImageGroup.activeSelf) ImageGroup.SetActive(false);
            if (VideoGroup.activeSelf) VideoGroup.SetActive(false);
        }

        private IEnumerator ScreenFadeRoutine()
        {
            if (FadeScreen.color.a >= 0f) FadeScreen.color = new Color(FadeScreen.color.r, FadeScreen.color.g, FadeScreen.color.b, 0);

            yield return StartCoroutine(ScreenFadeInRoutine());
            yield return StartCoroutine(ScreenFadeOutRoutine());

            if (FadeScreen.color.a >= 0f) FadeScreen.color = new Color(FadeScreen.color.r, FadeScreen.color.g, FadeScreen.color.b, 0);
            FadeInOutGroup.SetActive(false);
        }

        private IEnumerator ScreenFadeInRoutine()
        {
            FadeScreen.color = new Color(FadeScreen.color.r, FadeScreen.color.g, FadeScreen.color.b, 0);
            float timer = 0f;
            while (timer < FadeDuration)
            {
                FadeScreen.color = new Color(FadeScreen.color.r, FadeScreen.color.g, FadeScreen.color.b, timer / FadeDuration);
                timer += Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator ScreenFadeOutRoutine()
        {
            //FadeInOutGroup.SetActive(true);
            //FadeScreen.color = new Color(FadeScreen.color.r, FadeScreen.color.g, FadeScreen.color.b, 1);
            float timer = FadeDuration;
            while (timer > 0)
            {
                FadeScreen.color = new Color(FadeScreen.color.r, FadeScreen.color.g, FadeScreen.color.b, timer / FadeDuration);
                timer -= Time.deltaTime;
                yield return null;
            }
        }

        public void SwitchLoadSceneName(NextSceneNames nextScene)
        {
            //Save Next Stage's Scene Name
            _nextSceneName = nextScene.ToString();

            //Load LoadingScene
            //SceneManager.LoadScene(LoadSceneNames.LoadingScene.ToString());
        }

        public void StartAsyncLoadForNextScene() 
        {
            //Initialize
            if(_loadingProgress > 0f) _loadingProgress = 0f;

            //Start Loading
            StartCoroutine(AsyncLoadSceneProgress());
        }


        private void PlayVideo()
        {
            ReadyToMoveNextScene = false;
            VideoGroup.SetActive(true);
            VideoPlayer.Play();
            VideoPlayer.loopPointReached += VideoEndEvent;
            StartCoroutine(LoadingAnimForVideo());
        }

        private IEnumerator LoadingAnimForVideo()
        {
            VideoProgressImage.gameObject.SetActive(true);

            while (_loadingProgress <= 1f)
            {
                Debug.Log("필어마운트 동작중");
                VideoProgressImage.fillAmount = _loadingProgress;
                yield return null;
            }

            float timer = 0f;
            bool uptoggle = false;
            Debug.Log("hello");
            while (!ReadyToMoveNextScene)
            {
                Debug.Log($"{timer}, {uptoggle}");
                if (timer <= 0.2f) uptoggle = true;
                if (timer >= 1f) uptoggle = false;

                if (uptoggle)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    timer -= Time.deltaTime;
                }

                VideoProgressImage.color = new Color(VideoProgressImage.color.r, VideoProgressImage.color.g, VideoProgressImage.color.b, timer);
                yield return null;
            }
        }

        private void VideoEndEvent(VideoPlayer vip)
        {
            Debug.Log("비디오 끝");
            ReadyToMoveNextScene = true;
        }

        IEnumerator AsyncLoadSceneProgress()
        {
            yield return null;

            AsyncOperation op = SceneManager.LoadSceneAsync(_nextSceneName);
            op.allowSceneActivation = false;
            float timer = 0f;
            float duration = 3f;
            while (!op.isDone)
            {
                Debug.Log(_loadingProgress);
                if (op.progress < 0.9f)
                {
                    _loadingProgress = op.progress;
                }
                else
                {
                    timer += Time.unscaledDeltaTime;    
                    _loadingProgress = Mathf.Lerp(0.91f, 1f, timer / duration);
                    if (_loadingProgress >= 1f & Input.anyKeyDown)
                    {
                        ReadyToMoveNextScene = true;
                        VideoPlayer.Stop();
                        ScreenFadeInOut();
                        op.allowSceneActivation = true;
                        yield break;
                    }

                }
                yield return null;
            }
        }
        */
    }
}
