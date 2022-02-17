
out.txt : epsilon.exe
	mono epsilon.exe > out.txt

epsilon.exe : epsilon.cs
	mcs epsilon.cs

clean:
	rm -f out.txt epsilon.exe
