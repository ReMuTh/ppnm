using static Integrator;
using System;
using static System.Math;

class main{
	
	static double d = 1e-6;
	static double e = 1e-6;

	static void Main() {

		// epsilon and delta
		WL($"Homework quadratures PART A:");
		WL($"Doing Various Integrals using numeric quadratures, with precision delta={d}, epsilon={e}:");
		WL($"Comparing to scipy.integrate.quad with same accuracy goals");

		// Syntactic sugar allows to pass anonymous lambda function directly to the test and - in turn - integrator
	
		test_quad(x => Sqrt(x), 0, 1,2.0/3,"Sqrt(x)","quad",231);
		test_quad(x => 1/Sqrt(x), 0, 1,2,"1/Sqrt(x)","quad",231);
		test_quad(x => 4/Sqrt(1-x*x), 0, 1,2*Math.PI,"4/Sqrt(1-x*x)","quad",273);
		test_quad(x => Log(x)/Sqrt(x), 0, 1,-4,"Log(x)/Sqrt(x)","quad",315);
		test_quad(x => Sin(x), 0, 2*PI,0,"sin(x)","quad",21);


		WL("\nEvaluating the error function from z=-3 to z=3 and putting result in external file");

		double erf,err;
		var outstream=new System.IO.StreamWriter("erf.data.txt");

		for(double z = -3.0;z<3.01;z+=0.02) {
			// Not entirely happy with the efficiency of this since we're evaluating the entire
			// erf integral every time. We could just do slivers and add up to an accumulated result
			// But, since it's just a demo and we're only evaluating 300 z values, let it go…

			(erf,err) = quad(x => Exp(-x*x), 0, z, d, e);
			
			// Adding the front factor in the aftermath, no need to do that multiplication on all evalutation
			outstream.WriteLine($"{z}	{erf*2/Sqrt(PI)}");
		}
		outstream.Close();

	}

}