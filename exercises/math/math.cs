using static System.Math;

class mathdings{
	static void Main() {
		double sqrt2 = Sqrt(2.0);

		System.Console.Write($"sqrt(2) = {sqrt2}\n");
		System.Console.Write($"Kvadratrod To i anden = {sqrt2*sqrt2}\n");
		
		double exp_pi = Exp(PI);	
		System.Console.Write($"e opløftet til pi {exp_pi}\n");

		double pi_exp = Pow(PI,E);
		System.Console.Write($"Pi opløftet til e {pi_exp}\n");

		System.Console.Write("Jamen dog, det virker");
	}
}
