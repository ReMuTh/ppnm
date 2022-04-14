using System;
using static System.Math;

class main{
	
	static double d = 1e-6;
	static double e = 1e-6;

	static void Main() {


		// epsilon and delta

		WL($"Integrating using Clenshaw-Curtis variable substitution. delta={d}, epsilon={e}.");
		WL($"Comparing to not using substutution and Python's scipy.integrate.quad method:\n");

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

	}

	static void WL(string s="") {
		System.Console.WriteLine(s);
	} // lazy write

	static void test_quad(Func<double,double> f,double limit_a, double limit_b,
		double reference,string text,string method = "quad",
		double d=1e-6,double e=1e-6,int python_compare=0) {
		WL($"\nIntegrating {text} from {limit_a} to {limit_b} using {method} with accuracy delta={d} epsilon={e}");
		double res = 0;
		var INT = new integrator();

		if(method == "quad") {res = INT.quad(f, limit_a, limit_b, d, e);}
		if(method == "cc_quad") {res = INT.cc_quad(f, limit_a, limit_b, d, e);}

		WL($"Result is {res}");
		WL($"Reference value is {reference}");
		if (approx(res, reference, d, e)) WL("PASSED");
		else WL("FAILED");
		WL($"C# {method} routine used {INT.evals} integrant evaluations.");
		if(python_compare > 0) {
			WL($"Python used {python_compare} evaluations");
	
		}
	}

	static bool approx(double a, double b, double acc=1e-6, double eps=1e-6){
		if(Abs(a-b)<acc) return true;
		if(Abs(a-b)/Max(Abs(a),Abs(b)) < eps)return true;
	return false;
}

}