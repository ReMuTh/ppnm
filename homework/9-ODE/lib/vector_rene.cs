using System;
using System.IO;
using static System.Math;
using static System.Console;
public partial class vector{

	// Elementwise abs is useful
	public vector abs(){
	vector r=new vector(size);
	for(int i=0;i<size;i++) r[i]=Abs(this[i]);
	return r;
	}

}