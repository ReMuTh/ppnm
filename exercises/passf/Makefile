all: Out.txt
	cat Out.txt

Out.txt: main.exe
	mono main.exe > Out.txt

main.exe : main.cs ftable.dll
	mcs -target:exe -out:main.exe -reference:ftable.dll main.cs

ftable.dll : ftable.cs
	mcs -target:library -out:./ftable.dll ftable.cs