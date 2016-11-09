//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
//========================================================================
// ■ Player
//------------------------------------------------------------------------
//	プレイヤーのクラス。
//========================================================================

public class Player : Entity {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public Transform[] right_arms;
	public CharacterJoint ragdoll_joint;
	public Transform gun;
	public GameObject bullet;
	[HideInInspector] public Transform weapon_aim;
	[HideInInspector] public new Main_Camera camera;
	[HideInInspector] public bool is_ragdoll;
	[HideInInspector] public int death_count;
	[HideInInspector] public Player_Tool tool;
	bool last_is_grounded;
	float last_ground_y;
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	protected override void Start() {
		base.Start();
		

		camera = Camera.main.GetComponent<Main_Camera>();
		
//		gun.parent.gameObject.SetActive(false);

		enemy_tag = "Entity";
		
		tool = new Player_Tool(this);
		tool.initialize();
		
		fsm.set_death( new State_Death_Player() );
//		fsm.change( new State_Controll_Player() );
		fsm.change( new State_Base_Player() );

		is_ragdoll = true;
		set_ragdoll(false);

		last_ground_y = transform.position.y;
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	protected override void Update() {
		base.Update();


		var move_direction = new Vector3(
			Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") );
		if (move_direction.magnitude > 1)	move_direction.Normalize();
		move_direction *= Input.GetKey(KeyCode.LeftShift) ? 2 : 1;

		animator.SetFloat("Move_X", move_direction.x);
		animator.SetFloat("Move_Z", move_direction.z);
		animator.SetFloat("Attack_X", move_direction.x);
		animator.SetFloat("Attack_Z", move_direction.z);
		animator.SetBool("Is_Attack", Input.GetKey(KeyCode.Z));
		animator.SetBool("Is_Sleep", Input.GetKey(KeyCode.X));
		animator.SetBool("Is_Death", Input.GetKey(KeyCode.C));
	}
	//--------------------------------------------------------------------
	// ● 更新（遅）
	//--------------------------------------------------------------------
	protected override void LateUpdate() {
		base.LateUpdate();
		

		// 死亡＆復活テスト
		if ( Input.GetKeyDown(KeyCode.P) ) {
			if ( !fsm.is_death() )
				status.death();
			else {
				status.recovery();
				fsm.change( new State_Controll_Player() );
			}
		}
		
		// 落下時の地面衝突のダメージを処理
		var is_grounded = true;
		if (is_grounded != last_is_grounded && is_grounded) {
			float delta_y = last_ground_y - transform.position.y - 1;
			delta_y = Mathf.Max(delta_y, 0);
//			status.damage( rigidbody.mass * 4 * Mathf.Pow(delta_y, 2) );
		}
		if (is_grounded)	last_ground_y = transform.position.y;

		last_is_grounded = is_grounded;

//		Debug_EX.add("delta_ground : " +
//			(last_ground_y - transform.position.y) );
	}
	//--------------------------------------------------------------------
	// ● 人形化を設定
	//--------------------------------------------------------------------
	public void set_ragdoll(bool is_use) {
		return;

		if (is_ragdoll != is_use) {
			is_ragdoll = is_use;

			// 全子の剛体を設定
			var rs = get_all_components<Rigidbody>();
			foreach (var r in rs)
				r.isKinematic = !is_use;

			// 全子の衝突を設定
			var cs = get_all_components<Collider>();
			foreach (var c in cs)
				c.enabled = is_use;
			// 本体衝突を設定
			GetComponent<Collider>().enabled = !is_use;

			// 本体と人形関節の結合を設定
			ragdoll_joint.connectedBody =
				is_use ? rigidbody : null;

			// アニメーションを設定
			animator.enabled = !is_use;
			
			// 死んだ振り中に変更
			( (Player_Status)status ).is_play_dead = is_ragdoll;
		}
	}
	//--------------------------------------------------------------------
	// ● 衝突情報を受信
	//--------------------------------------------------------------------
	public override void send_collision_data(ref Collision_Data data) {
		base.send_collision_data(ref data);
	}
	//--------------------------------------------------------------------
	// ● エディタ描画
	//--------------------------------------------------------------------
	void OnDrawGizmosSelected() {
	}
}