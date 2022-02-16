
out.txt : math.exe
	mono math.exe > out.txt

math.exe : math.cs
	mcs math.cs

clean:
	rm -f out.txt math.exe
