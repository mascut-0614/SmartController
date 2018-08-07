using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;

public class ConnectController : MonoBehaviour {
	TcpClient tcpclient=new TcpClient();
	public InputField add;
	public Button start;
	public Button Red;
	public Button Blue;
	public Button Up;
	public Button Down;
	public Button Right;
	public Button Left;
	public Slider chack;
	public bool Upflag=false;
	public bool Downflag=false;
	public bool Rightflag=false;
	public bool Leftflag=false;

	NetworkStream ns;


	public void Connect_start(){
		try{
			tcpclient.Connect (add.text, 8080);
			add.gameObject.SetActive (false);
			start.gameObject.SetActive (false);
			Red.gameObject.SetActive(true);
			Blue.gameObject.SetActive(true);
			Up.gameObject.SetActive(true);
			Down.gameObject.SetActive(true);
			Right.gameObject.SetActive(true);
			Left.gameObject.SetActive(true);
			chack.gameObject.SetActive(true);
			ns=tcpclient.GetStream();
		}catch{
			add.text="Failed";
		}
	}
	public void SendMes(string mes) {
		if (tcpclient.Connected)
		{
			System.Text.Encoding enc = System.Text.Encoding.UTF8;
			byte[] sendBytes = enc.GetBytes(mes + '\n');
			ns.Write(sendBytes, 0, sendBytes.Length);
		}
	}
	IEnumerator KeepConnect(){
		SendMes(chack.value.ToString()+"|");
		yield return new WaitForSeconds (0.1f);
	}
	public void RedButton(){
		SendMes ("Red|");
	}
	public void BlueButton(){
		SendMes ("Blue|");
	}
	public void UpDown(){
		Upflag = true;
	}
	public void UpUp(){
		Upflag = false;
	}
	public void DownDown(){
		Downflag = true;
	}
	public void DownUp(){
		Downflag = false;
	}
	public void RightDown(){
		Rightflag = true;
	}
	public void RightUp(){
		Rightflag = false;
	}
	public void LeftDown(){
		Leftflag = true;
	}
	public void LeftUp(){
		Leftflag = false;
	}
	void Update(){
		StartCoroutine ("KeepConnect");
		if (Upflag) {
			SendMes ("Up|");
		}
		if (Downflag) {
			SendMes ("Down|");
		}
		if (Rightflag) {
			SendMes ("Right|");
		}
		if (Leftflag) {
			SendMes ("Left|");
		}
	}
}
