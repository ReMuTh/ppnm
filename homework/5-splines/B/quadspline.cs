using System;

public class quadspline {

	static double[] x,y,s;
	public double[] b,c;
	static int n;
	static int lastbin = 0;
	
	public quadspline(double[] xs,double[] ys){
		n = xs.Length;
		x = xs;
		y = ys;
		b = new double[n-1];
		c = new double[n-1];
		s = new double[n];
		
		// local arr to hold x intervals
		double[] dx = new double[n-1];
		double[] p = new double[n-1];
		
		// First run. Calculate interval lengths dx and linear coefficients
		for(int i = 0; i < (n-1); i ++) {		
			dx[i] = x[i+1] - x[i];
			p[i] = (y[i+1] - y[i])/dx[i];
		}
			
		// Second run. Forward recursion for quadratic coefficients
		c[0] = 0;
		for(int i = 0; i < n-2; i ++) c[i+1] = (p[i+1]-p[i]-c[i]*dx[i])/dx[i+1];
		
		// Third run. Backwards recursion in order to average quadratic coefficients
		c[n-2] /= 2;
		for(int i = n-3; i > -1; i --) c[i] = (p[i+1]-p[i]-c[i+1]*dx[i+1])/dx[i];
		
		// Fourth run. Calculate 'local' linear coefficients, b.
		// and accumulated coarse integration blocks
		s[0] = 0.0;
	    for(int i = 0; i < n-1; i ++) {
	    	b[i] = p[i] - c[i]*dx[i];
	    	s[i+1] = s[i] + y[i] * dx[i] + (b[i] * dx[i]*dx[i])/2 + (c[i] * dx[i]*dx[i]*dx[i])/3;
	    	
	    // System.Console.WriteLine($"{i} {c[i]}");
		}
	} // Constructor
	
	// Return value, derivative and integral as literal tuple.
	// should be possible from c# 7.0
	
	public (double value, double derivative, double integral) eval(double z) {
		
		int i = binsearch(z);
		double dx = z-x[i];
		
		double value = y[i] + b[i]*dx + c[i]*dx*dx;
		double derivative = b[i] + 2*c[i] * dx;
		double integral = s[i] + y[i] * dx + (b[i]*dx*dx)/2 + (c[i]*dx*dx*dx)/3;
		
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