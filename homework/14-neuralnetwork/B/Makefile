all: out.txt neuron_interpolation_calc.pdf Makefile;
	cat $<

neuron_interpolation_calc.pdf: data/interpol_data_der.txt data/interpol_data_int.txt data/interpol_data.txt data/subject_function_der.txt data/subject_function.txt data/training_points.txt plot_neuron.gpi
	gnuplot plot_neuron.gpi

out.txt data/interpol_data_der.txt data/interpol_data_int.txt data/interpol_data.txt data/subject_function_der.txt data/subject_function.txt data/training_points.txt: main.exe
	mono $< > $@

main.exe: main.cs matlib.dll
	mcs -reference:matlib.dll  main.cs

# Compiling minimization lib together with libs taken from earlier homework

matlib.dll : ../lib/vector.cs ../lib/vector_rene.cs ../lib/matrix.cs ../lib/matrix_rene.cs  ../lib/minimization.cs ../lib/neural_calc.cs
	mcs -target:library -out:$@ $^

clean:
	rm *.dll *.exe [Oo]ut*.txt data/*.txt