//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//========================================================================
// ■ ExPixel_Changer
//------------------------------------------------------------------------
//	ExPixelの変更クラス。
//========================================================================

public class ExPixel_Changer : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● 定数
	//--------------------------------------------------------------------
	public enum State {
		APPLICATION_CONNECT,
		NEWS,
		MAX
	}
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public AudioListener application_connect_audio_listener;
	public AudioListener news_audio_listener;
	public RenderTexture application_connect_texture;
	public RenderTexture news_texture;
	public RawImage view;
	public new Camera camera;
	[HideInInspector] public State state = State.APPLICATION_CONNECT;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	void Start() {
		change_state(State.APPLICATION_CONNECT);
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	void Update() {
		if ( Input.GetKeyDown(KeyCode.Alpha1) )
			change_state(State.APPLICATION_CONNECT);
		if ( Input.GetKeyDown(KeyCode.Alpha2) )
			change_state(State.NEWS);
	}
	//--------------------------------------------------------------------
	// ● 状態を変更
	//--------------------------------------------------------------------
	void change_state(State new_state = State.MAX) {
		state = new_state;
		if (state == State.MAX) {
			state = (State)(
				Mathf.Repeat( (int)state + 1, (int)State.MAX ) );
		}


		switch (state) {
			case State.APPLICATION_CONNECT:
				application_connect_audio_listener.enabled = true;
				news_audio_listener.enabled = false;
				view.texture = application_connect_texture;
				// 表示
				camera.cullingMask |=
					( 1 << LayerMask.NameToLayer("UI_Application_Connect") );
				// 非表示
				camera.cullingMask &=
					~( 1 << LayerMask.NameToLayer("UI_News") );
				break;

			case State.NEWS:
				application_connect_audio_listener.enabled = false;
				news_audio_listener.enabled = true;
				view.texture = news_texture;
				// 表示
				camera.cullingMask |=
					( 1 << LayerMask.NameToLayer("UI_News") );
				// 非表示
				camera.cullingMask &=
					~( 1 << LayerMask.NameToLayer("UI_Application_Connect") );
				break;
		}
	}
}