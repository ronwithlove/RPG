using UnityEngine;
using System.Collections;

public class CharacterCreation : MonoBehaviour {

	public GameObject[] characterPrefabs;
	private GameObject[] characterGameObjects;
	private int length;
	private int selectCharacterIndex=0;
	public UIInput inputName;

	// Use this for initialization
	void Start () {
		length=characterPrefabs.Length;//获得赋予characterPrefab的长度
		characterGameObjects = new GameObject[length];//新建一个和characterPrefab同样长度的GameObject数组

		for(int i=0;i<length;i++){
			characterGameObjects[i]=GameObject.Instantiate(characterPrefabs[i],transform.position,transform.rotation) as GameObject;
			//这里transorom.rotation,不用Quaternion.identity因为CharacterCreation被我转了180度。
			//这里还要强转为GameObject,Instantiate的是Object,赋值的变量characterGameObject是GameObject类型。
		}//实例化两个角色
		showCharacter();
	}
	
	// Update is called once per frame
	void Update () {
		///showCharacter();

	}

	void showCharacter(){
		for(int i=0;i<length;i++){
			if(i==selectCharacterIndex){
				characterGameObjects[i].SetActive(true);
			}else{
				characterGameObjects[i].SetActive(false);
			}
		}

	}


	public void onNextClick(){  //别忘了public一下
		selectCharacterIndex++;
		selectCharacterIndex%=length;// 当被除数等于除数的时候余数就为0，用这个方法来设selectCharacterIndex超过最大长度时候又从0开始往上加
		showCharacter();
	}

	public void onPrevClick(){
		selectCharacterIndex--;
		if(selectCharacterIndex<0) { //最小的0接下去就是最大的那个数。
			selectCharacterIndex=length-1;
		}
		showCharacter();
	}

	public void OnOkClick(){
		PlayerPrefs.SetInt("CharacterIndex",selectCharacterIndex);
		PlayerPrefs.SetString("PlayerName", inputName.value);// 别忘了.value

	}
}
