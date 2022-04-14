all: out.txt; cat $<

out.txt ode_sincos_data.txt: main.exe
	mono $< > $@

main.exe: main.cs lib.dll
	mcs -reference:lib.dll main.cs

lib.dll : ../lib/vector.cs ../lib/vector_rene.cs ../lib/matrix.cs ../lib/matrix_rene.cs ../lib/genlist.cs ../lib/rk45.cs
	mcs -target:library -out:$@ $^

clean:
	rm *.dll *.exe [Oo]ut* *.svg *.txt