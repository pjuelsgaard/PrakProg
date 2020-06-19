all: out.txt B.svg C.svg
	cat $<

C.svg : C.gpi out2.txt
	gnuplot $<

B.svg : B.gpi out2.txt
	gnuplot $<

out.txt out2.txt: BC.exe
	mono $< 1> out.txt 2>out2.txt

BC.exe : BC.cs matlib.dll
	mcs $< -reference:$(word 2,$^) -out:$@

matlib.dll : ../matrix.cs ../vector.cs ../root.cs ../rk.cs
	mcs -target:library -out:$@ $^

clean :
	rm -f *.dll *.txt *.exe *.svg
			    
