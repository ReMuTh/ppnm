using static Integrator;
using System;
using static System.Math;

class main{
	
	static double d = 1e-6;
	static double e = 1e-6;

	static void Main() {

		// epsilon and delta

		WL($"Homework quadratures PART B:");
		WL($"Integrating using Clenshaw-Curtis variable substitution. delta={d}, epsilon={e}.");
		WL($"Comparing to integration without substutution and Python's scipy.integrate.quad method:\n");

		WL($"First testing integrity of the a = -1, b = 1 substition with a non-divergent integral. A half circle:");

		test_quad(x => Sqrt(1-x*x), -1, 1,PI/2,"Sqrt(1-x*x)","quad",d,e,399);
		test_quad(x => Sqrt(1-x*x), -1, 1,PI/2,"Sqrt(1-x*x)","cc_quad",d,e,399);

		WL($"The substition (which becomes an intefral of sin^2(theta)) handles this case particularly well.");

		// Syntactic sugar allows to pass anonymous lambda function directly to the test and - in turn - integrator
		test_quad(x => 1/Sqrt(x), 0, 1,2.0,"1/Sqrt(x)","quad",d,e,231);
		test_quad(x => 1/Sqrt(x), 0, 1,2.0,"1/Sqrt(x)","cc_quad",d,e,231);
		// test_quad(x => Log(x)/Sqrt(x), 0, 1,-4,"ln(x)/Sqrt(x)");
		
		WL(@"
In this case using Clenshaw-Curtis reduces the number of integrand evaluations by a factor better than 1/300.
Although the substitution adds calculation of Sin and Cos for each evaluation, this is substantial difference.
The efficiency is in the ballpark of scipy.integrate.quad.
More examples:");

		test_quad(x => Log(x)/Sqrt(x), 0, 1,-4,"Log(x)/Sqrt(x)","quad",1e-4,1e-4);
		test_quad(x => Log(x)/Sqrt(x), 0, 1,-4,"Log(x)/Sqrt(x)","cc_quad",1e-4,1e-4,315);

		WL(@"Note: I actually had to increase the delta and epsilon on the Log(x)/Sqrt(x) integral (1e-6 => 1e-4)
To avoid stack overflow. I am not entirely sure why this happens.");

		test_quad(x => Sqrt(Tan(x)), 0, PI/2,PI/Sqrt(2),"Sqrt(Tan(x)),","quad",d,e);
		test_quad(x => Sqrt(Tan(x)), 0, PI/2,PI/Sqrt(2),"Sqrt(Tan(x))","cc_quad",d,e,189);

		WL("\nJupiter Notebook used to retrieve the Python results is included in the lib folder");

	}

}