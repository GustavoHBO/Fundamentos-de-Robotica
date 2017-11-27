using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class ComunicacaoSerial : MonoBehaviour {

	///Definiçao da variavel velocidade
   public float velocidade = 10;
   //Definiçao da Porta COM que sera utilizada
   SerialPort porta = new SerialPort("COM3", 115200);

   // Use this for initialization
   void Start () {
      //Abertura da Porta COM para conexao com o Arduino
      porta.Open ();
      porta.ReadTimeout = 1;
   }

   // Update is called once per frame
   void Update () {
      	//Caso a porta COM responda positivamente estao executaremos o metodo Mover()
      	if(porta.IsOpen){

	      	/* Aqui abaixo é controlado o giro entorno do pivot */
			if(Input.GetKey(KeyCode.RightArrow)){// Verifica se a seta para a direita está pressionada.
	            girarBase(0);
	        /* Aqui abaixo é controlado o giro entorno do pivot */
			} else if(Input.GetKey(KeyCode.LeftArrow)){// Verifica se a seta para a esquerda está pressionada.
	            girarBase(1);
			}
      	}
  	}

   	void girarBase(int i){
   		int vel = 0;
   		while(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)){
	   		if(i == 0){
	   			vel += (int)(velocidade * Time.deltaTime);
	   			porta.Write("#19 p" + vel + " t500 \r \n");
	   		} else {

	   		}
	   	}
   	}
}
