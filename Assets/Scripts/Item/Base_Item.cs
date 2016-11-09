//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;
//========================================================================
// ■ Base_Item
//------------------------------------------------------------------------
//	基盤アイテムのクラス。
//========================================================================

public class Base_Item : Collision_Manager {
	//--------------------------------------------------------------------
	// ● 定数
	//--------------------------------------------------------------------
	// 状態
	public enum State {
		PLACEMENT,	// 配置
		TRAP		// 罠
	}
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	[HideInInspector] public State state = State.PLACEMENT;
	Renderer[] renderers;
	Dictionary<string, AudioSource> ses;
	protected Entity_Status status;
	protected Entity entity;
	protected Item_Info info;
	[HideInInspector] public string item_name = "";
	protected string se_near = "";
	protected string se_put = "";
	protected string se_get = "";
	protected string se_hit = "";
	protected bool is_get_delete = true;
	protected bool is_hit_delete = true;
	protected bool is_use_collision = true;
	[HideInInspector] public Transform origin;
	[HideInInspector] public Vector3 offset = Vector3.up;
	protected Color light_color = Color.yellow * 0.3f;
	Color color = Color.black;
	float radian;
	//--------------------------------------------------------------------
	// ● ＳＥを再生
	//--------------------------------------------------------------------
	void play_se(string name) {
		if ( ses.ContainsKey(name) )	ses[name].Play();
	}
	//--------------------------------------------------------------------
	// ● ＳＥを停止
	//--------------------------------------------------------------------
	void stop_se(string name) {
		if ( ses.ContainsKey(name) )	ses[name].Stop();
	}
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	protected override void Start() {
		base.Start();
		
		if (item_name == "")	item_name = name;

		renderers = transform.GetComponentsInChildren<Renderer>();

		ses = new Dictionary<string, AudioSource>();
		foreach ( var a in GetComponentsInChildren<AudioSource>() )
			ses[a.name] = a;

		info = GameObject.FindWithTag("Item_Info")
			.GetComponent<Item_Info>();

		origin = transform;

		switch (state) {
			case State.PLACEMENT:
				play_se(se_near);
				break;
			case State.TRAP:
				play_se(se_put);
				break;
		}
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	protected virtual void Update() {
		switch (state) {
			case State.PLACEMENT:	update_placement();	break;
			case State.TRAP:		update_trap();		break;
		}
		

		foreach (var r in renderers) {
			foreach (var m in r.materials) {
				m.SetColor("_EmissionColor", color);
			}
		}
	}
	//--------------------------------------------------------------------
	// ● 更新（配置）
	//--------------------------------------------------------------------
	protected virtual void update_placement() {
		radian = Mathf.Repeat(
			radian + Mathf.PI * Time.deltaTime,
			Mathf.PI * 2);
		color = Color.Lerp(light_color, Color.black,
			(Mathf.Sin(radian) + 1) / 2);
	}
	//--------------------------------------------------------------------
	// ● 更新（罠）
	//--------------------------------------------------------------------
	protected virtual void update_trap() {
		color = Color.black;
	}
	//--------------------------------------------------------------------
	// ● 衝突情報を送る
	//--------------------------------------------------------------------
	public override void send_data(ref Collision_Data data) {
		base.send_data(ref data);

		if (!is_use_collision)	return;

		if (data.type != Collision_Data.Type.EXIT && !data.is_trigger) {
			entity = data.target.GetComponent<Entity>();
			status = data.target.GetComponent<Entity_Status>();

			if ( entity != null && !entity.fsm.is_death() ) {
				switch (state) {
					case State.PLACEMENT:	get();		break;
					case State.TRAP:		hit_trap();	break;
				}
			}
		}
	}
	//--------------------------------------------------------------------
	// ● 取得
	//--------------------------------------------------------------------
	public virtual void get() {
		stop_se(se_near);
		play_se(se_get);
		set_status_item();
		delete();
	}
	//--------------------------------------------------------------------
	// ● ステータスにアイテムを設定
	//--------------------------------------------------------------------
	protected virtual void set_status_item() {
//		if (item_name != "")	status.item_name = item_name;
	}
	//--------------------------------------------------------------------
	// ● 罠に掛かる
	//--------------------------------------------------------------------
	protected virtual void hit_trap() {
		play_se(se_hit);
		delete();
	}
	//--------------------------------------------------------------------
	// ● 削除
	//--------------------------------------------------------------------
	protected virtual void delete() {
		switch (state) {
			case State.PLACEMENT:
				if (is_get_delete)	Destroy(gameObject);
				break;
			case State.TRAP:
				if (is_hit_delete)	Destroy(gameObject);
				break;
		}
	}
	//--------------------------------------------------------------------
	// ● 情報を表示
	//--------------------------------------------------------------------
	public virtual void visible_info() {
		info.visible(this);
	}
}