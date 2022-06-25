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

	// Fill a vector with identical entries
	public vector fill(double x) {
		for(int i=0; i<size; i++) this.data[i] = x;
		return this;
	}

	public vector fill(double x,double y) {
		double del = (y-x)/(this.size-1);
		for(int i=0; i<size; i++) this.data[i] = x+i*del;
		return this;
	}

	public vector random(double x,double y) {
		Random rand = new Random();
		for(int i=0; i<size; i++) this.data[i] = rand.NextDouble()*(y-x)+x;
		return this;
	}

	public vector randomint(int i,int j) {
		Random rand = new Random();
		for(int k=0; k<size; k++) this.data[i] = rand.Next(i,j);
		return this;
	}

	public double sum() {
		double sum = 0;
		for(int i=0; i<size; i++) sum += data[i];
		return sum;
	}

	// get and set partial vectors. Will this work?
	public vector this[int i,int j]{
		get{
			vector r=new vector(j-i);
			for(int k=0;k<(j-i);k++) r[k]=this[k+i];
			return r;
		}
		set{
			for(int k=0;k<(j-i);k++) this[k+i]=value[k];
		}
	}
	
	// element wise product, also known as hadamard product
	public vector mult(vector v) {
		vector r=new vector(size);
		for(int i=0; i<size; i++) r[i]=this[i]*v[i];
		return r;
	}

}