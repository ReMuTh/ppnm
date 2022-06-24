all: out.txt neuron_interpolation.pdf;
	cat $<

neuron_interpolation.pdf: data/* plot_neuron.gpi
	gnuplot plot_neuron.gpi

out.txt: main.exe
	mono $< > $@

main.exe: main.cs matlib.dll
	mcs -reference:matlib.dll  main.cs

# Compiling minimization lib together with libs taken from earlier homework

matlib.dll : ../lib/vector.cs ../lib/vector_rene.cs ../lib/matrix.cs ../lib/matrix_rene.cs  ../lib/minimization.cs ../lib/neural.cs
	mcs -target:library -out:$@ $^

clean:
	rm *.dll *.exe [Oo]ut*