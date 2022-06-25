using System;
using static System.Math;
using static Minimization;

class main{

static void Main() {

	// var Multimin = new Minimization();

	WL("homework 14-neuaralnetwork part A:");
	WL("FIRSTLY\nTesting new nifty vector operations made to make neural class more compact");
	int n = 12;
	vector vec1 = new vector(n);
	vector vec2 = new vector(n);

	vec1.fill(5.0).print($"Filling vector with with {n} elements with identical elements:");

	
	vec1.fill(1,12).print($"Filling vector with with evenly distributed elements:");

	vec1[0,4].print("Vector containing first four elements:");
	vec1[4,8].print("Vector containing next four elements:");
	vec1[8,12].print("Vector containing last four elements:");

	vec1[0,4] = vec1[6,10];
	vec1.print("Assigning new values to a slice (using) another slice");

	WL("Testing element-wise multiplication (hadamard multiplication)");
	vec1.print("Vector 1");
	vec2.print("Vector 2");
	vec1.mult(vec2).print("Result");

	WL($"Sum of hadamard: {vec1.mult(vec2).sum()} Should be the same as dot product: {vec1*vec2}");
	WL("All seems to work as expected");


	vec1.randomint(0,100).print("Vector with random integers between 0 and 100");
	vec2.random(-1,1).print("Vector with random doubles between -1 and 1");


	int input_number =11;
	WL($"\n\nSECONDLY\nCompute {input_number} function values of the subject function Cos(5*x-1)*Exp(-x*x)");
	vector x = new vector(input_number);
	x.fill(-1,1);
	vector y = x.map(subject1);
	x.print("x values for training:");
	y.print("y values for training:");


	int neuron_number =20;
	WL($"\nTHIRDLY\nInitialize neural network with {neuron_number} neurons and gaussian_wavelet as activiation.");

	neural ann = new neural(neuron_number,gaussian_wavelet);
	WL("Now train network with input values, using minimization from homework 13");
	ann.train(x,y);

	WL($"response(x= 0.1): {ann.response(0.1)}");
	WL($"subject1(x= 0.1): {subject1(0.1)}");

	WL($"Ok that worked like a charm.");
	WL($"\nFOURTHLY\ninvestigating how the number of neurons matter by ploting interpolations.\nWriting output files for plotting…");

	vector_to_file("data/training_points.txt",x,y);

	int plot_number =251;
	vector x_plot = new vector(plot_number);
	x_plot.fill(-1,1);
	vector_to_file("data/subject_function.txt",x_plot,x_plot.map(subject1));


	for(neuron_number = 5;neuron_number<30;neuron_number+=5) {
		ann = new neural(neuron_number,gaussian_wavelet);
		WL($"Training using {neuron_number} neurons.");
		ann.train(x,y);
		vector_to_file($"data/interpol_{neuron_number}_neurons.txt",x_plot,x_plot.map(ann.response));
	}

	WL("As expected, if the number of neurons the network becomes 'overdetermined' and can sometimes predict strange paths between the input points.");

}


static double subject1(double x) {
	return Cos(5*x-1)*Exp(-x*x);
}

static double gaussian_wavelet(double x) {
	return x*Exp(-x*x);
}


static void vector_to_file(string filename, vector v,vector w) {
	var outstream = new System.IO.StreamWriter(filename);
	for (int i=0; i<v.size; i++) {
		outstream.WriteLine($"{v[i]} {w[i]}");
	}
	outstream.Close();
}

static void WL(string s="") {
	System.Console.WriteLine(s);
} // lazy write




}