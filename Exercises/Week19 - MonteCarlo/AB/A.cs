using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;
using System.Linq;

public static class main{
public static int Main(){
	Write("Part A:\n");
	// Some functions
	Func<vector,double> g=delegate(vector x){
		return Sin(x[0]);
		//return Exp(-x[0]*x[0]);
	};
	Func<vector,double> p=delegate(vector x){
		return Sqrt(x[0]*x[1]);
	};

	// Limits
	vector a = new vector(0.0,3.0);
	vector b = new vector(14.0,2.0);

	int N = 100000;
	vector resg = mc.plainmc(g,a,b, N);
	double gexact = 0.99061; // Courtesy of WolframAlpha
	vector resp = mc.plainmc(p,a,b,N);
	double pexact = -55.124; // Also WolframAlpha
	
	Write("[x,y]: [0,3] -> [14,2]\n");
	Write($"e^(-x^2):\t Result:{resg[0]}+-{resg[1]}\t Exact:{gexact}\n");
	Write($"sqrt(x*y):\t Result:{resp[0]}+-{resp[1]}\t Exact:{pexact}\n");
	Write($"It's apparently really bad at 1-dimensional functions...\n");
	
	// Funky function
	Func<vector, double> F = delegate(vector x){
		return 1/(1-Cos(x[0])*Cos(x[1])*Cos(x[2]))/PI/PI/PI;
	};

	a = new vector(0,0,0);
	b = new vector(PI,PI,PI);

	vector res = mc.plainmc(F,a,b,N);
	double exact = 1.393203929685676859;
	Write($"Funky:\t Result:{res[0]}+-{res[1]}\t Exact:{exact}\n");
	


	Write("\n\nPart B:\n");
	N=1000;
	List<double> SqrtNs = new List<double>();
	List<double> errs = new List<double>();
	for(int i =1; i<60; i++){
		N += i*i*10;
		res = mc.plainmc(F,a,b,N);
		Error.Write("{0}\t\t{1}\n", 1.0/Sqrt(Convert.ToDouble(N)), res[1]);
		SqrtNs.Add(1/Sqrt(Convert.ToDouble(N)));
		errs.Add(res[1]);
	}
	Error.Write("\n\n");
	// Fitting the errors to a 1/sqrt(x) function: sigma = c0+c1*(1/sqrt(N))
	matrix M = new matrix(SqrtNs.Count, 2);
	vector k = new vector(SqrtNs.Count);
	for(int i = 0; i<k.size; i++){
		k[i] = errs[i];
		M[i,0] = 1;
		M[i,1] = SqrtNs[i];
	}
	vector c = fit.lsfit(M,k);
	Func<double,double> fitfun = delegate(double s){
		return c[0]+c[1]*s;
	};
	for(int i=30; i<1001;i++){
		double j = 1.0/i;
		Error.Write("{0}\t\t{1}\n", j, fitfun(j));
	}
	Write("As can be seen in B.svg, the error does seem to be proportional with 1/Sqrt(N)\n");

	
	return 0;



}
}
