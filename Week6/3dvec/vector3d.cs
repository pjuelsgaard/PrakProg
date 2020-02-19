public struct vector3d{
	public double x,y,z;
	
	// Constructor
	public vector3d(double a, double b, double c){x=a; y=b; z=c;}
	
	// Operators
	public static vector3d operator *(vector3d v, double c){
		return new vector3d(c*v.x,c*v.y,c*v.z);
	}

	public static vector3d operator*(double c, vector3d v){
		return v*c;
	}

	public static vector3d operator +(vector3d u, vector3d v){
		return new vector3d(u.x+v.x, u.y+v.y, u.z+v.z);
	}
 
	public static vector3d operator -(vector3d u, vector3d v){
		return new vector3d(u.x-v.x, u.y-v.y, u.z-v.z);
	}

	public static vector3d operator /(vector3d v, double c){
		return v*(1/c);
	}


	// Methods
	public double dot_product(vector3d other){
		return this.x *other.x + this.y*other.y + this.z*other.z;
	}

	public vector3d vector_product(vector3d other){
		return new vector3d(this.y*other.z-this.z*other.y, this.z*other.x-this.x*other.z, this.x*other.y-this.y*other.x);
	}

	public double magnitude(){
		return System.Math.Sqrt(this.dot_product(this));
	}
	
	public void print(string s=""){
		System.Console.Write("{0} [{1}, {2}, {3}]\n", s, this.x, this.y, this.z);
	}

	public override string ToString(){
		return string.Format("{0} {1} {2}", this.x, this.y, this.z);
	}

}
