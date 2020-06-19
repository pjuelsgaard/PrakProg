using System;
using static System.Console;
using static System.Math;
using System.IO;
using System.Collections.Generic;

public class main{
public static int Main(){
	// Reading data from CERN.dat
	string filename = "CERN.dat";
	string[] lines = File.ReadAllLines(@filename);
	int n = lines.Length;
	double[] E = new double[n]; // Energy [GeV]
	double[] X = new double[n]; // this is a "Cross" [Qlorp] (unit I just invented)
	double[] err = new double[n];	// Error on crosssection [Qlorp]
	for(int i=0; i<n; i++){
		string[] vals = lines[i].Split(' '); // Data is space-separated
		E[i] = double.Parse(vals[0]); 
		X[i] = double.Parse(vals[1]);
		err[i] = double.Parse(vals[2]);
	}

	Func<vector, double> D = delegate(vector v){
		double A = v[0];
		double m = v[1];
		double G = v[2];
		double sum = 0;
		for(int i =0; i<n; i++){
			sum += Pow(A/(Pow(E[i]-m,2)+G*G/4) - X[i],2)/err[i]/err[i]; // Generating the deviation, weighted by 1/err^2 for the points
		}
		return sum;
	};
	vector v0 = new vector(1.0,120.0,10.0);
	var res = minimize.qnewton(D, v0);
	double amp = res.Item1[0];
	double mass = res.Item1[1];
	double Gamma = res.Item1[2];

	int steps = res.Item2;

	Write($"Using the quasi-newton routine, the scaling factor, mass and resonance width for the boson were found, with a starting guess at [{v0[0]}, {v0[1]}, {v0[2]}], to be\n");
	Write($"A={amp}, m={mass}, Gamma={Gamma}\n");
	Write($"In {steps} steps\n");

	return 0;
}
}
