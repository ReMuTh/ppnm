all: out.txt higgs_data_fit.pdf;
	cat $<

higgs_data_fit.pdf: higgs_data.txt fit_data.txt plot_higgs.gpi
	gnuplot plot_higgs.gpi

out.txt: main.exe higgs_data.txt
	mono main.exe < higgs_data.txt 1> out.txt

main.exe: main.cs matlib.dll
	mcs -reference:matlib.dll  main.cs

# Compiling minimization lib together with libs taken from earlier homework

matlib.dll : ../lib/vector.cs ../lib/vector_rene.cs ../lib/matrix.cs ../lib/matrix_rene.cs  ../lib/minimization.cs ../lib/genlist.cs
	mcs -target:library -out:$@ $^

clean:
	rm *.dll *.exe [Oo]ut*