all: out.txt; cat $<

out.txt: main.exe
	mono $< > $@

main.exe: main.cs matrix.dll QRGS.dll
	mcs -reference:matrix.dll -reference:QRGS.dll main.cs

QRGS.dll : QRGS.cs matrix.dll
	mcs QRGS.cs -target:library -reference:matrix.dll

matrix.dll : vector.cs matrix.cs
	mcs -target:library -out:$@ $^

clean:
	rm *.dll *.exe [Oo]ut*