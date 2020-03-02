using static System.Console;
using static cmath;
public static class BoolApprox{
public static bool approx(double a, double b, double tau, double epsilon){
	if (abs(a-b) < tau||abs(a-b)/(abs(a)+abs(b))<epsilon/2){
		return true;}
	else{return false;}
}
	


}

