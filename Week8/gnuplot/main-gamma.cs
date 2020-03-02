using static System.Console;

class main{
static int Main(){
	double eps = 1.0/32, dx = 1.0/16;
	for(double x=-4+eps;x<=6;x+=dx){     // Don't do for-loops over doubles, except if in binary form and not too big
		WriteLine("{0,10:f3} {1,15:f8}", x, math.gamma(x));
		}
double tg = 1;
	for(double x=1;x<=170;x+=1){
		double gx = math.gamma(x);
		Error.WriteLine("{0,10:f3} {1,25:f15} {2,25:f15}", x, gx, tg);
		tg = tg*x;
		}




return 0;
}
}
