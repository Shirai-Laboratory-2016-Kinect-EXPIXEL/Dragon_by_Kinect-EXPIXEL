//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
//========================================================================
// ■ Base_Scripts
//------------------------------------------------------------------------
//	基盤スクリプトのクラス。
//========================================================================

public class Base_Scripts : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● 定数
	//--------------------------------------------------------------------
	static int DEFAULT_FRAME_RATE = 60;
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	static Base_Scripts instance;	// 自身のインスタンス
	float next_check_fps_time;
	float frame_count;
	float fps;
	//--------------------------------------------------------------------
	// ● ゲッター
	//--------------------------------------------------------------------
	// 自身を取得
	public static Base_Scripts get_instance() {
		// 作成されていない場合
		if (instance == null) {
			GameObject go = Instantiate(
				Resources.Load<GameObject>("Base_Scripts") );
			DontDestroyOnLoad(go);	// 保存登録
			instance = go.GetComponent<Base_Scripts>(); // シングルトンに登録
		}
		return instance;
	}
	//--------------------------------------------------------------------
	// ● 初期化（早）
	//--------------------------------------------------------------------
	void Awake() {
// 反映されないので、コメント
//			Application.targetFrameRate = DEFAULT_FRAME_RATE;	// FPS を設定

		// マウスカーソルを非表示
//		Cursor.visible = false;
//		Cursor.lockState = CursorLockMode.Confined;
	}
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	void Start() {
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	void Update() {
		// ESC キーで終了
		if ( Input.GetKeyDown(KeyCode.Escape) )	Application.Quit();


		// ＦＰＳを計測
		if (next_check_fps_time < Time.time) {
			next_check_fps_time = Time.time + 1;
			fps = frame_count;
			frame_count = 0;
		}
		frame_count++;
		Debug_EX.add(Color.red);
		Debug_EX.add("FPS : " + fps);
		Debug_EX.add(Color.white);
	}
}