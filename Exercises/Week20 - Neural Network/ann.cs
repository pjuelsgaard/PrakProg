using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;

public class network{


public int n; // Amount of hidden neurons
public vector p;
public Func<double,double> f; // Function sent to ANN. Must be provided by user. 
public Func<double,double> df; // Derivative
public Func<double,double> adf; // Antiderivative

public network(int n, Func<double,double> f, Func<double,double> df, Func<double,double> adf){
	this.n = n; // Amount of hidden neurons
	this.f = f;
	this.df = df;
	this.adf = adf;
	this.p = new vector(n*3);
}

// Feeding a number to the layer of the n hidden neurons
public double feed(double x){
	double y=0; // Value passed on from summation neuron
	for(int i=0;i<n; i++){
		double a = p[3*i];
		double b = p[3*i+1];
		double w = p[3*i+2];
		y += f((x-a)/b)*w;
	}
	return y;
}

public double dfeed(Func<double,double> g, double x){
	double y=0;
	for(int i=0;i<n; i++){
		double a = p[3*i];
		double b = p[3*i+1];
		double w = p[3*i+2];
		y += g((x-a)/b)*w/b;
	}
	return y;
}

public double adfeed(Func<double,double> g, double x){
	double y=0;
	for(int i=0;i<n; i++){
		double a = p[3*i];
		double b = p[3*i+1];
		double w = p[3*i+2];
		y += g((x-a)/b)*w*b;
		y -= g((x0-a)/b)*w*b;
	}
	return y;
}



public double x0; // Used when calculating antiderivative

public vector train(double[] xs, double[] ys){
	int ncalls = 0;
	x0 = xs[0];

	Func<vector, double> deviation = delegate(vector u){
		ncalls++;
		p = u;
		double sum =0;
		for(int i=0; i<xs.Length; i++){
			sum += Pow(feed(xs[i])-ys[i],2);
		}
		return sum/xs.Length;
	};
	vector v = p;
	var res = minimize.qnewton(deviation, v);
	v = res.Item1;
	p=v;
	return new vector(res.Item2, ncalls); // n_iterations , ncalls
}
public vector train(List<double> xs, List<double> ys){
	return train(xs.ToArray(), ys.ToArray());
}










}
