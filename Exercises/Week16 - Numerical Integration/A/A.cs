using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;



public class main{
public static int Main(){

	// Testing integration on some integrals
	Func<double,double> f = delegate(double x){
		return Sqrt(x);
	};
	Func<double,double> g = delegate(double x){
		return 4*Sqrt(1-x*x);
	};

	// Running integrator. Results should be fres[0]=2/3, and gres[0] = pi
	vector fres = integrator.recint(f, 0,1);
	vector gres = integrator.recint(g, 0,1);

	Write("Integrating sqrt(x) from 0->1 gave {0}+-{1}. Analytically should be 0.667.\n", fres[0],fres[1]);
	Write("Integrating 4*sqrt(1+x*x) from 0->1 gave {0}+-{1}. Analytically should give pi.\n", gres[0], gres[1]);

	return 0;


}
}
