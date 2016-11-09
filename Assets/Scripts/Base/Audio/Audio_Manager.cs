//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
//========================================================================
// ■ Audio_Manager
//------------------------------------------------------------------------
//	音管理クラス。
//========================================================================

public class Audio_Manager : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public static GameObject speaker;	// 音発生用スピーカー
	// 各種管理クラス
	public static BGM_Manager bgm;		// BGM
	public static BGS_Manager bgs;		// BGS
	public static ME_Manager me;		// ME
	public static SE_Manager se;		// SE
	public static Voice_Manager voice;	// 声
	//--------------------------------------------------------------------
	// ● 初期化（早）
	//--------------------------------------------------------------------
	void Awake() {
		// スピーカーが生成されていない場合
		if (!speaker) {
			// 音発生用スピーカーを作成し、ゲーム内に配置
			speaker = new GameObject("Audio_Speaker");
			// カメラ位置に合わせる
			speaker.transform.position = Camera.main.transform.position;
			DontDestroyOnLoad(speaker);		// シーン切り替え後も保存

			// 各種管理クラスを作成
			bgm		= new BGM_Manager();	// BGM
			bgs		= new BGS_Manager();	// BGS
			me		= new ME_Manager();		// ME
			se		= new SE_Manager();		// SE
			voice	= new Voice_Manager();	// 声
		}
	}
	//--------------------------------------------------------------------
	// ● 更新（遅）
	//--------------------------------------------------------------------
	void LateUpdate() {
		// カメラ位置に合わせる
		speaker.transform.position = Camera.main.transform.position;
	}
	//--------------------------------------------------------------------
	// ● 全消去
	//--------------------------------------------------------------------
	public static void all_clear() {
		// 各種管理クラスを消去
		bgm.clear();	// BGM
		bgs.clear();	// BGS
		me.clear();		// ME
		se.clear();		// SE
		voice.clear();	// 声
	}
}