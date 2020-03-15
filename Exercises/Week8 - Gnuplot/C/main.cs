using System;
using static System.Console;
using static System.Math;
using static cmath;

class main{
	static int Main(){
		double dr = 1.0/32, di = 1.0/32;
		for(double r= -5; r<=5;r+=dr){
			for(double i=-5;i<=5;i+=di){
				complex z = new complex(r,i);
				Write("{0}\t{1}\t{2}\n", z.Re, z.Im, abs(cgamma(z)));
				}
			Write("\n");
		}
		return 0;
	}



	public static complex cgamma(complex z){
		if(z.Re<0)return PI/sin(PI*z)/cgamma(1-z);
		if(z.Re<20)return cgamma(z+1)/z;
		complex lngamma = z*log(z+1/(12*z-1/z/10))-z+log(2*PI/z)/2;
		return exp(lngamma);
	}
}
