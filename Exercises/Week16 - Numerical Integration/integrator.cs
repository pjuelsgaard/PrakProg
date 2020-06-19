using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;
using static System.Double;

public class integrator{
public static vector recint(Func<double,double> f, double a, double b, double acc=1e-6, double eps=1e-6,double f2=NaN, double f3=NaN, int iter=0){
	if(IsInfinity(a) || IsInfinity(b)) return recint_inf(f,a,b,acc,eps,iter);

	double dx = b-a; // We use this value a lot
	double f1 = f(a+dx/6);
	double f4 = f(a+5*dx/6);
	double Q = (2*f1+f2+f3+2*f4)/6.0*dx;
	double q = (f1+f2+f3+f4)/4.0*dx;
	double tol = acc+eps*Abs(Q);
	double err = Abs(Q-q);

	// Either way, returns a value for the integral, along with the error on the value
	if(err < tol) return new vector(Q, err, iter);
	if(iter>10000) return new vector(Q, err, iter);
	else{
		// Creates f2 and f3 for recursion on the first run of the integrator
		if(IsNaN(f2)){
			f2 = f(a+2*dx/6);
			f3 = f(a+4*dx/6);
		}
		// My system for recording the amount of iterations does not seem to be working...
		vector Q1 = recint(f, a, (a+b)/2.0, acc/Sqrt(2), eps, f1, f2, iter+1);
		vector Q2 = recint(f, (a+b)/2.0, b, acc/Sqrt(2), eps, f3, f4, iter+1);
		// Errors are calculated with error propagation
		return new vector(Q1[0]+Q2[0], Sqrt(Q1[1]*Q1[1]+Q2[1]*Q2[1]), Q1[2]+Q2[2]);
	}
}
		
public static vector CC(Func<double,double> f, double a, double b, double acc=1e-3, double eps=1e-3){
	Func<double,double> ccf = delegate(double x){
		return f((a+b+(b-a)*Cos(x))/2.0)*Sin(x)*(b-a)/2;
	};
	return recint(ccf, 0, PI, acc, eps);
}

public static vector recint_inf(Func<double,double> f, double a, double b, double acc=1e-6, double eps=1e-6, int iter=0){
	// In case of dumb limits
	if(a>b){
		vector res = recint_inf(f, b, a, acc, eps, iter);
		return new vector(-res[0], res[1], res[2]);
	}

	if(IsNegativeInfinity(a) && IsPositiveInfinity(b)){
		Func<double,double> f2 = delegate(double t){
			return f(t/(1-t*t))*(1+t*t)/(1-t*t)/(1-t*t);
		};
		return recint(f2, -1,1,acc,eps,iter);
	}
	if(!IsNegativeInfinity(a) && IsInfinity(b)){
		Func<double,double> f2 = delegate(double t){
			return f(a+t/(1-t))/(1-t)/(1-t);
		};
		return recint(f2, 0,1,acc,eps,iter);
	}
	if(IsNegativeInfinity(a) && !IsInfinity(b)){
		Func<double,double> f2 = delegate(double t){
			return f(b-(1-t)/t)/t/t;
		};
		return recint(f2, 0,1,acc,eps, iter);
	}
	return recint(f, a, b, acc, eps, iter);
}















// Dmitri's thingamajig for testing
// (C) 2020 Dmitri Fedorov; License: GNU GPL v3+; no warranty.
public static double o8av
(Func<double,double> f, double a, double b, double acc=1e-6, double eps=1e-6){
if(IsNegativeInfinity(a) && !IsInfinity(b))
	return o8av(t=>f(b-(1-t)/t)/t/t,0,1,acc,eps);
if(!IsInfinity(a) && IsPositiveInfinity(b))
	return o8av(t=>f(a+(1-t)/t)/t/t,0,1,acc,eps);
if(IsNegativeInfinity(a) && IsPositiveInfinity(b))
	return o8av(f,a,0,acc,eps)+o8av(f,0,b,acc,eps);
return o8a(t=>f(a+(b-a)*(3*t*t-2*t*t*t))*(b-a)*6*(t-t*t) ,0,1,acc,eps);
}//o8av

public static double o8a
(Func<double,double> f,double a,double b,double acc=1e-6,double eps=1e-6,
double f2=NaN,double f3=NaN, double f6=NaN,double f7=NaN,int nrec=0,int limit=100)
{ /// eight point open adaptive integrator
double h=b-a,sqr2=Sqrt(2);
double f1=f(a+h/12),f4=f(a+5*h/12),f5=f(a+7*h/12),f8=f(a+11*h/12);
if(IsNaN(f2))
	{f2=f(a+2*h/12);f3=f(a+4*h/12);f6=f(a+8*h/12);f7=f(a+10*h/12);nrec=0;}
const double
w1 = 0.2424792139077853, w2 =-0.1171075837742504, w3 = 0.499546485260771,
w4 =-0.1566137566137566, w5=0,
w6 = 0.3876795162509448, w7 =-0.091005291005291, w8 = 0.2350214159737969;
const double
u1 = 0.2350214159737969, u2 =-0.091005291005291, u3 = 0.3876795162509448,
u4=0, u5 = -0.1566137566137566,
u6 = 0.499546485260771, u7 = -0.1171075837742504, u8 = 0.2424792139077853;
double approx1= (w1*f1+w2*f2+w3*f3+w4*f4+w5*f5+w6*f6+w7*f7+w8*f8)*h;
double approx2= (u1*f1+u2*f2+u3*f3+u4*f4+u5*f5+u6*f6+u7*f7+u8*f8)*h;
double integral=(approx1+approx2)/2;
double error=Abs(approx1-approx2);
double tolerance=acc+eps*Abs(integral);
if(error<tolerance) return integral;
else if(++nrec>limit) return NaN;
else return o8a(f,a,(a+b)/2,acc/sqr2,eps,f1,f2,f3,f4,nrec,limit)+
		o8a(f,(a+b)/2,b,acc/sqr2,eps,f5,f6,f7,f8,nrec,limit);
}//o8a

public static double o8acc
(Func<double,double> f, double a, double b, double acc=1e-3, double eps=1e-3){
        Func<double,double> F = t => f((a+b)/2+(b-a)/2*Cos(t))*Sin(t)*(b-a)/2;
        return o8a(F,0,PI,acc,eps);
}//o8acc


	
	

}
