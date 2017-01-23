/***********************************************************************************************************
 * Produced by App Advisory - http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/




using UnityEngine;
using System.Collections;

/// <summary>
/// Class in charge to manage the sound in the game
/// </summary>
namespace BaiyiGame.JumpMrQ
{
	public class SoundManager : MonoBehaviorHelper
	{
        public static SoundManager Instance;

        public AudioSource music;
		public AudioSource fx;

		public AudioClip musicGame;

		public AudioClip musicGameOver;
		public AudioClip jumpFX;
		public AudioClip coinFX;

        //点击按钮的音效
        public AudioClip ButtonMusicFX;
        //解锁人物音效
        public AudioClip unlockRoleFX;

        void Awake()
        {
            bool isFirst = PlayerPrefs.GetInt("isFirst", 0) != 1;
            if (isFirst)
            {
                PlayerPrefs.SetInt("isFirst", 1);
                PlayerPrefs.SetInt("isPlayGameMusic", 1);
                PlayMusicGame(isFirst);
            }
            else
            {
                PlayMusicGame(PlayerPrefs.GetInt("isPlayGameMusic", 0) == 1);
            }
            Instance = this;
        }

        void OnEnable()
		{
			PlayerData.OnSetDiamond += PlayCoinFX;
		}

		void OnDisable()
		{
            PlayerData.OnSetDiamond -= PlayCoinFX;
		}

		public void PlayMusicGame(bool isPlayGameMusic)
		{
            if (isPlayGameMusic)
            {
                PlayMusic(musicGame);
            }
            else
            {
                PlayMusic(null);
            }
		}

		public void PlayMusicGameOver()
		{
			playFX (musicGameOver);
		}

		public void PlayJumpFX()
		{
			playFX (jumpFX,0.5f);
		}

		private void PlayCoinFX(int p)
		{
			playFX (coinFX,1f);
		}

        public void PlayBtnFx()
        {
            playFX(ButtonMusicFX,10f);
        }
        public void PalyUnlockRoleFX()
        {
            playFX(unlockRoleFX,10f);
        }

		private void PlayMusic(AudioClip a)
		{
			if (music != null && music.clip != null)
				music.Stop ();


			music.clip = a;
			music.Play ();
		}

		private void playFX(AudioClip a)
		{
			playFX(a,1);
		}

		private void playFX(AudioClip a, float volumeScale )
		{
			if (fx != null && fx.clip != null)
				fx.Stop ();

			fx.PlayOneShot (a, volumeScale);
		}


		public void MuteAllMusic()
		{
			music.Pause();
			fx.Pause();
		}

		public void UnmuteAllMusic()
		{
			music.Play();
			fx.Play();
		}
	}
}