using System;
using static System.Math;
public class montecarlo {

public static (double,double) plain_mc(Func<vector,double> f,vector a,vector b,int N){
		int dim=a.size;
		// Assuming all elements of b are bigger than a.
		// Integration volume is an n-dim cube, so calculating the volume is simple
		double V =((b-a).prod());

		var x = new vector(dim);

		double fx,sum=0,sum_sq=0;
        for(int i=0;i<N;i++) {
                	x.set_random(a,b);
                	fx = f(x);
                	sum += fx; sum_sq+=fx*fx;
                }
        double mean=sum/N, sigma=Sqrt(sum_sq/N-mean*mean);
        return (mean*V,V*sigma/Sqrt(N));
} // plain_mc

public static (double,double) quasirand_mc(Func<vector,double> f,vector a,vector b,int N){
		int dim=a.size;
		// Assuming all elements of b are bigger than a.
		// Integration volume is an n-dim cube, so calculating the volume is simple
		double V =((b-a).prod());
		// Halfing N - but we will end up with N samples
		N/=2;
		var x = new vector(dim);
		double fx,sum1=0,sum2=0;
        	for(int i=1;i<=N;i++) {
        	// collecting two sums 
                	x.set_halton(i,a,b);
                	fx = f(x);sum1+= fx;
                	x.set_halton(i+N,a,b);
                	fx = f(x);sum2+= fx;
                }
        // Estimating sigma as difference between the two sums
        double int1=sum1/N*V, int2=sum2/N*V;
        double sigma=Abs(int2-int1);
        // return the mean of the two results
        return ((int1+int2)/2.0,sigma);
} // plain_mc


} // static class montecarlo