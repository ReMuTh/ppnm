all: out.txt
	cat out.txt

out.txt: stdin_to_stdout.exe
	echo 0 1 2 3 4 5 6 9 10 | mono stdin_to_stdout.exe > out.txt

stdin_to_stdout.exe : stdin_to_stdout.cs
	mcs stdin_to_stdout.cs

clean:
	rm *.exe [Oo]ut.txt