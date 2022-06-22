using System;
using static System.Math;
using static Minimization;

class main{

static void Main() {
	// using my own Genlist from homework Genlist part B 
	// Updating it to accept bracket notation to reach elements…
	var energy = new genlist<double>();
	var signal = new genlist<double>();
	var error  = new genlist<double>();
	var separators = new char[] {' ','\t'};
	var options = StringSplitOptions.RemoveEmptyEntries;
	do{
        string line=Console.In.ReadLine();
        if(line==null)break;
        string[] words=line.Split(separators,options);
        energy.push(double.Parse(words[0]));
        signal.push(double.Parse(words[1]));
        error.push(double.Parse(words[2]));
	} while(true);
	
	int n = energy.size;

	WL("homework 13-minimization part B:");
	WL("Fitting Breit-Wigner resonance to CERS Higgs boson data");


	vector initial = new vector(128.0,5,1);
	initial.print("Iteration start point:");

	// Folding deviation function into taking vector argument, and passing experimental data along 
	Func<vector,double>  D = (x) => deviation(x[0],x[1],x[2],energy,signal,error);

	vector higgs_min = Minimization.qnewton(D,initial,1e-4);

	WL($"Mass [Gev/c^2]:{higgs_min[0],10:N2}\nWidth [Gev/c^2]:{higgs_min[1],9:N2}\nScaling [arb]:{higgs_min[2],11:N2}");

	WL("Observation: The iteration terminates in the ballbark of the CERN Higgs mass, but if a precision better than 1e-4 is requested the process doesn't conclude in reasonable time.");
	WL("Writing out list of Berit-Wigner-function values for a plot to visually inspect the quality of the fit.");

	WL("\nConclusion: Inspecting at the plot (higgs_data_fit.pdf) the fit looks acceptable.");
	
	var outstream = new System.IO.StreamWriter("fit_data.txt");
		
	double E = energy[0]-3;
	double E_max = energy[n-1]+3;
	double dE = (E_max-E)/1000;
	do {			
		outstream.WriteLine($"{E} {breitwigner(E,higgs_min[0],higgs_min[1],higgs_min[2])}");
		E += dE;
	} while (E <= E_max);
	outstream.Close();
}


static double deviation(double m, double G, double A, genlist<double> energy, genlist<double> signal, genlist<double> error) {
		int n = energy.size;
		double sum = 0;
		for (int i=0;i<n;i++) {
			sum += Pow((breitwigner(energy[i],m,G,A)-signal[i])/error[i],2);
		}
		return sum;
}

// Breit-Wigner function
static double breitwigner(double E, double m, double G, double A) {  //  energy E, mass m, Resonance width G (Gamma), A scaling
	return A/((E-m)*(E-m)+G*G/4);
}

static void WL(string s="") {
	System.Console.WriteLine(s);
} // lazy write


static bool approx(double a, double b, double acc=1e-3, double eps=1e-3) {
	if(Abs(a-b)/Max(Abs(a),Abs(b)) < eps)return true;
	return false;
} // approx

static void judge(vector result,vector reference) {
	result.print("Result is:");
	reference.print("Reference is:");
	bool passed = true;

	for (int i=0;i<reference.size;i++) {
		if (!approx(result[i],reference[i])) passed = false;
	}

	if (passed) WL("PASSED");
	else WL("FAILED");
}

}