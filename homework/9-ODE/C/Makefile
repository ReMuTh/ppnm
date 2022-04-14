all: three_body_1.svg three_body_2.svg out.txt; cat out.txt

three_body_2.svg: three_body_data_2.txt three_body_plot_2.gpi	
	gnuplot three_body_plot_2.gpi

three_body_1.svg: three_body_data_1.txt three_body_plot_1.gpi	
	gnuplot three_body_plot_1.gpi


out.txt three_body_data_1.txt: main.exe
	mono $< > $@

main.exe: main.cs lib.dll
	mcs -reference:lib.dll main.cs

lib.dll : ../lib/vector.cs ../lib/vector_rene.cs ../lib/matrix.cs ../lib/matrix_rene.cs ../lib/genlist.cs ../lib/rk45.cs
	mcs -target:library -out:$@ $^

clean:
	rm *.dll *.exe [Oo]ut* *.svg *.txt