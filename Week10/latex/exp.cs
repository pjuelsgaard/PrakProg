using System;
public static class nonef{
public static double exp(double x){
	if (x<0)return 1/exp(-x);
	if (x>4){
		double r = exp(x/4);
		return r*r*r*r;}
	if (x>2){
		double r = exp(x/2);
		return r*r;}
	Func<double, vector, vector> expeq = (t,y) => new vector(y[0]); // Has to call local var t, since some shit with variables
	double a = 0;
	vector ya = new vector(1.0);
	vector res = ode.rk23(expeq, a, ya, x);
	return res[0];

}
}
