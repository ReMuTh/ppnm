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
	public void set_random(vector a, vector b) {
		for(int k=0;k<this.size;k++)this[k]=a[k]+rand.NextDouble()*(b[k]-a[k]);
	}

	public void set_halton(int n){
	int[] bases ={2 ,3 ,5 ,7 ,11 ,13 ,17 ,19 ,23 ,29 ,31 ,37 ,41 ,43 ,47 ,53 ,59 ,61 ,67};
		// sizeof(int) is apparently _always_ 32 bits in cs#

	//	int maxd=sizeof(bases)/32; assert(x.size <= maxd);
		for(int i=0;i<this.size;i++) this[i]=this.corput(n,bases[i]);

	} // halton

	public void set_halton(int n,vector a,vector b){
		vector randunit = new vector(this.size);
		randunit.set_halton(n);
		for(int k=0;k<this.size;k++)this[k]=a[k]+randunit[k]*(b[k]-a[k]);

	} // halton_ab

	// Van der corput routine from https://en.wikipedia.org/wiki/Van_der_Corput_sequence translated from C
	// b is base.
	double corput(int n, int b){
	    double q=0;
	    double bk=1.0/b;

	    while (n > 0) {
	      q += (n % b)*bk;
	      n /= b;
	      bk /= b;
	    }
	    return q;
	}

}