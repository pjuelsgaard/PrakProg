using static System.Console;
using static System.Math;

class main{
	static int Main(){
		double dx = 1.0/32;
		for(double x = dx; x<=10000; x+=dx){
			Write("{0,9:f5}\t{1,16:f9}\n", x, math.lngamma(x));
		}
		return 0;
	}
}
