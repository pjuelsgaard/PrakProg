using static System.Console;
class main{
	static int Main(){

		double eps = 1.0/32, dx = 1.0/16;
		for(double x=-3+eps; x<=3; x+=dx){
			WriteLine("{0,10:f3} {1,15:f8}", x, math.erf(x));
		}
	return 0;
	}
}
