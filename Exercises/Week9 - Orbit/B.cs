using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;
using System.IO;

class main{
	static int Main(){
		// Initial conditions
		double a = 0;
		double b = 6*2*PI; // Letting the system revolve 6 times
		vector eps = new vector(0.0, 0.0, 0.015);
		double y0 = 1;
		vector y1s = new vector(0.0, -0.3, -0.3);
		string[] paths = new string[3]{"out0.B.txt", "out1.B.txt", "out2.B.txt"};

		for(int i = 0; i<3; i++){
			// Function u''=1+(eps-1)u, where y0=u, y1=u', y2=eps 
			Func<double, vector, vector> EqMotion = delegate(double x, vector y){
				return new vector(y[1], 1 - y[0] + eps[i]*y[0]*y[0], y[2]);};
			vector ya = new vector(y0, y1s[i], eps[i]);
			List<double> xs = new List<double>();
			List<vector> ys = new List<vector>();


			if(i==0){
			// For loop of various starting positions, to take care of the system not creating enough points to adequately create an "easy" circular orbit.
				for (double j=0;  j<=200; j+= 1){
					
					// Named as such, because they are not the actual x/y lists
					List<double> Notxs = new List<double>();
					List<vector> Notys = new List<vector>();
					ode.rk23(EqMotion, a+j*PI/100, ya, b+j*PI/100, Notxs, Notys);
					xs.Add(Notxs[Notxs.Count-1]);
					ys.Add(Notys[Notys.Count-1]);
				}
			}

			else{
			// System is solved by numerical solver
			ode.rk23(EqMotion, a, ya, b, xs, ys);
			}
			using (StreamWriter sw = File.CreateText(paths[i])){
				for(int j=0; j<xs.Count; j++){
					sw.Write("{0}\t{1}\n", xs[j], ys[j][0]);
				}
			}
		}
	return 0;
	}
}

