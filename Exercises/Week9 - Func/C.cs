using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;
using System.Linq;

// Constants n shit
static class Const{
	public const double inf = System.Double.PositiveInfinity;
	public const double nan = System.Double.NaN;
}

class main{

static void Main(){
	Write("\nPart C:\n");
	double eps = 1e-2;
	List<double> EVals = new List<double>();
	List<double> aVals = new List<double>();
	// Make a datapoint for each value of alpha in ]0:10]
	double a = eps;
	for (int i = 0;i<1e3;i++){
		Func<double, double> f = (x) => (-Pow(a*x,2)/2+a/2+x*x/2)*Exp(-a*Pow(x,2));
		Func<double, double> g = (x) => Exp(-a*Pow(x,2));
		aVals.Add(a);
		double E = quad.o8av(f, -Const.inf,Const.inf)/quad.o8av(g, -Const.inf, Const.inf);
		EVals.Add(E);
		a += eps;}
	
	double[] EValArr = EVals.ToArray();
	double E_min = EValArr.Min();
	int MinIndex = EVals.IndexOf(E_min);
	Write("The lowest energy E is E_min={0} at a_min={1}\n", E_min, aVals[MinIndex]);
	for (int q=0; q<= EVals.Count; q++){
		Error.Write("{0}\t{1}\n", aVals[q], EVals[q]);}




}
}
