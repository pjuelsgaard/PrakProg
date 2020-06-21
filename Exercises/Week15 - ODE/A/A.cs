using System;
using static System.Console;
using static rk;
using System.Collections.Generic;
using static System.Math;


public class main{
public static int Main(){
	/* The following function didn't work. I don't know why
	// I choose to use a Van der Pol oscillator as my function
	// d^2f/dt^2 - mu*(1-f^2)df/dt + f = 0. I let mu=1
	// I let y=[u, u', u'']. If result looks weird try switching order of the new vector components
	Func<double, vector, vector> f = delegate(double t, vector y){
		double f0 = (y[2]+y[0])/(1-y[0]*y[0]);
		double f1 = (1-y[0]*y[0])*y[1]-y[0];
		double f2 = -2*y[0]*y[1]+(1-y[0]*y[0])*(1-y[0]*y[0])*y[1]+y[0]*(1-y[0]*y[0])+y[1];
		return new vector(f0, f1, f2);
	};*/
	

	// Cosine function
	Func<double, vector, vector> f = delegate(double x, vector y){
		return new vector(y[1], -y[0]);};



	// Initial conditions
	double a = 0;
	double b = 2*PI;
	vector ya = new vector(1,0);
	double h = 0.01;
	double acc = 1e-6;
	double eps = 1e-6;
	
	var res = rk.driver(f, a, b, ya, h, acc, eps);
	List<double> xs = res.Item1;
	List<vector> ys = res.Item2;

	for(int i=0; i<xs.Count; i++){
		Write("{0}\t{1}\t{2}\n", xs[i], ys[i][0], Math.Cos(xs[i]));
	}
	return 0;



}
}
