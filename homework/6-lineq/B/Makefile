all: out.txt; cat $<

out.txt: main.exe
	mono $< > $@

main.exe: main.cs matrix.dll QRGS.dll
	mcs -reference:matrix.dll -reference:QRGS.dll main.cs

QRGS.dll : ../lib/QRGS.cs matrix.dll
	mcs -target:library -reference:matrix.dll -out:$@  ../lib/QRGS.cs

matrix.dll : ../lib/vector.cs ../lib/matrix.cs ../lib/matrix_rene.cs
	mcs -target:library -out:$@ $^

clean:
	rm *.dll *.exe [Oo]ut*