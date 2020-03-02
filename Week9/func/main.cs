using System;
using static System.Console;
using static System.Math;

class main{
delegate double myfun(double x); //Delegate within this class
Func<double,double> fun; //Generic function. Takes double arg -> double res

static Func<double, double> makefun(double y){
	double a;
	a = y;
	//return delegate(double x){return y*y;};
	return x=> a; // Lambda syntax
	}

// Evaluates fiven function f with target in x and returns res f(x)
static double eval(Func<double, double> f, double x){return f(x);}

static Func<double, Func<double,double>> makemakefun(double y){
	Func<double, Func<double,double>> result = (double x) => (t=>x);
	return result;}


static int ncalls;

static double gamma(double z){
	const double inf = System.Double.PositiveInfinity;
	const double nan = System.Double.NaN;
	if (z<0) return -PI/Sin(PI*z)/gamma(1+z);
	if (z<1) return gamma(z+1)/z;
	if (z>2) return gamma(z-1)*(z-1);
	Func<double,double> f=delegate(double x){
		ncalls++;
		return Pow(x,z-1)*Exp(-x);};
	ncalls = 0;
	return quad.o8av(f,0,inf);//, acc:1e-6, eps:0); //acc and eps are optional params
	}






	
static int Main(){
	Func<double,double> f = delegate(double x){return x;};
	myfun g = delegate(double x){return x;};
	Write("f({0})={1}\n", 9, f(9));
	
	double a=1;
	Func<double,double> h1 = (x) => a;
	a = 2;
	Func<double,double> h2 = (x) => a;
	Func<double,double> f1 = makefun(1);

	var f2 = makefun(2);

	Write($"f1(0)={f1(0)}, f2(0)={f2(0)}\n");
	Write($"a={a}\n h1(0)={h1(0)}, h2(0)={eval(h2,0)}\n");



	for(double x= 0.5; x<9; x+=0.5){
	Write($"{x:f5} {gamma(x)} {ncalls}\n");}






return 0;
}
}
