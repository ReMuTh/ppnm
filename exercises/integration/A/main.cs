using System;
using static System.Math;

class Integration {
static void Main() {
	Func<double,double> f = x => Log(x)/Sqrt(x);
	double result = integrate.quad(f,0,1);

	Console.Write($"Exercise part A, integration result: {result}");
}

public static double erf(double z){
	Func<double,double> f = x => Exp(-x*x);
	return integrate.quad(f,0,z)*2/Sqrt(PI);
}



}