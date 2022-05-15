CSC = mcs
RUN = mono

all: Out.txt
	cat Out.txt

Out.txt: main.exe
	$(RUN) main.exe > Out.txt

main.exe: main.cs vec.dll
	$(CSC) -target:exe -out:main.exe -reference:vec.dll main.cs

vec.dll:vec.cs
	$(CSC) -target:library -out:vec.dll vec.cs

clean:
	$(RM) *.exe *.dll [Oo]ut*.txt
