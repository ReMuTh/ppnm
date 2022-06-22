using System;

public class cubicspline {

	static double[] x,y,s;
	public double[] b,c,d;
	static int n;
	static int lastbin = 0;
	
	public cubicspline(double[] xs,double[] ys){
		n = xs.Length;
		if (n<2) throw new ArgumentException("Input data should contain at least two points.");

		// Transfer to local x,y
		x = xs;
		y = ys;

		// Arrays for spline coefficients
		b = new double[n];
		c = new double[n-1];
		d = new double[n-1];

		// s will hold array of coarse integration segments
		s = new double[n];
		
		// local arrs to hold x intervals, (named h in the note)
		// and matrix elements in eq. 22.
		double[] h = new double[n-1];
		double[] p = new double[n-1];
		double[] Q = new double[n-1];
		double[] D = new double[n];
		double[] B = new double[n];
		
		// Calculate interval lengths h and linear coefficients
		for(int i = 0; i < n-1; i ++) {		
			h[i] = x[i+1] - x[i];
			if (h[i]<0) throw new ArgumentException("Input x values must be in ascending order");
			p[i] = (y[i+1] - y[i])/h[i];
		}

		// Set up boundary conditions
		D[0] = 2; D[n-1] = 2;
		Q[0] = 1;
		B[0] = 3*p[0];B[n-1]=3*p[n-2];

		// Calculate matrix elements
		for(int i = 0; i < n-2; i ++) {
			D[i+1] = 2 * h[i]/h[i+1] + 2;
			Q[i+1] = h[i]/h[i+1];
			B[i+1] = 3*(p[i]+p[i+1]*h[i]/h[i+1]);
		}

		// Gauss elimination

		for(int i = 1; i < n; i ++) {
			D[i] -= Q[i-1]/D[i-1];
			B[i] -= B[i-1]/D[i-1];
		}

		// Back-substitution, seed and traverse
		b[n-1] = B[n-1]/D[n-1];

		for(int i = n-2; i >= 0; i --) {
			b[i] = (B[i]-Q[i]*b[i+1])/D[i];
		}

		// Now traverse to fill in c and d coeffs using eq 20
		// In the same run fill coarse integration values
		s[0] = 0.0;

		for(int i = 0; i < n-1; i ++) {
			c[i]=(-2*b[i]-b[i+1]+3*p[i])/h[i];
			d[i]=(b[i]+b[i+1]-2*p[i])/(h[i]*h[i]);

			s[i+1]=s[i] + y[i] * h[i] + (b[i] * h[i]*h[i])/2 + (c[i] * h[i]*h[i]*h[i])/3 + (d[i] * h[i]*h[i]*h[i]*h[i])/4;
		}

	} // Constructor
	
	// Return value, derivative and integral as literal tuple.
	// should be possible from c# 7.0
	
	public (double value, double derivative, double integral) eval(double z) {
		
		int i = binsearch(z);
		double dx = z-x[i];
		double dx2 = dx*dx;
		double dx3 = dx2*dx;
		double dx4 = dx3*dx;

		double value = y[i] + b[i]*dx + c[i]*dx2 + d[i]*dx3;
		double derivative = b[i] + 2*c[i] * dx + 3 * d[i] * dx2;
		double integral = s[i] + y[i] * dx + (b[i]*dx2)/2 + (c[i]*dx3)/3 + (d[i]*dx4)/4;
		
		return (value,derivative,integral);	
	}
	
	
	static int binsearch(double z) {
		// Checks if z is inside lastbin
		if(x[lastbin] <= z && z <= x[lastbin+1]) return lastbin;
		
		if(!(x[0]<=z && z<=x[n-1])) throw new Exception($"binsearch: z={z} out of x range {x[0]} {x[n-1]}");
		int i=0, j=n-1;
		while(j-i>1){
			int mid=(i+j)/2;
			if(z>x[mid]) i=mid; else j=mid;
			}
		lastbin = i;
		return i;
	}

	
}



