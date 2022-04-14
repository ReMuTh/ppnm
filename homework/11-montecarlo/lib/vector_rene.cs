using System;
using System.IO;
using static System.Math;
using static System.Console;
public partial class vector{

	Random rand = new Random();

	// Elementwise abs is useful
	public vector abs(){
	vector r=new vector(size);
	for(int i=0;i<size;i++) r[i]=Abs(this[i]);
	return r;
	}

	// Returns the product of all elements
	public double prod() {
		double p = 1;
		for(int i=0;i<size;i++){p*=this[i];}
		return p;
	}

	// Randomize between boundary vectors
	public void random(vector a, vector b) {
		for(int k=0;k<this.size;k++)this[k]=a[k]+rand.NextDouble()*(b[k]-a[k]);
	}

}