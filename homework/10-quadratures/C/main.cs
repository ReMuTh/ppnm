using static Integrator;
using System;
using static System.Math;

class main{
	
	static double d = 1e-6;
	static double e = 1e-6;

	static void Main() {

		// epsilon and delta
		WL($"Homework quadratures PART C:");

		WL($"Now testing improper integrals with one or both limits at infinite delta={d}, epsilon={e}.");
		WL($"Comparing to Python's scipy.integrate.quad method:\n");


		WL($"\nRight limit infinite:");

		test_quad(x => 1/(x*x), 1, double.PositiveInfinity,1,"1/x^2","quad",d,e,15);


		WL($"\nLeft limit minus infinite:");

		test_quad(x =>  1/(1+x*x) ,double.NegativeInfinity,0,PI/2,"1/(1+x^2)","quad",d,e,45);

		WL($"\nBoth limits infinite. Let's try a Gauss distribution:");

		test_quad(x => Exp(-(x*x)) ,double.NegativeInfinity,double.PositiveInfinity,Sqrt(PI),"exp(-x^2)","quad",d,e,210);

		WL("\nJupiter Notebook used to retrieve the Python results is included in the lib folder");

	}


}