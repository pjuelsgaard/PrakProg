using System;
using static System.Console;
using static matrix;
using System.Collections.Generic;


public static class main{
public static int Main(string[] args){
	var watch = new System.Diagnostics.Stopwatch();
	watch.Start();
	
	string input = args[0];
	// Read data from data.txt file
	System.IO.StreamReader infile = new System.IO.StreamReader(input);
	List<double> ts = new List<double>();
	List<double> delta_lny = new List<double>();
	List<double> lny = new List<double>();
	do{
		string s = infile.ReadLine();
		if(s==null)break;
		string[] ws = s.Split(' ');
		ts.Add(double.Parse(ws[0]));
		lny.Add(Math.Log(double.Parse(ws[1])));
		delta_lny.Add(1.0/20); // Error prop on ln(y) with delta_y=y/20
		Error.Write("{0}\t{1}\t{2}\n", double.Parse(ws[0]), double.Parse(ws[1]), double.Parse(ws[1])/20);
	}while(true);
	// Part A)
	// Constructing the A matrix from Ordinary Least Squares Problem (7)
	// for the system u = ln(y) = c1 + c2*t
	
	matrix A  = new matrix(ts.Count, 2);
	vector b = new vector(ts.Count);
	for(int i = 0; i<ts.Count; i++){
		b[i] = lny[i]/delta_lny[i];
		A[i,0] = 1/delta_lny[i];
		A[i,1] = ts[i]/delta_lny[i];
	}
	
	vector c = fit.lsfit(A,b);
	double a = Math.Exp(c[0]);
	double neg_l = c[1];
	double x = 0;
	
	// Part B)
	// Constructing the covariance matrix using (14)
	matrix S = fit.cov(A);
	vector var = new vector(S.size1);
	for(int i = 0; i<var.size; i++)var[i] = Math.Sqrt(S[i,i]);
	double sigma_F = var.norm();
	
	// Recreating the function
	Func<double, double, double ,double> F = (t, a1, l1) => a1*Math.Exp(l1*t);
	// Uncertainty of y is not calculated w. error propagation

	Error.Write("\n\n");
	while(x<22){
		Error.Write("{0}\t{1}\t{2}\t{3}\n", x, F(x, a, neg_l),F(x, a+Math.Sqrt(var[0]), neg_l-Math.Sqrt(var[1])), F(x, a-Math.Sqrt(var[0]), neg_l+Math.Sqrt(var[1])));  //F(x)+sigma_F*Math.Exp(F(x)), F(x)-sigma_F*Math.Exp(F(x)) ); // This outcommented part is by using error propagation
		x += 1e-1;
	}
	Write("\n\nt_1/2 = {0:f4} +- {1:f4}, which is relatively close to the modern accepted value of 3.6319 days\n", -Math.Log(2)/neg_l, Math.Sqrt(var[1])*2*Math.Log(2)/neg_l/neg_l);
	
	watch.Stop();
	Write($"Time: {watch.ElapsedTicks}\n");
	return 0;
}
}
