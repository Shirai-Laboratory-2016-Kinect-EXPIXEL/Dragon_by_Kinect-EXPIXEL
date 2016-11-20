//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;
//========================================================================
// ■ AI
//------------------------------------------------------------------------
//	人工知能のクラス。
//========================================================================

public abstract class AI : Mono_Behaviour_EX {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public float force = 20;
	public float torque = 20;
	[HideInInspector] public Vector3 force_direction;
	[HideInInspector] public Vector3 torque_direction;
	public float max_move_speed = 5;
	public float max_rotate_speed = 5;
	public float rotate_speed = 180;
	public float sight_distance = 10;
	public float sight_angle = 100;
	public Loitering_Points points;
	
	[HideInInspector] public Transform eye;
	[HideInInspector] public Transform center;
	[HideInInspector] public new Rigidbody rigidbody;
	[HideInInspector] public Animator animator;
	[HideInInspector] public State_Machine fsm;
	[HideInInspector] public AI_Status status;
	[HideInInspector] public Game_Manager game_manager;

	[HideInInspector] public Dictionary<string, AudioSource> ses;
	
// 不要物
	[HideInInspector] public string enemy_tag;
	//--------------------------------------------------------------------
	// ● 初期化（早）
	//--------------------------------------------------------------------
	protected virtual void Awake() {
		transform.SetParent(null);
	}
	//--------------------------------------------------------------------
	// ● 初期化
	//--------------------------------------------------------------------
	protected virtual void Start() {
		eye = transform.FindChild("Eye");
		center = transform.FindChild("Center");
		rigidbody = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		status = GetComponent<AI_Status>();

		game_manager = GameObject.FindWithTag("Scene_Manager")
			.GetComponent<Game_Manager>();

		ses = new Dictionary<string, AudioSource>();
		foreach ( var a in GetComponentsInChildren<AudioSource>() )
			ses[a.name] = a;

		fsm = new State_Machine( this, new State_Base_AI() );
		fsm.change( new State_Base_AI() );
	}
	//--------------------------------------------------------------------
	// ● 更新（定期）
	//--------------------------------------------------------------------
	protected virtual void FixedUpdate() {
		if (rigidbody == null)	return;

		
		// 加力方向は、各操舵の合力な為、正規化
		force_direction.Normalize();
		torque_direction.Normalize();
		// 剛体に加力
		rigidbody.AddForce(force_direction * force);
		rigidbody.AddTorque(torque_direction * torque);
		// 加力後は、加力方向を初期化
		force_direction = Vector3.zero;
		torque_direction = Vector3.zero;
		

		// 剛体の速度を範囲内に補正
		// 移動速度を補正
		var move_speed = rigidbody.velocity.magnitude;
		if (move_speed > max_move_speed) {
			rigidbody.velocity =
				rigidbody.velocity.normalized * max_move_speed;
		}
		// 回転速度を補正
		var rotate_speed = rigidbody.angularVelocity.magnitude;
		if (rotate_speed > max_rotate_speed) {
			rigidbody.angularVelocity =
				rigidbody.angularVelocity.normalized * max_rotate_speed;
		}
//		rigidbody.maxAngularVelocity = max_rotate_speed;
	}
	//--------------------------------------------------------------------
	// ● 更新
	//--------------------------------------------------------------------
	protected virtual void Update() {
		fsm.update();
	}
	//--------------------------------------------------------------------
	// ● ＩＫを補正
	//--------------------------------------------------------------------
	protected virtual void OnAnimatorIK() {
		fsm.update_ik();
	}
	//--------------------------------------------------------------------
	// ● 更新（遅）
	//--------------------------------------------------------------------
	protected virtual void LateUpdate() {
		fsm.late_update();
	}
	//--------------------------------------------------------------------
	// ● 衝突情報を送る
	//--------------------------------------------------------------------
	public virtual void send_collision_data(ref Collision_Data data) {
		fsm.send_collision_data(ref data);
	}
}