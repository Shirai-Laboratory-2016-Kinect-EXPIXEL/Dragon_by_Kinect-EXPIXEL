//------------------------------------------------------------------------
// ● 使用ライブラリの宣言
//------------------------------------------------------------------------
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//========================================================================
// ■ Item_Manager
//------------------------------------------------------------------------
//	アイテムの総合データクラス。
//========================================================================

public class Item_Manager : Text_Manager {
	//--------------------------------------------------------------------
	// ● メンバ変数
	//--------------------------------------------------------------------
	public Dictionary<string, Item> items;		// アイテムデータ
	int next_random_index;
	//--------------------------------------------------------------------
	// ● データ取得
	//--------------------------------------------------------------------
	public Item get(string name) {
		return items.ContainsKey(name) ? items[name] : null;
	}
	//--------------------------------------------------------------------
	// ● ランダムにデータ取得
	//--------------------------------------------------------------------
	public Item get_random() {
///*
		var i = next_random_index;
		next_random_index =
			(int)Mathf.Repeat(next_random_index + 1, items.Count);
//*/
//		var i = UnityEngine.Random.Range(0, items.Count);
		return items.ElementAt(i).Value;
	}
	//--------------------------------------------------------------------
	// ● コンストラクタ
	//--------------------------------------------------------------------
	public Item_Manager() {
		start_index = 3;	// 開始要素位置
		base.initialize();

		// アイテムデータを作成
		items = new Dictionary<string, Item>();

		path += "Item/";	// フォルダ階層を設定

		load();
//		initialize_test();
	}
	//--------------------------------------------------------------------
	// ● 初期化（テスト）
	//--------------------------------------------------------------------
	void initialize_test() {
		foreach (KeyValuePair<string, Item> pair in items) {
			Debug.Log(pair.Key);
			pair.Value.print();
		}
	}
	//--------------------------------------------------------------------
	// ● 文章の読み込み
	//--------------------------------------------------------------------
	override public void load(string name = null) {
		base.load(name);

		foreach (KeyValuePair<int, ArrayList> pair in datas) {
			if ( (string)pair.Value[0] != "file_name" ) {
				Item item = new Item(pair.Key, pair.Value);
				items[item.title] = item;
			}
		}
		base.clear();
	}
	//--------------------------------------------------------------------
	// ● 消去
	//--------------------------------------------------------------------
	override public void clear() {
		base.clear();
		items.Clear();
	}
}