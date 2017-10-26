#pragma config(Sensor, S3,		 lightSensor,					sensorLightActive)
#pragma config(Sensor, S1,		 touchSensor,		 sensorTouch)
//*!!Code automatically generated by 'ROBOTC' configuration wizard							 !!*//
#pragma platform(NXT)
#define speed 30
/*--------------------------------------------------------------------------------------------------------*\
|*																																																				*|
|*																					 - Line Tracker -																							*|
|*																						ROBOTC on NXT																								*|
|*																																																				*|
|*	This program allows your taskbot to follow a line in reverse.																					*|
|*																																																				*|
|*																				ROBOT CONFIGURATION																							*|
|*		NOTES:																																															*|
|*		1)	The Light Sensor is attached to the front of the robot.																					 *|
|*		2)	Be sure to take readings of your Light Sensor over the light and dark areas.	Once you have			*|
|*				the values, add them and divide by 2 to find your threshold.	Then, use your threshold as a			*|
|*				comparison in your program.																																			*|
|*																																																				*|
|*		MOTORS & SENSORS:																																										*|
|*		[I/O Port]							[Name]							[Type]							[Description]												*|
|*		Port B									motorB							NXT									Right motor													*|
|*		Port C									motorC							NXT									Left motor													*|
|*		Port 1									touchSensor					Touch Sensor				Front mounted												*|
|*		Port 3									lightSensor					Light Sensor				Front mounted												*|
\*--------------------------------------------------------------------------------------------------------*/
bool activate = false, eixoX = true;
void checkBTLinkConnected(){
	if (nBTCurrentStreamIndex >= 0)
		return; // An existing Bluetooth connection is present.
		setBluetoothRawDataMode();
		while (!bBTRawMode){
			// Wait for Bluecore to enter raw data mode
			wait1Msec(50);
		}
}

task Sender(){
	ubyte msg[4];
	int x = 0, y = 0;

	while (true){
		msg[0] = SensorValue(lightSensor);
		msg[1] = SensorValue(touchSensor);
		//se os motores forem resetados, os valores x e y devem ser tb
		if(nMotorEncoder(motorB)==0 && nMotorEncoder(motorC)==0){
		  x=0;
		  y=0;
		}
		if(eixoX){
		  x = ((nMotorEncoder(motorB)+nMotorEncoder(motorC))/2)/20.8092485549 -y;//resultado em cm
	  }else{
		  y = ((nMotorEncoder(motorB)+nMotorEncoder(motorC))/2)/20.8092485549 -x;//resultado em cm
	  }
		msg[2] = x;
		msg[3] = y;
		//envia
		nxtWriteRawBluetooth(msg, 4);
		wait1Msec(2);//delay necessario pra nao embaralhar os pacotes
	}
	return;
}

task Receiver(){
  int nNumbBytesRead;
	ubyte BytesRead[1];
  while (true){
    //faz uma leitura no buffer
	  nNumbBytesRead = nxtReadRawBluetooth(&BytesRead[0],  1);

		if (nNumbBytesRead == 0){//se nao tem nada, da um tempinho
			wait1Msec(10);
		}else if (nNumbBytesRead == 1 && BytesRead[0] == 'i'){//sinal de iniciar
		  nxtDisplayString(4, "Working");
		  activate =true;
		}else if (nNumbBytesRead == 1 && BytesRead[0] == 'p'){//sinal de parar
		  nxtDisplayString(4, "Idle");
		  activate =false;
		}
  }
}

task main(){
  checkBTLinkConnected();//inicia a conexao
  wait1Msec(50);// The program waits 50 milliseconds to initialize the light sensor.
  int i=0;
  int rote[3]= {-1,1,1};
  bool fromLeft =false;

  StartTask(Sender);//ativa o envio de dados
  StartTask(Receiver);//ativa a recepcao de dados
  while(true){if(activate){  //so executa se receber o sinal mudando activate pra true
    //sensor lendo preto
    if(SensorValue(lightSensor) < 45){//mantem direo reta
      motor[motorB] = speed;
      motor[motorC] = speed;
    //sensor lendo branco
    }else if (SensorValue(lightSensor) >= 45 && SensorValue(lightSensor) < 64){//branco
      if(fromLeft){//se veio da esquerda tenta a 1 correcao pra direita
	      motor[motorB] = speed-20;//direita
	      motor[motorC] = speed;
	      fromLeft =false;
	    }else{//se veio da direita tenta a 1 correcao pra esquerda
	      motor[motorB] = speed;//esquerda
	      motor[motorC] = speed-20;
	      fromLeft =true;
	    }
      wait1Msec(200);
      //se apos um tempo, continuar no branco
      if(SensorValue(lightSensor) >= 45 && SensorValue(lightSensor)< 64){//continua no branco
        if(fromLeft){//tenta pro lado oposto ao da 1 tentativa
		      motor[motorB] = speed-20;//vira para a direita
		      motor[motorC] = speed;
		      fromLeft =true;
		    }else{
		      motor[motorB] = speed;//vira para a esquerda
		      motor[motorC] = speed-20;
		      fromLeft =false;
		    }
		    wait1Msec(500);
		    //se apos outro intervalo de tempo continuar no branco, chegamos em uma curva
		    if(SensorValue(lightSensor) >= 45 && SensorValue(lightSensor) < 64){
		      //curva, rotaciona ate achar preto ou prata
		      while(SensorValue(lightSensor) >= 45 && SensorValue(lightSensor)< 64){
		        //o sentido da rotacao eh dado pelo vetor rote
		        motor[motorB] = rote[i]*speed;//curva
		        motor[motorC] = (-1)*rote[i]*speed;
		      }
		      i++;
		      if(i==1||i==3||i==4||i==6||i==8)//sabemos o eixo pela proxima curva (definidos com base no mapa)
		        //se a proxima curva for a 1, 3.. estamos andando no eixo Y
		        eixoX=false;
		      else
		        //se nao estamos andando no eixo X
		        eixoX=true;
		      if(i==17){//ultima curva
		        //zerar variaveis
		        i=0;
		        nMotorEncoder(motorB) = 0;
	          nMotorEncoder(motorC) = 0;
	          activate = false;
		      }
		    }
      }
    }
  }}
  //========================================================
  // codigo pra checar a odometria, faz andar em linha reta por 1,60m
  //wait1Msec(50);// The program waits 50 milliseconds to initialize the light sensor.
  // wait1Msec(1000);
  // motor[motorB] =0;
  // motor[motorC] =0;
	// nMotorEncoder(motorB) = 0;
	// nMotorEncoder(motorC) = 0;
	// while(nMotorEncoder(motorB) < (160*360)/17.3){
	 // motor[motorB] = 20;
	 // motor[motorC] = 20;
	// }
	// motor[motorB] =0;
  // motor[motorC] =0;
  //============================================================
}
