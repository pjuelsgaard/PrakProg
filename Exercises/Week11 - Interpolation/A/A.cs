using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;
using System.IO;
using static spline;

class main{
	static int Main(){
		string path = "../data.txt";
		string[] lines = File.ReadAllLines(@path);
		List<double> xs = new List<double>();
		List<double> ys = new List<double>();
		foreach(var line in lines){
			string[] words = line.Split('\t', ' ');
			double xi = double.Parse(words[0]);
			double yi = double.Parse(words[1]);
			xs.Add(xi);
			ys.Add(yi);
		}
		double q=0;
		while(q<=xs[xs.Count-1]){
			double S = lspline.Spline(xs, ys, q);
			double SInt = lspline.Int(xs, ys, q);
			Write("{0}\t{1}\t{2}\t{3}\t{4}\n", q, S, Sin(q), SInt, 1-Cos(q));
			q += 0.01;
		}	

	return 0;
	}
}
