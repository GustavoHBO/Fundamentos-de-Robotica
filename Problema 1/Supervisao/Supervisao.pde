import processing.serial.*;

Serial myPort; 
PImage img;
//dados recebidos por bluetooth
int luz = 0, toque = 0;
int eixoX = 0, eixoY = 0;
boolean decremento =false, incremento=true, reset =false;
//gps
//base: x = 110, y = 310 map2
//canto inf/esq: x = 81, y = 541;
int xIni = 86, yIni = 310, x=0, y=0, xDecrement = 0, yDecrement = 0;
//botoes
int iniciarX = 640, iniciarY= 110, iniciarH = 140, iniciarW = 90;
int pararX = 780, pararY= 110, pararH = 90, pararW = 90;
int resetX = 705, resetY= 210, resetH = 100, resetW = 40;
//labels
int  distancia = 0, dist = 0, distAnt =0;

void setup() {
  size(1000, 620); // Creating the display window and defining its' size.
  smooth();
  x=xIni; y=yIni;
  img = loadImage("map2.png");
  println(Serial.list());
  String portName = Serial.list()[2]; 
  myPort = new Serial(this, portName, 9600); // Initializing the serial port.
}

void draw() {
  background(240, 240, 240);    
  textSize (20);
  
  fill (0, 0, 0);
  text("Supervisão:", 690, 80); //titulo
  image(img, 50, 30);

  fill (30, 140, 40); //verde
  //rect ( x, y, largura, altura, arredondamento)
  rect(iniciarX, iniciarY, iniciarH+40, iniciarW, 20); //start
  fill(255);
  text("INICIAR", 669, 160); 
  fill (160, 50, 20); //vermelho
  rect(pararX, pararY, pararH, pararW, 20); //stop
  fill(255);
  text("PARAR", 795, 160);
  fill (255, 205, 0); //amarelo
  rect(resetX, resetY, resetH, resetW, 12); //reset
  fill(0);
  text("RESET", 728, 238);

  fill(0);
  text("Sensor de Luz:             " + luz, 630, 340);
  text("Sensor de Carga:          " + toque, 630, 380);
  text("Dist. percorrida (cm):    " + distancia, 630, 420);
  //println(inByte);
  
  //gps
  fill (70, 230, 70); //verde
  rect(x, y, 20, 20, 50); //gps  
  
  //Sensor communication
  while (myPort.available() > 0) {
    luz = myPort.read();
    //println("Luz: "+luz);
    toque = myPort.read();
    //println("Toque: "+toque);
    eixoX = myPort.read(); 
    if(eixoX>110)
      eixoX = eixoX-255;       
    println("Eixo X: "+eixoX);
    println("EixoAnt X: "+xIni);
    eixoY = myPort.read();  
    if(eixoY>100)
      eixoY = eixoY-255;
    println("Eixo Y: "+eixoY);
    println("EixoAnt Y: "+yIni);
    dist = myPort.read();    
    println("-------------------- Distancia: "+dist);
    
    if(dist>=0 && dist<10 && distancia>=10 && reset){
      distAnt = distancia + dist;
      xIni = x + eixoX*2;
      yIni = y + eixoY*2;
      reset =false;
    }
    if(dist>=10)
      reset =true;
    distancia = dist + distAnt;    
    x = eixoX*2 + xIni;
    y = eixoY*2 + yIni;
  }
}

//botoes iniciar e parar
void mousePressed() {
    if(mouseX>iniciarX && mouseX <iniciarX+iniciarH && mouseY>iniciarY && mouseY <iniciarY+iniciarW){
      println("INICIAR!"); 
      myPort.write('i'); 
    }
    if(mouseX>pararX && mouseX <pararX+pararH && mouseY>pararY && mouseY <pararY+pararW){
      println("PARAR!"); 
      myPort.write('p');
    }
    if(mouseX>resetX && mouseX <resetX+resetH && mouseY>resetY && mouseY <resetY+resetW){
      println("RESET!");       
      myPort.write('r');
      decremento =false; incremento=true; reset =false;
      xIni = 86; yIni = 310; x=0; y=0; xDecrement = 0; yDecrement = 0;
      //botoes
      iniciarX = 640; iniciarY= 110; iniciarH = 140; iniciarW = 90;
      pararX = 780; pararY= 110; pararH = 90; pararW = 90;
      resetX = 705; resetY= 210; resetH = 100; resetW = 40;
      //labels
      distancia = 0; dist = 0; distAnt =0;
    }
}