class main{
static int Main(){
	int n=3;
	vector v = new vector(n);
	vector u = new vector(n);
	for (int i = 0; i<n; i++){
		u[i] = i;
		v[i] = 2*i;
	}
	v.print("v=");
	u.print("u=");
	vector w = u+v;
	w.print("w=");
	(v*2).print("v*2=");
	return 0;
	}
}
