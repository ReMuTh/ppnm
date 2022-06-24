using System;
using static Minimization;
public class neural{

	int n; // hidden neurons
	Func<double,double> f; // activation function 
	public vector p; // concatenated network parameters
	public vector x,y; // training vectors

	public neural(int n,Func<double,double> f) {
		this.n = n;
		this.f = f;
		this.p = new vector(3*n);
	}

	public double response(double x) {
		// End of course sports: Can I code the response compactly using vector operations?
		// we need vector with x in all entries
		vector xs = new vector(n).fill(x);

		// Using my nuveau vector slicing to retrieve a = p[0,n];b = p[n,2*n];w = p[n*2,n*3];
		return ((xs-this.p[0,n])/this.p[n,2*n]).map(this.f).hadamard(this.p[n*2,n*3]).sum();
	}

	public void train(vector x,vector y){
		this.x = x;
		this.y = y;
		// I think it's costumary to start with random network parameters, with mean zero 
		// and same variance in all layers (we only have one layer…)
		this.p.random(-1,1);
		this.p = Minimization.qnewton(this.cost,p,1e-4);
	}

	private double cost(vector p) {
		this.p = p;
		// syntactic sugar is very sweet. Mapping to lambda function to square elements.
		return (this.x.map(this.response)-this.y).map(x => x*x).sum();
	}
}