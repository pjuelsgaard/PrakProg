using static System.Console;
using static vector3d;

class main{
	public static int Main(){
	vector3d v = new vector3d(2,427,-17);
	vector3d u = new vector3d(351, -379, 1);
	double c = System.Math.PI;
	v.print("v=");
	u.print("u=");
	(v+u).print("v+u=");
	(v-u).print("v-u=");
	double vu = v.dot_product(u);
	Write($"v.u={vu}\n");
	(v.vector_product(u)).print("v x u =");
	Write("|v|={0}\n", v.magnitude());

	(v*c).print($"v*{c}=");

	return 0;
	}
}
