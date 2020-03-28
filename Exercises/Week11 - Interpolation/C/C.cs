using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static cspline;

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
		while(q<=xs.Max()){
			double S = cspline.Spline(xs, ys, q, 0);
			double SInt = cspline.Spline(xs, ys, q, -1);
			double SD =  cspline.Spline(xs, ys, q, 1);
			//Write("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\n",S,S,S,S,S,S,S);
			Write("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\n", q, S, Sin(q), SInt, 1-Cos(q), SD, Cos(q));
			q += 0.01;
		}	

	return 0;
	}
}
