all: out.txt cubic_spline_sin.svg cubic_spline_random.svg cubic_spline_artefact.svg
	cat out.txt

cubic_spline_artefact.svg : input_data_artefact.txt interpolated_data_artefact.txt qubicspline_plot_artefact.gpi Makefile
	gnuplot qubicspline_plot_artefact.gpi

cubic_spline_random.svg : input_data_random.txt interpolated_data_random.txt qubicspline_plot_random.gpi Makefile
	gnuplot qubicspline_plot_random.gpi

cubic_spline_sin.svg : input_data_sin.txt interpolated_data_sin.txt qubicspline_plot_sin.gpi Makefile
	gnuplot qubicspline_plot_sin.gpi


interpolated_data_artefact.txt: main.exe input_data_sin.txt Makefile
	mono main.exe -input:input_data_artefact.txt -output:interpolated_data_artefact.txt -n:200

interpolated_data_random.txt: main.exe input_data_random.txt Makefile
	mono main.exe -input:input_data_random.txt -output:interpolated_data_random.txt -n:200

interpolated_data_sin.txt out.txt: main.exe input_data_sin.txt Makefile
	mono main.exe -input:input_data_sin.txt -output:interpolated_data_sin.txt -n:200 -z:2.56 >out.txt

main.exe: main.cs cubicspline.dll
	mcs -target:exe -out:main.exe -reference:cubicspline.dll main.cs

cubicspline.dll:cubicspline.cs
	mcs -target:library -out:cubicspline.dll cubicspline.cs

clean:
	rm *.exe *.dll *.svg
