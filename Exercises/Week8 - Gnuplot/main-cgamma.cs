using static System.Console;
using static math;

class main{
	static in Main(){
		double di= 1.0/32, dr = di; // Stepsize for imaginary and real parts
		for (double r = -5; r<=5; r+=dr){
			for(double i = -5; i<= 5; i+= di){
				complex z = new complex(r,i);
				Write($"{r}\t{i}\t{abs(gamma(z))}\n");
			}
		}
	return 0;
	}
}
