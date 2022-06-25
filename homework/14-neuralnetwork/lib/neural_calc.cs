using System;
using static Minimization;
public class neural_calc{

	int n; // hidden neurons
	Func<double,double> f,f_der,f_int; // activation function 
	public vector p; // concatenated network parameters
	public vector x,y; // training vectors

	public neural_calc(int n,Func<double,double> f,Func<double,double> f_der,Func<double,double> f_int) {
		this.n = n;
		this.f = f;
		this.f_der = f_der;
		this.f_int = f_int;
		this.p = new vector(3*n);
	}

	public double response(double x) {
		vector xs = new vector(n).fill(x);

		vector a = this.p[0,n];
		vector b = this.p[n,2*n];
		vector w = this.p[2*n,3*n];

		return ((xs-a)/b).map(this.f).mult(w).sum();
	}

	public double response_der(double x) {
		vector xs = new vector(n).fill(x);

		vector a = this.p[0,n];
		vector b = this.p[n,2*n];
		vector w = this.p[2*n,3*n];

		return ((xs-a)/b).map(this.f_der).mult(w/b).sum();
	}

	public double response_int(double x) {
		vector xs = new vector(n).fill(x);

		vector a = this.p[0,n];
		vector b = this.p[n,2*n];
		vector w = this.p[2*n,3*n];

		return ((xs-a)/b).map(this.f_int).mult(w).mult(b).sum();
	}


	public void train(vector x,vector y){
		this.x = x;
		this.y = y;
		// I think it's costumary to start with random network parameters, with mean zero 
		// and same variance in all layers (we only have one layer…)
		this.p.random(-0.1,0.1);
		vector p0 = this.p;
		this.p = Minimization.qnewton(this.cost,p0,1e-3);
	}

	private double cost(vector p) {
		this.p = p;
		// syntactic sugar is very sweet. Mapping to lambda function to square elements.
		vector c = this.x.map(this.response);
		c = c - this.y;
		c = c.map(x => x*x);
		return c.sum();

		// return (this.x.map(this.response)-this.y).map(x => x*x).sum();
	}
}