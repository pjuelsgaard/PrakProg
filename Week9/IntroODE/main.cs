using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;

class main{
	static int Main(string[] args){
		double a = 0;
		double y0 = 0;
		double y1 = 1;
		double b = 10;
		foreach(string s in args){
			string[] ws = s.Split('=');
			if(ws[0]=="a"){ a = double.Parse(ws[1]);}
			if(ws[0]=="y0"){ y0 = double.Parse(ws[1]);}
			if(ws[0]=="y1"){ y1 = double.Parse(ws[1]);}
			if(ws[0]=="b"){ b = double.Parse(ws[1]);}
			}
		Func<double, vector, vector> sincos= delegate(double x, vector y){
			return new vector(y[1], -y[0]);
		};
		// Init-conds
		vector ya = new vector(y0,y1);
		List<double> xs = new List<double>();
		List<vector> ys = new List<vector>();
		vector yb = ode.rk23(sincos, a, ya, b, xs, ys);
		Error.Write($"sin({b})={Sin(b)} cos({b})={Cos(b)}\n");
		for(int i =0; i<xs.Count;i++){
			WriteLine($"{xs[i]}\t{ys[i][0]}\t{ys[i][1]}");}
	return 0;
	}
}
