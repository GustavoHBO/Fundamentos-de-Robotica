using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class ComunicacaoSerial : MonoBehaviour {

	///Definiçao da variavel velocidade
   public float velocidade = 35;

   public string[][] posicoes = new string[10][6];

   public int posC = 0;
   public int posL = 0;

   public float posDBase = 1500;
   public float posDBraco = 1500;
   public float posDAntebraco = 1500;
   public float posDGarra = 750;
   public float posDGarraL = 1500;
   public float posDGarraAB = 750;

   public int limiteBase = 0;
   public int limiteBraco = 0;
   public int limiteAntebraco = 0;
   public int limiteGarra = 0;
   public int limiteGarraL = 0;
   public int limiteGarraAB = 0;
   //Definiçao da Porta COM que sera utilizada
   SerialPort porta;
   // Use this for initialization
   void Start () {
      //Abertura da Porta COM para conexao com o Arduino
      porta = new SerialPort("\\\\.\\COM10", 115200);

      porta.Open ();
      porta.ReadTimeout = 1;
      if(porta.IsOpen)
         posicaoInicial();
   }

   // Update is called once per frame
   void Update () {
   	//Caso a porta COM responda positivamente estao executaremos o metodo Mover()
	   	if(porta.IsOpen){

	      	girarBase();// Responsável pelo giro da base.
	         girarBraco();// Responsável pelo giro do braço.
	         girarAntebraco();// Responsável pelo giro do antebraço.
	         girarGarra();// Responsável pelo giro vertical da garra.
	         girarGarraL();// Responsável pelo giro horizontal da garra.
	         girarGarraAB();// Responsável pelo giro da ferramenta.
            gravarPosicao();
            executarPos();
	   	}
  	}

	void girarBase(){
      int vel = 0;
      if(Input.GetKey(KeyCode.RightArrow) && limiteBase < 50){
         	vel = (int)(posDBase + (limiteBase * velocidade));
         	limiteBase++;
      		porta.Write("#21 p" + vel + " t500 \r \n");
      } else if(Input.GetKey(KeyCode.LeftArrow)&& limiteBase > -50){
			vel -= (int)(posDBase + (limiteBase * velocidade));
      		limiteBase--;
      		porta.Write("#21 p" + vel + " t500 \r \n");
      }
   }

   void girarBraco(){
      int vel = 0;
	if(Input.GetKey(KeyCode.UpArrow) && limiteBraco < 60){
		vel = (int)(posDBraco + (limiteBraco * velocidade));
        limiteBraco++;
      	porta.Write("#20 p" + vel + " t500 \r \n");
   	} else if(Input.GetKey(KeyCode.DownArrow)&& limiteBraco > -60){
		vel -= (int)(posDBraco + (limiteBraco * velocidade));
        limiteBraco--;
      	porta.Write("#20 p" + vel + " t500 \r \n");
      }
   }

   void girarAntebraco(){
      int vel = 0;
      if(Input.GetKey("s") && limiteAntebraco < 40){
			vel = (int)(posDAntebraco + (limiteAntebraco * velocidade));
         	limiteAntebraco++;
      		porta.Write("#19 p" + vel + " t500 \r \n");
      } else if(Input.GetKey("w")&& limiteAntebraco > -50){
			vel -= (int)(posDAntebraco + (limiteAntebraco * velocidade));
         	limiteAntebraco--;
      		porta.Write("#19 p" + vel + " t500 \r \n");
      }
   }

   void girarGarra(){
      int vel = 0;
      if(Input.GetKey("e") && limiteGarra < 60){
			vel = (int)(posDGarra + (limiteGarra * velocidade));
         	limiteGarra++;
      		porta.Write("#18 p" + vel + " t500 \r \n");
      } else if(Input.GetKey("d")&& limiteGarra > -20){
			vel -= (int)(posDGarra + (limiteGarra * velocidade));
         	limiteGarra--;
      		porta.Write("#18 p" + vel + " t500 \r \n");
      }
   }

   void girarGarraL(){
      int vel = 0;
      if(Input.GetKey("r") && limiteGarraL < 60){
			vel = (int)(posDGarraL + (limiteGarraL * velocidade));
        	limiteGarraL++;
      		porta.Write("#17 p" + vel + " t500 \r \n");
      } else if(Input.GetKey("f")&& limiteGarraL > -60){
			vel -= (int)(posDGarraL + (limiteGarraL * velocidade));
         	limiteGarraL--;
      		porta.Write("#17 p" + vel + " t500 \r \n");
      }
   }

   void girarGarraAB(){
      int vel = 0;
      if(Input.GetKey("t") && limiteGarraAB < 60){
		vel = (int)(posDGarraAB + (limiteGarraAB * velocidade));
        limiteGarraAB++;
      	porta.Write("#16 p" + vel + " t500 \r \n");
      } else if(Input.GetKey("g")&& limiteGarraAB > -60){
			vel -= (int)(posDGarraAB + (limiteGarraAB * velocidade));
         	limiteGarraAB--;
      		porta.Write("#16 p" + vel + " t500 \r \n");
      }
   }

   void gravarPosicao(){
      if(Input.GetKey("p")){
         if(posC == 10){
            posC = 0;
            posL = 0;
         }
			pos[posC][posL++] = (string)("#16" + (int)(posDBase + (limiteBase * velocidade) + "t1000 \r \n"));
			pos[posC][posL++] = (string)("#17" + (int)(posDBraco + (limiteBraco * velocidade) + "t1000 \r \n"));
			pos[posC][posL++] = (string)("#18" + (int)(posDAntebraco + (limiteAntebraco * velocidade) + "t1000 \r \n"));
			pos[posC][posL++] = (string)("#19" + (int)(posDGarra + (limiteGarra * velocidade) + "t1000 \r \n"));
			pos[posC][posL++] = (string)("#20" + (int)(posDGarraL + (limiteGarraL * velocidade) + "t1000 \r \n"));
			pos[posC][posL++] = (string)("#21" + (int)(posDGarraAB + (limiteGarraAB * velocidade) + "t1000 \r \n"));
         posC++;
      }
   }

   void executarPos(){
         int i = 0;
         int j = 0;
      if(Input.GetKey("o")){
         for (i = 0; i < 10; i++){
            for (j = 0; j < 6; j++){
               porta.Write(pos[i][j]);// Coloca o pulso na posição inicial
            }
         }
      }
   }

   void posicaoInicial(){
      porta.Write("#16 p1500 t1000 \r \n");// Coloca o pulso na posição inicial
      porta.Write("#17 p1500 t1000 \r \n");// Coloca o pulso na posição inicial
      porta.Write("#18 p750 t1000 \r \n");// Coloca o pulso na posição inicial
      porta.Write("#19 p1500 t1000 \r \n");// Coloca o pulso na posição inicial
      porta.Write("#20 p1500 t1000 \r \n");// Coloca o pulso na posição inicial
      porta.Write("#21 p1500 t1000 \r \n");// Coloca o pulso na posição inicial
   }
}
