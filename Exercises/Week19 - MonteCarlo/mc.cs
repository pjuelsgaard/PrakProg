using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;

public class mc{
public static vector plainmc(Func<vector, double> f, vector a, vector b, int N, Random rnd=null){
	if(rnd==null) rnd = new Random();
	double V = 1.0;
	for(int i=0; i<a.size; i++){
		V *= b[i] - a[i];
	}

	double frndx; // Function evalueated at generated random x
	double sum1 = 0;
	double sum2 = 0;

	for(int i=0; i<N; i++){
		vector rndx = rnd_x(a,b, rnd);
		frndx = f(rndx);
		sum1 += frndx;
		sum2 += frndx * frndx;
	}

	double avg = sum1/N;
	double sig = Sqrt(sum2/N - avg*avg);
	double SIG = sig/Sqrt(N);
	double err = SIG*V;

	return new vector(avg*V, err);
} 
	// Generate a random x
public static vector rnd_x(vector a, vector b, Random rnd){
	vector goat = new vector(a.size);
	for(int i=0;i<goat.size;i++){
		goat[i] = a[i] + (b[i]-a[i])*rnd.NextDouble();
	}
	return goat;
}
}

