using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

	public int lvl=1;
	public int hp=100;
	public int mp=100;
	public int coins=200;

	public int strength=20;
	public int defence=20;
	public int speed=20;
	public int str_puls=0;
	public int def_plus=0;
	public int speed_plus=0;
	public int remainPoints=0;

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
