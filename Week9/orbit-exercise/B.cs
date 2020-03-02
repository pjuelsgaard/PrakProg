using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;
using System.IO;

class main{
	static int Main(){
		// Function u''=1+(eps-1)u, where y0=u, y1=u', y2=eps 
		Func<double, vector, vector> EqMotion = delegate(double x, vector y){
			return new vector(y[1], 1+y[2]*y[0]*y[0]-y[0], y[2]);};
		
		// Initial conditions
		double a = 0;
		double b = 2*PI; // Letting the system revolve 1 times
		vector eps = new vector(0.0, 0.0, 0.001);
		double y0 = 1;
		vector y1s = new vector(0.0, 0.5, -0.5);
		string[] paths = new string[3]{"out0.B.txt", "out1.B.txt", "out2.B.txt"};

		for(int i = 0; i<3; i++){
			vector ya = new vector(y0, y1s[i], eps[i]);
			List<double> xs = new List<double>();
			List<vector> ys = new List<vector>();
			vector yb = ode.rk23(EqMotion, a, ya, b, xs, ys);
			using (StreamWriter sw = File.CreateText(paths[i])){
				for(int j=0; j<xs.Count; j++){
					sw.Write("{0}\t{1}\n", xs[j], ys[j][0]);
				}
			}
		}
	return 0;
	}
}

