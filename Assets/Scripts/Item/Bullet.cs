//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ Bullet
//------------------------------------------------------------------------
//	銃弾のクラス。
//========================================================================

public class Bullet : Base_Item {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public GameObject spark;
	new Rigidbody rigidbody;
	LineRenderer line;
	float next_set_position_second;
	ArrayList line_positions;
	int rebound_count;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	protected override void Start() {
		state = State.TRAP;
		is_hit_delete = false;
		
		base.Start();
		
		line_positions = new ArrayList();
		line = GetComponent<LineRenderer>();
		for (var i = 0; i < 2; i++)
			add_line_position();
		
		rigidbody = GetComponent<Rigidbody>();
		rigidbody.AddForce(
			transform.forward * 200 * rigidbody.mass, ForceMode.Impulse);

		Destroy(gameObject, 1);
	}
	//--------------------------------------------------------------------
	// ● 線位置を追加
	//--------------------------------------------------------------------
	void add_line_position() {
		line_positions.Add(transform.position);
		if (line_positions.Count > 2)	line_positions.RemoveAt(0);
		
		line.SetPositions(
			(Vector3[])line_positions.ToArray( typeof(Vector3) ) );
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	protected override void Update() {
		base.Update();
		
		if (next_set_position_second < Time.time) {
			next_set_position_second = Time.time + 0.1f;
			add_line_position();
		}
	}
	//--------------------------------------------------------------------
	// ● 衝突情報を受信
	//--------------------------------------------------------------------
	public override void send_data(ref Collision_Data data) {
		base.send_data(ref data);
		
		if (data.type != Collision_Data.Type.EXIT && !data.is_trigger) {
			if ( data.target.gameObject.layer ==
					LayerMask.NameToLayer("Ground") )
				create_effect();

			if (rebound_count >= 5)
				Destroy(gameObject);
			rebound_count++;
		}
	}
	//--------------------------------------------------------------------
	// ● 罠に掛かる
	//--------------------------------------------------------------------
	protected override void hit_trap() {
		base.hit_trap();

		if (status != null) {
			status.damage(
				rigidbody.velocity.magnitude / Time.deltaTime
				* rigidbody.mass);
			create_effect();
		}
	}
	//--------------------------------------------------------------------
	// ● エフェクトを作成
	//--------------------------------------------------------------------
	void create_effect() {
		Destroy(
			Instantiate(spark, transform.position, transform.rotation),
			1);
	}
}