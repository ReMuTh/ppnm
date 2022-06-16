all: out.txt; cat $<

out.txt: main.exe
	mono $< > $@

main.exe: main.cs roots.dll
	mcs -reference:roots.dll -reference:QRGS.dll  main.cs

roots.dll: ../lib/roots.cs QRGS.dll
	mcs -target:library -reference:QRGS.dll -out:roots.dll ../lib/roots.cs

# Compiling QRGS routines taken from linear equations homework

QRGS.dll : ../lib/vector.cs ../lib/vector_rene.cs ../lib/matrix.cs ../lib/matrix_rene.cs  ../lib/QRGS.cs
	mcs -target:library -out:$@ $^

clean:
	rm *.dll *.exe [Oo]ut*