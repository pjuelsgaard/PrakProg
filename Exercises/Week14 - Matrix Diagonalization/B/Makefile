all : B.svg

B.svg : B.gpi out.txt
	gnuplot $<

out.txt : B.exe
	mono $< > $@

B.exe : B.cs matrix.dll 
	mcs $< -reference:$(word 2, $^)

matrix.dll : ~/matlib/matrix/vector.cs ~/matlib/matrix/matrix.cs ~/matlib/matrix/diag.cs ~/matlib/fit.cs
	mcs $^ -target:library -out:$@
