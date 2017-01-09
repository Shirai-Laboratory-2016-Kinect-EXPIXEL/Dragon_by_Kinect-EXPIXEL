//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//========================================================================
// ■ Movie
//------------------------------------------------------------------------
//	動画のクラス。
//========================================================================

public class Movie : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public string movie_name;
	new AudioSource audio;
	ExPixel_Changer ex_pixel;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	void Start () {
		audio = GetComponent<AudioSource>();
		ex_pixel = GameObject.FindWithTag("ExPixel_Changer")
			.GetComponent<ExPixel_Changer>();

		StartCoroutine( moviePlay(movie_name) );
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	void Update () {
		// 動画テクスチャでは、何故か3Dサウンドにならない
		audio.volume = ex_pixel.state == ExPixel_Changer.State.NEWS ?
			1 : 0;
	}
	//--------------------------------------------------------------------
	// ● 動画再生
	//--------------------------------------------------------------------
	private IEnumerator moviePlay(string movie_name) {
		string movieTexturePath =
			Application.dataPath + "/Movies/" + movie_name;
		string url = "file://" + movieTexturePath;
		WWW movie = new WWW(url);

		while (!movie.isDone) {
			yield return null;
		}

		MovieTexture movieTexture = movie.movie;

		while (!movieTexture.isReadyToPlay) {
			yield return null;
		}

		var image = GetComponent<RawImage>();
		image.texture = movieTexture;

		movieTexture.loop = true;
		movieTexture.Play();
		
		var audioSource = GetComponent<AudioSource>();
		audioSource.clip = movieTexture.audioClip;
		audioSource.loop = true;
		audioSource.Play();
	}
}