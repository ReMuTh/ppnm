using System;
using static System.Math;
using static System.Console;
using static System.Double;

public static class Roots{

	public static vector NewtonRaphson(Func<vector, vector> f, vector x0, double eps=1e-4,int verbose = 0) {

		int n = x0.size;
		double delta = 1.0;
		double delta_min = 1.0/64;
		vector x = x0.copy();

		while(true) {
			// Using one my additional vector operations: Elementwise abs
			vector delx = x.abs() * Pow(2, -26);

			// Calculate Jacobian matrix
			matrix J = new matrix(n, n);
			vector fx = f(x);
			
			for(int i=0;i<n;i++){

				for(int j=0;j<n;j++){
					vector xdelx = x.copy();
					xdelx[j] += delx[i];
					J[i,j] = (f(xdelx)[i]-fx[i])/delx[i];
				}
			}

			var lineq = new QRGS(J);
			vector xstep = lineq.solve(-f(x));

			// If x0 is chosen such that the step is divergent, take minor step and try again
			// check if any dimension has gone rogue

			if(IsNaN(xstep.norm())) {

				xstep = x.abs();
			}

			else {
				// else line search as described in note
				delta = 1;
				while(f(x+delta*xstep).norm() > (1-delta/2)*f(x).norm() && delta > delta_min) {
				delta /= 2;
				if (verbose > 0) WriteLine($"Delta loop. Delta is {delta}");
			}

			}


			x = x + delta*xstep;
			if (verbose > 0) { WriteLine($"STEP TAKEN xstep.norm()={xstep.norm()}");x.print("x =");}

			if(f(x).norm() < eps || xstep.norm() < delx.norm()) break;
		}

	return x;
}

}


