all: A.svg

A.svg: A.gpi out.txt
	gnuplot $<
	cat out.txt

DAT = "../data.txt"
out.txt : A.exe  matrix.dll
	mono $< $(DAT) > $@ 

A.exe : A.cs matrix.dll
	mcs $< -reference:matrix.dll

matrix.dll : ~/matlib/matrix/matrix.cs ~/matlib/matrix/vector.cs fit.cs
	mcs $^ -target:library -out:$@ 

clean:
	rm -f *.txt *.exe *.dll *.svg
