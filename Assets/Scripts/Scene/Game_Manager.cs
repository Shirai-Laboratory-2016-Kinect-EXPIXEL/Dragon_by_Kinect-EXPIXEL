//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
//========================================================================
// ■ Game_Manager
//------------------------------------------------------------------------
//	ゲーム管理クラス。
//========================================================================

public class Game_Manager : Scene_Manager {
	//--------------------------------------------------------------------
	// ● 定数
	//--------------------------------------------------------------------
	// ゲーム状態
	public enum Game_State {
		START,
		GAME,
		CLEAR,
		OVER
	}
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	[HideInInspector] public Game_State game_state = Game_State.START;
	float start_second = 5;
	float end_second = 5;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	protected override void Start() {
		base.Start();

		start_second += Time.time;
		
		// 各種音を読み込み
		Audio_Manager.bgm.load("hureai");
		Audio_Manager.bgm.load("nyusuityu");
		Audio_Manager.bgs.load("kaze");
		Audio_Manager.bgs.load("suityu");
		Audio_Manager.me.load("Fanfare");

		// ＢＧＳを再生
		Audio_Manager.bgs.set_volume(0.2f);
		Audio_Manager.bgs.play("kaze");
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	protected override void Update() {
		base.Update();
	}
	//--------------------------------------------------------------------
	// ● 更新（更新）
	//--------------------------------------------------------------------
	protected override void update_update() {
		base.update_update();


		if ( Input.GetKeyDown(KeyCode.Backspace) )
			next_scene_name = SceneManager.GetActiveScene().name;

		switch (game_state) {
			case Game_State.START:	update_start();	break;
			case Game_State.GAME:	update_game();	break;
			case Game_State.CLEAR:	update_clear();	break;
			case Game_State.OVER:	update_over();	break;
		}
		
		Debug_EX.add("Game\t :" + game_state);
/*
		if (Input.GetKeyDown(KeyCode.F12)) {
			next_scene_name = "Title";
		}
*/
	}
	//--------------------------------------------------------------------
	// ● 更新（開始）
	//--------------------------------------------------------------------
	void update_start() {
		if ( start_second < Time.time || Input.GetKeyDown(KeyCode.Space) )
			game_state = Game_State.GAME;
	}
	//--------------------------------------------------------------------
	// ● 更新（ゲーム）
	//--------------------------------------------------------------------
	void update_game() {
	}
	//--------------------------------------------------------------------
	// ● 更新（クリア）
	//--------------------------------------------------------------------
	void update_clear() {
//		if (end_second == 5)	end_second += Time.time;
//		if (end_second < Time.time)
			next_scene_name = "Ending";
	}
	//--------------------------------------------------------------------
	// ● 更新（失敗）
	//--------------------------------------------------------------------
	void update_over() {
		if (end_second == 5)	end_second += Time.time;

//		if ( end_second < Time.time || Input.GetMouseButtonDown(0) )
		if ( Input.GetMouseButtonDown(0) )
			next_scene_name = SceneManager.GetActiveScene().name;
	}
	//--------------------------------------------------------------------
	// ● 更新（終了）
	//--------------------------------------------------------------------
	protected override void update_end() {
		var status =
			GameObject.FindWithTag("Dragon")
			.GetComponent<AI_Status>();
		var save = Base_Scripts.get_instance()
			.GetComponent<Save_Data>();
		if (next_scene_name == "Ending") {
//			save.score = status.score;
//			save.item_unused = status.item_unused;
//			save.cleaning_evaluation = status.cleaning_evaluation;
		}
//		save.scene_name = SceneManager.GetActiveScene().name;


		base.update_end();
	}
}