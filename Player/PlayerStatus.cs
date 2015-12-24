using UnityEngine;
using System.Collections;

public enum PlayerClass{
	Swordman,
	Magican
}
public class PlayerStatus : MonoBehaviour {

	public int lvl=1;
	public string playerName="施大宝";
	public int maxHP=100;
	public int maxMP=100;
	public float hp=100;//游戏时候的即时HP
	public float mp=100;
	public int coins=200;


	public int strength=20;
	public int defence=20;
	public int speed=20;
	public int str_puls=0;
	public int def_plus=0;
	public int speed_plus=0;
	public int remainPoints=0;

	public PlayerClass playerClass=PlayerClass.Magican;

	public void getCoins(int co){
		coins+=co;
	}

	public void StrengthPlusClick(){
		if (remainPoints>0){
			str_puls++;
			remainPoints--;
		}
	}
	public void DefencePlusClick(){
		if (remainPoints>0){
			def_plus++;
			remainPoints--;
		}
	}
	public void SpeedPlusClick(){
		if (remainPoints>0){
			speed_plus++;
			remainPoints--;
		}
	}
}
