all: out.txt quadratic_spline_artefact.svg quadratic_spline_interpolation.svg artefact_interpol.txt coefficient_test.txt
	cat coefficient_test.txt
	cat out.txt


quadratic_spline_artefact.svg : input_data_artefact.txt artefact_interpol.txt artefact_plot.gpi Makefile
	gnuplot artefact_plot.gpi

quadratic_spline_interpolation.svg : input_data_sin.txt interpolated_data.txt quadspline_plot.gpi Makefile
	gnuplot quadspline_plot.gpi

artefact_interpol.txt: main.exe input_data_artefact.txt Makefile
	mono main.exe -input:input_data_artefact.txt -output:artefact_interpol.txt

interpolated_data.txt out.txt: main.exe input_data_sin.txt Makefile
	mono main.exe -input:input_data_sin.txt -output:interpolated_data.txt -n:200 -z:2.56 >out.txt

coefficient_test.txt: test_qsplines.exe
	mono test_splines.exe > coefficient_test.txt

main.exe: main.cs quadspline.dll
	mcs -target:exe -out:main.exe -reference:quadspline.dll main.cs

test_qsplines.exe: test_qsplines.cs quadspline.dll
	mcs -target:exe -out:test_splines.exe -reference:quadspline.dll test_qsplines.cs

quadspline.dll:quadspline.cs
	mcs -target:library -out:quadspline.dll quadspline.cs

clean:
	rm *.exe *.dll *.svg
	rm out.txt
	rm coefficient_test.txt