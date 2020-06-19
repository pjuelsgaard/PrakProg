using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;
//using static Accord.Math;

public class main{
public static int Main(){
	Func<double,double> activate = delegate(double x){
		return Exp(-x*x);
	};
	Func<double,double> df = delegate(double x){
		return -2*Exp(-x*x)*x;
	};
	Func<double,double> adf = delegate(double x){
		return Sqrt(PI)*math.erf(x)/2;
	};

	Func<double,double> fitfun = delegate(double x){
		return x*Exp(-x*x); // I'm not terribly imaginative with my functions
	};
	Write("Part A:\n");
	int n = 8; // Wikibooks said this was a typical amount
	var ann = new network(n, activate, df, adf);
	double a=-2;
	double b=2;
	
	int nx = 50;
	double[] xs = new double[nx];
	double[] ys = new double[nx];
	for(int i=0; i<nx; i++){
		xs[i] = a+(b-a)*i/(nx-1);
		ys[i] = fitfun(xs[i]);
		Error.Write("{0}\t{1}\n", xs[i], ys[i]);
	}

	Error.Write("\n\n");
	for(int i=0; i<n; i++){
		ann.p[3*i] = a+(b-a)*i/(n-1);
		ann.p[3*i+1] = 1.0;
		ann.p[3*i+2] = 1.0;
	}
	ann.p.print("Initial p=");
	vector time = ann.train(xs, ys); // time=[minimizeriterations, functioncalls]
	ann.p.print("Post-training p=");
	Write($"The minimiztion took {time[0]} iterations, and the deviation function was called {time[1]} times\n");
	double z = a;
	for(int i=1; i<=100; i++){
		Error.Write($"{z}\t{ann.feed(z)}\n");
		z += (b-a)/100;
	}

	Write("\n The calculated function can be seen in A.svg\n\n");


	Write("Part B:\n");
	
	// Create derivative and anti-derivative functions
	Func<double,double> dfun = delegate(double x){
		return Exp(-x*x)*(1-2*x*x);
	};
	Func<double,double> adfun = delegate(double x){
		return -Exp(-x*x)/2.0;
	};

	Error.Write("\n\n");
	z=a;
	for(int i=1; i<=100; i++){ // For the derivative
		Error.Write($"{z}\t{ann.dfeed(df, z)}\t{dfun(z)}\n");
		z += (b-a)/100;
	}
	Error.Write("\n\n");

	z=a;
	for(int i=1; i<=1000; i++){ // For the antiderivative
		Error.Write($"{z}\t{ann.adfeed(adf, z)}\t{adfun(z)}\n");
		z += (b-a)/1000;
	}
	Write("The resulting derivates and antiderivatives can be seen in B.svg\n");

	return 0;
}
}
