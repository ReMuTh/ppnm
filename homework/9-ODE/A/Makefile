all: out.txt ode_sincos.svg pend_fric.svg; cat $<

pend_fric.svg: pend_fric_data.txt pend_fric_plot.gpi	
	gnuplot pend_fric_plot.gpi

ode_sincos.svg: ode_sincos_data.txt sincos_plot.gpi	
	gnuplot sincos_plot.gpi

out.txt ode_sincos_data.txt: main.exe
	mono $< > $@

main.exe: main.cs matlib.dll
	mcs -reference:matlib.dll main.cs

matlib.dll : ../lib/vector.cs ../lib/vector_rene.cs ../lib/matrix.cs ../lib/matrix_rene.cs ../lib/genlist.cs ../lib/rk45.cs
	mcs -target:library -out:$@ $^

clean:
	rm *.dll *.exe [Oo]ut* *.svg *.txt