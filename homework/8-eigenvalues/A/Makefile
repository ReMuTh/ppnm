all: out.txt; cat $<

out.txt: main.exe
	mono $< > $@

main.exe: main.cs matrix.dll jacobi.dll
	mcs -reference:matrix.dll -reference:jacobi.dll main.cs

jacobi.dll : ../matlib/jacobi.cs matrix.dll
	mcs -target:library -reference:matrix.dll -out:$@  ../matlib/jacobi.cs

matrix.dll : ../matlib/vector.cs ../matlib/matrix.cs ../matlib/matrix_rene.cs
	mcs -target:library -out:$@ $^

clean:
	rm *.dll *.exe [Oo]ut*