using System;
using static System.Math;
public class akimaspline {

	static double[] x,y,s;
	public double[] b,c,d;
	static int n;
	static int lastbin = 0;
	
	public akimaspline(double[] xs,double[] ys, string endslopes = "akima"){
		n = xs.Length;
		if (n<4) throw new ArgumentException("Input data should contain at least two points.");
		
		// Transfer to local x,y
		x = xs;
		y = ys;

		// Arrays for spline coefficients
		b = new double[n];
		c = new double[n-1];
		d = new double[n-1];

		// s will hold array of coarse integration segments
		s = new double[n];
		
		// local arrs to hold x intervals, (named h)
		// linear slopes p, and weights used for the akima derivatives
		double[] h = new double[n-1];
		double[] p = new double[n-1];
		double[] w = new double[n-2];

		
		// Calculate interval lengths h and linear slopes p
		for(int i = 0; i < n-1; i ++) {		
			h[i] = x[i+1] - x[i];
			if (h[i]<0) throw new ArgumentException("Input x values must be in ascending order");
			p[i] = (y[i+1] - y[i])/h[i];
		}
		// Calculate weights. Notice that the indexing will be shifted
		// by one compared to eq. 35 in the notes. Otherwise w[0] would
		// be left unused and the array one item too long. Can't have that.

		for(int i = 0; i < n-2; i ++) {		
			w[i] = Abs(p[i+1]-p[i]);
		}		


		// Calculating b[2] though b[n-3] values according to DVF eq 1.32 and 1.33

		for(int i = 2; i < n-2; i++) {
			// evaluate denominator
			double den = w[i] + w[i-2];
			if(den == 0) b[i] = (p[i-1] + p[i]) / 2;
			else b[i] = (w[i]*p[i-1] + w[i-2]*p[i])/den;
		}

		// Special treatment on two and last two first derivatives:

		// naive scheme as suggested in the interpolation eq. 36. 
		if (endslopes == "naive") {
			b[0] = p[0];
			b[1] = (p[0]+p[1])/2;
			b[n-2] = (p[n-2]+p[n-3])/2;
			b[n-1] = p[n-2];
		}

		// Akima scheme
		else if (endslopes == "akima") {
			// the four elements in p_e will contain extended index values of p
			// corresponding to p[-2],p[-1],p[n],p[n+1] respectively
			// also defining w_e for ex
			double[] p_e = new double[4];
			double[] w_e = new double[4];

			p_e[0] = 3*p[0] - 2*p[1];
			p_e[1] = 2*p[0] - p[1];
			p_e[2] = 2*p[n-2] - p[n-3];
			p_e[3] = 3*p[n-2] - 2*p[n-3];

			w_e[0] = Abs(p_e[1]-p_e[0]);
			w_e[1] = Abs(p[0]-p_e[1]);
			w_e[2] = Abs(p_e[2]-p[n-2]);
			w_e[3] = Abs(p_e[3]-p_e[2]);

			// now verbatim calculating b[0],b[1],b[n-2],b[n-1] using 
			// extra elements with the same recipe as the loop above

			// I attempted to just make p and w four elements longer
			// but that would only simplify code if you could use a 
			// global loop to traverse all p's, and calculate w's and b's
			// and also use that in the case of "naive" p's by strategically choosing
			// the four extra p's so the weights would then replicate p[0],p[1],p[n-2],p[n-1]
			// of that scheme correcty. I however found that was impossible
			// due to mathemathical contraints. /rene

			// i = 0
			double den = w[0] + w_e[0];
			if(den == 0) b[0] = (p_e[1] + p[0]) / 2;
			else b[0] = (w[0]*p_e[1]  + w_e[0]*p[0])/den;
			// i = 1
			den = w[1] + w_e[1];
			if(den == 0) b[1] = (p[0] + p[1]) / 2;
			else b[1] = (w[1]*p[0] + w_e[1]*p[1])/den;
			// i = n-2
			den = w_e[2] + w[n-4];
			if(den == 0) b[n-2] = (p[n-3] + p[n-2]) / 2;
			else b[n-2] = (w_e[2]*p[n-3] + w[n-4]*p[n-2])/den;
			// i = n-1
			den = w_e[3] + w[n-3];
			if(den == 0) b[n-1] = (p[n-2] + p_e[2]) / 2;
			else b[n-1] = (w_e[3]*p[n-2] + w[n-3]*p_e[2])/den;
		}

		// Bica scheme
		else if (endslopes == "bica") {
			double h02 = Pow(h[0],2);
			double h12 = Pow(h[1],2);
			double h03 = Pow(h[0],3);
			double h13 = Pow(h[1],3);
			double den = h03 + h13;
			double del = (7.0*h03 + 16.0*h13) / (16.0*den);

			b[0] = ((y[1]-y[0])/h[0]/4.0 + 9.0/16*h13/den*b[2] + 3.0/16*h02/den*(y[1]-y[0]) + 3.0/16*h12/den*(y[2]-y[1])) / del;
			b[1] = (3.0/4*h13/den*b[2] + 7.0/16*h02/den*(y[1]-y[0]) + h12/4.0/den*(y[2]-y[1])) / del;
			
			// reusing variables symmetric for the end points so same expressions apply
			h02 = Pow(h[n-2],2);
			h12 = Pow(h[n-3],2);
			h03 = Pow(h[n-2],3);
			h13 = Pow(h[n-3],3);

			den = h03 + h13;
			del = (7.0*h03 + 16.0*h13) / (16.0*den);
			
			b[n-1] = ((y[n-1]-y[n-2])/h[0]/4.0 + 9.0/16*h13/den*b[n-3] + 3.0/16*h02/den*(y[n-2]-y[n-1]) + 3.0/16*h12/den*(y[n-3]-y[n-2])) / del;
			b[n-2] = (3.0/4*h13/den*b[n-3] + 7.0/16*h02/den*(y[n-1]-y[n-2]) + h12/4.0/den*(y[n-2]-y[n-3])) / del;
		
		}

		// Now traverse to fill in c and d coeffs using eq. 32
		// (this is identical to regular cubic splines)
		// In the same run fill in coarse integration values
		s[0] = 0.0;

		for(int i = 0; i < n-1; i ++) {

			c[i]=(3*p[i]-2*b[i]-b[i+1])/h[i];
			d[i]=(b[i]+b[i+1]-2*p[i])/(h[i]*h[i]);

			s[i+1]=s[i] + y[i] * h[i] + (b[i] * h[i]*h[i])/2 + (c[i] * Pow(h[i],3))/3 + (d[i] * Pow(h[i],4))/4;
		}



	} // Constructor
	
	// Return value, derivative and integral as literal tuple.
	// should be possible from c# 7.0
	
	public (double value, double derivative, double integral) eval(double z) {
		
		int i = binsearch(z);
		double dx = z-x[i];
		double dx2 = Pow(dx,2);
		double dx3 = Pow(dx,3);
		double dx4 = Pow(dx,4);

		double value = y[i] + b[i]*dx + c[i]*dx2 + d[i]*dx3;
		double derivative = b[i] + 2*c[i] * dx + 3 * d[i] * dx2;
		double integral = s[i] + y[i] * dx + (b[i]*dx2)/2 + (c[i]*dx3)/3 + (d[i]*dx4)/4;
		
		return (value,derivative,integral);	
	}
	
	
	static int binsearch(double z) {
		// Checks if z is inside lastbin
		if(x[lastbin] <= z && z <= x[lastbin+1]) return lastbin;
		
		if(!(x[0]<=z && z<=x[n-1])) throw new ArgumentException($"binsearch: z={z} out of x range [{x[0]}, {x[n-1]}]");
		int i=0, j=n-1;
		while(j-i>1){
			int mid=(i+j)/2;
			if(z>x[mid]) i=mid; else j=mid;
			}
		lastbin = i;
		return i;
	}

	
}



