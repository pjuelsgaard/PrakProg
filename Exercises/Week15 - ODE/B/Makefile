all : B1.svg B2.svg B3.svg

B3.svg : B3.gpi out.txt
	gnuplot $<

B2.svg : B2.gpi out.txt
	gnuplot $<

B1.svg : B1.gpi out.txt
	gnuplot $<
	

out.txt : B.exe
	mono $< > $@

B.exe : B.cs matlib.dll
	mcs $< -reference:$(word 2,$^) >$@

matlib.dll :  ../rk.cs ~/matlib/matrix/vector.cs
	mcs -target:library -out:$@ $^


clean:
	rm -f *.svg *.dll *.txt
