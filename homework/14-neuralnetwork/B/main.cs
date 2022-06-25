using System;
using static System.Math;
using static Minimization;

class main{

static void Main() {
	// Define functions more compactly than in A part
    Func<double, double> gaussian_wavelet = x => x*Exp(-x*x);
    Func<double, double> gaussian_wavelet_derivative = x => Exp(-x*x)*(1.0 - 2.0*x*x);
    Func<double, double> gaussian_wavelet_antiderivative = x => -Exp(-x*x)/2.0;

    // Also define subject functions with delegates, also defining the analytical (anti-)derivative for comparison
    Func<double, double> subject1 = x => Cos(5*x-1)*Exp(-x*x);
	Func<double, double> subject1_der = x => Exp(-x*x)*(5*Sin(1-5*x)-2*x*Cos(1-5*x));	

	WL("homework 14-neuaralnetwork part B:");
	WL("Testing calculus on neural network");
	int input_number =13;
	WL($"\n\nCompute {input_number} function values of the subject function Cos(5*x-1)*Exp(-x*x)");
	vector xs = new vector(input_number);
	xs.fill(-1,1);
	vector ys = xs.map(subject1);
	xs.print("x values for training:");
	ys.print("y values for training:");

	int neuron_number = 7;
	WL($"\nInitialize neural network with {neuron_number} neurons and gaussian_wavelet and (anti-)derivative as activiation.");

	var ann = new neural_calc(neuron_number,gaussian_wavelet,gaussian_wavelet_derivative,gaussian_wavelet_antiderivative);
	WL("Now training network.");
	ann.train(xs,ys);

//	WL($"At x=0.1: Respone {ann.response(0.1)} Response2 {ann.response2(0.1)}");
	int plot_number = 201;
	vector x_plot = new vector(plot_number);
	x_plot.fill(-1,1);

	vector_to_file("data/training_points.txt",xs,ys);
	vector_to_file("data/subject_function.txt",x_plot,x_plot.map(subject1));	
	vector_to_file("data/subject_function_der.txt",x_plot,x_plot.map(subject1_der));	
	vector_to_file("data/interpol_data.txt",x_plot,x_plot.map(ann.response));
	vector_to_file("data/interpol_data_der.txt",x_plot,x_plot.map(ann.response_der));
	vector_to_file("data/interpol_data_int.txt",x_plot,x_plot.map(ann.response_int));

	WL("Conclusion: The network replicates the analytical function, the derivatives and anti derivatives satifactory.");
	WL("See neuron_interpolation_calc.pdf for comparisons with analytical values");
	WL("The anti-derivative differs by a constant offset from the analytical but that's expected.");
	WL("NB: The analytical anti-derivative of Cos(5*x-1)*Exp(-x*x) is pretty toxic, and the table for plotting is as comparison was generated in WolframAlpha");
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