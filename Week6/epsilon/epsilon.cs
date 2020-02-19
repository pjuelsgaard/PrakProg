using static System.Console;
using static System.Math;
using static cmath;
using static BoolApprox;
class main{
	public static int Main(){
		int i=1;
		while(i+1>i){i++;}
		Write("Maxval of int. i is\n {0}\n which is {1:f2} of int.MaxValue\n", i, i/int.MaxValue);
		i = 1;
		while(i-1<i){i--;}	
		Write("Minval of int. i is\n {0}\n which is {1:f2} of int.MinValue\n", i, i/int.MinValue);
		
		MachineEpsilon();
		HarmSum();
		bool q = approx( 5000, 5000, 1e-9,  1e-9);
		Write("The statement »The final function worked« was {0}\n", q);
	return 0;
	}
	public static int MachineEpsilon(){
		double x = 1;
		while (1+x!=1){x/=2;} // Divide x by 2 untill we've gone 1 step too far
		x*=2; // To make up for our step too far
		Write("For doubles, the apparent machine epsilon is {0}\n", x);

		float y=1F;
		while ((float)(1F+y) != 1F){y/=2F;}
		y*=2F;
		Write("For floats, the apparent machine epsilon is {0}\n", y);
	return 0;
	}

	public static int HarmSum(){
		// Calculating harmonic sums w. floats
		int max = int.MaxValue/2;
		float float_sum_up = 0;
		float float_sum_down = 0;
		for (int i=1; i<= max;i++){
			float_sum_up += 1/i;
			float_sum_down += 1/(max-i+1);
			}

		Write($"Floats:\n Summing up, we get {float_sum_up}, and summing down we get {float_sum_down}, with a difference {float_sum_up-float_sum_down}\n");
		// Then w. doubles
		double double_sum_up = 0;
		double double_sum_down = 0;
		for (int j=1; j<=max; j++){
			double_sum_up += 1/j;
			double_sum_down += 1/(max-j+1);
			}
		Write($"Doubles:\n Summing up, we get {double_sum_up}, and summing down we get {double_sum_down}, with a difference {double_sum_up-double_sum_down}\n");

	return 0;
	}
}
