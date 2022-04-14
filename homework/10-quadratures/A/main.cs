using System;
using static System.Math;

class main{
	
	static double d = 1e-6;
	static double e = 1e-6;

	static void Main() {


		// epsilon and delta

		WL($"Doing Various Integrals using numeric quadratures, with precision delta={d}, epsilon={e}:");
		WL($"Comparing to scipy.integrate.quad with same accuracy goals");

		// Syntactic sugar allows to pass anonymous lambda function directly to the test and - in turn - integrator
	
		test_quad(x => Sqrt(x), 0, 1,2.0/3,"Sqrt(x)","quad",231);
		test_quad(x => 1/Sqrt(x), 0, 1,2,"1/Sqrt(x)","quad",231);
		test_quad(x => 4/Sqrt(1-x*x), 0, 1,2*Math.PI,"4/Sqrt(1-x*x)","quad",273);
		test_quad(x => Log(x)/Sqrt(x), 0, 1,-4,"Log(x)/Sqrt(x)","quad",315);
		test_quad(x => Sin(x), 0, 2*PI,0,"sin(x)","quad",21);


		WL("\nEvaluating the error function from z=-3 to z=3 and putting result in external file");

		double erf;
		var outstream=new System.IO.StreamWriter("erf.data.txt");
		var INT = new integrator();
		for(double z = -3.0;z<3.01;z+=0.02) {
			// Not entirely happy with the efficiency of this since we're evaluating the entire
			// erf integral every time. We could just do slivers and add up to an accumulated result
			// But, since it's just a demo and we're only evaluating 300 z values, let it go…
			erf = 2/Sqrt(PI)*INT.quad(x => Exp(-x*x), 0, z, d, e);
			outstream.WriteLine($"{z}	{erf}");
		}
		outstream.Close();

	}

	static void WL(string s="") {
		System.Console.WriteLine(s);
	} // lazy write

	static void test_quad(Func<double,double> f,double limit_a, double limit_b, double reference,string text,string method = "quad",int python_compare=0) {
		WL($"\nIntegrating {text} from {limit_a} to {limit_b} using {method} with accuracy delta={d} epsilon={e}");
		double res = 0;
		var INT = new integrator();

		if(method == "quad") {res = INT.quad(f, limit_a, limit_b, d, e);}
		if(method == "cc_quad") {res = INT.cc_quad(f, limit_a, limit_b, d, e);}

		WL($"Result is {res}");
		WL($"Reference value is {reference}");
		if (approx(res, reference, d, e)) WL("PASSED");
		else WL("FAILED");
		WL($"C# routine used {INT.evals} integrant evaluations.");
		if(python_compare > 0) {
			WL($"Python used {python_compare} evaluations");
	
		}
	}

	static bool approx(double a, double b, double acc=1e-6, double eps=1e-6){
		if(Abs(a-b)<acc)return true;
		if(Abs(a-b)/Max(Abs(a),Abs(b)) < eps)return true;
	return false;
}

}