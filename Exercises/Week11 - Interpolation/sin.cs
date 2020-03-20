using static System.Console;
using static System.Math;

// Script for creating virtual data of sine function
class main{
	public static int Main(){
		for(double i=0; i<=20; i++){Write("{0}\t{1}\t{2}\n", i*PI/10, Sin(i*PI/10), 1-Cos(i*PI/10));}
		return 0;
	}
}
