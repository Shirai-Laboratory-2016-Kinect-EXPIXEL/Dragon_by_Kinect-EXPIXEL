//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//========================================================================
// ■ UI_Talk
//------------------------------------------------------------------------
//	会話のＵＩクラス。
//========================================================================

public class UI_Talk : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public Text text;
	public GameObject back_ground;
	public GameObject yes;
	public GameObject no;
	[HideInInspector] public Mono_Behaviour_EX ai;
	bool is_question;
	[HideInInspector] public bool is_event;
	float apply_end_event_second;
	Player player;
	new Main_Camera camera;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	void Start() {
		player = GameObject.FindWithTag("Player").GetComponent<Player>();
		camera = Camera.main.GetComponent<Main_Camera>();

		apply_end_event();
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	void Update() {
		if (apply_end_event_second != 0 &&
			apply_end_event_second < Time.time)
		{
			apply_end_event();
		}
	}
	//--------------------------------------------------------------------
	// ● イベントを開始
	//--------------------------------------------------------------------
	public void start_event(string text, bool is_question,
							Mono_Behaviour_EX ai) {
		this.text.text = text;
		this.is_question = is_question;

		back_ground.SetActive(true);
		yes.SetActive(is_question);
		no.SetActive(is_question);

		this.ai = ai;

		is_event = true;
		apply_end_event_second = 0;
		
		if ( !player.fsm.is_death() )
			player.fsm.change( new State_Wait_Player(
				float.PositiveInfinity, float.PositiveInfinity) );
		if ( !camera.fsm.is_death() )
			camera.fsm.change( new State_Wait_Camera() );
	}
	//--------------------------------------------------------------------
	// ● イベント終了を予約
	//--------------------------------------------------------------------
	public void end_event() {
		if (apply_end_event_second < Time.time)
			apply_end_event_second = Time.time + 0.2f;
	}
	//--------------------------------------------------------------------
	// ● イベントを終了
	//--------------------------------------------------------------------
	public void apply_end_event() {
		text.text = "";
		back_ground.SetActive(false);
		yes.SetActive(false);
		no.SetActive(false);
		ai = null;
		is_event = false;
		apply_end_event_second = 0;
		
		if (!player.fsm.is_death() &&
			player.fsm.state() is State_Wait_Player)
		{
			player.fsm.change( new State_Controll_Player() );
		}
		if (!camera.fsm.is_death() &&
			camera.fsm.state() is State_Wait_Camera)
		{
			camera.fsm.change( new State_TPS_Camera() );
		}
	}
	//--------------------------------------------------------------------
	// ● ボタン押下コールバック関数
	//--------------------------------------------------------------------
	public override void on_click(GameObject button) {
		base.on_click(button);

		ai.on_click(button);
	}
}