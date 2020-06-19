using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;
using System.Linq;

public class rk{

public static vector rkstep45(Func<double,vector, vector>f, double t0, vector y0, double h, vector err){
	// Using the RKF45 butcher's table
	// t is current argument; y=y(t); h is stepsize;
	// Generate the k_i vectors
	vector k_0 = f(t0,y0);
	vector k_1 = f(t0+h/4, y0+h/4*k_0);
	vector k_2 = f(t0+h*3/8, y0 + h*3/32*k_0 + h*9/32*k_1);
	vector k_3 = f(t0+h*12/13, y0 + h*1932/2197*k_0 - h*7200/2197*k_1 + h*7296/2197*k_2);
	vector k_4 = f(t0+h, y0 + h*439/216*k_0 - h*8*k_1 + h*3680/513*k_2 - h*845/4104*k_3);
	vector k_5 = f(t0+h/2, y0 - h*8/27*k_0 + h*2*k_1 - h*3544/2565*k_2 + h*1859/4104*k_3 - h*11/40*k_4);
	vector k_sum = 16.0/135*k_0 + 6656.0/12825*k_2 + 28561.0/56430*k_3 - 9.0/50*k_4 + 2.0/55*k_5;
	
	// The embedded lower order method made with b_i^* instead of b_i
	vector k_star = 25.0/216*k_0 + 1408.0/2565*k_2 + 2197.0/4104*k_3 - 1.0/5*k_4;
	err = (k_sum - k_star)*h;
	for(int j =0; j<err.size; j++)err[j]=Abs(err[j]); // The error estimate is set to always be positive (because that juts makes more sense to me)
	vector y_next = y0 + h*k_sum;
	
	return y_next;
}

// acc is the absolute precision, and eps is the relative precision on each step
public static Tuple<List<double>, List<vector>> driver(Func<double, vector, vector> f, double a, double b, vector ya, double h, double acc=1e-6, double eps=1e-6){
	// Initialize lists for ts and ys with t=a and y(a) already in them
	List<double> ts = new List<double>(){a};
	List<vector> ys = new List<vector>(){ya};
	
	// Within this loop a serves as t_current and ya serves as y_current
	while(a<b){
		// Check for overshoot, to make sure the function actually ends at b
		if(b<= a+h)h = b-a;
	
	// Do the actual work
	vector err_step = new vector(ya.size);
	vector y_next = rkstep45(f, a, ya, h, err_step);


	// Do error stuffs using eq.s 40 and onwards from ODE reading
	vector tol = new vector(y_next.size);
	for(int j=0;j<y_next.size; j++){
		tol[j] = Sqrt(h/(b-a))*(eps*Abs(y_next[j]) + acc);
		// Dividing by zero is a bad idea, so...
		if(err_step[j]==0)err_step[j]=tol[j]/4; 
	}
	// Array for relative errors
	double[] rel_arr = new double[err_step.size];
	for(int j=0; j<err_step.size; j++) rel_arr[j]=tol[j]/err_step[j];

	// Check if step is crap
	bool crap = false; // Of course I hope that the step works
	for(int j=0; j<tol.size; j++){
		if(rel_arr[j]<1.0){
			crap= true;
			break;
		}
	}
	
	// If step is good, move the starting point for the next iteration to the end point of the current
	if(!crap){
		a+=h;
		ya = y_next;
		ts.Add(a);
		ys.Add(ya);
	}

	// Calculating the next step size, using eq. 40 
	//h *= Pow(rel_arr.Min(),0.25)*0.95;
	} //End of while-loop
	return Tuple.Create(ts,ys);
}
}
