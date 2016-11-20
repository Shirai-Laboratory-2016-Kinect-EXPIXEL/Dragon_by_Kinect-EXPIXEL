using UnityEngine;
using System.Collections;

public class Test_Behavior : StateMachineBehaviour {

	// 開始時に実行
	public override void OnStateEnter(Animator animator,
										AnimatorStateInfo stateInfo,
										int layerIndex) {
	}

	// 終了時に実行
	public override void OnStateExit(Animator animator,
										AnimatorStateInfo stateInfo,
										int layerIndex) {
	}

	// スクリプト付きから遷移、開始時に実行
	public override void OnStateMachineEnter(Animator animator,
												int stateMachinePathHash) {
	}

	// スクリプト付きから遷移、終了時に実行
	public override void OnStateMachineExit(Animator animator,
											int stateMachinePathHash) {
	}

	// 更新
	public override void OnStateUpdate(Animator animator,
										AnimatorStateInfo stateInfo,
										int layerIndex) {
	}

	// MonoBehaviour.OnAnimatorMoveの直後に実行
	public override void OnStateMove(Animator animator,
										AnimatorStateInfo stateInfo,
										int layerIndex) {
	}
	
	// MonoBehaviour.OnAnimatorIKの直後に実行
	public override void OnStateIK(Animator animator,
									AnimatorStateInfo stateInfo,
									int layerIndex) {
	}
}