using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;

public class main{
public static int Main(){
	// Testfunction, f(x) = |x|*x
	
	Func<vector, vector> f = delegate(vector x){
		return new vector(-2*(1-x[0]), -2*(1-x[1]));
	};
	
	/*
	*/
	vector x0 = new vector(10,10);
	vector res = root.newton(f,x0);
	Write($"The minimum of testfun (-2(1-x[0]),-2(1-x[1])) is\n");
	for(int i=0; i<res.size;i++)Write($"{res[i]}\n");
	Write("\n");

	// Now for the weird functi0n
	

	Func<vector, vector> g = delegate(vector x){

		double dfdx0 = -2+2*x[0]+400*(x[1]*x[0]-x[0]*x[0]*x[0]);
		double dfdx1 = 200*(x[1]-x[0]*x[0]);
		return new vector(dfdx0,dfdx1);
	};

	vector x1 = new vector(0.0, 0.0);
	vector res2 = root.newton(g,x1, 1e-9);
	Write($"A minima of Rosenbrock's valley is at\n");
	for(int i=0; i<res.size;i++)Write($"{res2[i]}\n");
	Write(" For both functions, this fits the analytic solutions\n");



	return 0;



}
}
