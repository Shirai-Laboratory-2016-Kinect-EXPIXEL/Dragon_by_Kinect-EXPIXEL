//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ Item_Drone
//------------------------------------------------------------------------
//	ドローンのアイテムクラス。
//========================================================================

public class Item_Drone : Base_Item {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	Player player;
	UI_Talk talk;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	protected override void Start() {
		is_get_delete = false;
		is_use_collision = false;
		light_color = Color.black;

		base.Start();

		player = GameObject.FindWithTag("Player").GetComponent<Player>();
		entity = GetComponent<Entity>();
		talk = GameObject.FindWithTag("UI_Talk").GetComponent<UI_Talk>();
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	protected override void Update() {
		base.Update();
		
		if (talk.ai == this && talk.is_event) {
			var delta =
				player.center.transform.position - transform.position;
			if (delta.magnitude > 3)
				talk.apply_end_event();
		}
	}
	//--------------------------------------------------------------------
	// ● 取得
	//--------------------------------------------------------------------
	public override void get() {
		base.get();

		if (!player.fsm.is_death() && !talk.is_event) {
			if ( !entity.fsm.is_death() )
				talk.start_event("ショートさせますか？", true, this);
			else {
				talk.start_event(
					"旧式のドローン・・・一体何故こんな場所に・・・？",
					false, this);
			}
		}
	}
	//--------------------------------------------------------------------
	// ● ボタン押下コールバック関数
	//--------------------------------------------------------------------
	public override void on_click(GameObject button) {
		base.on_click(button);
		

		switch (button.name) {
			case "Button_Yes":
				entity.status.death();
				talk.end_event();
				break;
			case "Button_No":
				talk.end_event();
				break;
			case "Button_Back_Ground":
				talk.end_event();
				break;
		}
	}
}