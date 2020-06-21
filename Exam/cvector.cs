// Peter Juelsgaard, basically copied from Dmitri Fedorov
using System;
using static System.Math;
using System.Collections.Generic;
public partial class cvector{

private List<complex> data;
public int size{ get{return data.Count;}}

public complex this[int i]{
	get{return data[i];}
	set{data[i]=value;}
}

public vector(int n){data=new List<complex>
