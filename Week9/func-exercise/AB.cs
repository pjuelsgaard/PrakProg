using System;
using static System.Console;
using static System.Math;

// Class for declaring constants
static class Const{
	public const double inf = System.Double.PositiveInfinity;
	public const double nan = System.Double.NaN;
	}



class main{


static double gamma(double z){
	if (z<0) return -PI/Sin(PI*z)/gamma(1+z);
	if (z<1) return gamma(z+1)/z;
        if (z>2) return gamma(z-1)*(z-1);
        Func<double,double> f=delegate(double x){
                return Pow(x,z-1)*Exp(-x);};
        return quad.o8av(f,0,Const.inf);
        }


static void Main(){
	Write("Part A:\n");

	// Integral from 0->1 of ln(x)/sqrt(x) dx
	Func<double, double> f1 = (x) => Log(x)/Sqrt(x);
	double res1, res2, res3;
	res1 = quad.o8av(f1, 0, 1);
	Write("int_0^1 ln(x)/sqrt(x) dx = {0}\n", res1);

	Func<double, double> f2 = (x) => Exp(-x*x);
	res2 = quad.o8av(f2, -Const.inf, Const.inf);
	Write("int_-inf^inf exp(-x^2) dx = {0} = sqrt(pi) = {1}\n", res2, Sqrt(PI));
	Write("int_0^1 ln(1/x)^p = result = Gamma(p+1)\n");
	for (double p = 0; p<9; p+=1){
		Func<double, double> f3 = (x) => Pow(Log(1/x),p);
		res3 = quad.o8av(f3, 0, 1);
		Write("p = {0} : {1} = Gamma(p+1) = {2}\n", p, res3, gamma(p+1));}

	
	Write("\n\nPart B:\n");
	double res;
	Func<double, double> f4 = (x) => x*x*x/(Exp(x)-1);
	res = quad.o8av(f4, 0, Const.inf);
	Write("int_0^inf x^3/(exp(x)-1) = {0} = pi^4/15 = {1}\n", res, Pow(PI,4)/15);

	Write("Gaussian integral int_0^inf e^(-ax^2):\n");
	for(double a= 0.5; a<9; a+=0.5){
		Func<double,double> f5 = (x) => Exp(-a*x*x);
		res = quad.o8av(f5, 0, Const.inf);
		Write("a = {0} :  \t{1} = 1/2*sqrt(pi/a) = {2}\n", a, res, 0.5*Sqrt(PI/a));}






}
}
