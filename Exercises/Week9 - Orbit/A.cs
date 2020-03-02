using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;
using System.IO;

class main{
	static int Main(){
		// Function
		Func<double, vector, vector> LogFun = delegate(double x, vector y){
			return new vector(y[0]*(1-y[0]));
		};
		// Initial conditions
		double a = 0;
		double b = 3;
		vector ya = new vector(0.5);
		List<double> xs = new List<double>();
		List<vector> ys = new List<vector>();
		vector yb = ode.rk23(LogFun, a, ya, b, xs, ys);
		for(int i=0; i<xs.Count;i++){
			Write($"{xs[i]}\t{ys[i][0]}\n");
		}

		Func<double, double> ALog = (x) => 1/(1+Exp(-x));
		double eps = 1.0/32;
		double xi = 0;
		// Create file to write data to
		string path = "out1.A.txt";
		using (StreamWriter sw = File.CreateText(path)){
			while(xi<=3){
				sw.Write("{0}\t{1}\n", xi, ALog(xi));
				xi += eps;
			}
		}

	return 0;
	}
}
