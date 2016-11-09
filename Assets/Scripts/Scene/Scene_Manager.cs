//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.SceneManagement;
//========================================================================
// ■ Scene_Manager
//------------------------------------------------------------------------
//	場面管理クラス。
//========================================================================

public class Scene_Manager : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● 定数
	//--------------------------------------------------------------------
	public enum State {
		INITIALIZE,
		UPDATE,
		FINALIZE,
		END
	}
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	protected State state = State.INITIALIZE;
	protected string next_scene_name = "";
	Fade fade;
	//--------------------------------------------------------------------
	// ● 初期化（早）
	//--------------------------------------------------------------------
	void Awake() {
		Base_Scripts.get_instance();
	}
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	protected virtual void Start() {
		fade = Base_Scripts.get_instance().GetComponent<Fade>();
		fade.change_state(Fade.State.IN);
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	protected virtual void Update() {
		switch (state) {
			case State.INITIALIZE:	update_initialize();	break;
			case State.UPDATE:		update_update();		break;
			case State.FINALIZE:	update_finalize();		break;
			case State.END:			update_end();			break;
		}
	}
	//--------------------------------------------------------------------
	// ● 更新（初期化）
	//--------------------------------------------------------------------
	protected virtual void update_initialize() {
		if ( fade.is_finish(Fade.State.IN) )
			state = State.UPDATE;
	}
	//--------------------------------------------------------------------
	// ● 更新（更新）
	//--------------------------------------------------------------------
	protected virtual void update_update() {
		if (next_scene_name != "")
			state = State.FINALIZE;
	}
	//--------------------------------------------------------------------
	// ● 更新（終了）
	//--------------------------------------------------------------------
	protected virtual void update_finalize() {
		fade.change_state(Fade.State.OUT);
		if ( fade.is_finish(Fade.State.OUT) )
			state = State.END;
	}
	//--------------------------------------------------------------------
	// ● 更新（終）
	//--------------------------------------------------------------------
	protected virtual void update_end() {
		if (next_scene_name != "") {
			SceneManager.LoadScene(next_scene_name);
		} else {
			Application.Quit();
		}
	}
}