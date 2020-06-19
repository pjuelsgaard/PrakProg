using System;
using static System.Console;
using static rk;
using static System.Math;
using System.Collections.Generic;

public class main{
public static int Main(){
	// Deals with the SIR model. Values supposedly useful for Denmark.
	// dS/dt = -IS/(NT_c)
	// dI/dt = IS/(NT_c) -I/T_r
	// dR/dt = I/T_r


	for(int i = 1; i<4; i++){

	// Parameters and initial conditions
	double N = 6.0*1e6;
	double Tr = 20.0;
	double Tc = 2.0*i; // By varying Tc I simulate people staying indoors
	double I0 = 200;
	double R0 = 0.0;
	double S0 = N-I0;

	// Choose function to be u=[S,I,R] (hence the name)
	Func<double,vector,vector> SIR = delegate(double t, vector y){
		double f0 = -y[1]*y[0]/(N*Tc); // dSdt
		double f1 = y[1]*y[0]/(N*Tc)-y[1]/Tr; // dIdt
		double f2 = y[1]/Tr; //dRdt
		return new vector(f0,f1,f2);
	};


	double a = 0.0;
	double b = 360.0; // I think around a year would have enough people dead to see how it goes
	vector ya = new vector(S0,I0,R0);
	double h=0.1;
	double acc=1e-3;
	double eps =1e-3;
	var res = rk.driver(SIR,a,b,ya,h,acc,eps);
	List<double> ts = res.Item1;
	List<vector> ys = res.Item2;

	for(int j =0; j<ts.Count;j++){
		Write("{0}\t{1}\t{2}\t{3}\n", ts[j], ys[j][0], ys[j][2], ys[j][1]);
	}
	Write("\n\n"); // Switches between datasets for gnuplot
	} // End of i-loop
	return 0;
}
}
